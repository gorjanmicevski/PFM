using System.Collections.Generic;
using AutoMapper;
using PFM.Database.Entities;
using PFM.Models;

namespace PFM.Mappings{
    class AutoMapperProfile : Profile{
        public AutoMapperProfile(){
            CreateMap<TransactionEntity,Transaction>();
            CreateMap<Transaction,TransactionEntity>();
            CreateMap<TransactionPagedList<TransactionEntity>,TransactionPagedList<Transaction>>();
            CreateMap<List<Category>,List<CategoryEntity>>();
            CreateMap<CategoryEntity,Category>();
            CreateMap<Category,CategoryEntity>();
           
            CreateMap<SplitTransactionEntity,SplitTransaction>();

            //CreateMap<List<SplitTransactionEntity>,List<SplitTransaction>>();
        }
    }   
}