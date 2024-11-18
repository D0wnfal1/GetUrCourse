using System.Reflection;
using FluentValidation;

namespace GetUrCourse.Services.CourseAPI.Shared;

public static class CustomValidator
{
    public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
        this IRuleBuilder<T, TElement> ruleBuilder,
        Func<TElement, Result<TValueObject>> factoryMethod)
    {
        return ruleBuilder.Custom((value, context) =>
        {
            var result = factoryMethod(value);

            if (result is not ValidationResult<TValueObject>) return;

            var errorsProperty = result.GetType().GetProperty("Errors", BindingFlags.Public | BindingFlags.Instance);
            var errors = (errorsProperty?.GetValue(result) as List<Error>)?
                .Select(x => x.Message)
                .Aggregate((x, y) => $"{x}|{y}");

            context.AddFailure(errors);
        });
    }
}