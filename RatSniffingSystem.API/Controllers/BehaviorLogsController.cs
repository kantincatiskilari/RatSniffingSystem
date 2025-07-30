using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using RatSniffingSystem.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BehaviorLogsController : ControllerBase
    {
        private readonly IBehaviorLogService<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto> _behaviorLogService;
        private readonly ILogger<BehaviorLogsController> _logger;

        public BehaviorLogsController(IBehaviorLogService<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto> behaviorLogService, ILogger<BehaviorLogsController> logger)
        {
            _behaviorLogService = behaviorLogService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm davranis kayitlarini getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<BehaviorLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<BehaviorLogDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var behaviorLogs = await _behaviorLogService.GetAllAsync(ct);
                return Ok(behaviorLogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Davranislar getirilirken hata meydana geldi");
                return StatusCode(500, "Davranislar getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre davranis kaydi getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(BehaviorLogDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<BehaviorLogDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var behaviorLog = await _behaviorLogService.GetByIdAsync(id, ct);
                if (behaviorLog == null)
                {
                    return NotFound($"{id}'li davranis kaydi bulunamadi.");
                }
                return Ok(behaviorLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li davranis kaydi getirilirken hata meydana geldi", id);
                return Problem(
                   title: "Davranis kaydi getirilirken bir hata meydana geldi.",
                   detail: ex.Message,
                   statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Yeni koku oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BehaviorLogDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBehaviorLogDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Davranis kaydi verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdRat = await _behaviorLogService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdRat.Id }, createdRat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir davranis kaydi olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir davranis olusturulurken hata meydana geldi");
            }
        }



        /// <summary>
        /// Kokuyu siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var exists = await _behaviorLogService.ExistsAsync(id, ct);
                if (!exists)
                {
                    return NotFound();
                }

                await _behaviorLogService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li davranis kaydi silinirken hata meydana geldi", id);
                return Problem(
                    title: "Davranis kaydi silinirken bir hata meydana geldi.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #endregion

        #region Behavior Log Specific Operations
        /// <summary> Belirli bir davranis etiketine sahip sicanlari getirir </summary>
        /// 
        [HttpGet("ByBehaviorType/{type}")]
        [ProducesResponseType(typeof(IReadOnlyList<BehaviorLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<BehaviorLogDto>>> GetByBehaviorType(BehaviorType type, CancellationToken ct)
        {
            try
            {
                var behaviorLogs = await _behaviorLogService.GetByBehaviorTypeAsync(type, ct);
                if (behaviorLogs == null || !behaviorLogs.Any())
                {
                    return NotFound($"{type} davranis etiketine sahip kayit bulunamadi.");
                }
                return Ok(behaviorLogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Type} davranis etiketine sahip kayitlar getirilirken hata meydana geldi", type);
                return StatusCode(500, $"{type} davranis etiketine sahip kayitlar getirilirken hata meydana geldi");


            }
        }
        #endregion


    }
}