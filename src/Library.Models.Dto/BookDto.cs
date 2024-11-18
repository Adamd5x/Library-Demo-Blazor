using System.ComponentModel.DataAnnotations;

namespace Library.Models.Dto;

public class BookDto
{
    public int Id { get; set; }

    [Required]
    public string Isbn { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string State { get; set; }

    [Required]
    public string Author { get; set; }
}
