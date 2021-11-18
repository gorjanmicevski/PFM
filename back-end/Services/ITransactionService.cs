using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PFM.Commands;
using PFM.Models;

namespace PFM.Services{
    public interface ITransactionService{
        Task<TransactionPagedList<Models.Transaction>> GetTransactios(string transactionKind,string startDate, string endDate,int page=1,int pageSize=10,string SortBy=null,SortOrder sortOrder=SortOrder.asc);
        Task<List<Models.Transaction>> ImportTransactions(List<Transaction> transactions);
        Task<Transaction> CategorizeTransaction(string id,TransactionCategorizeCommand command);

        Task<Transaction> SplitTransaction(string id,SplitTransactionCommand command);
    }
}