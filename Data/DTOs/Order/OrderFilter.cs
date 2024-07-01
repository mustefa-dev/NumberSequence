namespace OrderNumberSequence.DATA.DTOs
{

    public class OrderFilter : BaseFilter 
    {
        public string? Note { get; set; }
        public int? OrderNumber { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
