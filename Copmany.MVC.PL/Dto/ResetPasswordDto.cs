using System.ComponentModel.DataAnnotations;

namespace Copmany.MVC.PL.Dto
{
    public class ResetPasswordDto
    {
        public string NewPassword { get; set; }
        [DataType(DataType.Password)] //**** 
        [Required(ErrorMessage = "Confirm Password is Required !!")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm Password desn`t match Password")]
        public string ConfirmPassword { get; set; }
    }
}
