using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class OwnerMap : EntityTypeConfiguration<Owner>
    {
        public OwnerMap()
        {
            // Primary Key
            HasKey(t => t.OwnerID);

            // Properties
            Property(t => t.OwnerID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Name)
                .HasMaxLength(300);

            Property(t => t.Description)
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
            ToTable("Owners");
            Property(t => t.OwnerID).HasColumnName("OwnerID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.IsCharonCompany).HasColumnName("IsCharonCompany");
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
