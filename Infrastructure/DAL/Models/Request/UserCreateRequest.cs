using FluentValidation;

namespace DAL.Models.Request;

public record UserCreateRequest(
    string AccountId,
    string ClientId,
    string ClientSecret

    );
public class UserCreateRequestValidator : AbstractValidator<UserCreateRequest>
{
    public UserCreateRequestValidator()
    {
        RuleFor(e => e.AccountId).NotEmpty();
        RuleFor(e => e.ClientId).NotEmpty();
        RuleFor(e => e.ClientSecret).NotEmpty();
    }
}


