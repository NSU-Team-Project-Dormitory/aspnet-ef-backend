using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class RoomRepository
{
    private readonly DormitoryDbContext _dbContext;

    public RoomRepository(DormitoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<RoomEntity>> Get()
    {
        return await _dbContext.Rooms
            .AsNoTracking()
            .OrderBy(r => r.Title)
            .ToListAsync();
    }
    
    public async Task<List<RoomEntity>> GetByFloorId(Guid floorId)
    {
        return await _dbContext.Rooms
            .AsNoTracking()
            .Where(r => r.Id == floorId)
            .ToListAsync();
    }
    
    public async Task Add(Guid id, string title, int capacity, Guid floorId)
    {
        var newRoom = new RoomEntity
        {
            Id = id,
            Title = title,
            Capacity = capacity,
            FloorId = floorId
        };
        _dbContext.Rooms.Add(newRoom);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(Guid id, string title, int capacity, Guid floorId)
    {
        var updateRoom = await _dbContext.Rooms
                           .AsNoTracking()
                           .FirstOrDefaultAsync(r => r.Id == id)
                       ?? throw new Exception($"No such room with title {title} and id {id} for update");
        updateRoom.Title = title;
        updateRoom.Capacity = capacity;
        updateRoom.FloorId = floorId;

        _dbContext.Rooms.Update(updateRoom);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(Guid id)
    {
        var removeRoom = await _dbContext.Rooms
                           .AsNoTracking()
                           .FirstOrDefaultAsync(r => r.Id == id)
                       ?? throw new Exception($"No such room with id {id} for delete");

        _dbContext.Rooms.Remove(removeRoom);
        await _dbContext.SaveChangesAsync();
    }
}