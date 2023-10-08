using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ebay.ViewModel;

public class AuthRegisterVm
{
    [Required]
    [DisplayName("First Name")]
    public string? FirstName { get; set; }
    [Required]
    [DisplayName("Last Name")]
    public string? LastName { get; set; }
    [EmailAddress]
    [Required]
    [DisplayName("Email Address")]
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
}
