using Charon.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Charon.Data.EntityFramework.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            HasKey(t => t.AddressID);

            // Properties
            Property(t => t.AddressID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 

            Property(t => t.FirstName)
                .HasMaxLength(4000);

            Property(t => t.LastName)
                .HasMaxLength(4000);

            Property(t => t.Company)
                .HasMaxLength(4000);

            Property(t => t.Address1)
                .HasMaxLength(4000)
                .IsRequired();

            Property(t => t.Address2)
                .HasMaxLength(4000);

            Property(t => t.City)
                .HasMaxLength(4000)
                .IsRequired();

            Property(t => t.ZipPostalCode)
                .HasMaxLength(4000);

            Property(t => t.Email)
                .HasMaxLength(4000);

            Property(t => t.PhoneNumber)
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
            ToTable("Addresses");
            Property(t => t.AddressID).HasColumnName("AddressID");
            Property(t => t.FirstName).HasColumnName("FirstName");
            Property(t => t.LastName).HasColumnName("LastName");
            Property(t => t.Company).HasColumnName("Company");
            Property(t => t.Address1).HasColumnName("Address1");
            Property(t => t.Address2).HasColumnName("Address2");
            Property(t => t.City).HasColumnName("City");
            Property(t => t.StateProvinceID).HasColumnName("StateProvinceID");
            Property(t => t.ZipPostalCode).HasColumnName("ZipPostalCode");
            Property(t => t.CountryID).HasColumnName("CountryID");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.PhoneNumber).HasColumnName("PhoneNumber");
            Property(t => t.IsDeleted).HasColumnName("IsDeleted");
            Property(t => t.CreatedBy).HasColumnName("CreatedBy");
            Property(t => t.CreatedDate).HasColumnName("CreatedDate");
            Property(t => t.ModifiedBy).HasColumnName("ModifiedBy");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            //Ignores
            Ignore(t => t.State);


            // Relationships
            HasOptional(t => t.StateProvince)
                .WithMany(t => t.Addresses)
                .HasForeignKey(a => a.StateProvinceID);

            HasRequired(t => t.Country)
                .WithMany(t => t.Addresses)
                .HasForeignKey(a => a.CountryID);
        }
    }
}
