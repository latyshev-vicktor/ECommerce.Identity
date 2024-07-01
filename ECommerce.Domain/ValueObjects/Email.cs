using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace ECommerce.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        private const string EMAIL_PATTREN = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        public string Value { get; }
        protected Email() { }

        protected Email(string email) => Value = email;

        public static Result<Email> Create(string email)
        {
            if(string.IsNullOrWhiteSpace(email)) return Result.Failure<Email>("Email не может быть пустым");

            if (!Regex.IsMatch(email, EMAIL_PATTREN)) return Result.Failure<Email>("Введенное значение не является email");

            return Result.Success(new Email(email));
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
