using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class MarketplaceMap : EntityTypeConfiguration<Marketplace>
    {
        public MarketplaceMap()
        {
            // Primary Key
            HasKey(t => t.MarketplaceID);

            // Properties
            Property(t => t.MarketplaceID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Reference)
                .IsRequired()
                .HasMaxLength(100);

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
            ToTable("Marketplaces");
            Property(t => t.MarketplaceID).HasColumnName("MarketplaceID");
            Property(t => t.Reference).HasColumnName("Reference");
            Property(t => t.Name).HasColumnName("Name");
            Property(t => t.LanguageID).HasColumnName("LanguageID");
            Property(t => t.CountryID).HasColumnName("CountryID");
            Property(t => t.CurrencyID).HasColumnName("CurrencyID");
            Property(t => t.IntegrationTypeID).HasColumnName("IntegrationTypeID");
            Property(t => t.FBAWarehouseID).HasColumnName("FBAWarehouseID");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasRequired(t => t.Language)
                .WithMany(t => t.Marketplaces)
                .HasForeignKey(a => a.LanguageID);

            HasRequired(t => t.Country)
                .WithMany(t => t.Marketplaces)
                .HasForeignKey(a => a.CountryID);

            HasRequired(t => t.Currency)
                .WithMany(t => t.Marketplaces)
                .HasForeignKey(a => a.CurrencyID);

            HasRequired(t => t.IntegrationType)
                .WithMany(t => t.Marketplaces)
                .HasForeignKey(a => a.IntegrationTypeID);

            HasOptional(t => t.FBAWarehouse)
                .WithMany(t => t.FBAWarehouseMarketplaces)
                .HasForeignKey(a => a.FBAWarehouseID);
        }
    }
}
