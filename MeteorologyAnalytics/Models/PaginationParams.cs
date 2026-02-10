using System.ComponentModel.DataAnnotations;

namespace MeteorologyAnalytics.Models;

public class PaginationParams
{
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }
    [Range(1, 50, ErrorMessage =  "Page number must be between 1 and 50")]
    public int PageSize { get; set; }
}