using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class WarehouseMap : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseMap()
        {
            // Primary Key
            HasKey(t => t.WarehouseID);

            // Properties
            Property(t => t.WarehouseID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Reference)
                .HasMaxLength(100);

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
            ToTable("Warehouses");
            Property(t => t.WarehouseID).HasColumnName("WarehouseID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Reference).HasColumnName("Reference");
            Property(t => t.IsCharonWarehouse).HasColumnName("IsCharonWarehouse");
            Property(t => t.IsDistributionCentre).HasColumnName("IsDistributionCentre");
            Property(t => t.IsAmazonWarehouse).HasColumnName("IsAmazonWarehouse");
            Property(t => t.WarehouseAddressID).HasColumnName("WarehouseAddressID");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasRequired(t => t.WarehouseAddress)
                .WithMany(t => t.Warehouses)
                .HasForeignKey(a => a.WarehouseAddressID);

        }
    }
}
