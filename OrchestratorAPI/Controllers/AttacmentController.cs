using Microsoft.AspNetCore.Mvc;
using OrchestratorAPI.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Http.Metadata;

namespace OrchestratorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttacmentController : Controller
    {
        TurnDbContext _context;
        IWebHostEnvironment _appEnvironment;
        public AttacmentController(TurnDbContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        [HttpPost("attachment")]
        public async Task<IActionResult> AddFile(/*IFormFile uploadedFile, FromBodyAttribute TenderBody*/ )
        {

            //var ff = uploadedFile;
            //if (uploadedFile != null)
            //{
            //    string path = $@"{uploadedFile.FileName}";
            //    using (var fileStream = new FileStream(_appEnvironment.ContentRootPath + path, FileMode.Create))
            //    {
            //        await uploadedFile.CopyToAsync(fileStream);
            //    }
            //}
            //var fff = TenderBody;
            var f = HttpContext.Request;
            var ff = f.Form["TenderBody"];
            var fff = f.Form.Files;
            var s = f.Form;
            var ss = f.Body;
            var sss = f.BodyReader;
            //if (uploadedFile != null)
            //{
            //    // путь к папке Files
            //    string path = "/Files/" + uploadedFile.FileName;
            //    // сохраняем файл в папку Files в каталоге wwwroot
            //    //using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
            //    //{
            //    //    await uploadedFile.CopyToAsync(fileStream);
            //    //}
            //    //FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
            //    //_context.Files.Add(file);
            //    //_context.SaveChanges();
            //}

            return Ok("Index");
        }
    }
}
