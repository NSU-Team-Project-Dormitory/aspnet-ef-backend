namespace WebApiApplication.Contracts;

public record CreateRoomRequest(string RoomTitle, int Capacity, string FloorTitle);