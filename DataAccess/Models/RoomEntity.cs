namespace DataAccess.Models;

public class RoomEntity
{
    public Guid Id { get; init; }
    
    public string Title { get; set; } = String.Empty;

    public int Capacity { get; set; } = 0;

    public ICollection<ResidentEntity> Residents { get; set; } = new List<ResidentEntity>();
    
    public Guid FloorId { get; set; }
    
    public FloorEntity Floor { get; set; }
}