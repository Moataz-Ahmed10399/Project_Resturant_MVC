using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Resturant_MVC.Models
{
    public class OrderItem : BaseEntity
    {
        [Range(1, 100)]
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]

        public decimal Subtotal { get; set; }

        // Foreign Keys
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }

        // Navigation Properties
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [ForeignKey("MenuItemId")]
        public MenuItem MenuItem { get; set; }
    }
}
