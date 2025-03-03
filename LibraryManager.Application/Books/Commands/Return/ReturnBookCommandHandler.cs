using LibraryManager.Application.Models.ViewModels;
using LibraryManager.Domain.Repositories;
using MediatR;

namespace LibraryManager.Application.Books.Commands.Return;

public class ReturnBookCommandHandler(IBookRepository bookRepository, ILoanRepository loanRepository) : IRequestHandler<ReturnBookCommand, ResultViewModel>
{
    public async Task<ResultViewModel> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
    {
        var book = await bookRepository.GetBookByIdAsync(request.BookId);
        
        if (book == null)
            return ResultViewModel.Error("Book not found");
        if(!book.CurrentLoanId.HasValue)
            return ResultViewModel.Error("Book is not borrowed");
        var loan = await loanRepository.GetLoanByIdAsync(book.CurrentLoanId.Value);
        if (loan == null)
            return ResultViewModel.Error
                ("book is flagged as borrowed and Loan does not exist. Database inconsistency.");
        book.ReturnBook();
        await bookRepository.UpdateBookAsync(book);
        loan.ReturnBook();
        await loanRepository.UpdateLoanAsync(loan);

        var message = loan.IsLate() ? "Book returned, but loan was late." : "Book returned successfully.";
        return ResultViewModel.Success(message);

    }
}