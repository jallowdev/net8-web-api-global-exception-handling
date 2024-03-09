using System.ComponentModel.DataAnnotations;

namespace global_exception_handling.Controllers.dto;

public class ProductRequest
{
    
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }

    public string Currency { get; set; } = "CFA";
    
    [Range(0, 999)]
    public double Weight { get; set; }
}