using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PFM.Commands;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Database.Repositories{
    class TransactionsRepository : ITransactionsRepository{
        private readonly TransactionsDbContext _dbContext;
        public TransactionsRepository(TransactionsDbContext dbContext){
            _dbContext=dbContext;
        }

        public async Task<List<SplitTransactionEntity>> DeleteIfExists(string id)
        {
            var toRemove=await _dbContext.SplitTransactions.ToListAsync();
            toRemove=toRemove.Where(x=>x.TransactionId==id).ToList();
            if(toRemove!=null && toRemove.Count!=0)
           {
                _dbContext.SplitTransactions.RemoveRange(toRemove.ToArray());
                await _dbContext.SaveChangesAsync();
           }
            return toRemove;
        }

        public async Task<TransactionPagedList<TransactionEntity>> Get(TransactionKind? transactionKind,string startDate, string endDate,
            int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.asc)
        {
            var query= _dbContext.Transactions.AsQueryable();
            if(transactionKind!=null)
                query=query.Where(x=>x.Kind==transactionKind);
            var total= await query.CountAsync();
            var totalP=(int)Math.Ceiling(total*1.0/pageSize);
            
            if(!string.IsNullOrEmpty(sortBy)){
                if(sortOrder==SortOrder.desc){
                    query=query.OrderByDescending(x=>x.Id);
                }else{
                    query=query.OrderBy(x=>x.Id);
                }
            }else{
                if(sortOrder==SortOrder.desc)
                query=query.OrderByDescending(x=>x.Id);
                else
                query=query.OrderBy(x=>x.Id);
            }
            query=query.Skip((page-1)*pageSize).Take(pageSize);
            return new TransactionPagedList<TransactionEntity>{
                Page=page,
            PageSize=pageSize,
            SortBy=sortBy,
            SortOrder=sortOrder,
            TotalCount=total,
            TotalPages=totalP,
              Items=await query.ToListAsync()
            };
        }
        public async Task<TransactionEntity> GetTransaction(string id){
            return await _dbContext.Transactions.FirstOrDefaultAsync(x=>x.Id==id);
        }
        public async Task<TransactionEntity> ImportTransaction(TransactionEntity transactionEntity)
        {
            await _dbContext.Transactions.AddAsync(transactionEntity);
            await _dbContext.SaveChangesAsync();
            return transactionEntity;

        }

        public async Task<SplitTransactionEntity> ImportSplitTransactionEntity(SplitTransactionEntity split)
        {
            
            await _dbContext.SplitTransactions.AddAsync(split);
            await _dbContext.SaveChangesAsync();
            return split;
        }

        public async Task<TransactionEntity> UpdateTransaction(TransactionEntity transaction)
        {
            _dbContext.Transactions.Update(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }
    }
}