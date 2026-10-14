namespace EF_Task.DTO
{
    public class ProductListDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string CategoryName { get; set; }

        public byte[] RowVersion { get; set; } = null!;
    }
}
