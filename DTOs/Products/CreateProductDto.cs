namespace NadinSoftTask.DTOs.Products
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public DateTime ProduceDate { get; set; }
        public bool IsAvailable { get; set; }
        public string ManufactureEmail { get; set; }
        public string ManufacturePhone { get; set; }
    }

}
