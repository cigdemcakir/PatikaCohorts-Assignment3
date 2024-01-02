using WebApi.DbOperations;

namespace WebApi.BookOperations.DeleteBook;

public class DeleteBookCommand
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; }
    
    public DeleteBookCommand(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Handle()
    {
        if (BookId <= 0)
        {
            throw new ArgumentException("Invalid Book ID", nameof(BookId));
        }
        
        var book = _dbContext.Books.SingleOrDefault(x => x.Id==BookId);

        if (book is null)  throw new InvalidOperationException($"Book with ID {BookId} not found.");

        _dbContext.Books.Remove(book);
        _dbContext.SaveChanges(); 
    }

}