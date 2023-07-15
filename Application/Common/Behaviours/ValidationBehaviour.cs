using FluentValidation;
using MediatR;
namespace Application.Common.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                //var firstTask = Task.Run(() => Console.WriteLine(""));
                //var secondTask = Task.Run(() => Console.WriteLine(""));
                //var thirdTask = Task.Run(() => Console.WriteLine(""));

                //await Task.WhenAll(firstTask, secondTask, thirdTask);

                var valitadionResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = valitadionResults.Where(r => r.Errors.Any())
                    .SelectMany(r => r.Errors)
                    .ToList();
                if (failures.Any())
                {
                    throw new ValidationException(failures);
                }

            }
            return await next();

        }
    }
}
