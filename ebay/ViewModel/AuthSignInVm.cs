using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ebay.ViewModel;

public class AuthSignInVm
{
    [Required]
    [EmailAddress]
    [DisplayName("Email")]
    public string? Username { get; set; }
    public string? ReturnUrl { get; set; }
    [Required]
    public string? Password { get; set; }
    public string? ErrorMessage;

}
