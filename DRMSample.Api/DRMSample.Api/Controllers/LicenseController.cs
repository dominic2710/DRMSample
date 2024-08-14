using Microsoft.AspNetCore.Mvc;

namespace DRMSample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseController : Controller
    {
        [HttpGet("{fileName}")]
        public async Task<IActionResult> Get(string fileName)
        {
            var filePath = Path.Combine(@"D:\TEST", fileName);
            if (!System.IO.File.Exists(filePath))
                return BadRequest("License không tồn tại!");

            var imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(imageBytes, "application/octet-stream", "encryption.key"); // You can adjust the content type based on your image format
        }

        [HttpGet("GetVideo/{fileName}")]
        public async Task<IActionResult> GetVideo(string fileName)
        {
            var filePath = Path.Combine(@"D:\TEST", fileName);
            if (!System.IO.File.Exists(filePath))
                return BadRequest("Không thể truy cập video!");

            var fileType = "application/vnd.apple.mpegurl";
            if (fileName.EndsWith(".ts"))
            {
                fileType = "video/MP2T";
            }

            var file = new StreamReader(filePath);

            var result = new FileStreamResult(file.BaseStream, fileType)
            {
                EnableRangeProcessing = true
            };

            return await Task.FromResult(result);
        }
    }
}
