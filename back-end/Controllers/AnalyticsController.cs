using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PFM.Models;
using PFM.Services;

namespace PFM.Controllers{
    [ApiController]
    [Route("/spending-analytics")]
    public class AnalyticsController : ControllerBase {
         private readonly ILogger<AnalyticsController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly ICategoriesService _categoryService;
        public AnalyticsController(ILogger<AnalyticsController> logger,ITransactionService transactionService,ICategoriesService categoriesService){
            _logger=logger;
            _transactionService=transactionService;
            _categoryService=categoriesService;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string catCode,[FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,[FromQuery] Direction? direction){
            var r=await _transactionService.GetAnalytics(catCode,startDate,endDate,direction);
            return Ok(r);
        }
    }
}