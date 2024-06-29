using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class FloorRepository : IFloorRepository
{
    private readonly DormitoryDbContext _dbContext;

    public FloorRepository(DormitoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<FloorEntity>> Get()
    {
        return await _dbContext.Floors
            .AsNoTracking()
            .OrderBy(r => r.Title)
            .ToListAsync();
    }

    public async Task<FloorEntity> GetById(Guid id)
    {
        return await _dbContext.Floors
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id)
            ?? throw new Exception($"No such floor with id {id}");
    }

    public async Task Add(FloorEntity floor)
    {
        _dbContext.Floors.Add(floor);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Update(FloorEntity floor)
    {
        _dbContext.Floors.Update(floor);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(FloorEntity floor)
    {
        _dbContext.Floors.Remove(floor);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteById(Guid id)
    {
        FloorEntity floor = await _dbContext.Floors
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(r => r.Id == id)
                                        ?? throw new Exception("No such resident to delete");

        _dbContext.Floors.Remove(floor);
        await _dbContext.SaveChangesAsync();
    }
}