using CSharpFunctionalExtensions;

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

        public static Result<FullName> Create(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName)) return Result.Failure<FullName>("Имя пользователя не может быть пустым");
            if (string.IsNullOrWhiteSpace(lastName)) return Result.Failure<FullName>("Фамилия пользователя не может быть пустой");

            return Result.Success(new FullName(firstName, lastName));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
