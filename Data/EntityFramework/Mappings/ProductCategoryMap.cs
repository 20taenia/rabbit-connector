using Charon.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class ProductCategoryMap : EntityTypeConfiguration<ProductCategory>
    {
        public ProductCategoryMap()
        {
            // Primary Key
            HasKey(t => t.ProductCategoryID);

            // Properties
            Property(t => t.ProductCategoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 

            Property(t => t.Name)
                .HasMaxLength(500);

            Property(t => t.Description)
                .HasMaxLength(4000);

            Property(t => t.ProductAttribute1Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute2Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute3Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute4Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute5Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute6Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute7Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute8Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute9Name)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute10Name)
                .HasMaxLength(500);

            Property(t => t.IsDeleted)
                .IsRequired();

            Property(t => t.CreatedBy)
                .HasMaxLength(500)
                .IsRequired();

            Property(t => t.CreatedDate)
                .IsRequired();

            Property(t => t.ModifiedBy)
                .HasMaxLength(500)
                .IsRequired();

            Property(t => t.ModifiedDate)
                .IsRequired();

            // Table & Column Mappings
            ToTable("ProductCategories");
            Property(t => t.ProductCategoryID).HasColumnName("ProductCategoryID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.ProductAttribute1Name).HasColumnName("ProductAttribute1Name");
            Property(t => t.ProductAttribute2Name).HasColumnName("ProductAttribute2Name");
            Property(t => t.ProductAttribute3Name).HasColumnName("ProductAttribute3Name");
            Property(t => t.ProductAttribute4Name).HasColumnName("ProductAttribute4Name");
            Property(t => t.ProductAttribute5Name).HasColumnName("ProductAttribute5Name");
            Property(t => t.ProductAttribute6Name).HasColumnName("ProductAttribute6Name");
            Property(t => t.ProductAttribute7Name).HasColumnName("ProductAttribute7Name");
            Property(t => t.ProductAttribute8Name).HasColumnName("ProductAttribute8Name");
            Property(t => t.ProductAttribute9Name).HasColumnName("ProductAttribute9Name");
            Property(t => t.ProductAttribute10Name).HasColumnName("ProductAttribute10Name");
            Property(t => t.ParentCategoryID).HasColumnName("ParentCategoryID");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasOptional(t => t.ParentCategory)
                .WithMany()
                .HasForeignKey(a => a.ParentCategoryID);
        }
    }
}
