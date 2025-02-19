using LibraryManager.API.Entities;

namespace LibraryManager.API.Models;

public class CreateBookInputModel
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public DateTime PublishDate { get; set; }
    
    public Book ToEntity()=>
    new Book(Title, Author, Isbn, PublishDate);
}