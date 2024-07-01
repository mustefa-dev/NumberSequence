namespace OrderNumberSequence.DATA.DTOs
{

    public class ProductDto : BaseDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
