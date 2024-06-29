using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(k => k.Id);
        builder
            .HasMany(room => room.Residents)
            .WithOne(res => res.Room);
        builder
            .HasOne(room => room.Floor)
            .WithMany(floor => floor.Rooms)
            .HasForeignKey(room => room.FloorId);
    }
}