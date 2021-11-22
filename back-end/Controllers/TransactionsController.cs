using System;
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
        public async Task<IActionResult> GetTransactions([FromQuery(Name ="transaction-kind")] string transactionKind, 
        [FromQuery(Name="start-date")] DateTime? startDate, [FromQuery(Name ="end-date")] DateTime? endDate,
         [FromQuery] int? page, [FromQuery(Name ="page-size")] int? pageSize, [FromQuery(Name ="sort-by")] string sortBy,
          [FromQuery(Name ="sort-order")] SortOrder? sortOrder){
           page ??= 1;
           pageSize ??= 10;
           sortOrder??=SortOrder.asc;
           var transactions=await _transactionService.GetTransactios(transactionKind,startDate,endDate,page.Value,pageSize.Value,sortBy,sortOrder.Value);
           return Ok(transactions);
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportTransactions([FromForm] IFormFile file){
            await _transactionService.ImportTransactions(_csvImportService.ImportTransactionsCsv(file));
            return Ok();
        }

        [HttpPost("{id}/split")]
        public async Task<IActionResult> SplitTransaction([FromRoute] string id, [FromBody] SplitTransactionCommand command){
            try{         
            var rez=await _transactionService.SplitTransaction(id,command);
            return Ok(rez);
            }catch(ErrorException e){
                return BadRequest(e.Error);
            }
        }

        [HttpPost("{id}/categorize")]
        public async Task<IActionResult> CategorizeTransaction([FromRoute] string id, [FromBody] TransactionCategorizeCommand command){
            try{
                var rez=(await _transactionService.CategorizeTransaction(id,command));
                return Ok(rez);
            }catch(ErrorException e){
                return BadRequest(e.Error);
            }   
        } 
    }
}