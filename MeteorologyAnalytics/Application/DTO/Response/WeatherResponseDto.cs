namespace MeteorologyAnalytics.Application.DTO.Response;

public class WeatherResponseDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public string Summary { get; set; }

    public int TemperatureF { get; set; }
}