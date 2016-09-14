using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class MarketplaceProductMap : EntityTypeConfiguration<MarketplaceProduct>
    {
        public MarketplaceProductMap()
        {
            // Primary Key
            HasKey(t => t.MarketplaceProductID);

            // Properties
            Property(t => t.MarketplaceProductID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.Reference)
                .HasMaxLength(100);

            Property(t => t.SKU)
                .HasMaxLength(100);

            Property(t => t.Title)
                .HasMaxLength(1000);

            Property(t => t.Description)
                .HasMaxLength(4000);

            Property(t => t.KeyFeature1)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature2)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature3)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature4)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature5)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature6)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature7)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature8)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature9)
                .HasMaxLength(1000);

            Property(t => t.KeyFeature10)
                .HasMaxLength(1000);

            Property(t => t.Keyword1)
                .HasMaxLength(1000);

            Property(t => t.Keyword2)
                .HasMaxLength(1000);

            Property(t => t.Keyword3)
                .HasMaxLength(1000);

            Property(t => t.Keyword4)
                .HasMaxLength(1000);

            Property(t => t.Keyword5)
                .HasMaxLength(1000);

            Property(t => t.Keyword6)
                .HasMaxLength(1000);

            Property(t => t.Keyword7)
                .HasMaxLength(1000);

            Property(t => t.Keyword8)
                .HasMaxLength(1000);

            Property(t => t.Keyword9)
                .HasMaxLength(1000);

            Property(t => t.Keyword10)
                .HasMaxLength(1000);

            Property(t => t.BrowseNode1)
                .HasMaxLength(50);

            Property(t => t.BrowseNode2)
                .HasMaxLength(50);

            Property(t => t.BrowseNode3)
                .HasMaxLength(50);

            Property(t => t.BrowseNode4)
                .HasMaxLength(50);

            Property(t => t.BrowseNode5)
                .HasMaxLength(50);

            Property(t => t.VariationTheme1)
                .HasMaxLength(50);

            Property(t => t.VariationTheme2)
                .HasMaxLength(50);

            Property(t => t.VariationTheme3)
                .HasMaxLength(50);

            Property(t => t.VariationTheme4)
                .HasMaxLength(50);

            Property(t => t.VariationTheme5)
                .HasMaxLength(50);

            Property(t => t.ProductID)
                .IsRequired();

            Property(t => t.MarketplaceID)
                .IsRequired();

            Property(t => t.FulfilmentWarehouseID)
                .IsRequired();

            Property(t => t.DoNotResend)
                .IsRequired();

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
            ToTable("MarketplaceProducts");
            Property(t => t.MarketplaceProductID).HasColumnName("MarketplaceProductID");
            Property(t => t.Reference).HasColumnName("Reference");
            Property(t => t.SKU).HasColumnName("SKU");
            Property(t => t.Title).HasColumnName("Title");
            Property(t => t.Description).HasColumnName("Description");
            Property(t => t.RecommendedRetailPrice).HasColumnName("RecommendedRetailPrice");
            Property(t => t.StrikeOutPrice).HasColumnName("StrikeOutPrice");
            Property(t => t.StrikeOutStartDate).HasColumnName("StrikeOutStartDate");
            Property(t => t.StrikeOutEndDate).HasColumnName("StrikeOutEndDate");
            Property(t => t.ProductID).HasColumnName("ProductID");
            Property(t => t.MarketplaceID).HasColumnName("MarketplaceID");
            Property(t => t.FulfilmentWarehouseID).HasColumnName("FulfilmentWarehouseID");
            Property(t => t.ProductConditionID).HasColumnName("ProductConditionID");
            Property(t => t.VariationThemeID).HasColumnName("VariationThemeID");
            Property(t => t.ImageMedia1ID).HasColumnName("ImageMedia1ID");
            Property(t => t.ImageMedia2ID).HasColumnName("ImageMedia2ID");
            Property(t => t.ImageMedia3ID).HasColumnName("ImageMedia3ID");
            Property(t => t.ImageMedia4ID).HasColumnName("ImageMedia4ID");
            Property(t => t.ImageMedia5ID).HasColumnName("ImageMedia5ID");
            Property(t => t.ImageMedia6ID).HasColumnName("ImageMedia6ID");
            Property(t => t.ImageMedia7ID).HasColumnName("ImageMedia7ID");
            Property(t => t.ImageMedia8ID).HasColumnName("ImageMedia8ID");
            Property(t => t.ImageMedia9ID).HasColumnName("ImageMedia9ID");
            Property(t => t.ImageMedia10ID).HasColumnName("ImageMedia10ID");
            Property(t => t.KeyFeature1).HasColumnName("KeyFeature1");
            Property(t => t.KeyFeature2).HasColumnName("KeyFeature2");
            Property(t => t.KeyFeature3).HasColumnName("KeyFeature3");
            Property(t => t.KeyFeature4).HasColumnName("KeyFeature4");
            Property(t => t.KeyFeature5).HasColumnName("KeyFeature5");
            Property(t => t.KeyFeature6).HasColumnName("KeyFeature6");
            Property(t => t.KeyFeature7).HasColumnName("KeyFeature7");
            Property(t => t.KeyFeature8).HasColumnName("KeyFeature8");
            Property(t => t.KeyFeature9).HasColumnName("KeyFeature9");
            Property(t => t.KeyFeature10).HasColumnName("KeyFeature10");
            Property(t => t.Keyword1).HasColumnName("Keyword1");
            Property(t => t.Keyword2).HasColumnName("Keyword2");
            Property(t => t.Keyword3).HasColumnName("Keyword3");
            Property(t => t.Keyword4).HasColumnName("Keyword4");
            Property(t => t.Keyword5).HasColumnName("Keyword5");
            Property(t => t.Keyword6).HasColumnName("Keyword6");
            Property(t => t.Keyword7).HasColumnName("Keyword7");
            Property(t => t.Keyword8).HasColumnName("Keyword8");
            Property(t => t.Keyword9).HasColumnName("Keyword9");
            Property(t => t.Keyword10).HasColumnName("Keyword10");
            Property(t => t.BrowseNode1).HasColumnName("BrowseNode1");
            Property(t => t.BrowseNode2).HasColumnName("BrowseNode2");
            Property(t => t.BrowseNode3).HasColumnName("BrowseNode3");
            Property(t => t.BrowseNode4).HasColumnName("BrowseNode4");
            Property(t => t.BrowseNode5).HasColumnName("BrowseNode5");
            Property(t => t.VariationTheme1).HasColumnName("VariationTheme1");
            Property(t => t.VariationTheme2).HasColumnName("VariationTheme2");
            Property(t => t.VariationTheme3).HasColumnName("VariationTheme3");
            Property(t => t.VariationTheme4).HasColumnName("VariationTheme4");
            Property(t => t.VariationTheme5).HasColumnName("VariationTheme5");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.DoNotResend).HasColumnName("DoNotResend");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);

            // Relationships
            HasRequired(t => t.Product)
                .WithMany(t => t.MarketplaceProducts)
                .HasForeignKey(a => a.ProductID);
            HasRequired(t => t.FulfilmentWarehouse)
                .WithMany(t => t.FulfilmentWarehouseMarketplaceProducts)
                .HasForeignKey(a => a.FulfilmentWarehouseID);
            HasOptional(t => t.VariationTheme)
                .WithMany(t => t.MarketplaceProductsVariationThemes)
                .HasForeignKey(a => a.VariationThemeID);
            HasRequired(t => t.Marketplace)
                .WithMany(t => t.MarketplaceProducts)
                .HasForeignKey(a => a.MarketplaceID);
            HasOptional(t => t.ImageMedia1)
                .WithMany(t => t.MarketplaceProductsImageMedia1)
                .HasForeignKey(a => a.ImageMedia1ID);
            HasOptional(t => t.ImageMedia2)
                .WithMany(t => t.MarketplaceProductsImageMedia2)
                .HasForeignKey(a => a.ImageMedia2ID);
            HasOptional(t => t.ImageMedia3)
                .WithMany(t => t.MarketplaceProductsImageMedia3)
                .HasForeignKey(a => a.ImageMedia3ID);
            HasOptional(t => t.ImageMedia4)
                .WithMany(t => t.MarketplaceProductsImageMedia4)
                .HasForeignKey(a => a.ImageMedia4ID);
            HasOptional(t => t.ImageMedia5)
                .WithMany(t => t.MarketplaceProductsImageMedia5)
                .HasForeignKey(a => a.ImageMedia5ID);
            HasOptional(t => t.ImageMedia6)
                .WithMany(t => t.MarketplaceProductsImageMedia6)
                .HasForeignKey(a => a.ImageMedia6ID);
            HasOptional(t => t.ImageMedia7)
                .WithMany(t => t.MarketplaceProductsImageMedia7)
                .HasForeignKey(a => a.ImageMedia7ID);
            HasOptional(t => t.ImageMedia8)
                .WithMany(t => t.MarketplaceProductsImageMedia8)
                .HasForeignKey(a => a.ImageMedia8ID);
            HasOptional(t => t.ImageMedia9)
                .WithMany(t => t.MarketplaceProductsImageMedia9)
                .HasForeignKey(a => a.ImageMedia9ID);
            HasOptional(t => t.ImageMedia10)
                .WithMany(t => t.MarketplaceProductsImageMedia10)
                .HasForeignKey(a => a.ImageMedia10ID);
            HasOptional(t => t.ProductCondition)
                .WithMany(t => t.MarketplaceProductsConditions)
                .HasForeignKey(a => a.ProductConditionID);

        }
    }
}
