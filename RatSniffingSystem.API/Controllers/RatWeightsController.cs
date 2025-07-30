using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatWeightsController : ControllerBase
    {
        private readonly ILogger<RatWeightsController> _logger;
        private readonly IRatWeightService<RatWeight, RatWeightDto, CreateRatWeightDto> _ratWeightService;

        public RatWeightsController(ILogger<RatWeightsController> logger, IRatWeightService<RatWeight, RatWeightDto, CreateRatWeightDto> ratWeightService)
        {
            _logger = logger;
            _ratWeightService = ratWeightService;
        }

        #region CRUD Operations
        /// <summary> Butun rat agirliklarini getirir. </summary>
        /// 
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<RatWeightDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<RatWeightDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var ratWeights = await _ratWeightService.GetAllAsync(ct);
                if (ratWeights == null || !ratWeights.Any())
                {
                    _logger.LogWarning("Hiç rat ağırlığı bulunamadı.");
                    return NotFound();
                }
                return Ok(ratWeights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rat agirliklari getirilirken bir hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Belli bir ID'ye sahip rat agirligini getirir. </summary>
        /// 
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RatWeightDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RatWeightDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var ratWeight = await _ratWeightService.GetByIdAsync(id, ct);
                if (ratWeight == null)
                {
                    _logger.LogWarning("Rat agirligi bulunamadi. ID: {RatWeightId}", id);
                    return NotFound();
                }
                return Ok(ratWeight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rat agirligi getirilirken bir hata meydana geldi. ID: {RatWeightId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        /// <summary> Yeni bir rat agirligi olusturur. </summary>

        [HttpPost]
        [ProducesResponseType(typeof(RatWeightDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<RatWeightDto>> Create([FromBody] CreateRatWeightDto createRatWeightDto, CancellationToken ct)
        {
            try
            {
                if (createRatWeightDto == null)
                {
                    _logger.LogWarning("Gecersiz rat agirligi olusturma istegi alindi.");
                    return BadRequest("Gecersiz veri.");
                }
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("Rat agirligi olusturma istegi gecerli degil.");
                    return BadRequest(ModelState);
                }
                var createdRatWeight = await _ratWeightService.CreateAsync(createRatWeightDto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdRatWeight.Id }, createdRatWeight);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rat agirligi olusturulurken bir hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                await _ratWeightService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rat agirligi silinirken bir hata meydana geldi. ID: {RatWeightId}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");

            }
        }

        #endregion

        #region Rat Weight Specific Operations
        /// <summary> Belirli bir kilo araligina sahip rat agirliklarini getirir. </summary>
        /// 
        [HttpGet("by-weight-range")]
        [ProducesResponseType(typeof(IReadOnlyList<RatWeightDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<RatWeightDto>>> GetByWeightRange([FromQuery] decimal minGram, [FromQuery] decimal maxGram)
        {
            try
            {
                var ratWeights = await _ratWeightService.GetByWeightRangeAsync(minGram, maxGram);
                if (ratWeights == null || !ratWeights.Any())
                {
                    _logger.LogWarning("Belirtilen kilo araliginda rat agirligi bulunamadi. Min: {MinGram}, Max: {MaxGram}", minGram, maxGram);
                    return NotFound();
                }
                return Ok(ratWeights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Rat agirliklari kilo araligina gore getirilirken bir hata meydana geldi. Min: {MinGram}, Max: {MaxGram}", minGram, maxGram);
                return StatusCode(StatusCodes.Status500InternalServerError, "Istek islenirken bir hata meydana geldi.");
            }
        }
        #endregion
    }
}
