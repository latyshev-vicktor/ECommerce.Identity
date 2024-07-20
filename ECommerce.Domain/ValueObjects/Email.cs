using CSharpFunctionalExtensions;
using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private const string EMAIL_PATTREN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public string Value { get; }
        protected Email() { }

        protected Email(string email) => Value = email;

        public static IExecutionResult<Email> Create(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) return ExecutionResult.Failure<Email>(UserErrors.EmailNotBeEmpty());

            if (!Regex.IsMatch(email, EMAIL_PATTREN)) return ExecutionResult.Failure<Email>(UserErrors.EmailNotValid());

            return ExecutionResult.Success(new Email(email));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
