using FluentValidation;

namespace DAL.Models.Request;

public record CheckoutGetIdentifierRequest(string Identifier);
public class CheckoutGetIdentifierRequestValidator : AbstractValidator<CheckoutGetIdentifierRequest>
{
    public CheckoutGetIdentifierRequestValidator()
    {
        RuleFor(e => e.Identifier).NotEmpty();
    }
}
