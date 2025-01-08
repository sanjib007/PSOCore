using FluentValidation;

namespace DAL.Models.Request.Merchant;

public class TransactionRequest
{
    public string TransactionId { get; set; }
}
public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
{
    public TransactionRequestValidator()
    {
        RuleFor(e => e.TransactionId).NotEmpty();
    }
}