using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class FactoryProductMap : EntityTypeConfiguration<FactoryProduct>
    {
        public FactoryProductMap()
        {
            // Primary Key
            HasKey(t => t.FactoryProductID);

            // Properties
            Property(t => t.FactoryProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Reference)
                .HasMaxLength(500);

            Property(t => t.CommodityDescription)
                .HasMaxLength(500);

            Property(t => t.ProductID)
                .IsRequired();

            Property(t => t.FactoryID)
                .IsRequired();

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
            ToTable("FactoryProducts");
            Property(t => t.FactoryProductID).HasColumnName("FactoryProductID");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.FactoryID).HasColumnName("FactoryID");
            Property(t => t.Reference).HasColumnName("Reference");
            Property(t => t.ProductionCost).HasColumnName("ProductionCost");
            Property(t => t.MaterialCost).HasColumnName("MaterialCost");
            Property(t => t.LabourCost).HasColumnName("LabourCost");
            Property(t => t.CrossBorderCost).HasColumnName("CrossBorderCost");
            Property(t => t.ManufacturingDuration).HasColumnName("ManufacturingDuration");
            Property(t => t.CommodityDescription).HasColumnName("CommodityDescription");
            Property(t => t.MinimumOrderQuantity).HasColumnName("MinimumOrderQuantity");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.IsDiscontinued).HasColumnName("IsDiscontinued");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasOptional(t => t.ArtworkAIMedia)
                .WithMany(t => t.FactoryProductsArtworkAIMedia)
                .Map(a => a.MapKey("ArtworkAIMediaID"));
            HasOptional(t => t.ArtworkPDFMedia)
                .WithMany(t => t.FactoryProductsArtworkPDFMedia)
                .Map(a => a.MapKey("ArtworkPDFMediaID"));
            HasRequired(t => t.Product)
                .WithMany(t => t.FactoryProducts)
                .HasForeignKey(a => a.ProductID);
            HasRequired(t => t.Factory)
                .WithMany(t => t.FactoryProducts)
                .HasForeignKey(a => a.FactoryID);
        }
    }
}
