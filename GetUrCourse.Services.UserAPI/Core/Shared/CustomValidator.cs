using System.Reflection;
using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Enums;
using GetUrCourse.Services.UserAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Role = GetUrCourse.Services.UserAPI.Core.Enums.Role;

namespace GetUrCourse.Services.UserAPI.Core.Shared;

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

    public static IRuleBuilderOptions<T, string> IsEmailUnique<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        UserDbContext context)
    {
        return ruleBuilder.MustAsync(async (email, t) =>
        {
            return !await context.Users.AnyAsync(u => u.Email == email, cancellationToken: t);
        }).WithMessage("Email is already taken");
    }

    public static IRuleBuilderOptions<T, Guid> IsUserExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        UserDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Users.AnyAsync(u => u.Id == id, cancellationToken: t);
        }).WithMessage("User not found");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsStudentExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        UserDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Students.AnyAsync(u => u.UserId == id, cancellationToken: t);
        }).WithMessage("Student is not found");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsAuthorExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        UserDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Authors.AnyAsync(u => u.UserId == id, cancellationToken: t);
        }).WithMessage("Author is not found");
    }
    

    public static IRuleBuilderOptions<T, Role> IsRoleValid<T>(
        this IRuleBuilder<T, Role> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Role is required")
            .Must(x => Enum.IsDefined(typeof(Role), x))
            .WithMessage("Role is not valid");
    }

    public static IRuleBuilderOptions<T, Sex?> IsSexValid<T>(
        this IRuleBuilder<T, Sex?> ruleBuilder)
    {
        
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Department is required.")
            .Must(x => Enum.IsDefined(typeof(Sex), x.HasValue))
            .WithMessage("Sex is not valid");
    }
    public static IRuleBuilderOptions<T, Department> IsDepartmentValid<T>(
        this IRuleBuilder<T, Department> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Department is required.")
            .Must(x =>
                Enum.IsDefined(typeof(Department), x))
            .WithMessage("Department is not valid");
    }
    
    public static IRuleBuilderOptions<T, Department?> IsDepartmentValid<T>(
        this IRuleBuilder<T, Department?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Department is required.")
            .Must(x =>
                x.HasValue &&
                Enum.IsDefined(typeof(Department), x.Value))
            .WithMessage("Department is not valid");
    }
}