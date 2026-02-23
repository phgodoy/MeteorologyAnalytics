using System.ComponentModel.DataAnnotations;

namespace MeteorologyAnalytics.Models;

public class KeysetParams
{
    public int? LastId { get; set; }

    [Range(1,50)]
    public int PageSize { get; set; } = 10;
}