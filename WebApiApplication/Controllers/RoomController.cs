using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Contracts;

namespace WebApiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomController : ControllerBase
{
    private readonly DormitoryDbContext _dbContext;
    private readonly ILogger<ResidentsController> _logger;
    private readonly IResidentRepository _residentRepository;
    private readonly IFloorRepository _floorRepository;
    private readonly IRoomRepository _roomRepository;
    
    public RoomController(DormitoryDbContext dbContext, 
        ILogger<ResidentsController> logger,
        IResidentRepository residentRepository,
        IRoomRepository roomRepository,
        IFloorRepository floorRepository)
    {
        _dbContext = dbContext;
        _logger = logger;
        _residentRepository = residentRepository;
        _roomRepository = roomRepository;
        _floorRepository = floorRepository;
    }
    
    [HttpPost("CreateRoom")]
    public async Task<IActionResult> CreateRoom([FromBody] CreateRoomRequest request)
    {
        var currFloor = await _floorRepository.GetByTitle(request.FloorTitle);
        if (currFloor == null)
        {
            return NotFound($"No such floor with title {request.FloorTitle}");
        }
        var newRoom = new RoomEntity
        {
            Title = request.RoomTitle,
            Capacity = request.Capacity,
            FloorId = currFloor.Id
        };
        await _roomRepository.Add(newRoom);
        return Ok($"Room {newRoom.Title} created");
    }
    
    [HttpPost("DeleteRoom")]
    public async Task<IActionResult> DeleteRoom([FromBody] DeleteRoomRequest request)
    {
        var deleteRoom = await _roomRepository.GetByTitle(request.RoomTitle);
        if (deleteRoom == null)
        {
            return NotFound($"No such room with title {request.RoomTitle}");
        }
        await _roomRepository.Delete(deleteRoom);
        return Ok($"Room {deleteRoom.Title} deleted");
    }
}