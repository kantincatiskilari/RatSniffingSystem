using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Persistence.Services;

using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Domain.Enums;
using RatSniffingSystem.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RatSniffingSystem.Domain.Entities;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatsController : ControllerBase
    {
        private readonly IRatService<Rat,RatDto, CreateRatDto, UpdateRatDto> _ratService;
        private readonly ILogger<RatsController> _logger;

        public RatsController(IRatService<Rat, RatDto, CreateRatDto, UpdateRatDto> ratService, ILogger<RatsController> logger)
        {
            _ratService = ratService;
            _logger = logger;
        }

        #region CRUD Operations

        /// <summary>
        /// Tüm sıçanları getirir
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<RatDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyList<RatDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var rats = await _ratService.GetAllAsync(ct);
                return Ok(rats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Sicanlar getirilirken hata meydana geldi");
                return StatusCode(500, "Sicanlar getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// ID'ye göre sıçan getirir
        /// </summary>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RatDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RatDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var rat = await _ratService.GetByIdAsync(id);
                if (rat == null)
                {
                    return NotFound($"{id}'li sican bulunamadi.");
                }
                return Ok(rat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li sican getirilirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li sican getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Yeni sıçan oluşturur
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RatDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateRatDto createRatDto, CancellationToken ct)
        {
            try
            {
                if (createRatDto == null)
                {
                    return BadRequest("Rat verisi bos.");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createdRat = await _ratService.CreateAsync(createRatDto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdRat.Id }, createdRat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Bir rat olusturulurken hata meydana geldi.");
                return StatusCode(500, "Bir rat olusturulurken hata meydana geldi");
            }
        }

        /// <summary>
        /// Sıçan bilgilerini günceller
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(RatDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, UpdateRatDto dto, CancellationToken ct)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Guncellenecek rat verisi saglayiniz.");

                if (dto.Id != id)
                    return BadRequest("URL'deki ID ile gövdedeki ID uyuşmuyor.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                await _ratService.UpdateAsync(id, dto, ct);

                var updatedRat = await _ratService.GetByIdAsync(id, ct);
                if (updatedRat == null)
                    return NotFound($"{id}'li sican bulunamadi.");

                return Ok(updatedRat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li sican güncellenirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li sican güncellenirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Sıçanı siler
        /// </summary>
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                await _ratService.DeleteAsync(id, ct);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Id} id'li sican silinirken hata meydana geldi", id);
                return StatusCode(500, $"{id} id'li sican silinirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Specific Rat Operations

        /// <summary>
        /// Proje etiketine göre sıçanları getirir
        /// </summary>
        [HttpGet("project/{projectTag}")]
        public async Task<IActionResult> GetByProjectTag(string projectTag)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(projectTag))
                    return BadRequest("Proje etiketi boş olamaz.");

                var rats = await _ratService.GetByProjectTagAsync(projectTag);

                if (rats == null || !rats.Any())
                    return NotFound($"'{projectTag}' proje etiketine sahip sıçan bulunamadı.");

                return Ok(rats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{ProjectTag} proje etiketli sicanlar getirilirken hata meydana geldi", projectTag);
                return StatusCode(500, $"'{projectTag}' proje etiketli sicanlar getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Koda göre sıçan getirir
        /// </summary>
        [HttpGet("get-by-code/{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(code))
                    return BadRequest("Sıçan kodu boş olamaz.");

                var rat = await _ratService.FindByCodeAsync(code);
                if (rat == null)
                {
                    return NotFound($"Kod {code} ile eşleşen bir rat bulunamadı.");
                }
                return Ok(rat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Code} kodlu sican getirilirken hata meydana geldi", code);
                return StatusCode(500, $"'{code}' kodlu sican getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Doğum tarih aralığına göre sıçanları getirir
        /// </summary>
        [HttpGet("birth-date-range")]
        public async Task<IActionResult> GetByBirthDateRange(
            [FromQuery] DateTime startDate,
            [FromQuery] DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    return BadRequest("Başlangıç tarihi bitiş tarihinden büyük olamaz.");

                if (startDate > DateTime.Now || endDate > DateTime.Now)
                    return BadRequest("Gelecek tarihler girilemez.");

                var rats = await _ratService.GetByBirthDateRangeAsync(startDate, endDate);

                if (rats == null || !rats.Any())
                    return NotFound("Belirtilen tarih aralığında sıçan bulunamadı.");

                return Ok(rats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{StartDate} - {EndDate} tarih aralığında sicanlar getirilirken hata meydana geldi",
                    startDate, endDate);
                return StatusCode(500, "Tarih aralığında sicanlar getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Cinsiyete göre sıçanları getirir
        /// </summary>
        [HttpGet("gender/{gender}")]
        public async Task<IActionResult> GetByGender(Gender gender)
        {
            try
            {
                if (!Enum.IsDefined(typeof(Gender), gender))
                    return BadRequest("Geçersiz cinsiyet değeri.");

                var rats = await _ratService.GetByGenderAsync(gender);

                if (rats == null || !rats.Any())
                    return NotFound($"'{gender}' cinsiyetinde sıçan bulunamadı.");

                return Ok(rats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Gender} cinsiyetindeki sicanlar getirilirken hata meydana geldi", gender);
                return StatusCode(500, $"'{gender}' cinsiyetindeki sicanlar getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Aktiflik durumuna göre sıçanları getirir
        /// </summary>
        [HttpGet("status/{isActive}")]
        public async Task<IActionResult> GetByStatus(bool isActive)
        {
            try
            {
                var rats = await _ratService.GetByStatusAsync(isActive);

                string statusText = isActive ? "aktif" : "pasif";

                if (rats == null || !rats.Any())
                    return NotFound($"{statusText} durumunda sıçan bulunamadı.");

                return Ok(new
                {
                    Status = statusText,
                    TotalCount = rats.Count,
                    Data = rats
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{IsActive} durumundaki sicanlar getirilirken hata meydana geldi", isActive);
                return StatusCode(500, $"{(isActive ? "Aktif" : "Pasif")} sicanlar getirilirken bir hata meydana geldi");
            }
        }

        #endregion

        #region Statistics and Additional Endpoints

        /// <summary>
        /// Aktif sıçanların sayısını getirir
        /// </summary>
        [HttpGet("count/active")]
        public async Task<IActionResult> GetActiveRatCount()
        {
            try
            {
                var activeRats = await _ratService.GetByStatusAsync(true);
                return Ok(new { ActiveRatCount = activeRats?.Count ?? 0 });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Aktif sican sayisi getirilirken hata meydana geldi");
                return StatusCode(500, "Aktif sican sayisi getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Cinsiyet dağılımını getirir
        /// </summary>
        [HttpGet("statistics/gender-distribution")]
        public async Task<IActionResult> GetGenderDistribution()
        {
            try
            {
                var males = await _ratService.GetByGenderAsync(Gender.Male);
                var females = await _ratService.GetByGenderAsync(Gender.Female);

                var result = new
                {
                    Male = males?.Count ?? 0,
                    Female = females?.Count ?? 0,
                    Total = (males?.Count ?? 0) + (females?.Count ?? 0)
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cinsiyet dagilimi getirilirken hata meydana geldi");
                return StatusCode(500, "Cinsiyet dagilimi getirilirken bir hata meydana geldi");
            }
        }

        /// <summary>
        /// Proje bazlı sıçan sayılarını getirir
        /// </summary>
        [HttpGet("statistics/project-distribution")]
        public async Task<IActionResult> GetProjectDistribution()
        {
            try
            {
                var allRats = await _ratService.GetAllAsync();

                var projectGroups = allRats
                    .GroupBy(r => r.ProjectTag)
                    .Select(g => new { ProjectTag = g.Key, Count = g.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                return Ok(projectGroups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Proje dagilimi getirilirken hata meydana geldi");
                return StatusCode(500, "Proje dagilimi getirilirken bir hata meydana geldi");
            }
        }

        #endregion
    }
}