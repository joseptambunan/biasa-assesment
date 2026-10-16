using System.ComponentModel.DataAnnotations;

namespace AspNetMvcLogin.Models;

public sealed class LoginViewModel
{
    [Required(ErrorMessage = "Username wajib diisi.")]
    [Display(Name = "Username")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password wajib diisi.")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Ingat saya")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}

public sealed class ApiLoginRequest
{
    [Required]
    public string Username { get; init; } = string.Empty;

    [Required]
    public string Password { get; init; } = string.Empty;

    public bool RememberMe { get; init; }
}

public sealed record CurrentUserResponse(string Id, string Username, string DisplayName, string Role);

public sealed class ApiKategoriRequest {
    [Required]
    public string Name { get; set; } = string.Empty;
}

public sealed class ApiProductRequest {
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int kategori_id { get; set; } 

    [Required]
    public int price { get; set; }

    public int product_id { get; set; } 
}
