using Domain.Common;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Street { get; } = default!;
        public string City { get; } = default!;
        public string Country { get; } = default!;
        public string Location => $"{Country}, {City}, {Street}";
        
        public Address() {}
        public Address(string street, string city, string country)
        {
            Street = street;
            City = city;
            Country = country;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return City;
            yield return Country;
        }
        public override string ToString()
        {
            return Location;
        }
    }
}