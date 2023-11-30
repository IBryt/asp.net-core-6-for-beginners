using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Product
{
    public long Id { get; set; }
    //[BindNever]
    [Required(ErrorMessage = "Please enter a Name")]
    public string Name { get; set; }
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a Price")]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, long.MaxValue, ErrorMessage = "Please select a Category")]
    public long CategoryId { get; set; }
    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Category? Category { get; set; }
}
