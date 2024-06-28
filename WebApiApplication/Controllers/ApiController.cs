using DataAccess;
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
    private readonly ResidentRepository _residentRepository;
    private readonly FloorRepository _floorRepository;
    private readonly RoomRepository _roomRepository;

    public ApiController(DormitoryDbContext dbContext, 
                        ILogger<ApiController> logger,
                        ResidentRepository residentRepository,
                        RoomRepository roomRepository,
                        FloorRepository floorRepository)
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
        return Ok();
    }
    
    [HttpGet(Name = "GetResidentsByRoomNumber")]
    public async Task<IActionResult> Get([FromQuery] GetResidentsRequest request, CancellationToken ct)
    {
        // var residentsQuery = _dbContext.Residents
        //     .Where(s => string.IsNullOrWhiteSpace(request.RoomId) ||
        //                 s.RoomNumber.ToLower().Contains(request.RoomId.ToLower()));
        // residentsQuery = residentsQuery.OrderBy(n => n.RoomNumber);
        //
        // var residentDtos = await residentsQuery.Select(
        //     s => new ResidentsDto(s.FirstName, s.LastName, s.Patronymic, s.RoomNumber))
        //     .ToListAsync(ct);
        // return Ok(new GetResidentsResponse(residentDtos));
        return Ok();
    }
}