using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PFM.Commands;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Database.Repositories{
    interface ITransactionsRepository {
        Task<TransactionPagedList<TransactionEntity>> Get(TransactionKind? transactionKind,DateTime? startDate, DateTime? endDate,int page=1,int pageSize=10,string sortBy=null,SortOrder sortOrder=SortOrder.asc);
        Task<TransactionEntity> ImportTransaction(List<TransactionEntity> transaction);
        public Task<List<SplitTransactionEntity>> GetSplitsIfExists(string id);
        Task<TransactionEntity> UpdateTransaction(TransactionEntity transaction);
        Task<TransactionEntity> GetTransaction(string id);
        Task<SplitTransactionEntity> ImportSplitTransactionEntity(SplitTransactionEntity split);
        Task<List<SplitTransactionEntity>> DeleteIfExists(string id);
        Task<List<TransactionEntity>> GetAnalytics(string catCode, DateTime? startDate, DateTime? endDate, Direction? direction);
    }
}