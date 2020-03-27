using System.Threading.Tasks;
using BlobStorage.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlobStorage.Controllers
{
    [ApiController]
    [Route("blob")]
    public class BlobController : ControllerBase
    {
        private readonly ILogger<BlobController> _logger;
        private readonly IBlobService _blobService;

        public BlobController(ILogger<BlobController> logger, IBlobService blobService)
            => (_logger, _blobService) = (logger, blobService);

        /// <summary>
        /// Get list of contents in storage container
        /// </summary>
        [HttpGet("list")]
        public async Task<IActionResult> Get()
            => Ok(await _blobService.GetAllAsync());

        /// <summary>
        /// Get content of the specified file
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
            => Ok(await _blobService.GetAsync(name));

        /// <summary>
        /// Upload the file from disk to storage
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost("uploadfile")]
        public async Task UploadFile(string filePath, string fileName)
            => await _blobService.UploadFileAsync(filePath, fileName);

        /// <summary>
        /// Upload the content with file name specified to storage
        /// </summary>
        /// <param name="content"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpPost("uploadcontent")]
        public async Task UploadContent(string content, string fileName)
            => await _blobService.UploadContentAsync(content, fileName);

        /// <summary>
        /// Delete the file if exists in storage
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task Delete(string fileName)
            => await _blobService.DeleteAsync(fileName);
    }
}
