using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeteorologyAnalytics.Domain;

[Table("weather")]
public class Weather
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("date")]
    public DateTime Date { get; set; }

    [Column("temperature_c")]
    public int TemperatureC { get; set; }

    [Column("temperature_f")]
    public int TemperatureF { get; set; }

    [Column("summary")]
    public string Summary { get; set; }
}