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
    public class ExperimentalNotesController : ControllerBase
    {
        private readonly IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto> _experimentalNoteService;
        private readonly ILogger<ExperimentalNotesController> _logger;

        public ExperimentalNotesController(IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto> experimentalNoteService, ILogger<ExperimentalNotesController> logger)
        {
            _experimentalNoteService = experimentalNoteService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm deney notlarini getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<ExperimentalNoteDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<ExperimentalNoteDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var experimentalNotes = await _experimentalNoteService.GetAllAsync(ct);
                return Ok(experimentalNotes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Deney notlari getirilirken hata meydana geldi");
                return StatusCode(500, "Deney notlari getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre deney notu getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ExperimentalNoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ExperimentalNote>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var experimentalNote = await _experimentalNoteService.GetByIdAsync(id, ct);
                if (experimentalNote == null)
                {
                    return NotFound($"{id}'li deney notu bulunamadi.");
                }
                return Ok(experimentalNote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li deney notu getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li deney notu getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni deney notu oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ExperimentalNoteDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateExperimentalNoteDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Deney notu verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdExperimentalNote = await _experimentalNoteService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdExperimentalNote.Id }, createdExperimentalNote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir deney notu olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir deney notu olusturulurken hata meydana geldi");
            }
        }

        

        /// <summary>
        /// Deney notunu siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var exists = await _experimentalNoteService.ExistsAsync(id, ct);
                if(!exists)
                    return NotFound($"{id}'li deney notu bulunamadi.");

                await _experimentalNoteService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li session silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li session silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Experimantal Note Specific Operations
        /// <summary>
        /// Belirli bir baslik etiketine sahip deney notlarini getirir.
        /// </summary>
        [HttpGet("by-title/{title}")]
        [ProducesResponseType(typeof(ExperimentalNoteDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<ExperimentalNoteDto>> GetByTitle(string title)
        {
            try
            {
                var experimentalNote = await _experimentalNoteService.GetByTitleAsync(title);
                if (experimentalNote == null)
                {
                    return NotFound($"'{title}' baslik etiketine sahip deney notu bulunamadi.");
                }
                return Ok(experimentalNote);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "'{Title}' baslik etiketine sahip deney notu getirilirken hata meydana geldi", title);
                return StatusCode(500, $"'{title}' baslik etiketine sahip deney notu getirilirken bir hata meydana geldi");
            }
        }

        #endregion


    }
}