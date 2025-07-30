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
    public class InterventionsController : ControllerBase
    {
        private readonly IInterventionService<Intervention, InterventionDto, CreateInterventionDto> _interventionService;
        private readonly ILogger<InterventionsController> _logger;

        public InterventionsController(IInterventionService<Intervention, InterventionDto, CreateInterventionDto> interventionService, ILogger<InterventionsController> logger)
        {
            _interventionService = interventionService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm mudaheleleri getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<InterventionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<InterventionDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var interventions = await _interventionService.GetAllAsync(ct);
                return Ok(interventions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Alinan besinler getirilirken hata meydana geldi");
                return StatusCode(500, "Alinan besinler getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre mudaheleleyi getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(InterventionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<InterventionDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var intervention = await _interventionService.GetByIdAsync(id, ct);
                if (intervention == null)
                {
                    return NotFound($"{id}'li mudahele bulunamadi.");
                }
                return Ok(intervention);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li mudahele getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li  mudahele getirilirken getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni bir mudahele oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(InterventionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateInterventionDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Mudahele verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdInterventionDto = await _interventionService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdInterventionDto.Id }, createdInterventionDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir mudahele olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir mudahele olusturulurken hata meydana geldi");
            }
        }



        /// <summary>
        /// Bir mudaheleyi siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var exists = await _interventionService.ExistsAsync(id, ct);
                if (!exists)
                    return NotFound($"{id}'li mudahele bulunamadi.");

                await _interventionService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li mudahele silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li mudahele silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Food Intake Log Specific Operations
        /// <summary>
        /// Verilen maddeye gore mudahele getirir.
        /// </summary>

        [HttpGet("by-substance")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(IReadOnlyList<InterventionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<InterventionDto>>> GetBySubstance([FromQuery] string substance)
        {
            try
            {
                var interventions = await _interventionService.GetBySubstanceAsync(substance);
                if(interventions == null || interventions.Count == 0)
                {
                    return NotFound($"{substance} maddesine gore herhangi bir mudahele bulunamadi.");
                }
                return Ok(interventions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Substance} maddesine gore mudaheleler getirilirken hata meydana geldi", substance);
                return StatusCode(500, $"{substance} maddesine gore mudaheleler getirilirken bir hata meydana geldi");
            }
        }

        #endregion


    }
}