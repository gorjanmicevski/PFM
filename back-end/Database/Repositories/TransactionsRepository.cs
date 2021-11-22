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

        public async Task<TransactionPagedList<TransactionEntity>> Get(TransactionKind? transactionKind,DateTime? startDate, DateTime? endDate,
            int page = 1, int pageSize = 10, string sortBy = null, SortOrder sortOrder = SortOrder.asc)
        {
            var query= _dbContext.Transactions.AsQueryable();
            if(transactionKind!=null)
                query=query.Where(x=>x.Kind==transactionKind);
            if(startDate!=null)
                query=query.Where(x=>x.Date>=startDate);
            if(endDate!=null)
                query=query.Where(x=> x.Date<=endDate);
            var total= await query.CountAsync();
            var totalP=(int)Math.Ceiling(total*1.0/pageSize);
            
            if(!string.IsNullOrEmpty(sortBy)){
                if(sortOrder==SortOrder.desc){
                  //problem 
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
            var items=query.ToList();
            if(pageSize>total)
            pageSize=total;
            return new TransactionPagedList<TransactionEntity>{
                Page=page,
                PageSize=pageSize,
                SortBy=sortBy,
                SortOrder=sortOrder,
                TotalCount=total,
                TotalPages=totalP,
              Items=items
            };
        }
        public async Task<TransactionEntity> GetTransaction(string id){
            return await _dbContext.Transactions.FirstOrDefaultAsync(x=>x.Id==id);
        }
        public async Task<TransactionEntity> ImportTransaction(List<TransactionEntity> transactions)
        {
            //await _dbContext.Transactions.AddAsync(transactionEntity);
            await _dbContext.Transactions.AddRangeAsync(transactions);
            await _dbContext.SaveChangesAsync();
            return transactions[0];

        }
        public async Task<List<SplitTransactionEntity>> GetSplitsIfExists(string id){
            var splits=_dbContext.SplitTransactions.Where(x=>x.TransactionId==id);
            return await splits.ToListAsync();
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

        public async Task<List<TransactionEntity>> GetAnalytics(string catCode, DateTime? startDate, DateTime? endDate, Direction? direction)
        {
            var ret=_dbContext.Transactions.AsQueryable();
            if(catCode!=null)
            ret=ret.Where(x=>x.CatCode==catCode);
            if(startDate!=null)
            ret=ret.Where(x=>x.Date>=startDate);
            if(endDate!=null)
            ret=ret.Where(x=>x.Date<=endDate);
            if(direction!=null)
            ret=ret.Where(x=>x.Direction==direction);

            return await ret.ToListAsync();
        }
    }
}