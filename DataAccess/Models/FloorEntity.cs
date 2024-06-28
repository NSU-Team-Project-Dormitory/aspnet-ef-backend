namespace DataAccess.Models;

public class FloorEntity
{
    public Guid Id { get; init; }
    
    public string Title { get; set; } = String.Empty;

    public List<RoomEntity>? Rooms { get; set; }
}