using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Charon.Data.EntityFramework.Mapping;
using Charon.Core.Entities;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace Charon.Data.EntityFramework
{
    public partial class CharonContext : DbContext
    {
        static CharonContext()
        {
            Database.SetInitializer<CharonContext>(null);
        }

        public CharonContext()
            : base("Name=CharonContext")
        {
#if DEBUG
            // Log eveything to output window for debugging purpose
            Database.Log = sql => Debug.Write(sql);
#endif
            Database.CommandTimeout = 180;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Factory> Factories { get; set; }
        public DbSet<FactoryProductionDuration> FactoryProductionDurations { get; set; }
        public DbSet<FactoryProduct> FactoryProducts { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MarketplaceProduct> MarketplaceProducts { get; set; }
        public DbSet<Marketplace> Marketplaces { get; set; }
        public DbSet<IntegrationType> MarketplaceIntegrationTypes { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCondition> ProductConditions { get; set; }
        public DbSet<ProductPhysicalAttribute> ProductPhysicalAttributes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StateProvince> StateProvinces { get; set; }
        public DbSet<VariationTheme> VariationThemes { get; set; }
        public DbSet<WarehouseProduct> WarehouseProducts { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new CurrencyMap());
            modelBuilder.Configurations.Add(new FactoryMap());
            modelBuilder.Configurations.Add(new FactoryProductionDurationMap());
            modelBuilder.Configurations.Add(new FactoryProductMap());
            modelBuilder.Configurations.Add(new LanguageMap());
            modelBuilder.Configurations.Add(new MarketplaceProductMap());
            modelBuilder.Configurations.Add(new MarketplaceMap());
            modelBuilder.Configurations.Add(new IntegrationTypeMap());
            modelBuilder.Configurations.Add(new MediaMap());
            modelBuilder.Configurations.Add(new OwnerMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Configurations.Add(new ProductConditionMap());
            modelBuilder.Configurations.Add(new ProductPhysicalAttributeMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new StateProvinceMap());
            modelBuilder.Configurations.Add(new VariationThemeMap());
            modelBuilder.Configurations.Add(new WarehouseProductMap());
            modelBuilder.Configurations.Add(new WarehouseMap());
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var dbValidationException = new FormattedDbEntityValidationException(ex);
                throw dbValidationException;
            }
            catch (DbUpdateException ex)
            {
                var dbUpdateException = new BaseDbException(ex);
                throw dbUpdateException;
            }
        }
    }
}
