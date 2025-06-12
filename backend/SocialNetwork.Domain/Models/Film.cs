using System.ComponentModel.DataAnnotations;
using FilmMatch.Domain.Entities;

namespace SocialNetwork.Domain.Models;

public class Film
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; }
    
    [Required]
    public DateTime ReleaseDate { get; set; }
    
    public string? ImageUrl { get; set; }
    
    [Required]
    public string ShortDescription { get; set; }
    
    [Required]
    public string LongDescription { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
} 