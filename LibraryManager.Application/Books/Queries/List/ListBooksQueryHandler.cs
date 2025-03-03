using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Books.Queries.List;

public class ListBooksQueryHandler(IBookRepository repository)
: IRequestHandler<ListBooksQuery, ResultViewModel<List<BooksViewModel>>>
{
    public async Task<ResultViewModel<List<BooksViewModel>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        var books = await repository.ListBooksAsync(request.Search);
        var model = books.Select(BooksViewModel.FromEntity).ToList();
        
        return ResultViewModel<List<BooksViewModel>>.Success(model);
    }
}