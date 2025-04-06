using System.ComponentModel.DataAnnotations;

namespace Copmany.MVC.PL.Dto
{
    public class ForgetPasswordDto
    {
        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress]
        public string Email { get; set; }
        
    }
}
