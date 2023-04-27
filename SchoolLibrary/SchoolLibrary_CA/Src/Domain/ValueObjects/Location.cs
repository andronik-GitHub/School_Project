using Domain.Common;

namespace Domain.ValueObjects
{
    public class Location : ValueObject
    {
        public string Value { get; } = default!;
        
        public Location() { }
        public Location(string value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Location is not valid");
            
            Value = value;
        }


        public static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}