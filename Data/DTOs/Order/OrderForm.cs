using OrderNumberSequence.DATA.DTOs.OrderProduct;

namespace OrderNumberSequence.DATA.DTOs
{

    public class OrderForm 
    {
        public string? Note { get; set; }
        public List<OrderProductForm>? OrderProducts { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
