using Microsoft.AspNetCore.Identity;

namespace Project_Resturant_MVC.Models
{
    public class ApplicationUser  : IdentityUser
    {
        public string? Address { get; set; }
        public bool IsBlocked { get; set; } = false;


    }
}
