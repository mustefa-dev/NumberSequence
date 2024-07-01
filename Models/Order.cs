using System.Text.Json.Serialization;

namespace Auth.Models;

public class Order : BaseEntity<Guid>
{
    public string? Note { get; set; }

    public int OrderNumber { get; set; }
        
    [JsonIgnore]
    public List<OrderProduct>? OrderProducts { get; set; }
}