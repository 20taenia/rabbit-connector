using Charon.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class FactoryProductionDurationMap : EntityTypeConfiguration<FactoryProductionDuration>
    {
        public FactoryProductionDurationMap()
        {
            // Primary Key
            HasKey(t => t.ProductionDurationID);

            // Properties
            Property(t => t.ProductionDurationID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

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
            ToTable("FactoryProductionDuration");
            Property(t => t.ProductionDurationID).HasColumnName("ProductionDurationID");
            Property(t => t.NoOfUnits).HasColumnName("NoOfUnits");
            Property(t => t.DaysToProduction).HasColumnName("DaysToProduction");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasOptional(t => t.Factory)
                .WithMany(t => t.FactoryProductionDurations)
                .Map(a => a.MapKey("FactoryID"));
        }
    }
}
