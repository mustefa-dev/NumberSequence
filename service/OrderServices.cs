using Auth.Models;
using AutoMapper;
using NumberSequence.Helper;
using OrderNumberSequence.DATA.DTOs;
using ScopeSky.Models;

namespace NumberSequence.service;
public interface IOrderServices
{
    Task<CustomResponse> Create(OrderForm orderForm);
    Task<(List<OrderDto> orders, int? totalCount, string? error)> GetAll(OrderFilter filter);
    
    Task<CustomResponse> CreateBulk(OrderForm orderForm);
}
public class OrderServices : IOrderServices
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;
    private readonly OrderNumberGenerator _orderNumberGenerator;
    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    public OrderServices(IMapper mapper, DataContext context, OrderNumberGenerator orderNumberGenerator)
    {
        _mapper = mapper;
        _context = context;
        _orderNumberGenerator = orderNumberGenerator;
    }

    public async Task<CustomResponse> Create(OrderForm orderForm)
    {
        await _semaphore.WaitAsync();
        try
        {
            var order = _mapper.Map<Order>(orderForm);
            order.OrderNumber = await _orderNumberGenerator.GetNextOrderNumber();

            var product = await _context.Products.FirstOrDefaultAsync(x =>
                x.Id == orderForm.OrderProducts.FirstOrDefault().ProductId);
            if (product == null) return new CustomResponse(404, "Product not found");

            var addedOrder =  _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            if (addedOrder == null) return new CustomResponse(500, "Order couldn't be created");

            var response = new CustomResponse(200, order, "Order created successfully");
            return response;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<CustomResponse> CreateBulk(OrderForm orderForm)
    {
        var tasks = Enumerable.Range(0, 15)
            .Select(_ => Create(orderForm))
            .ToList();
         
        var results = await Task.WhenAll(tasks);
         
        var createdOrders = results
            .Where(r => r.Status == 200)
            .Select(r => r.Data as Order)
            .ToList();
             var response = new CustomResponse(200, createdOrders, "Orders created successfully");
        return response;
    }

    public async Task<(List<OrderDto> orders, int? totalCount, string? error)> GetAll(OrderFilter filter)
    {
        var orders = await _context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.Product).ToListAsync();
        var orderDtos = _mapper.Map<List<OrderDto>>(orders);
        return (orderDtos, orderDtos.Count, null);
    }

    
}