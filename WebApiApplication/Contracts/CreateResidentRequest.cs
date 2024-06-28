namespace WebApiApplication.Contracts;

public record CreateResidentRequest(string FirstName, string LastName, string Patronymic, string? RoomNumber);