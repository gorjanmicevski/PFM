using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PFM.Commands;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Services{
    public interface ITransactionService{
        Task<TransactionPagedList<Models.Transaction>> GetTransactios(string transactionKind,DateTime? startDate, DateTime? endDate,int page=1,int pageSize=10,string SortBy=null,SortOrder sortOrder=SortOrder.asc);
        Task ImportTransactions(List<TransactionEntity> transactions);
        Task<Transaction> CategorizeTransaction(string id,TransactionCategorizeCommand command);

        Task<Transaction> SplitTransaction(string id,SplitTransactionCommand command);
        Task<SpendingByCategory> GetAnalytics(string catCode, DateTime? startDate, DateTime? endDate, Direction? direction);
    }
}