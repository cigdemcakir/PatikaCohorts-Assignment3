using WebApi.DbOperations;

namespace WebApi.BookOperations.CreateBook;

public class CreateBookCommand
{ 
    public CreateBookModel Model { get; set; }
    
    private readonly BookStoreDbContext _dbContext; 
    
    public CreateBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        if (Model == null)
        {
            throw new ArgumentNullException(nameof(Model), "Model cannot be null");
        }
        
        var book = _dbContext.Books.SingleOrDefault(x => x.Title==Model.Title);

        if (book is not null)  throw new InvalidOperationException($"A book with the title '{Model.Title}' already exists.");

        book = new Book
        {
            Title = Model.Title,
            PublishDate = Model.PublishDate,
            PageCount = Model.PageCount,
            GenreId = Model.GenreId
        };
        
        _dbContext.Books.Add(book);
        _dbContext.SaveChanges(); 
    }

}
public class CreateBookModel
{
    public string Title { get; set; }

    public int GenreId { get; set; }
    
    public int PageCount { get; set; }

    public DateTime PublishDate { get; set; }   
}