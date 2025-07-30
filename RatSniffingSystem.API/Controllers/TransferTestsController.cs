using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;

namespace RatSniffingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferTestsController : ControllerBase
    {
        private readonly ITransferTestService<TransferTest, TransferTestDto, CreateTransferTestDto> _transferTestService;
        private readonly ILogger<TransferTestsController> _logger;

        public TransferTestsController(ITransferTestService<TransferTest, TransferTestDto, CreateTransferTestDto> transferTestService, ILogger<TransferTestsController> logger)
        {
            _transferTestService = transferTestService;
            _logger = logger;
        }

        #region CRUD OPERATIONS
        ///<summary>Tum transfer testi gecmisini getirir.</summary>
        ///
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyList<TransferTestDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<TransferTestDto>>> GetAll(CancellationToken ct)
        {
            try
            {
                var transferTests = await _transferTestService.GetAllAsync(ct);
                return Ok(transferTests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transfer testler getilirken hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatası meydana geldi.");
            }
        }

        ///<summary>Id'ye gore transfer testi gecmisini getirir.</summary>
        ///
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TransferTestDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<TransferTestDto>> GetById(Guid id, CancellationToken ct)
        {
            try
            {
                var transferTest = await _transferTestService.GetByIdAsync(id, ct);
                if (transferTest == null)
                {
                    return NotFound($"{id}'li transfer test bulunamadi.");
                }
                return Ok(transferTest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{id}'li transfer testi getirilirken hata olustu.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatasi meydana geldi.");
            }
        }

        ///<summary>Yeni transfer testi gecmisi olusturur.</summary>
        ///
        [HttpPost]
        [ProducesResponseType(typeof(TransferTestDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTransferTestDto createTransferTestDto, CancellationToken ct)
        {
            try
            {
                if(createTransferTestDto == null)
                {
                    return BadRequest("Gecersiz transfer testi verisi.");
                }

                if(ModelState.IsValid == false)
                {
                    return BadRequest(ModelState);
                }

                var createdTransferTest = await _transferTestService.CreateAsync(createTransferTestDto, ct);
                return CreatedAtAction(nameof(GetById), new { id = createdTransferTest.Id }, createdTransferTest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Transfer testi olusturulurken hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatasi meydana geldi.");
            }   
        }
        ///<summary>Transfer testi gecmisini siler</summary>
        ///
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            try
            {
                var existingTransferTest = await _transferTestService.GetByIdAsync(id, ct);
                if(existingTransferTest == null)
                {
                    return NotFound($"{id}'li transfer testi bulunamadi.");
                }
                await _transferTestService.DeleteAsync(id, ct);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{id}'li transfer testi silinirken hata meydana geldi.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatasi meydana geldi.");
            }
        }
        #endregion

        #region Transfer Test Specific Operations
        ///<summary>Basari durumuna gore transfer testi gecmisini getirir.</summary>
        ///
        [HttpGet("bysuccessstatus/{status:bool}")]
        [ProducesResponseType(typeof(IReadOnlyList<TransferTestDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IReadOnlyList<TransferTestDto>>> GetBySuccessStatus(bool status)
        {
            try
            {
                var transferTests = await _transferTestService.GetBySuccessStatusAsync(status);
                if (transferTests == null)
                {
                    return NotFound("Belirtilen basari durumuna sahip transfer testi bulunamadi.");
                }
                return Ok(transferTests);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Basari durumuna gore transfer testleri getirilirken hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatasi meydana geldi.");
            }
        }

        ///<summary>%80 basariya ulasana kadar gecen deney oturum sayisini getirir.</summary>
        ///
        [HttpGet("sessiontosuccesscount/{minCount:int}/{maxCount:int}")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<int>> GetSessionToSuccessCount(int minCount, int maxCount)
        {
            try
            {
                var count =  await _transferTestService.GetSessionToSuccessCountAsync(minCount, maxCount);
                return Ok(count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "%80 basariya ulasana kadar gecen deney oturum sayisi getirilirken hata meydana geldi.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Sunucu hatasi meydana geldi.");
            }
        }


        #endregion



    }
}
