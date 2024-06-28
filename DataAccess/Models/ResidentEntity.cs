namespace DataAccess.Models;

public class ResidentEntity
{
    public Guid Id { get; init; }

    public string FirstName { get; set; } = String.Empty;
    
    public string LastName { get; set; } = String.Empty;
    
    public string Patronymic { get; set; } = String.Empty;
    
    public DateTime ContractStart { get; set; }
    
    public DateTime ContractEnd { get; set; }
    
    public Guid? RoomId { get; set; }
    
    public RoomEntity? Room { get; set; } 
}