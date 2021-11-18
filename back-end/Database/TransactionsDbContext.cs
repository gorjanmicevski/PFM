using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PFM.Database.Entities;

namespace PFM.Database{
    class TransactionsDbContext : DbContext{
        public DbSet<TransactionEntity> Transactions {get;set;}
        public DbSet<CategoryEntity> Categories {get;set;}
        public DbSet<SplitTransactionEntity> SplitTransactions {get;set;}
        
        public TransactionsDbContext(){
            
        }
        public TransactionsDbContext(DbContextOptions options):base(options){
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}