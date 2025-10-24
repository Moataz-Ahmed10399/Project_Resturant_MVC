using System.ComponentModel.DataAnnotations;

namespace Project_Resturant_MVC.Models
{
    public class Category : BaseEntity
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public ICollection<MenuItem> MenuItems { get; set; }
    }
}
