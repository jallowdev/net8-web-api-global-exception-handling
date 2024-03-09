using global_exception_handling.Controllers.dto;
using global_exception_handling.domains.services;
using Microsoft.AspNetCore.Mvc;

namespace global_exception_handling.Controllers;

[ApiController]
[Route("/Products")]
public class ProductController:Controller
{

    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet(Name = "Products")]
    public IActionResult GetProducts()
    {
       return Ok(_productService.GetProducts());
    }
    
    [HttpPost]
    public IActionResult PostProducts(ProductRequest request)
    {
        return Ok(_productService.CreateProduct(request));
    }
    
    [Route("/{id}")]
    [HttpPost]
    public IActionResult GetById(string id)
    {
        return Ok(_productService.FindById(id));
    }
}