using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AspNetMvcLogin.Models;

[Table("tbl_product_stock")]
public sealed class ProductStocks
{
    [Key]
    [Column("id")]
    public int Id { get; set; }


    [Required]
    [Column("product_id")]
    public int ProductId { get; set; } 

    [Required]
    [MaxLength(100)]
    [Column("stock")]
    public int stock { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("created_at")]
    public string CreatedAt { get; set; }

    [JsonIgnore]
    public Product Product { get; set; } = null!;
}
