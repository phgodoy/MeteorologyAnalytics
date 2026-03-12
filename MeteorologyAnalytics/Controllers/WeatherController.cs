using MeteorologyAnalytics.Application.Interfaces;
using MeteorologyAnalytics.Domain.Filters;
using MeteorologyAnalytics.Extensions;
using MeteorologyAnalytics.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _service;

    public WeatherController(IWeatherService service)
    {
        _service = service;
    }

    [HttpGet("offset")]
    public async Task<IActionResult> Get([FromQuery]PaginationParams paginationParams)
    {
        var result = await _service.GetAllAsync
            (paginationParams.PageNumber, paginationParams.PageSize);
        
        Response.AddPaginationHeader(
            new PaginationHeader(
                result.CurrentPage,
                result.PageSize,
                result.TotalPages,
                result.TotalCount
            ));
        
        return Ok(result);
    }
    
    [HttpGet("keyset")]
    public async Task<IActionResult> GetKeyset([FromQuery] KeysetParams param)
    {
        var result = await _service.GetByCursorAsync(param.LastId, param.PageSize);

        Response.AddPaginationHeader(
            new CursorPaginationHeader(
                result.NextCursor,
                result.HasMore
            )
        );

        return Ok(result);
    }
    
    [HttpGet("filter")]
    public async Task<IActionResult> GetByFilter(
        [FromQuery] WeatherStationFilter filter)
    {
        var result = await _service.GetByFilterAsync(
            filter.PageNumber,
            filter.PageSize,
            filter
        );

        Response.AddPaginationHeader(
            new PaginationHeader(
                result.CurrentPage,
                result.PageSize,
                result.TotalCount,
                result.TotalPages
            )
        );

        return Ok(result);
    }
}