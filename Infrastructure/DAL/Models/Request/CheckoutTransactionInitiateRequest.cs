using FluentValidation;

namespace DAL.Models.Request;

public record CheckoutTransactionInitiateRequest(string Identifier, long ChannelTypeId);
public class CheckoutTransactionInitiateRequestValidator : AbstractValidator<CheckoutTransactionInitiateRequest>
{
    public CheckoutTransactionInitiateRequestValidator()
    {
        RuleFor(e => e.Identifier).NotEmpty();
        RuleFor(e => e.ChannelTypeId).NotEmpty();
    }
}
