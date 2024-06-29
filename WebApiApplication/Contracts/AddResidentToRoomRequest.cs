namespace WebApiApplication.Contracts;

public record AddResidentToRoomRequest(Guid ResidentId, string RoomTitle);