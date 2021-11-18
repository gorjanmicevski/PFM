using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM.Database.Entities;

namespace PFM.Database.Configurations{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {

            builder.ToTable("categories");
            builder.HasKey(t=>t.Code);
            builder.Property(x=>x.Code).IsRequired();
            builder.Property(x=>x.ParentCode);
            builder.Property(x=>x.Name).IsRequired();
            builder.HasOne<CategoryEntity>(x=>x.ParentCat).WithMany(x=>x.ChildCat).HasForeignKey(x=>x.ParentCode);
            
        }
    }
}