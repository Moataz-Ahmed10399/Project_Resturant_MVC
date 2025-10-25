using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Resturant_MVC.Models
{
    public class MenuItem : BaseEntity
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(0.01, 100000)]
        [Column(TypeName = "decimal(18,2)")]  // ← ضيف السطر ده
        public decimal Price { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(1, 300)]
        public int PreparationTimeMinutes { get; set; }
        //[StringLength(255)]
        //public string? ImageUrl { get; set; }
        public bool IsAvailable { get; set; } = true;

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
