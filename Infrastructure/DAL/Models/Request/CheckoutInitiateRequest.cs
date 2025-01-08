using FluentValidation;

namespace DAL.Models.Request;

public record CheckoutInitiateRequest(
    string ReferenceId,
    string OrderId,
    string ClientId,
    string ClientSecret,
    decimal Amount,

    string Currency,
    string SuccessUrl,
    string FailUrl,
    string CancelUrl,

    string BillerName,
    string BillerPhone,
    string BillingAddress
    );

public class CheckoutInitiateRequestValidator : AbstractValidator<CheckoutInitiateRequest>
{
    public CheckoutInitiateRequestValidator()
    {
        RuleFor(e => e.ReferenceId).NotEmpty();
        RuleFor(e => e.OrderId).NotEmpty();
        RuleFor(e => e.ClientId).NotEmpty();
        RuleFor(e => e.ClientSecret).NotEmpty();
        RuleFor(e => e.Amount).NotEmpty();
        RuleFor(e => e.Currency).NotEmpty();

        RuleFor(e => e.SuccessUrl).NotEmpty();
        RuleFor(e => e.FailUrl).NotEmpty();
        RuleFor(e => e.CancelUrl).NotEmpty();
    }
}
