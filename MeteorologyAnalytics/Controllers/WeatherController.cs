using MeteorologyAnalytics.Application.Interfaces;
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

    [HttpGet]
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
}