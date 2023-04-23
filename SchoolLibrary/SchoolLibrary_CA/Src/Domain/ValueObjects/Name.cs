using System.Text.RegularExpressions;
using Domain.Common;

namespace Domain.ValueObjects
{
    public class Name : ValueObject
    {
        private static readonly Regex ValidationRegex = new Regex(
            @"^[\p{L}\p{M}]{1,100}\z",
            RegexOptions.Singleline | RegexOptions.Compiled);
        
        public string Value { get; } = default!;


        protected Name() { }
        public Name(string value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Name is not valid");
            
            Value = value;
        }


        public static bool IsValid(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && ValidationRegex.IsMatch(value);
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}