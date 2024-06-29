using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Contracts;

namespace WebApiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ResidentsController : ControllerBase
{
    private readonly DormitoryDbContext _dbContext;
    
    private readonly ILogger<ResidentsController> _logger;
    private readonly IResidentRepository _residentRepository;
    private readonly IFloorRepository _floorRepository;
    private readonly IRoomRepository _roomRepository;

    public ResidentsController(DormitoryDbContext dbContext, 
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
    
    [HttpPost("CreateResident")]
    public async Task<IActionResult> CreateResident([FromBody] CreateResidentRequest request)
    {
        var newResident = new ResidentEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic
        };
        if (!string.IsNullOrEmpty(request.RoomNumber))
        {
            var room = await _roomRepository.GetByTitle(request.RoomNumber);
            newResident.RoomId = room?.Id;
        }

        await _residentRepository.Add(newResident);
        return Ok();
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
        return Ok();
    }

    [HttpPost("AddResidentToRoom")]
    public async Task<IActionResult> AddResidentToRoom([FromBody] AddResidentToRoomRequest request)
    {
        var resident = await _residentRepository.GetById(request.ResidentId);
        if (resident == null)
        {
            return NotFound($"No such resident with id {request.ResidentId}");
        }
        
        var room = await _roomRepository.GetByTitle(request.RoomTitle);
        if (room == null)
        {
            return NotFound($"No such room with title {request.RoomTitle}");
        }

        resident.RoomId = room.Id;
        room.Residents.Add(resident);
        await _residentRepository.Update(resident);
        // await _roomRepository.Update(room);
        return Ok();
    }
    
    [HttpGet("GetResidentsByRoomTitle")]
    public async Task<IActionResult> GetResidentsByRoomTitle([FromQuery] GetResidentsRequest request)
    {
        if (!string.IsNullOrEmpty(request.RoomTitle))
        {
            var room = await _roomRepository.GetByTitle(request.RoomTitle);
            if (room == null)
            {
                return NotFound($"No such room with title {request.RoomTitle}");
            }
            var residents = room.Residents;
            List<ResidentsDto> residentsDtos = new List<ResidentsDto>();
            
            foreach (var resident in residents)  
            {
                residentsDtos.Add(new ResidentsDto(
                    resident.FirstName,
                    resident.LastName,
                    resident.Patronymic,
                    resident.Room?.Title));
            }

            return Ok(new GetResidentsResponse(residentsDtos));
        }
        return NotFound();
    }
}