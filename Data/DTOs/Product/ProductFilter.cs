namespace OrderNumberSequence.DATA.DTOs
{

    public class ProductFilter : BaseFilter 
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
    }
}
