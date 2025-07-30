using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Persistence.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OdorsController : ControllerBase
    {
        private readonly IOdorService<Odor, OdorDto, CreateOdorDto> _odorService;
        private readonly ILogger<OdorsController> _logger;

        public OdorsController(IOdorService<Odor, OdorDto, CreateOdorDto> odorService, ILogger<OdorsController> logger)
        {
            _odorService = odorService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm kokulari getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<OdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<OdorDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var odors = await _odorService.GetAllAsync(ct);
                return Ok(odors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kokular getirilirken hata meydana geldi");
                return StatusCode(500, "Kokular getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre koku getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(OdorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OdorDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var odor = await _odorService.GetByIdAsync(id, ct);
                if (odor == null)
                {
                    return NotFound($"{id}'li koku bulunamadi.");
                }
                return Ok(odor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li koku getirilirken hata meydana geldi", id);
                return Problem(
                   title: "Koku getirilirken bir hata meydana geldi.",
                   detail: ex.Message,
                   statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Yeni koku oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(OdorDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateOdorDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                {
                    return BadRequest("Koku verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdOdor = await _odorService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdOdor.Id }, createdOdor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir koku kaydi olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir koku olusturulurken hata meydana geldi");
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
                var exists = await _odorService.ExistsAsync(id, ct);
                if (!exists)
                {
                    return NotFound();
                }

                await _odorService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li koku silinirken hata meydana geldi", id);
                return Problem(
                    title: "Koku silinirken bir hata meydana geldi.",
                    detail: ex.Message,
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }

        #endregion

        #region Odor Specific Operations
        /// <summary> Belirli bir isme sahip kokulari getirir </summary>
        [HttpGet("by-name/{name}")]
        [ProducesResponseType(typeof(IReadOnlyList<OdorDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<OdorDto?>>> GetByName(string name)
        {
            try
            {
                var odors = await _odorService.GetByNameAsync(name);
                return Ok(odors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kokular isimlerine göre getirilirken hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }
        /// <summary> Belirli bir external koda sahip kokuyu getirir </summary>
        /// 
        [HttpGet("by-external-code")]
        [ProducesResponseType(typeof(OdorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<OdorDto>> GetByExternalCode([FromQuery] string externalCode)
        {
            try
            {
                var odor = await _odorService.GetByExternalCodeAsync(externalCode);
                if (odor == null)
                {
                    _logger.LogWarning("Koku bulunamadi. External Code: {ExternalCode}", externalCode);
                    return NotFound();
                }
                return Ok(odor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Koku dissal koda gore getirilirken bir hata meydana geldi. External Code: {ExternalCode}", externalCode);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belirli bir kategorideki kokulari getirir </summary>
        /// 
        [HttpGet("by-category/{category}")]
        [ProducesResponseType(typeof(IReadOnlyList<OdorDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<OdorDto>>> GetByCategory(Domain.Enums.OdorCategory category, CancellationToken ct)
        {
            try
            {
                var odors = await _odorService.GetByCategoryAsync(category, ct);
                if (odors == null || odors.Count == 0)
                {
                    return NotFound($"{category} kategorisinde koku bulunamadi.");
                }
                return Ok(odors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Category} kategorisindeki kokular getirilirken hata meydana geldi", category);
                return StatusCode(500, $"{category} kategorisindeki kokular getirilirken hata meydana geldi");
            }
        }

        /// <summary> Kokunun olumcul olup olmamasina gore kayitlari getirir </summary>
        /// 
        [HttpGet("by-hazardous-status/{isHazardous}")]
        [ProducesResponseType(typeof(IReadOnlyList<OdorDto?>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IReadOnlyList<OdorDto?>>> GetByHazardousStatus(bool isHazardous)
        {
            try
            {
                var odors = await _odorService.GetByHazardousStatusAsync(isHazardous);
                if (odors == null || odors.Count == 0)
                {
                    return NotFound($"Olumcul durumu '{isHazardous}' olan koku bulunamadi.");
                }
                return Ok(odors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Olumcul durumu '{IsHazardous}' olan kokular getirilirken hata meydana geldi.", isHazardous);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }
        #endregion
    }
}