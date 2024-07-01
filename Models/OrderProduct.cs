using System.Text.Json.Serialization;

namespace Auth.Models;

public class OrderProduct : BaseEntity<Guid>
{
    public Guid OrderId { get; set; }
    [JsonIgnore]

    public Order? Order { get; set; }
    public Guid ProductId { get; set; }
    public Product? Product { get; set; }
    public int Quantity { get; set; }
}