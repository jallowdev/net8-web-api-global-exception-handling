using global_exception_handling.Controllers.dto;
using global_exception_handling.domains.entities;

namespace global_exception_handling.domains.services;

public interface IProductService
{
    public List<Product> GetProducts();
    public Product CreateProduct(ProductRequest request);
    public Product FindById(string id);
}