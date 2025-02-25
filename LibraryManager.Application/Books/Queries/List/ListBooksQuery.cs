using LibraryManager.Application.Models.ViewModels;
using MediatR;

namespace LibraryManager.Application.Books.Queries.List;

public class ListBooksQuery(string search) : IRequest<ResultViewModel<List<BooksViewModel>>>
{
    public string Search { get; set; } = search;
}