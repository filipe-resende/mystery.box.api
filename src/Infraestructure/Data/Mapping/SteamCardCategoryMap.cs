using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Mapping
{
    public class SteamCardCategoryMap : IEntityTypeConfiguration<SteamCardCategory>
    {
        public void Configure(EntityTypeBuilder<SteamCardCategory> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(p => p.Active).HasDefaultValue(true);
        }
    }
}