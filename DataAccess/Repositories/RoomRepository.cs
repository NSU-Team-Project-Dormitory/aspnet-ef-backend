using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class RoomRepository : IRoomRepository
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
    
    public async Task<RoomEntity> GetById(Guid id)
    {
        return await _dbContext.Rooms
                   .AsNoTracking()
                   .FirstOrDefaultAsync(f => f.Id == id)
               ?? throw new Exception($"No such room with id {id}");
    }
    
    public async Task<RoomEntity> GetByTitle(string title)
    {
        return await _dbContext.Rooms
                   .AsNoTracking()
                   .FirstOrDefaultAsync(f => f.Title == title)
               ?? throw new Exception($"No such room with title {title}");
    }
    
    public async Task<List<RoomEntity>> GetByFloorId(Guid floorId)
    {
        return await _dbContext.Rooms
            .AsNoTracking()
            .Where(r => r.Id == floorId)
            .ToListAsync();
    }
    
    public async Task Add(RoomEntity room)
    {
        _dbContext.Rooms.Add(room);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(RoomEntity room)
    {
        _dbContext.Rooms.Update(room);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(RoomEntity room)
    {
        _dbContext.Rooms.Remove(room);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteById(Guid id)
    {
        var removeRoom = await _dbContext.Rooms
                           .AsNoTracking()
                           .FirstOrDefaultAsync(r => r.Id == id)
                       ?? throw new Exception($"No such room with id {id} for delete");

        _dbContext.Rooms.Remove(removeRoom);
        await _dbContext.SaveChangesAsync();
    }
}