using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider));
        }

        [HttpGet("{fileid}")]
        public ActionResult GetFile(string fileId)
        {
            fileId = "Dag1v1.pdf";

            if (!System.IO.File.Exists(fileId))
            {
                return NotFound();
            }

            if(!_fileExtensionContentTypeProvider.TryGetContentType(fileId, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(fileId);
            return File(bytes, contentType, Path.GetFileName(fileId));
        }
    }
}
