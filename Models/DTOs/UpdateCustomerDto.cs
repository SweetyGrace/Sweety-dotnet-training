using System.ComponentModel.DataAnnotations;
namespace MyLearningWebApi.Models.DTOs;
public class UpdateCustomerDto
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(30, ErrorMessage = "Name can't be longer than 30 characters")]
    [MinLength(2, ErrorMessage = "Name must be at least 2 characters long")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string Phone { get; set; } = string.Empty;

}