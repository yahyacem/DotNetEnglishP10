using System.ComponentModel.DataAnnotations;

namespace Mediscreen.WebApp.Attributes
{
    public class DateBeforeNowAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            ErrorMessage = ErrorMessageString;
            var currentValue = (DateTime)value!;

            return currentValue >= DateTime.Now ? new ValidationResult(ErrorMessage) : ValidationResult.Success;
        }
    }
}
