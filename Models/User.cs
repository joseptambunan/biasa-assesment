using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcLogin.Models;

[Table("tbl_user")]
public sealed class User
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("username")]
    public string Username { get; set; } = string.Empty;

    // Kolom database bernama password, tetapi nilainya harus berupa hash.
    [Required]
    [MaxLength(500)]
    [Column("password")]
    public string PasswordHash { get; set; } = string.Empty;

    public UserGroup UserGroup { get; set; } = null!;
}
