using Auth.Models;
using Microsoft.AspNetCore.Mvc;
using NumberSequence.service;
using OrderNumberSequence.DATA.DTOs;

namespace Auth.Controllers;
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderServices _orderServices;

    public OrdersController(IOrderServices orderServices)
    {
        _orderServices = orderServices;
    }

    [HttpGet]
    public async Task<ActionResult> GetAll([FromQuery] OrderFilter filter)
    {
        var result = await _orderServices.GetAll(filter);
        var response = new
        {
            Orders = result.orders,
            TotalCount = result.totalCount,
            Error = result.error
        };
        return Ok(response);
    }
    [HttpPost]
    public async Task<ActionResult<Order>> Create([FromBody] OrderForm orderForm) => Ok(await _orderServices.Create(orderForm));
    [HttpPost("bulk")]
    public async Task<ActionResult<List<Order>>> CreateBulk([FromBody] OrderForm orderForm) => Ok(await _orderServices.CreateBulk(orderForm));
}