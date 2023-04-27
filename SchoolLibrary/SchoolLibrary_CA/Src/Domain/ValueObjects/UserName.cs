using Domain.Common;

namespace Domain.ValueObjects
{
    public class UserName : ValueObject
    {
        public Name FirstName { get; init; } = default!;
        public Name LastName { get; init; } = default!;
        public string FullName => $"{FirstName.Value} {LastName.Value}";

        public UserName() { }
        public UserName(Name firstName, Name lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
        
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return FirstName;
            yield return LastName;
        }
        public override string ToString()
        {
            return FullName;
        }
    }
}