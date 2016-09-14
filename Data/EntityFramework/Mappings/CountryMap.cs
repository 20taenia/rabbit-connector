using Charon.Core.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            // Primary Key
            HasKey(t => t.CountryID);

            // Properties
            Property(t => t.CountryID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(80);

            Property(t => t.ISO3Code)
                .IsFixedLength()
                .HasMaxLength(3);

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
            ToTable("Countries");
            Property(t => t.CountryID).HasColumnName("CountryID");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.ISO3Code).HasColumnName("ISO3Code");
            Property(t => t.NumericCode).HasColumnName("NumericCode");
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
