using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IRoomRepository : IRepository<RoomEntity>
{
    Task<RoomEntity> GetByTitle(string title);
    Task<List<RoomEntity>> GetByFloorId(Guid floorId);
}