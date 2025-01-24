using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Woodbrass.Models;

public class User : IdentityUser
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Username { get; set; }

    [Required]
    [StringLength(100)]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public UserRole Role { get; set; } = UserRole.User;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Cart> Carts { get; set; }
}