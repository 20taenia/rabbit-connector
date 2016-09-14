using Charon.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class StateProvinceMap : EntityTypeConfiguration<StateProvince>
    {
        public StateProvinceMap()
        {
            // Primary Key
            HasKey(t => t.StateProvinceID);

            // Properties
            Property(t => t.StateProvinceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.Abbreviation)
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
            ToTable("StateProvinces");
            Property(t => t.StateProvinceID).HasColumnName("StateProvinceID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.Abbreviation).HasColumnName("Abbreviation");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasRequired(t => t.Country)
                .WithMany(t => t.StateProvinces)
                .Map(a => a.MapKey("CountryID"));
        }
    }
}
