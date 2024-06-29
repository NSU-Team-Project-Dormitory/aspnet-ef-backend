using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IFloorRepository : IRepository<FloorEntity>
{
    Task<FloorEntity?> GetByTitle(string title);
}