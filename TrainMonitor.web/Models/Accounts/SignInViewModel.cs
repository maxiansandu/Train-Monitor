using System.ComponentModel.DataAnnotations;

namespace TrainMonitor.web.Models.Accounts;

public class SignInViewModel
{
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public required string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public required string Password { get; set; }
}