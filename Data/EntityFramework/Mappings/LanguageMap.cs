using Charon.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class LanguageMap : EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            // Primary Key
            HasKey(t => t.LanguageID);

            // Properties
            Property(t => t.LanguageID)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(2);

            Property(t => t.NameEN)
                .IsRequired()
                .HasMaxLength(96);

            Property(t => t.NameFR)
                .IsRequired()
                .HasMaxLength(96);

            Property(t => t.NameDE)
                .IsRequired()
                .HasMaxLength(96);

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
            ToTable("Languages");
            Property(t => t.LanguageID).HasColumnName("LanguageID");
            Property(t => t.NameEN).HasColumnName("NameEN");
            Property(t => t.NameFR).HasColumnName("NameFR");
            Property(t => t.NameDE).HasColumnName("NameDE");
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
