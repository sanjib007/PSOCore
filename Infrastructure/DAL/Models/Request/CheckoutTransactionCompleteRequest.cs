using FluentValidation;

namespace DAL.Models.Request;

public record CheckoutTransactionCompleteRequest(string TransactionId,string Status, string ChannelReferenceId);
public class CheckoutTransactionCompleteRequestValidator : AbstractValidator<CheckoutTransactionCompleteRequest>
{
    public CheckoutTransactionCompleteRequestValidator()
    {
        RuleFor(e => e.TransactionId).NotEmpty();
        RuleFor(e => e.ChannelReferenceId).NotEmpty();
        RuleFor(e => e.Status).NotEmpty();
    }
}