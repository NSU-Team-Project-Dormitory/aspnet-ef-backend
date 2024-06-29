using DataAccess.Models;

namespace DataAccess.Repositories;

public interface IResidentRepository : IRepository<ResidentEntity>
{
    Task<List<ResidentEntity>> GetWithRooms();
    Task<List<ResidentEntity>> GetByRoomTitle(string roomTitle);
}