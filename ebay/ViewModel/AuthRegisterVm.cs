using System.ComponentModel.DataAnnotations;

namespace ebay.ViewModel;

public class AuthRegisterVm
{
    [Required]
    public string? FirstName { get; set; }
    [Required]
    public string? LastName { get; set; }
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}
