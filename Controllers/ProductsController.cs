using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using NumberSequence.Helper;
using NumberSequence.service;
using OrderNumberSequence.DATA.DTOs;

namespace NumberSequence.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductServices _productService;

    public ProductsController(IProductServices productService)
    {
        _productService = productService;
    }

    [HttpGet("/api/all")]
    public async Task<ActionResult> GetAll([FromQuery] ProductFilter filter)
    {
        var result = await _productService.GetAll(filter);
        var response = new
        {
            Products = result.products,
            TotalCount = result.totalCount,
            Error = result.error
        };
        return Ok(response);
    }
    
    [HttpPost("/api/create")]
    public async Task<ActionResult<ProductDto>> Create([FromBody] ProductForm productForm) => Ok(await _productService.Create(productForm));
    
    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> Update([FromBody] ProductUpdate productUpdate, Guid id) => Ok(await _productService.Update(id, productUpdate));
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> Delete(Guid id) => Ok(await _productService.Delete(id));
}