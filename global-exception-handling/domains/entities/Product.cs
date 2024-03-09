
namespace global_exception_handling.domains.entities;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }=String.Empty;
    public string Currency { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public double Weight { get; set; }
}