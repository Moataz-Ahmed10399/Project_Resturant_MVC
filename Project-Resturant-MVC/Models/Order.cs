using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Resturant_MVC.Models
{
    public class Order : BaseEntity
    {
        [StringLength(100)]
        public string CustomerName { get; set; }

        [StringLength(15)]
        [Phone]
        public string CustomerPhone { get; set; }

        [StringLength(500)]
        public string? DeliveryAddress { get; set; }

        [Column(TypeName = "decimal(18,2)")]  
        public decimal Subtotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Tax { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public OrderType Type { get; set; }

        public int? EstimatedDeliveryMinutes { get; set; }

        public DateTime? ReadyAt { get; set; }

        public DateTime? DeliveredAt { get; set; }

        // Navigation Property
        public ICollection<OrderItem> OrderItems { get; set; }
        public string? UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser? User { get; set; }
    }
}
