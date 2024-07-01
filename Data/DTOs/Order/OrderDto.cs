namespace OrderNumberSequence.DATA.DTOs
{

    public class OrderDto : BaseDto<Guid>
    {
        public string? Note { get; set; }
        public List<OrderProductDto>? OrderProducts { get; set; }

        public decimal TotalPrice { get; set; }
        
    }
}
