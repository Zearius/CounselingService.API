using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CounselingService.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        //Dynamically figureout file extension type.
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider 
                ?? throw new System.ArgumentNullException(nameof(fileExtensionContentTypeProvider)); 

        }

        [HttpGet("{fileID}")]
        public ActionResult GetFile(string fileID)
        {
            //lookup file
            //demonstration example
            var pathToFile = "Logo.png";

            //validate file existence
            if (!System.IO.File.Exists(pathToFile))
            {
                return NotFound(); 
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType)) 
            {
                //Catch-all file type for binary file data.
                contentType = "application/octet-stream";
            }

            var bytes = System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}
