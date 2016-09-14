using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            // Primary Key
            HasKey(t => t.ProductID);

            // Properties
            Property(t => t.ProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Barcode)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.Name)
                .HasMaxLength(1000);

            Property(t => t.Description)
                .HasMaxLength(4000);

            Property(t => t.OwnerID)
                .IsRequired();

            Property(t => t.ProductAttribute1)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute2)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute3)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute4)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute5)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute6)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute7)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute8)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute9)
                .HasMaxLength(500);

            Property(t => t.ProductAttribute10)
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
            ToTable("Products");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.Barcode).HasColumnName("Barcode");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.OwnerID).HasColumnName("OwnerID");
            Property(t => t.CategoryID).HasColumnName("ProductCategoryID");
            Property(t => t.PhysicalAttributeID).HasColumnName("PhysicalAttributeID");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.ProductAttribute1).HasColumnName("ProductAttribute1");
            Property(t => t.ProductAttribute2).HasColumnName("ProductAttribute2");
            Property(t => t.ProductAttribute3).HasColumnName("ProductAttribute3");
            Property(t => t.ProductAttribute4).HasColumnName("ProductAttribute4");
            Property(t => t.ProductAttribute5).HasColumnName("ProductAttribute5");
            Property(t => t.ProductAttribute6).HasColumnName("ProductAttribute6");
            Property(t => t.ProductAttribute7).HasColumnName("ProductAttribute7");
            Property(t => t.ProductAttribute8).HasColumnName("ProductAttribute8");
            Property(t => t.ProductAttribute9).HasColumnName("ProductAttribute9");
            Property(t => t.ProductAttribute10).HasColumnName("ProductAttribute10");
            Property(t => t.QuantityInBox).HasColumnName("QuantityInBox");
            Property(t => t.QuantityInnerBox).HasColumnName("QuantityInnerBox");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.IsDiscontinued).HasColumnName("IsDiscontinued");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasRequired(t => t.Owner)
                .WithMany(t => t.Products)
                .HasForeignKey(a => a.OwnerID);

            HasOptional(t => t.PhysicalAttribute)
                .WithMany(t => t.ProductsPhysicalAttributes)
                .HasForeignKey(a => a.PhysicalAttributeID);

            HasOptional(t => t.Category)
                .WithMany(t => t.CategoryProducts)
                .HasForeignKey(a => a.CategoryID);
        }
    }
}
