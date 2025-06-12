using MythTechTest.Models.DTOs;
using MythTechTest.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MythTechTest.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SportsEventsController : ControllerBase
{
    private readonly ISportsEventRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<SportsEventsController> _logger; 

    public SportsEventsController(
        ISportsEventRepository repository, 
        IMapper mapper,
        ILogger<SportsEventsController> logger) 
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger; 
    }

    [HttpGet("{id?}")]
    public async Task<ActionResult<SportsEventDto>> GetSportsEvent(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            _logger.LogWarning("GetSportsEvent called with null or empty ID");
            return BadRequest("Event ID cannot be null or empty.");
        }

        try 
        {
            _logger.LogInformation("Retrieving sports event with ID: {EventId}", id);
            
            var sportsEvent = await _repository.GetByIdAsync(id);

            if (sportsEvent == null)
            {
                _logger.LogInformation("Sports event not found: {EventId}", id);
                return NotFound($"Sports event with ID '{id}' not found.");
            }

            var dto = _mapper.Map<SportsEventDto>(sportsEvent);
            return Ok(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving sports event: {EventId}", id);
            return StatusCode(500, "An error occurred while retrieving the sports event.");
        }
    }
}
