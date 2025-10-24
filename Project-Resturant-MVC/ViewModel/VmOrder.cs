using Microsoft.AspNetCore.Mvc.Rendering;
using Project_Resturant_MVC.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Resturant_MVC.ViewModel
{
    public class VmOrder
    {

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 100 characters")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "Customer phone is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [StringLength(15, MinimumLength = 7, ErrorMessage = "Phone number must be between 7 and 15 digits")]
        public string CustomerPhone { get; set; } = null!;

        [Required(ErrorMessage = "DeliveryAddress is required")]

        [StringLength(500, ErrorMessage = "Delivery address cannot exceed 500 characters")]
        public string? DeliveryAddress { get; set; }

        [Required(ErrorMessage = "At least one order item is required")]
        [MinLength(1, ErrorMessage = "You must add at least one item to the order")]
        public List<VmOrderItem> Items { get; set; } = new List<VmOrderItem>();


        [Required]
        public OrderType Type { get; set; }


        public SelectList? OrderTypes { get; set; }

        public SelectList? MenuItemsSelect { get; set; }
    }
}
