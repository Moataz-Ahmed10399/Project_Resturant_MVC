using System.ComponentModel.DataAnnotations;

namespace Project_Resturant_MVC.ViewModel
{
    public class RoleVm
    {
        [Display(Name ="Role Name")]
        public string RoleName { get; set; }
    }
}
