using global_exception_handling.Controllers.dto;
using global_exception_handling.domains.entities;
using global_exception_handling.exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace global_exception_handling.domains.services;

public class ProductService:IProductService
{
    private static List<Product> _products = new List<Product>();
    private readonly ILogger<ProductService> _logger;

    public ProductService(ILogger<ProductService> logger)
    {
        _logger = logger;
    }

    public List<Product> GetProducts()
    {
        _logger.LogInformation($"#### LIST PRODUCT TOTAL : {_products.ToList().Count}");
        return _products.ToList();
    }
    
    public Product CreateProduct(ProductRequest request)
    {
        var product = new Product()
        {
           Name = request.Name,
           Price = request.Price,
           Currency = request.Currency,
           Weight = request.Weight

        };
        _products.Add(product);
        _logger.LogInformation($"#### Created : {_products.ToList().Count}");
        return FindById(product.Id.ToString());
    }

    public Product FindById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new BadHttpRequestException("The Id is Null or White Space");
        var Id = new Guid(id);
       var product= _products.FirstOrDefault(p => p.Id.Equals(Id) );
       if(product is null)
           throw new NotFoundException("The Product is not found.");
       return product;
    }

}