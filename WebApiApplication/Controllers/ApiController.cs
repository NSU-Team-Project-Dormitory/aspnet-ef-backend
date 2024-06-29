using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Contracts;

namespace WebApiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    private readonly DormitoryDbContext _dbContext;
    
    private readonly ILogger<ApiController> _logger;
    private readonly IResidentRepository _residentRepository;
    private readonly IFloorRepository _floorRepository;
    private readonly IRoomRepository _roomRepository;

    public ApiController(DormitoryDbContext dbContext, 
                        ILogger<ApiController> logger,
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
    
    [HttpPost]
    public async Task<IActionResult> CreateResident([FromBody] CreateResidentRequest request, CancellationToken ct)
    {
        var newResident = new ResidentEntity
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Patronymic = request.Patronymic
        };
        if (!string.IsNullOrEmpty(request.RoomNumber))
        {
            try
            {
                var room = await _roomRepository.GetByTitle(request.RoomNumber);
                newResident.RoomId = room.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        await _residentRepository.Add(newResident);
        return Ok();
    }
    
    [HttpGet("GetResidentsByRoom")]
    public async Task<IActionResult> Get([FromQuery] GetResidentsRequest request, CancellationToken ct)
    {
        if (!string.IsNullOrEmpty(request.RoomTitle))
        {
            try
            {
                var room = await _roomRepository.GetByTitle(request.RoomTitle);
                var residents = room.Residents;
                List<ResidentsDto> residentsDtos = new List<ResidentsDto>();

                if (residents != null){
                    foreach (var resident in residents)  
                    {
                        residentsDtos.Add(new ResidentsDto(
                            resident.FirstName,
                            resident.LastName,
                            resident.Patronymic,
                            resident.Room != null ? resident.Room.Title : null));
                    }
                }

                return Ok(new GetResidentsResponse(residentsDtos));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        return NotFound();
    }
}