using System.ComponentModel.DataAnnotations;

namespace ebay.ViewModel;

public class AuthSignInVm
{
    [Required]
    [EmailAddress]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }

}
