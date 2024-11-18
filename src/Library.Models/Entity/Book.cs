namespace Library.Models.Entity;

public class Book
{
    public int Id { get; set; }
    public string Isbn { get; set; }
    public string Title { get; set; }
    public int State { get; set; }
    public string Author { get; set; }
}
