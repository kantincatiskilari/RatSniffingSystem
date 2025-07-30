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
    public class SessionsController : ControllerBase
    {
        private readonly ISessionService<Session, SessionDto, CreateSessionDto, UpdateSessionDto> _sessionService;
        private readonly ILogger<SessionsController> _logger;

        public SessionsController(ISessionService<Session, SessionDto, CreateSessionDto, UpdateSessionDto> sessionService, ILogger<SessionsController> logger)
        {
            _sessionService = sessionService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm sessionlari getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<TrialDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var sessions = await _sessionService.GetAllAsync(ct);
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sessionlar getirilirken hata meydana geldi");
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre session getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TrialDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var session = await _sessionService.GetByIdAsync(id, ct);
                if (session == null)
                {
                    return NotFound($"{id}'li session bulunamadi.");
                }
                return Ok(session);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li session getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li session getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni session oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateSessionDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Session verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdSession = await _sessionService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdSession.Id }, createdSession);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir session olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir session olusturulurken hata meydana geldi");
            }
        }

        

        /// <summary>
        /// Session'i siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var exists = await _sessionService.ExistsAsync(id, ct);
                if(!exists)
                    return NotFound($"{id}'li session bulunamadi.");

                await _sessionService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li session silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li session silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Session Specific Operations
        ///<summary>Belirli bir RatId'ye sahip oturumlari getirir.</summary>
        ///
        [ProducesResponseType(typeof(SessionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ByRat/{ratId:guid}")]
        public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetByRatId(Guid ratId)
        {
            try
            {
                var sessions = await _sessionService.GetByRatIdAsync(ratId);
                if (sessions == null || sessions.Count == 0)
                {
                    return NotFound($"RatId'si {ratId} olan session bulunamadi.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RatId'si {RatId} olan sessionlar getirilirken hata meydana geldi", ratId);
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        ///<summary>Belirli bir TrainerId'ye sahip oturumlari getirir.</summary>
        ///
        [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ByTrainer/{trainerId:guid}")]

        public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetByTrainerId(Guid trainerId)
        {
            try
            {
                var sessions = await _sessionService.GetByTrainerIdAsync(trainerId);
                if (sessions == null || sessions.Count == 0)
                {
                    return NotFound($"TrainerId'si {trainerId} olan session bulunamadi.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "TrainerId'si {TrainerId} olan sessionlar getirilirken hata meydana geldi", trainerId);
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        ///<summary>Kafes Koduna gore oturumlari getirir.</summary>
        ///
        [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ByCageCode/{cageCode}")]
        public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetByCageCode(string cageCode)
        {
            try
            {
                var sessions = await _sessionService.GetByCageCodeAsync(cageCode);
                if (sessions == null || sessions.Count == 0)
                {
                    return NotFound($"Kafes Kodu '{cageCode}' olan session bulunamadi.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kafes Kodu '{CageCode}' olan sessionlar getirilirken hata meydana geldi", cageCode);
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        ///<summary>Belirli aralikta sureye sahip oturumlari getirir.</summary>
        ///
        [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ByDurationRange")]
        public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetByDurationRange([FromQuery] int minDurationMinutes, [FromQuery] int maxDurationMinutes)
        {
            try
            {
                var sessions = await _sessionService.GetByDurationRangeAsync(minDurationMinutes, maxDurationMinutes);
                if (sessions == null || sessions.Count == 0)
                {
                    return NotFound($"Sure araligi {minDurationMinutes}-{maxDurationMinutes} dakika olan session bulunamadi.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sure araligi {MinDuration}-{MaxDuration} dakika olan sessionlar getirilirken hata meydana geldi", minDurationMinutes, maxDurationMinutes);
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        ///<summary>Belirtilen tarih aralığındaki tüm seansları getirir.</summary>
        ///
        [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ByDateRange")]
        public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sessions = await _sessionService.GetByDateRangeAsync(startDate, endDate);
                if (sessions == null || sessions.Count == 0)
                {
                    return NotFound($"Tarih araligi {startDate.ToShortDateString()} - {endDate.ToShortDateString()} olan session bulunamadi.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Tarih araligi {StartDate} - {EndDate} olan sessionlar getirilirken hata meydana geldi", startDate, endDate);
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        ///<summary>Belirtilen materyal tipindeki tüm seansları getirir.</summary>
        ///
        [ProducesResponseType(typeof(IReadOnlyList<SessionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ByMaterialType/{materialType}")]
        public async Task<ActionResult<IReadOnlyList<SessionDto>>> GetByMaterialType(string materialType)
        {
            try
            {
                var sessions = await _sessionService.GetByMaterialTypeAsync(materialType);
                if (sessions == null || sessions.Count == 0)
                {
                    return NotFound($"Materyal tipi '{materialType}' olan session bulunamadi.");
                }
                return Ok(sessions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Materyal tipi '{MaterialType}' olan sessionlar getirilirken hata meydana geldi", materialType);
                return StatusCode(500, "Sessionlar getirilirken bir hata meydana geldi");
            }
        }

        ///<summary>Tüm seanslarin ortalama sure hesabini getirir.</summary>
        ///
        [ProducesResponseType(typeof(double), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("AverageDuration")]
        public async Task<ActionResult<double>> GetAverageDuration()
        {
            try
            {
                var averageDuration = await _sessionService.GetAverageDurationAsync();
                return Ok(averageDuration);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sessionlarin ortalama suresi getirilirken hata meydana geldi");
                return StatusCode(500, "Sessionlarin ortalama suresi getirilirken bir hata meydana geldi");
            }
        }   
        #endregion

    }
}