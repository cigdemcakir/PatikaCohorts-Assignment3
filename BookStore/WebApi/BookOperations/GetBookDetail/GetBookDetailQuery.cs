using WebApi.BookOperations.GetBooks;
using WebApi.Common;
using WebApi.DbOperations;

namespace WebApi.BookOperations.GetBookDetail;

public class GetBookDetailQuery
{
    private readonly BookStoreDbContext _dbContext;
    public int BookId { get; set; } 

    public GetBookDetailQuery(BookStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public BookDetailViewModel Handle()
    {
        if (BookId <= 0)
        {
            throw new ArgumentException("Invalid Book ID", nameof(BookId));
        }
        
        var book = _dbContext.Books.Where(x => x.Id==BookId).SingleOrDefault();

        if (book is null) throw new InvalidOperationException($"Book with ID {BookId} not found.");
        
        BookDetailViewModel viewModel = new BookDetailViewModel();
        
        viewModel.Title = book.Title;
        viewModel.PageCount = book.PageCount;
        viewModel.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");
        viewModel.Genre = ((GenreEnum)book.GenreId).ToString();

        return viewModel;
    }
}

public class BookDetailViewModel
{
    public string Title { get; set; }

    public string Genre { get; set; }
    
    public int PageCount { get; set; }

    public string PublishDate { get; set; }   
}