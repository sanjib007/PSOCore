using System.ComponentModel.DataAnnotations;

namespace System.ComponentModel.DataAnnotations;
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class NotEqualAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public NotEqualAttribute(string comparisonProperty)
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var currentValue = value;

        var property = validationContext.ObjectType.GetProperty(_comparisonProperty);
        if (property == null)
        {
            throw new ArgumentException("Property with this name not found");
        }

        var comparisonValue = property.GetValue(validationContext.ObjectInstance);

        if (currentValue != null && currentValue.Equals(comparisonValue))
        {
            return new ValidationResult(ErrorMessage ?? "The values must not be equal");
        }

        return ValidationResult.Success;
    }
}