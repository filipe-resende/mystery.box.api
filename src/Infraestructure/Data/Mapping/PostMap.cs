using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Data.Mapping
{
    public class PostMap : IEntityTypeConfiguration<SteamCard>
    {
        public void Configure(EntityTypeBuilder<SteamCard> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Title);
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("getdate()");
            builder.Property(p => p.Active).HasDefaultValue(true);
        }
    }
}
