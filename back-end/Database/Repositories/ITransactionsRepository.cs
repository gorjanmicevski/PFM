using System.Collections.Generic;
using System.Threading.Tasks;
using PFM.Commands;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Database.Repositories{
    interface ITransactionsRepository {
        Task<TransactionPagedList<TransactionEntity>> Get(TransactionKind? transactionKind,string startDate, string endDate,int page=1,int pageSize=10,string sortBy=null,SortOrder sortOrder=SortOrder.asc);
        Task<TransactionEntity> ImportTransaction(TransactionEntity transactionEntity);

        Task<TransactionEntity> UpdateTransaction(TransactionEntity transaction);
        Task<TransactionEntity> GetTransaction(string id);
        Task<SplitTransactionEntity> ImportSplitTransactionEntity(SplitTransactionEntity split);
        Task<List<SplitTransactionEntity>> DeleteIfExists(string id);
    }
}