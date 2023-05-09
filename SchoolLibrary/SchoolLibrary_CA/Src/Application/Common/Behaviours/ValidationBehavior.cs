using FluentValidation;
using MediatR;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Common.Behaviours
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            // All objects inherited from AbstractValidator are collected throughout the project
            _validators = validators;
        }

        
        public async Task<TResponse> Handle
            (TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // Checks if the request has any validation rules.
            // If there are none, the request will be passed on for processing
            if (!_validators.Any()) return await next();

            // ValidationContext object that contains information about the request and
            // the validation rules passed in the constructor
            var context = new ValidationContext<TRequest>(request);

            // Validates each rule and collects errors into a single object
            var errorsDictionary = _validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Where(x => x != null)
                .GroupBy(
                    x => x.PropertyName,
                    x => x.ErrorMessage,
                    (propertyName, errorMessages) => new
                    {
                        Key = propertyName,
                        Values = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(x => x.Key, x => x.Values);

            // If there are errors in the dictionary, then a throw ValidationException
            if (errorsDictionary.Any()) throw new ValidationException(errorsDictionary);

            return await next();
        }
    }
}