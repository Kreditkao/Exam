using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book
{
    [Key]
    public int BookId { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; }

    [Required]
    [MaxLength(100)]
    public string Author { get; set; }

    [MaxLength(100)]
    public string Publisher { get; set; }

    public int PageCount { get; set; }

    [MaxLength(50)]
    public string Genre { get; set; }

    public int Year { get; set; }

    [MaxLength(13)]
    public string ISBN { get; set; }

    public decimal CostPrice { get; set; }
    public decimal SalePrice { get; set; }
    public int StockQuantity { get; set; }
    public decimal? DiscountPercentage { get; set; }
    public bool IsReserved { get; set; }
    public string? ReservedBy { get; set; } 
    public bool IsDiscounted { get; set; }
    public string? PromotionName { get; set; }   
}

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [Required]
    [MaxLength(100)]
    public string Password { get; set; }

    public bool IsAdmin { get; set; }
}
