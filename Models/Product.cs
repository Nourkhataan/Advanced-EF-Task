using System.ComponentModel.DataAnnotations;

namespace EF_Task.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public bool IsDeleted { get; set; } = false; // Soft delete flag


        [Timestamp]
        public byte[] RowVersion { get; set; } = null!; //For concurrency control
    }
}
