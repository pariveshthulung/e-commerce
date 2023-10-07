using System.ComponentModel.DataAnnotations;
using ebay.Constants;

namespace ebay.Entity;

public class User
{
    public int Id { get; set; }      
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    [EmailAddress]
    [Required]
    public string? Email { get; set; }
    [Required]
    public string? PasswordHash { get; set; }
    public string? UserStatus { get; set; } = UserStatusConstants.Active;
    public string? UserType { get; set; } = UserTypeConstants.Customer;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
