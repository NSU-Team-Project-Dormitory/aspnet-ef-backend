using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ResidentRepository
{
    private readonly DormitoryDbContext _dbContext;

    public ResidentRepository(DormitoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ResidentEntity>> Get()
    {
        return await _dbContext.Residents
            .AsNoTracking()
            .OrderBy(r => r.LastName)
            .ToListAsync();
    }
    
    public async Task<List<ResidentEntity>> GetWithRooms()
    {
        return await _dbContext.Residents
            .AsNoTracking()
            .OrderBy(r => r.LastName)
            .Include(r => r.Room)
            .ToListAsync();
    }
    
    public async Task<ResidentEntity?> GetById(Guid id)
    {
        return await _dbContext.Residents
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }
    
    public async Task<List<ResidentEntity>> GetByRoomTitle(string roomTitle)
    {
        return await _dbContext.Residents
            .AsNoTracking()
            .Where(r => r.Room.Title == roomTitle)
            .ToListAsync();
    }
    
    public async Task Add(Guid id, 
        string firstName, 
        string lastName, 
        string patronymic,
        string roomTitle)
    {
        RoomEntity room = await _dbContext.Rooms
                              .AsNoTracking()
                              .FirstOrDefaultAsync(r => r.Title == roomTitle)
                          ?? throw new Exception($"No such room with title {roomTitle}");
        
        var resident = new ResidentEntity
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Patronymic = patronymic,
            RoomId = room.Id
        };
        _dbContext.Add(resident);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(
        Guid id, 
        string firstName, 
        string lastName, 
        string patronymic,
        Guid roomId)
    {
        ResidentEntity resident = await _dbContext.Residents
                                       .AsNoTracking()
                                       .FirstOrDefaultAsync(r => r.Id == id)
                                   ?? throw new Exception("No such resident to update");
        resident.FirstName = firstName;
        resident.LastName = lastName;
        resident.Patronymic = patronymic;
        resident.RoomId = roomId;

        _dbContext.Residents.Update(resident);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        ResidentEntity residentEntity = await _dbContext.Residents
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(r => r.Id == id)
                                         ?? throw new Exception("No such resident to delete");

        _dbContext.Residents.Remove(residentEntity);
        await _dbContext.SaveChangesAsync();
    }
}