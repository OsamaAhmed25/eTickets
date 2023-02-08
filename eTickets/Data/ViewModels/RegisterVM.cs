using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace eTickets.Data.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Full Name is required"), Display(Name = "Full Name")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email Address is required"), Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Confirm Password")]
        [Required (ErrorMessage ="Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password Not Match")]
        public string ConfirmPassword { get; set; }


    }
}
