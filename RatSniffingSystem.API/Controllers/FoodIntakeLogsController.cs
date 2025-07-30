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
    public class FoodIntakeLogsController : ControllerBase
    {
        private readonly IFoodIntakeLogService<FoodIntakeLog, FoodIntakeLogDto, CreateFoodIntakeLogDto> _foodIntakeLogService;
        private readonly ILogger<FoodIntakeLogsController> _logger;

        public FoodIntakeLogsController(IFoodIntakeLogService<FoodIntakeLog, FoodIntakeLogDto, CreateFoodIntakeLogDto> foodIntakeLogService, ILogger<FoodIntakeLogsController> logger)
        {
            _foodIntakeLogService = foodIntakeLogService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm alinan besinleri getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<FoodIntakeLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<FoodIntakeLogDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var foodIntakeLogs = await _foodIntakeLogService.GetAllAsync(ct);
                return Ok(foodIntakeLogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alinan besinler getirilirken hata meydana geldi");
                return StatusCode(500, "Alinan besinler getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre alinan besini getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(FoodIntakeLogDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<FoodIntakeLogDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var foodIntakeLog = await _foodIntakeLogService.GetByIdAsync(id, ct);
                if (foodIntakeLog == null)
                {
                    return NotFound($"{id}'li deney notu bulunamadi.");
                }
                return Ok(foodIntakeLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li alinan besin getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li alinan besin getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni alinan besin oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(FoodIntakeLogDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateFoodIntakeLogDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Alinan besin verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdFoodIntakeLog = await _foodIntakeLogService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdFoodIntakeLog.Id }, createdFoodIntakeLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir alinan besin olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir alinan besin olusturulurken hata meydana geldi");
            }
        }

        

        /// <summary>
        /// Alinan besini siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var exists = await _foodIntakeLogService.ExistsAsync(id, ct);
                if(!exists)
                    return NotFound($"{id}'li deney notu bulunamadi.");

                await _foodIntakeLogService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li session silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li session silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Food Intake Log Specific Operations
        /// <summary>
        /// CC araligina gore kayitlari getirir.
        /// </summary>
        /// 
        [HttpGet("cc-range")]
        [ProducesResponseType(typeof(IReadOnlyList<FoodIntakeLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<FoodIntakeLogDto>>> GetByCcRange([FromQuery] double first, [FromQuery] double last)
        {
            try
            {
                var foodIntakeLogs = await _foodIntakeLogService.GetByCcRangeAsync(first, last);
                if (foodIntakeLogs == null || foodIntakeLogs.Count == 0)
                {
                    return NotFound($"CC degeri {first} ile {last} arasinda olan alinan besin kaydi bulunamadi.");
                }
                return Ok(foodIntakeLogs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "CC araligina gore alinan besinler getirilirken hata meydana geldi");
                return StatusCode(500, "CC araligina gore alinan besinler getirilirken bir hata meydana geldi");
            }
        }


        #endregion


    }
}