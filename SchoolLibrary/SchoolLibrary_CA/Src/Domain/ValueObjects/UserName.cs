using Domain.Common;

namespace Domain.ValueObjects
{
    public class UserName : ValueObject
    {
        public Name FirstName { get; }
        public Name LastName { get; }
        public string FullName => $"{FirstName} {LastName}";

        public UserName(Name firstName, Name lastName)
        {
            FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
            LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
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