using System.ComponentModel.DataAnnotations;

namespace TrainMonitor.web.Models.Accounts;

public class SignUpViewModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [MinLength(8)]
    public required string Password { get; set; }
    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm password")]
    [MinLength(8)]
    public required string ConfirmPassword { get; set; }

}