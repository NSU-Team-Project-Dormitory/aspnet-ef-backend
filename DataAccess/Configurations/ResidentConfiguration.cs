using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class ResidentConfiguration : IEntityTypeConfiguration<ResidentEntity>
{
    public void Configure(EntityTypeBuilder<ResidentEntity> builder)
    {
        builder.HasKey(k => k.Id);
        builder
            .HasOne(r => r.Room)
            .WithMany(room => room.Residents);
    }
}