using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryManager.Application.Books.Queries.List;

public class ListBooksQueryHandler(LibraryManagerDbContext context)
: IRequestHandler<ListBooksQuery, ResultViewModel<List<BooksViewModel>>>
{
    public async Task<ResultViewModel<List<BooksViewModel>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await context.Books
            .Where
                (b => (request.Search == "" || b.Title.Contains(request.Search) || b.Author.Contains(request.Search)) && !b.IsDeleted)
            .ToListAsync(cancellationToken: cancellationToken);
        var model = books.Select(BooksViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<BooksViewModel>>.Success(model);
    }
}