using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class FloorRepository
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

    public async Task Add(Guid id, string title)
    {
        var newFloor = new FloorEntity
        {
            Id = id,
            Title = title
        };
        _dbContext.Floors.Add(newFloor);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Update(Guid id, string title)
    {
        var updateFloor = await _dbContext.Floors
                             .AsNoTracking()
                             .FirstOrDefaultAsync(r => r.Id == id)
                         ?? throw new Exception($"No such floor with title {title} and id {id} for update");
        updateFloor.Title = title;

        _dbContext.Floors.Update(updateFloor);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(Guid id)
    {
        var removeFloor = await _dbContext.Floors
                             .AsNoTracking()
                             .FirstOrDefaultAsync(r => r.Id == id)
                         ?? throw new Exception($"No such floor with id {id} for delete");

        _dbContext.Floors.Remove(removeFloor);
        await _dbContext.SaveChangesAsync();
    }
}