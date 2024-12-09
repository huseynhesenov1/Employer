using System.ComponentModel.DataAnnotations;

namespace Employe.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        [Required]
        [Display(Prompt = "FirstName")]
        public string FirstName {  get; set; }
        [Required]
        [Display(Prompt = "LastName")]
        public string LastName { get; set; }
        [Display(Prompt = "Email")]
        [Required]
        public string Email { get; set; }
        [Display(Prompt = "UserName")]
        [Required]
        public string UserName { get; set; }
        [Display(Prompt = "Password")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Prompt = "PasswordConfirmed")]
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}