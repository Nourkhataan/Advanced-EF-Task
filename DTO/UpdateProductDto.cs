using System.ComponentModel.DataAnnotations;

namespace EF_Task.DTO
{
    public class UpdateProductDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; } 

        [Range(typeof(decimal), "1", "999999999")]
        public decimal Price { get; set; }

        [Required]
        public byte[] RowVersion { get; set; } 
    }
}
