using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
}
