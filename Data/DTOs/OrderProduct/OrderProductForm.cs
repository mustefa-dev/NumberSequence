using System.ComponentModel.DataAnnotations;

namespace OrderNumberSequence.DATA.DTOs.OrderProduct
{

    public class OrderProductForm 
    {
        [Required]public Guid ProductId { get; set; }
        [Required]public int Quantity { get; set; } = 1;
    }
}
