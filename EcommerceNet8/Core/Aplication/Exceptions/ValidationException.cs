using FluentValidation.Results;

namespace EcommerceNet8.Core.Aplication.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public IDictionary<string, string[]>Errors { get; }

        public ValidationException() : base("Se presentaron uno o mas errores de validation")
        {
            Errors = new Dictionary<string, string[]>();
        }


        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failuresGroup => failuresGroup.Key, failuresGroup => failuresGroup.ToArray());
        }

    }
}
