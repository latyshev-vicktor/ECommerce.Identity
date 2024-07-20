using CSharpFunctionalExtensions;
using ECommerce.Domain.Common;
using ECommerce.Domain.Errors;

namespace ECommerce.Domain.ValueObjects
{
    public class FullName : ValueObject
    {
        public string FirstName { get; }
        public string LastName { get; }

        protected FullName() { }
        protected FullName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static IExecutionResult<FullName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) return ExecutionResult.Failure<FullName>(UserErrors.FirstNameNotBeEmpty());

            if (string.IsNullOrWhiteSpace(lastName)) return ExecutionResult.Failure<FullName>(UserErrors.LastNameNotBeEmpty());

            return ExecutionResult.Success(new FullName(firstName, lastName));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
