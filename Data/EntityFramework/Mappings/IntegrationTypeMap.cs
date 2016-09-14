using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class IntegrationTypeMap : EntityTypeConfiguration<IntegrationType>
    {
        public IntegrationTypeMap()
        {
            // Primary Key
            HasKey(t => t.IntegrationTypeID);

            // Properties
            Property(t => t.IntegrationTypeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Name)
                .IsRequired()
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
            ToTable("IntegrationTypes");
            Property(t => t.IntegrationTypeID).HasColumnName("IntegrationTypeID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);
        }
    }
}
