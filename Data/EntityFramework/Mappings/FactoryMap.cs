using Charon.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class FactoryMap : EntityTypeConfiguration<Factory>
    {
        public FactoryMap()
        {
            // Primary Key
            HasKey(t => t.FactoryID);

            // Properties
            Property(t => t.FactoryID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Reference)
                .HasMaxLength(1000);

            Property(t => t.Name)
                .HasMaxLength(4000);

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
            ToTable("Factories");
            Property(t => t.FactoryID).HasColumnName("FactoryID");
            Property(t => t.Reference).HasColumnName("Reference");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Deposit).HasColumnName("Deposit");
            Property(t => t.FactoryAddressID).HasColumnName("FactoryAddressID");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasOptional(t => t.FactoryAddress)
                .WithMany(t => t.Factories)
                .HasForeignKey(a => a.FactoryAddressID);
        }
    }
}
