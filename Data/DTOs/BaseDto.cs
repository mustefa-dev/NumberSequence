using System.ComponentModel.DataAnnotations;

namespace OrderNumberSequence.DATA.DTOs;

public class BaseDto<TId>
{
    [Key] public TId Id { get; set; }

    public DateTime? CreationDate { get; set; } = DateTime.UtcNow;
}