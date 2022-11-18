namespace Api.Dtos
{
    public class ProductToReturnDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string Brand { get; set; }
    }
}
