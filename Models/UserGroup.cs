using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcLogin.Models;

[Table("tbl_user_group")]
public sealed class UserGroup
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("user_id")]
    public int UserId { get; set; } 

    // Kolom database bernama password, tetapi nilainya harus berupa hash.
    [Required]
    [MaxLength(500)]
    [Column("group_id")]
    public int GroupId { get; set; } 

    public User User { get; set; } = null!;

    public Group Group { get; set; } = null!;
}
