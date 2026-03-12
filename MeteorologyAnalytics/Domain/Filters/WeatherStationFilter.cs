using MeteorologyAnalytics.Domain.Pagination;

namespace MeteorologyAnalytics.Domain.Filters;

public class WeatherStationFilter : PagedFilter<Weather>
{
    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public int? MinTemperatureC { get; set; }

    public int? MaxTemperatureC { get; set; }

    public string? Summary { get; set; }

    public override IQueryable<Weather> Apply(IQueryable<Weather> query)
    {
        if (StartDate.HasValue)
        {
            query = query.Where(x => x.Date >= StartDate.Value);
        }

        if (EndDate.HasValue)
        {
            query = query.Where(x => x.Date <= EndDate.Value);
        }

        if (MinTemperatureC.HasValue)
        {
            query = query.Where(x => x.TemperatureC >= MinTemperatureC.Value);
        }

        if (MaxTemperatureC.HasValue)
        {
            query = query.Where(x => x.TemperatureC <= MaxTemperatureC.Value);
        }

        if (!string.IsNullOrWhiteSpace(Summary))
        {
            query = query.Where(x => x.Summary.Contains(Summary));
        }

        return query;
    }
}