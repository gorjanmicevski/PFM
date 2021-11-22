using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM.Database.Entities;

namespace PFM.Database.Configurations{
    class SplitTransactionEntityConfiguration : IEntityTypeConfiguration<SplitTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<SplitTransactionEntity> builder)
        {
            builder.ToTable("splits");
            builder.HasKey(split=>new {split.CatCode,split.TransactionId});
            builder.Property(s=>s.Amount);
            builder.Property(s=>s.TransactionId);
            builder.Property(s=>s.CatCode);
            builder.HasOne<TransactionEntity>(s=>s.Transaction).WithMany(x=>x.splits).HasForeignKey(x=>x.TransactionId);
            builder.HasOne<CategoryEntity>(s=>s.Category).WithMany().HasForeignKey(x=>x.CatCode);
        }
    }
}