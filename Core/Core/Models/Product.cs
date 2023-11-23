﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models;

public class Product
{
    public long Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [Range(0.01, double.MaxValue)]
    [Column(TypeName = "decimal(8, 2)")]
    public decimal Price { get; set; }

    [Required]
    [Range(1, long.MaxValue)]
    public long CategoryId { get; set; }
    //[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Category? Category { get; set; }
}
