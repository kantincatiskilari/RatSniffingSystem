using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrialsController : ControllerBase
    {
        private readonly ITrialService<Trial, TrialDto, CreateTrialDto> _trialService;
        private readonly ILogger<TrialsController> _logger;

        public TrialsController(ITrialService<Trial, TrialDto, CreateTrialDto> trialService, ILogger<TrialsController> logger)
        {
            _trialService = trialService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm trialleri getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<TrialDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<TrialDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var trials = await _trialService.GetAllAsync(ct);
                return Ok(trials);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trialler getirilirken hata meydana geldi");
                return StatusCode(500, "Trialler getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre trial getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrialDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrialDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var trial = await _trialService.GetByIdAsync(id, ct);
                if (trial == null)
                {
                    return NotFound($"{id}'li trial bulunamadi.");
                }
                return Ok(trial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li trial getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li trial getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni trial oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TrialDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTrialDto createTrialDto, CancellationToken ct)
        {
            try
            {
                if (createTrialDto == null)
                {
                    return BadRequest("Trial verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTrial = await _trialService.CreateAsync(createTrialDto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdTrial.Id }, createdTrial);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir trial olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir trial olusturulurken hata meydana geldi");
            }
        }

        

        /// <summary>
        /// Trial'i siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var exists = await _trialService.GetByIdAsync(id, ct);
                if(exists is null)
                    return NotFound($"{id}'li trial bulunamadi.");

                await _trialService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li trial silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li trial silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Trial Specific Operations
        /// <summary>
        /// Trial numarasina gore trial getirir.
        /// </summary>

        [HttpGet("byTrialNumber/{trialNumber}")]
        [ProducesResponseType(typeof(List<TrialDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TrialDto>>> GetByTrialNumber(int trialNumber)
        {
            try
            {
                var trial = await _trialService.GetByTrialNumberAsync(trialNumber);
                if (trial == null || trial.Count == 0)
                {
                    return NotFound($"Trial numarasi {trialNumber} olan trial bulunamadi.");
                }
                return Ok(trial);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "{trialNumber} numarali trial getirilirken hata meydana geldi.");
                return StatusCode(500, $"{trialNumber} numarali trial getirilirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir hedef kokuya (target odor) sahip denemeleri getirir. </summary>
        /// 
        [HttpGet("byTargetOdor/{targetOdor}")]
        [ProducesResponseType(typeof(List<TrialDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TrialDto>>> GetByTargetOdor(string targetOdor)
        {
            try
            {
                var trials = await _trialService.GetByTargetOdorAsync(targetOdor);
                if (trials == null || trials.Count == 0)
                {
                    return NotFound($"Hedef kokusu '{targetOdor}' olan trial bulunamadi.");
                }
                return Ok(trials);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "'{targetOdor}' hedef kokusuna sahip trial getirilirken hata meydana geldi.", targetOdor);
                return StatusCode(500, $"'{targetOdor}' hedef kokusuna sahip trial getirilirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir oturumda doğru yanıt (TP veya TN) verilmiş tüm denemeleri getirir. </summary>
        /// 
        [HttpGet("correctResponses/{sessionId:guid}")]
        [ProducesResponseType(typeof(List<TrialDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TrialDto>>> GetCorrectResponses(Guid sessionId)
        {
            try
            {
                var trials = await _trialService.GetCorrectResponsesAsync(sessionId);
                if (trials == null || trials.Count == 0)
                {
                    return NotFound($"{sessionId} oturumunda dogru yanit verilmis trial bulunamadi.");
                }
                return Ok(trials);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{sessionId} oturumundaki dogru yanitli trialler getirilirken hata meydana geldi.", sessionId);
                return StatusCode(500, $"{sessionId} oturumundaki dogru yanitli trialler getirilirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir oturumda yanlış yanıt (FP veya FN) verilmiş tüm denemeleri getirir. </summary>
        /// 
        [HttpGet("incorrectResponses/{sessionId:guid}")]
        [ProducesResponseType(typeof(List<TrialDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<TrialDto>>> GetIncorrectResponses(Guid sessionId)
        {
            try
            {
                var trials = await _trialService.GetIncorrectResponsesAsync(sessionId);
                if (trials == null || trials.Count == 0)
                {
                    return NotFound($"{sessionId} oturumunda yanlis yanit verilmis trial bulunamadi.");
                }
                return Ok(trials);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{sessionId} oturumundaki yanlis yanitli trialler getirilirken hata meydana geldi.", sessionId);
                return StatusCode(500, $"{sessionId} oturumundaki yanlis yanitli trialler getirilirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir oturumdaki denemelere ait ortalama ilk yanıt süresini (FirstResponseTime) hesaplar. </summary>
        /// 
        [HttpGet("averageResponseTime/{sessionId:guid}")]
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<double>> GetAverageResponseTime(Guid sessionId)
        {
            try
            {
                var averageResponseTime = await _trialService.GetAverageResponseTimeAsync(sessionId);
                return Ok(averageResponseTime);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{sessionId} oturumundaki triallerin ortalama yanit suresi hesaplanirken hata meydana geldi.", sessionId);
                return StatusCode(500, $"{sessionId} oturumundaki triallerin ortalama yanit suresi hesaplanirken bir hata meydana geldi.");
            }
        }
        #endregion


    }
}