using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Charon.Core.Entities;

namespace Charon.Data.EntityFramework.Mapping
{
    public class ProductPhysicalAttributeMap : EntityTypeConfiguration<ProductPhysicalAttribute>
    {
        public ProductPhysicalAttributeMap()
        {
            // Primary Key
            HasKey(t => t.PhysicalAttributeID);

            // Properties
            Property(t => t.PhysicalAttributeID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

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
            ToTable("ProductPhysicalAttributes");
            Property(t => t.PhysicalAttributeID).HasColumnName("PhysicalAttributeID");
            Property(t => t.Length).HasColumnName("Length");
            Property(t => t.Width).HasColumnName("Width");
            Property(t => t.Height).HasColumnName("Height");
            Property(t => t.PackagingLength).HasColumnName("PackagingLength");
            Property(t => t.PackagingWidth).HasColumnName("PackagingWidth");
            Property(t => t.PackagingHeight).HasColumnName("PackagingHeight");
            Property(t => t.Weight).HasColumnName("Weight");
            Property(t => t.PackagedWeight).HasColumnName("PackagedWeight");
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
