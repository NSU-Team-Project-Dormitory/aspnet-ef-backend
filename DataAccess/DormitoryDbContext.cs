using DataAccess.Configurations;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess;

public class DormitoryDbContext : DbContext
{
    public DormitoryDbContext(DbContextOptions<DormitoryDbContext> options) : base(options) { }
    
    public DbSet<ResidentEntity> Residents { get; set; }
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<FloorEntity> Floors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ResidentConfiguration());
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
        modelBuilder.ApplyConfiguration(new FloorConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}