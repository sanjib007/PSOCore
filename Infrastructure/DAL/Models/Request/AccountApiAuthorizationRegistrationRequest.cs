using FluentValidation;

namespace DAL.Models.Request;

public record AccountApiAuthorizationRegistrationRequest(
    long AccountId,
    string ClientId,
    string ClientSecret
    );
public class AccountApiAuthorizationRegistrationRequestValidator : AbstractValidator<AccountApiAuthorizationRegistrationRequest>
{
    public AccountApiAuthorizationRegistrationRequestValidator()
    {
        RuleFor(e => e.AccountId).NotEmpty();
        RuleFor(e => e.ClientId).NotEmpty();
        RuleFor(e => e.ClientSecret).NotEmpty();
    }
}
