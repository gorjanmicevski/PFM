using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM.Database.Entities;

namespace PFM.Database.Configurations{
    public class TransactionEntityConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {

            builder.ToTable("transactions");
            builder.HasKey(t=>t.Id);
            builder.Property(x=>x.Id).IsRequired();
            builder.Property(x=>x.BeneficiaryName);
            builder.Property(x=>x.CatCode);
            builder.Property(x=>x.Date).IsRequired();
            builder.Property(x=>x.Currency).IsRequired();
            builder.Property(x=>x.Amount).IsRequired();
            builder.Property(x=>x.Direction).IsRequired().HasConversion<string>();
            builder.Property(x=>x.Description);
            builder.Property(x=>x.Mcc);
            builder.Property(x=>x.Kind).IsRequired().HasConversion<string>();
            builder.HasOne<CategoryEntity>(x=>x.Category).WithMany().HasForeignKey(x=>x.CatCode);
        }
    }
}