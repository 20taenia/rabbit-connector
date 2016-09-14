using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class VariationThemeMap : EntityTypeConfiguration<VariationTheme>
    {
        public VariationThemeMap()
        {
            // Primary Key
            HasKey(t => t.VariationThemeID);

            // Properties
            Property(t => t.VariationThemeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Reference)
                .HasMaxLength(100);

            Property(t => t.Title)
                .HasMaxLength(500);

            Property(t => t.ProductType)
                .HasMaxLength(500);

            Property(t => t.VariationThemeTypeName)
                .HasMaxLength(500);

            Property(t => t.VariationTheme1Name)
                .HasMaxLength(500);

            Property(t => t.VariationTheme2Name)
                .HasMaxLength(500);

            Property(t => t.VariationTheme3Name)
                .HasMaxLength(500);

            Property(t => t.VariationTheme4Name)
                .HasMaxLength(500);

            Property(t => t.VariationTheme5Name)
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
            ToTable("VariationThemes");
            Property(t => t.VariationThemeID).HasColumnName("VariationThemeID");
            Property(t => t.Reference).HasColumnName("Reference");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.ProductType).HasColumnName("ProductType");
            Property(t => t.VariationThemeTypeName).HasColumnName("VariationThemeTypeName");
            Property(t => t.VariationTheme1Name).HasColumnName("VariationTheme1Name");
            Property(t => t.VariationTheme2Name).HasColumnName("VariationTheme2Name");
            Property(t => t.VariationTheme3Name).HasColumnName("VariationTheme3Name");
            Property(t => t.VariationTheme4Name).HasColumnName("VariationTheme4Name");
            Property(t => t.VariationTheme5Name).HasColumnName("VariationTheme5Name");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasOptional(t => t.VariationThemeMedia)
                .WithMany(t => t.VariationThemesImageMedia)
                .Map(a => a.MapKey("ImageID"));
        }
    }
}
