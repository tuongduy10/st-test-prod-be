namespace WebServer.Application.Abstractions.Behaviors;

public class ValidationBehavivor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavivor(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
            return await next();

        var errorsDictionary = _validators
            .Select(_ => _.Validate(request))
            .SelectMany(_ => _.Errors)
            .Where(_ => _ is not null)
            .Select(_ => new Error(
                _.PropertyName,
                _.ErrorMessage))
            .Distinct()
            .ToArray();

        if (errorsDictionary.Any())
            return CreateValidationResult<TResponse>(errorsDictionary);

        return await next();
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors)
        where TResult : Result
    {
        if (typeof(TResult) == typeof(Result))
            return (ValidationResult.WithErrors(errors) as TResult)!;

        object validationResult = typeof(ValidationResult<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult<TResult>.WithErrors))!
            .Invoke(null, new[] { errors })!;

        return (TResult)validationResult;
    }

}
