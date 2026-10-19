using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetMvcLogin.Models;

[Table("tbl_product")]
public sealed class Product
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("kategori_id")]
    public int Kategori_id { get; set; } 

    [Required]
    [Column("price")]
    public int Price { get; set; } 

    [Required]
    [Column("stock")]
    public int stock { get; set; } 

    [Required]
    [Column("product_code")]
    public string ProductCode { get; set; }  = string.Empty;
}
