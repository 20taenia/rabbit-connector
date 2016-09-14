using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class WarehouseProductMap : EntityTypeConfiguration<WarehouseProduct>
    {
        public WarehouseProductMap()
        {
            // Primary Key
            HasKey(t => t.WarehouseProductID);

            // Properties
            Property(t => t.WarehouseProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.ProductID)
                .IsRequired();

            Property(t => t.WarehouseID)
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

            Property(t => t.HarmonisedTariffCode)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WarehouseProducts");
            Property(t => t.WarehouseProductID).HasColumnName("WarehouseProductID");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.WarehouseID).HasColumnName("WarehouseID");
            Property(t => t.HarmonisedTariffCode).HasColumnName("HarmonisedTariffCode");
            Property(t => t.DaysofBuffer).HasColumnName("DaysofBuffer");
            Property(t => t.SeaFreightCost).HasColumnName("SeaFreightCost");
            Property(t => t.AirFreightCost).HasColumnName("AirFreightCost");
            Property(t => t.DutyCost).HasColumnName("DutyCost");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.IsDiscontinued).HasColumnName("IsDiscontinued");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasRequired(t => t.Product)
                .WithMany(t => t.WarehouseProducts)
                .HasForeignKey(a => a.ProductID);
            HasRequired(t => t.Warehouse)
                .WithMany(t => t.WarehouseWarehouseProducts)
                .HasForeignKey(a => a.WarehouseID);
        }
    }
}
