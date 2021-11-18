using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PFM.Commands;
using PFM.Models;
using PFM.Services;

namespace PFM.Controllers{
    [ApiController]
    [Route("/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly ITransactionService _transactionService;
        private readonly ICsvImportService _csvImportService;
        public TransactionsController(ILogger<TransactionsController> logger, ITransactionService service,ICsvImportService csvImportService){
            _logger = logger;
            _transactionService=service;
            _csvImportService=csvImportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromQuery] string transactionKind, [FromQuery] string startDate, [FromQuery] string endDate,
         [FromQuery] int? page, [FromQuery] int? pageSize, [FromQuery] string sortBy, [FromQuery] SortOrder? sortOrder){
           page ??= 1;
           pageSize ??= 10;
           sortOrder??=SortOrder.asc;
           var transactions=await _transactionService.GetTransactios(transactionKind,startDate,endDate,page.Value,pageSize.Value,sortBy,sortOrder.Value);
           return Ok(transactions);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactions([FromForm] IFormFile file){
            var rez=await _transactionService.ImportTransactions(_csvImportService.ImportTransactionsCsv(file));
            return Ok();
        }

        [HttpPost("{id}/split")]
        public async Task<IActionResult> SplitTransaction([FromRoute] string id, [FromBody] SplitTransactionCommand command){
            await _transactionService.SplitTransaction(id,command);
            return Ok();
        }

        [HttpPost("{id}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] string id, [FromBody] TransactionCategorizeCommand command){
            var rez=(await _transactionService.CategorizeTransaction(id,command));
            return Ok(rez);
            
        }

        [HttpPost("auto-categorize")]
        public IActionResult CategorizeTransactions(){
            return Ok();
        }
    }
}