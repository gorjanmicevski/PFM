using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PFM.Models;
using PFM.Services;

namespace PFM.Controllers{
    [ApiController]
    [Route("/categories")]
    public class CategoriesController : ControllerBase{
         private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoriesService _categoriesService;
        private readonly ICsvImportService _csvImportService;
        public CategoriesController(ILogger<CategoriesController> logger, ICategoriesService service,ICsvImportService csvImportService){
            _logger = logger;
            _categoriesService=service;
            _csvImportService=csvImportService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string parentId){
            var ret=await _categoriesService.Get(parentId);
            return Ok(ret);
        }
        [HttpPost("import")]
        public async Task<IActionResult> ImportCategories([FromForm] IFormFile file){
            
            var posted=await _categoriesService.ImportCategories(_csvImportService.ImportCategoriesCsv(file));
            return Ok(posted);
        }
    }
}