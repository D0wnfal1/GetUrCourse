using FluentValidation;
using GetUrCourse.Services.UserAPI.Core.Shared;
using MediatR;
using Error = GetUrCourse.Services.UserAPI.Core.Shared.Error;

namespace GetUrCourse.Services.UserAPI.Application.Behavior;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        
        var validationResults = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(failures => failures.Errors)
            .Select(failure => new Error(failure.PropertyName, failure.ErrorMessage))
            .Distinct()
            .ToList();
        
        if (errors.Any())
        {
            logger.LogWarning("Validation errors - {RequestType}: {@ValidationErrors}", 
                typeof(TRequest).Name, errors);
            return CreateValidationResult<TResponse>(errors);
        }
        
        var response = await next();
        return response;
    }

    private static TResult CreateValidationResult<TResult>(List<Error> errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
        {
            return (ValidationResult.WithErrors(errors) as TResult)!;
        }

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, new object?[] { errors })!;

        return (TResult)validationResult;
    }
    
}