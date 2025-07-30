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
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService<Trainer, TrainerDto, CreateTrainerDto, UpdateTrainerDto> _trainerService;
        private readonly ILogger<TrainersController> _logger;

        public TrainersController(ITrainerService<Trainer, TrainerDto, CreateTrainerDto, UpdateTrainerDto> trainerService, ILogger<TrainersController> logger)
        {
            _trainerService = trainerService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm eğitmenleri getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var trainers = await _trainerService.GetAllAsync(ct);
                return Ok(trainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Egitmenler getirilirken hata meydana geldi");
                return StatusCode(500, "Egitmenler getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre eğitmen getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<TrainerDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var trainer = await _trainerService.GetByIdAsync(id, ct);
                if (trainer == null)
                {
                    return NotFound($"{id}'li egitmen bulunamadi.");
                }
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li egitmen getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li egitmen getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni eğitmen oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateTrainerDto createTrainerDto, CancellationToken ct)
        {
            try
            {
                if (createTrainerDto == null)
                {
                    return BadRequest("Egitmen verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdTrainer = await _trainerService.CreateAsync(createTrainerDto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir egitmen olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir egitmen olusturulurken hata meydana geldi");
            }
        }

        /// <summary>
        /// Eğitmen bilgilerini günceller
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrainerDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Guncellenecek egitmen verisi saglayiniz.");

                if (dto.Id != id)
                    return BadRequest("URL'deki ID ile gövdedeki ID uyuşmuyor.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var trainer = await _trainerService.UpdateAsync(id, dto, ct);
                if (trainer == null)
                    return NotFound($"{id}'li egitmen bulunamadi.");

                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li egitmen güncellenirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li egitmen güncellenirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Eğitmeni siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var exists = await _trainerService.ExistsAsync(id);
                if (!exists)
                    return NotFound($"{id}'li egitmen bulunamadi.");

                await _trainerService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li egitmen silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li egitmen silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Specific Trainer Operations

        /// <summary>
        /// E-posta adresine göre eğitmen getirir
        /// </summary>
        [HttpGet("email/{email}")]
        public async Task<ActionResult<TrainerDto?>> GetByEmail(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return BadRequest("E-posta adresi boş olamaz.");

                var trainer = await _trainerService.GetByEmailAsync(email);
                if (trainer == null)
                {
                    return NotFound($"E-posta adresi {email} olan egitmen bulunamadi.");
                }
                return Ok(trainer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "E-posta adresi {Email} olan egitmen getirilirken hata meydana geldi", email);
                return StatusCode(500, "E-posta ile egitmen getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Aktif eğitmenleri getirir
        /// </summary>
        [HttpGet("active")]
        public async Task<ActionResult<List<TrainerDto>>> GetActiveTrainers()
        {
            try
            {
                var trainers = await _trainerService.GetAllAsync();
                var activeTrainers = trainers?.Where(t => t.IsActive == true).ToList();

                if (activeTrainers == null || !activeTrainers.Any())
                    return NotFound("Aktif egitmen bulunamadi.");

                return Ok(activeTrainers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktif egitmenler getirilirken hata meydana geldi");
                return StatusCode(500, "Aktif egitmenler getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Eğitmen sayısını getirir
        /// </summary>
        [HttpGet("count")]
        public async Task<ActionResult<int>> GetTrainerCount()
        {
            try
            {
                var trainers = await _trainerService.GetAllAsync();
                return Ok(trainers?.Count ?? 0);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Egitmen sayisi getirilirken hata meydana geldi");
                return StatusCode(500, "Egitmen sayisi getirilirken bir hata meydana geldi");
            }
        }

        #endregion
    }
}