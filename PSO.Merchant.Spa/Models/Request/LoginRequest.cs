using System.ComponentModel.DataAnnotations;

namespace PSO.Merchant.Spa.Models.Request;
public class LoginRequest
{
    [Required(ErrorMessage = "UserId is required")]
    public string clientId { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string clientSecret { get; set; }
}

