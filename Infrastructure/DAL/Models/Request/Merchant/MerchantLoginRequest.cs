using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models.Request.Merchant;

public class MerchantLoginRequest
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}

public class MerchantLoginRequestValidator : AbstractValidator<MerchantLoginRequest>
{
    public MerchantLoginRequestValidator()
    {
        RuleFor(e => e.ClientId).NotEmpty();
        RuleFor(e => e.ClientSecret).NotEmpty();
    }
}


public class ChangePasswordRequest
{
    public string CurrentPassword { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
{
    public ChangePasswordRequestValidator()
    {
        RuleFor(e => e.CurrentPassword).NotEmpty();
        RuleFor(e => e.Password).NotEmpty();
        RuleFor(e => e.ConfirmPassword).NotEmpty();
    }
}

