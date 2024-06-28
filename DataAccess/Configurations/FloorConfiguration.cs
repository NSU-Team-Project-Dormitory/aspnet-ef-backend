using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class FloorConfiguration : IEntityTypeConfiguration<FloorEntity>
{
    public void Configure(EntityTypeBuilder<FloorEntity> builder)
    {
        builder.HasKey(k => k.Id);
        builder
            .HasMany(floor => floor.Rooms)
            .WithOne(room => room.Floor)
            .HasForeignKey(room => room.FloorId);
    }
}