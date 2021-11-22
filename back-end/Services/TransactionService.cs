using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using PFM.Commands;
using PFM.Database.Entities;
using PFM.Database.Repositories;
using PFM.Models;
using PFM.Repositories;

namespace PFM.Services{
    class TransactionService : ITransactionService
    {
        private readonly ITransactionsRepository _transactionRepository;
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        public TransactionService (ITransactionsRepository transactionRepository,IMapper mapper,ICategoriesRepository categoriesRepository){
            _mapper=mapper;
                _transactionRepository=transactionRepository;
                _categoriesRepository=categoriesRepository;
        }
        public async Task<TransactionPagedList<Transaction>> GetTransactios(string transactionKind,DateTime? startDate, DateTime? endDate,
        int page = 1, int pageSize = 10,string sortBy = null, SortOrder sortOrder = SortOrder.asc)
        {
            var transactionList=new TransactionPagedList<TransactionEntity>();
            if(string.IsNullOrEmpty(transactionKind))
            transactionList= await _transactionRepository.Get(null,startDate,endDate,page,pageSize,sortBy,sortOrder);
            else{
                var kind=(TransactionKind)Enum.Parse(typeof(TransactionKind),transactionKind,true);
                transactionList= await _transactionRepository.Get(kind,startDate,endDate,page,pageSize,sortBy,sortOrder);
            }
            var transactions=_mapper.Map<TransactionPagedList<Transaction>>(transactionList);
            foreach(Transaction t in transactions.Items){
                var splited=await _transactionRepository.GetSplitsIfExists(t.Id);
                foreach(SplitTransactionEntity s in splited){
                   t.splits.Add(_mapper.Map<SplitTransaction>(s));
                }
            
            }
            return transactions;
        }

        public async Task ImportTransactions(List<TransactionEntity> transactions)
        {
            await _transactionRepository.ImportTransaction(transactions);
        }
        public async Task<Transaction> CategorizeTransaction(string id,TransactionCategorizeCommand command){
            
            var transactionToCategorize=await _transactionRepository.GetTransaction(id);
            if(transactionToCategorize==null)
               throw new ErrorException(new Error("transaction","not found"));
            if(string.IsNullOrEmpty(command.catcode))
                throw new ErrorException(new Error("category code","invalid input"));
            transactionToCategorize.CatCode=command.catcode;
            var category=await _categoriesRepository.GetCategory(command.catcode);
            if(category==null)
                throw new ErrorException(new Error("category","not found"));
            transactionToCategorize.Category=category;
            var updated=  _mapper.Map<Transaction>(await _transactionRepository.UpdateTransaction(transactionToCategorize));
            return updated; 
        }

        public async Task<Transaction> SplitTransaction(string id, SplitTransactionCommand command)
        {
            var addedSplits=new List<SplitTransactionEntity>();
            var transactionToSplit=await _transactionRepository.GetTransaction(id);
            if(transactionToSplit==null)
                throw new ErrorException(new Error("transaction","not found"));
            var totalAmount=transactionToSplit.Amount;
            Double checkAmount=0;
            foreach(SingleCategorySplit split in command.splits){
                checkAmount+=split.amount;
            }
            if(checkAmount!=totalAmount)
                throw new ErrorException(new Error("transactions splits","not defined correctly"));
            var check=await _transactionRepository.DeleteIfExists(id);
            transactionToSplit.splits=new List<SplitTransactionEntity>();
            foreach(SingleCategorySplit split in command.splits){
                var catCode=split.catcode;
                if(string.IsNullOrEmpty(catCode))
                    throw new ErrorException(new Error("category code","invalid input"));
                var category=await _categoriesRepository.GetCategory(catCode);
                if(category==null){
                    throw new ErrorException(new Error("category","not found"));
                }
                var amount=split.amount;
                var splitEntity=new SplitTransactionEntity(){
                CatCode=catCode,
                Transaction=transactionToSplit,
                Amount=amount,
                TransactionId=id,
                Category=category};
                transactionToSplit.splits.Add(splitEntity);
                if(totalAmount-amount>0){
                    await _transactionRepository.ImportSplitTransactionEntity(splitEntity);
                }
                else{
                    splitEntity.Amount=totalAmount;
                    await _transactionRepository.ImportSplitTransactionEntity(splitEntity);
                    break;
                }
                totalAmount=totalAmount-amount;
            }
            var p=await _transactionRepository.UpdateTransaction(transactionToSplit);
            return _mapper.Map<Transaction>(transactionToSplit);
        }

        public async Task<SpendingByCategory> GetAnalytics(string catCode, DateTime? startDate, DateTime? endDate, Direction? direction)
        {
            var spendingByCategory=new SpendingByCategory{
                Groups=new List<SpendingInCategory>()
            };
            var transactions= _mapper.Map<List<Transaction>>(await _transactionRepository.GetAnalytics(catCode,startDate,endDate,direction)) ;
            var categories= new List<string>();
            foreach(Transaction transaction in transactions){
                if(transaction.CatCode!=null)
                if(!categories.Contains(transaction.CatCode))
                categories.Add(transaction.CatCode);
            }
            foreach(String cat in categories){
                var count=0;
                var amount=0.0;
                foreach(Transaction t in transactions){
                    if(t.CatCode==cat){
                        count++;
                        amount+=t.Amount.Value;
                    }
                }
                spendingByCategory.Groups.Add(new SpendingInCategory{
                    Amount=amount,
                    CatCode=cat,
                    Count=count
                });
            }
            return spendingByCategory;
        }
    }
}