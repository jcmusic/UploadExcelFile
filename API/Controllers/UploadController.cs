using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace UploadExcelFile.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogger<UploadController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "Orders/Excel")]
        public IActionResult Post([FromForm] FileUploadRequest fileUploadRequest)
        {
            try
            {
                var orderList = ExcelDeserializer.Deserialize<OrderImportDto>(fileUploadRequest.File);
                if (orderList.Count == 0)
                {
                    return BadRequest("No orders found in the file");
                }

                return Ok(orderList);
            }
            
            catch (DataException e)
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error while processing the file");
                return StatusCode(500, "Error while processing the file");
            }
        }
    }
}