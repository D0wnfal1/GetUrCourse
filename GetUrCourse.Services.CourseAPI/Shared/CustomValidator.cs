using System.Reflection;
using FluentValidation;
using GetUrCourse.Services.CourseAPI.Core.Enums;
using GetUrCourse.Services.CourseAPI.Core.Exceptions;
using GetUrCourse.Services.CourseAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
    
    public static IRuleBuilderOptions<T, string?> NotEmptyAndNotLongerThan<T>(
        this IRuleBuilder<T, string?> ruleBuilder,
        string name,
        int maxLength)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(DomainExceptions.Empty(name))
            .MaximumLength(maxLength)
            .WithMessage(DomainExceptions.MaxLength(name, maxLength))
            .When(t => t != null);
    }
    
    public static IRuleBuilderOptions<T, Guid> IsStudentExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Students.AnyAsync(s => s.Id == id, cancellationToken: t);
        }).WithMessage("Student is not found");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsCourseExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Courses.AnyAsync(c => c.Id == id, cancellationToken: t);
        }).WithMessage("Course is not found");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsAuthorExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Authors.AnyAsync(a => a.Id == id, cancellationToken: t);
        }).WithMessage("Author is not found");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsCourseCommentExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.CourseComments.AnyAsync(cc => cc.Id == id, cancellationToken: t);
        }).WithMessage("Course comment is not found");
    }
    
    public static IRuleBuilderOptions<T, Guid?> IsCategoryExist<T>(
        this IRuleBuilder<T, Guid?> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Categories.AnyAsync(c => c.Id == id, cancellationToken: t);
        }).WithMessage("Category is not found");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsCategoryExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Categories.AnyAsync(c => c.Id == id, cancellationToken: t);
        }).WithMessage("Category is not found");
    }
    
    public static IRuleBuilderOptions<T, int> IsSubscriptionExist<T>(
        this IRuleBuilder<T, int> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.Subscriptions.AnyAsync(c => c.Id == id, cancellationToken: t);
        }).WithMessage("Category is not found");
    }
    
    public static IRuleBuilderOptions<T, Language> IsLanguageValid<T>(
        this IRuleBuilder<T, Language> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Language is required")
            .Must(x => Enum.IsDefined(typeof(Language), x))
            .WithMessage("Language is not valid");
    }
    public static IRuleBuilderOptions<T, Language?> IsLanguageValid<T>(
        this IRuleBuilder<T, Language?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Language is required")
            .Must(x => Enum.IsDefined(typeof(Language), x))
            .WithMessage("Language is not valid");
    }
    
    public static IRuleBuilderOptions<T, Level> IsLevelValid<T>(
        this IRuleBuilder<T, Level> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Level is required")
            .Must(x => Enum.IsDefined(typeof(Level), x))
            .WithMessage("Level is not valid");
    }
    public static IRuleBuilderOptions<T, Level?> IsLevelValid<T>(
        this IRuleBuilder<T, Level?> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Level is required")
            .Must(x => Enum.IsDefined(typeof(Level), x))
            .WithMessage("Level is not valid");
    }
    
    
    public static IRuleBuilderOptions<T, SubscriptionStatus> IsStatusValid<T>(
        this IRuleBuilder<T, SubscriptionStatus> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("Status is required")
            .Must(x => Enum.IsDefined(typeof(SubscriptionStatus), x))
            .WithMessage("Status is not valid");
    }
    
    public static IRuleBuilderOptions<T, Guid> IsModuleExist<T>(
        this IRuleBuilder<T, Guid> ruleBuilder,
        CourseDbContext context)
    {
        return ruleBuilder.MustAsync(async (id, t) =>
        {
            return await context.CourseModules.AnyAsync(c => c.Id == id, cancellationToken: t);
        }).WithMessage("Category is not found");
    }
    
}