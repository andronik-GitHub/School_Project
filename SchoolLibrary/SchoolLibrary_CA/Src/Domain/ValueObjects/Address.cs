using Domain.Common;

namespace Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Location Street { get; init; } = default!;
        public Location City { get; init; } = default!;
        public Location Country { get; init; } = default!;
        public string Location => $"{Country.Value}, {City.Value}, {Street.Value}";
        
        public Address() { }
        public Address(Location street, Location city, Location country)
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