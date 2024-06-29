using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApiApplication.Contracts;

namespace WebApiApplication.Controllers;

[ApiController]
[Route("[controller]")]
public class FloorController : ControllerBase
{
    private readonly DormitoryDbContext _dbContext;
    private readonly ILogger<ResidentsController> _logger;
    private readonly IResidentRepository _residentRepository;
    private readonly IFloorRepository _floorRepository;
    private readonly IRoomRepository _roomRepository;

    public FloorController(DormitoryDbContext dbContext,
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
    
    [HttpPost("CreateFloor")]
    public async Task<IActionResult> CreateFloor([FromBody] CreateFloorRequest request)
    {
        var newFloor = new FloorEntity
        {
            Title = request.FloorTitle,
        };
        await _floorRepository.Add(newFloor);
        return Ok($"Floor {newFloor.Title} created");
    }
    
    [HttpPost("DeleteFloor")]
    public async Task<IActionResult> DeleteFloor([FromBody] DeleteFloorRequest request)
    {
        var deleteFloor = await _floorRepository.GetByTitle(request.FloorTitle);
        if (deleteFloor == null)
        {
            return NotFound($"No such floor with title {request.FloorTitle}");
        }
        await _floorRepository.Delete(deleteFloor);
        return Ok($"Floor {deleteFloor.Title} deleted");
    }
}