using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class ResidentRepository : IResidentRepository
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
            .Where(r => r.Room != null && r.Room.Title == roomTitle)
            .ToListAsync();
    }
    
    public async Task Add(ResidentEntity resident)
    {
        _dbContext.Add(resident);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(ResidentEntity resident)
    {
        _dbContext.Residents.Update(resident);
        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(ResidentEntity resident)
    {
        _dbContext.Residents.Remove(resident);
        await _dbContext.SaveChangesAsync();
    }
    
    
    public async Task DeleteById(Guid id)
    {
        ResidentEntity residentEntity = await _dbContext.Residents
                                             .AsNoTracking()
                                             .FirstOrDefaultAsync(r => r.Id == id)
                                         ?? throw new Exception($"No such resident with if {id} to delete");

        _dbContext.Residents.Remove(residentEntity);
        await _dbContext.SaveChangesAsync();
    }
}