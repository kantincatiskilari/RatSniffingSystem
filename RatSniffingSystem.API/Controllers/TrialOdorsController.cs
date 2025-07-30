using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Application.DTOs;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrialOdorsController : ControllerBase
    {
        private readonly ILogger<TrialOdorsController> _logger;
        private readonly ITrialOdorService<TrialOdor, TrialOdorDto, CreateTrialOdorDto> _trialOdorsService;
        public TrialOdorsController(ITrialOdorService<TrialOdor, TrialOdorDto, CreateTrialOdorDto> trialOdorService, ILogger<TrialOdorsController> logger)
        {
            _trialOdorsService = trialOdorService;
            _logger = logger;
        }

        #region CRUD Operations
        /// <summary>
        /// Butun trial kokularini getirir.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken ct)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetAllAsync(ct);
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokulari getirilirken bir hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary>
        /// Belli bir ID'ye sahip trial kokusunu getirir.
        /// </summary>

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrialOdorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrialOdorDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var trialOdor = await _trialOdorsService.GetByIdAsync(id, ct);
                if (trialOdor == null)
                {
                    _logger.LogWarning("Trial kokusu bulunamadi. ID: {TrialOdorId}", id);
                    return NotFound();
                }
                return Ok(trialOdor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokusu getirilirken bir hata meydana geldi. ID: {TrialOdorId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Bir trial kokusu olusturur. </summary>
        /// 
        [HttpPost]
        [ProducesResponseType(typeof(TrialOdorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrialOdorDto>> Create([FromBody] CreateTrialOdorDto createTrialOdorDto, CancellationToken ct)
        {
            try
            {
                if(createTrialOdorDto == null)
                {
                    _logger.LogWarning("Gecersiz trial kokusu olusturma istegi alindi.");
                    return BadRequest("Gecersiz veri.");
                }
                if(!ModelState.IsValid)
                {
                    _logger.LogWarning("Gecersiz model durumu ile trial kokusu olusturma istegi alindi.");
                    return BadRequest(ModelState);
                }

                var createdTrialOdor = await _trialOdorsService.CreateAsync(createTrialOdorDto, ct);

                return CreatedAtAction(nameof(GetById), new { id = createdTrialOdor.OdorId }, createdTrialOdor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokusu olusturulurken bir hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Bir trial kokusunu siler. </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                await _trialOdorsService.DeleteAsync(id, ct);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokusu silinirken bir hata meydana geldi. ID: {TrialOdorId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");

            }
        }
        #endregion

        #region Trial Odors Specific Operations
        /// <summary> Belirli bir denemeye (trial) ait tüm kokuları getirir. </summary>
        [HttpGet("by-trial/{trialId:guid}")]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetByTrialId(Guid trialId, CancellationToken ct)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetByTrialIdAsync(trialId);
                if (trialOdors == null || trialOdors.Count == 0)
                {
                    _logger.LogWarning("Trial kokulari bulunamadi. Trial ID: {TrialId}", trialId);
                    return NotFound();
                }
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokulari getirilirken bir hata meydana geldi. Trial ID: {TrialId}", trialId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir pozisyon indeksine sahip kokuları getirir. </summary>
        /// 
        [HttpGet("by-position/{positionIndex:int}")]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetByPositionIndex(int positionIndex, CancellationToken ct)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetByPositionIndexAsync(positionIndex);
                if (trialOdors == null || trialOdors.Count == 0)
                {
                    _logger.LogWarning("Trial kokulari bulunamadi. Pozisyon Indeksi: {PositionIndex}", positionIndex);
                    return NotFound();
                }
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokulari getirilirken bir hata meydana geldi. Pozisyon Indeksi: {PositionIndex}", positionIndex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir odor ID’sine sahip tüm TrialOdor kayıtlarını getirir. </summary>
        /// 
        [HttpGet("by-odor/{odorId:guid}")]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByOdorId(Guid odorId, CancellationToken ct)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetByOdorIdAsync(odorId);
                if (trialOdors == null || trialOdors.Count == 0)
                {
                    _logger.LogWarning("Trial kokulari bulunamadi. Odor ID: {OdorId}", odorId);
                    return NotFound();
                }
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokulari getirilirken bir hata meydana geldi. Odor ID: {OdorId}", odorId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir koku tipine (OdorType) sahip kokulari getirir. </summary>
        /// 
        [HttpGet("by-odor-type/{odorType}")]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetByOdorType(string odorType, CancellationToken ct)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetByOdorTypeAsync((Domain.Enums.OdorType)Enum.Parse(typeof(Domain.Enums.OdorType), odorType, true));
                if (trialOdors == null || trialOdors.Count == 0)
                {
                    _logger.LogWarning("Trial kokulari bulunamadi. Odor Tipi: {OdorType}", odorType);
                    return NotFound();
                }
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Trial kokulari getirilirken bir hata meydana geldi. Odor Tipi: {OdorType}", odorType);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> False positive (yanlış pozitif) olarak işaretlenmiş tüm TrialOdor kayıtlarını getirir. </summary>
        /// 
        [HttpGet("false-positives")]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetFalsePositives(CancellationToken ct)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetFalsePositivesAsync();
                if (trialOdors == null || trialOdors.Count == 0)
                {
                    _logger.LogWarning("False positive trial kokulari bulunamadi.");
                    return NotFound();
                }
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "False positive trial kokulari getirilirken bir hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir deneme içerisindeki false positive kokuları getirir. </summary>
        /// 
        [HttpGet("false-positives/by-trial/{trialId:guid}")]
        [ProducesResponseType(typeof(IReadOnlyList<TrialOdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetFalsePositivesByTrialId(Guid trialId)
        {
            try
            {
                var trialOdors = await _trialOdorsService.GetFalsePositivesByTrialIdAsync(trialId);
                if (trialOdors == null || trialOdors.Count == 0)
                {
                    _logger.LogWarning("Deneme icindeki false positive trial kokulari bulunamadi. Trial ID: {TrialId}", trialId);
                    return NotFound();
                }
                return Ok(trialOdors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deneme icindeki false positive trial kokulari getirilirken bir hata meydana geldi. Trial ID: {TrialId}", trialId);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        #endregion

    }

}
