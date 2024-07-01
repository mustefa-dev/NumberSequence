using Auth.Models;
using AutoMapper;
using OrderNumberSequence.DATA.DTOs;
using ScopeSky.Models;

namespace NumberSequence.service;

public interface IProductServices
{
    Task<CustomResponse> Create(ProductForm productForm);
    Task<(List<ProductDto> products, int? totalCount, string? error)> GetAll(ProductFilter filter);
    Task<(Product? product, string? error)> Update(Guid id, ProductUpdate productUpdate);
    Task<(Product? product, string? error)> Delete(Guid id);
}

public class ProductServices : IProductServices
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public ProductServices(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<CustomResponse> Create(ProductForm productForm)
    {
        var product = _mapper.Map<Product>(productForm);
        var result = _context.Products.Add(product);
        await _context.SaveChangesAsync();
        var productDto = _mapper.Map<ProductDto>(product);
        var response = new CustomResponse(200, productDto, "Product created successfully");

        return response;
    }

    public async Task<(List<ProductDto> products, int? totalCount, string? error)> GetAll(ProductFilter filter)
    {
        var products = _context.Products.ToList();
        var productDto = _mapper.Map<List<ProductDto>>(products);
        return (productDto, productDto.Count, null);
    }

    public async Task<(Product? product, string? error)> Update(Guid id, ProductUpdate productUpdate)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == id);
        if (product == null) return (null, "product not found");
        var updatedProduct = _mapper.Map(productUpdate, product);
        return (updatedProduct, null);
    }

    public async Task<(Product? product, string? error)> Delete(Guid id)
    {
        var product = _context.Products.FirstOrDefault(x => x.Id == id);
        if (product == null) return (null, "product not found");
        product.Deleted = true;
        _context.Products.Update(product);
        return (product, null);
    }
}

