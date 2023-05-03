using System.Dynamic;
using System.Reflection;
using Application.Common.HATEOS;
using Application.Common.Interfaces;

namespace Application.Common.Helpers
{
    public class DataShaper<T> : IDataShaper<T>
    {
        private PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            // Get all the properties of an input class
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }


        public IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string? fieldsString)
        {
            // Parsing the input string and returns just the properties we need to return to the controller
            var requiredProperties = GetRequiredProperties(fieldsString);

            // and return extracted values from required prepared properties
            return FetchData(entities, requiredProperties);
        }
        public ShapedEntity ShapeData(T entity, string? fieldsString)
        {
            // Parsing the input string and returns just the properties we need to return to the controller
            var requiredProperties = GetRequiredProperties(fieldsString);

            // and return extracted values from required prepared properties
            return FetchDataForEntity(entity, requiredProperties);
        }

        // Parses the input string and returns just the properties we need to return to the controller
        private IEnumerable<PropertyInfo> GetRequiredProperties(string? fieldsString)
        {
            var requiredProperties = new List<PropertyInfo>();

            // If the fieldsString is not empty
            if (!string.IsNullOrWhiteSpace(fieldsString))
            {
                // it split
                var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                // and check if the fields match the properties in our entity
                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi =>
                        pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    // If they do, add them to the list of required properties
                    if (property != null) requiredProperties.Add(property);
                }
            }
            // if fieldsString is empty, consider all properties to be required
            else requiredProperties = Properties.ToList();

            return requiredProperties;
        }

        // Private methods to extract values from required prepared properties
        private static ShapedEntity FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requiredProperties)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            var shapedObject = new ShapedEntity();

            // Iterating over the required properties
            foreach (var property in requiredProperties)
            {
                // and using a bit of reflection, to extract the values and add them to ExpandoObject
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.Entity.TryAdd(property.Name, objectPropertyValue);
            }
            
            var objectProperty = entity
                .GetType()
                .GetProperties()
                .FirstOrDefault(pi => pi.Name.Contains("Id")); // searching param in class
            
            if (objectProperty == null) throw new ArgumentNullException(nameof(entity));
            shapedObject.Id = (Guid)objectProperty.GetValue(entity)!;

            return shapedObject;
        }
        private static IEnumerable<ShapedEntity> FetchData
            (IEnumerable<T> entities, IEnumerable<PropertyInfo> requiredProperties)
        {
            var shapedData = new List<ShapedEntity>();

            // Looping through the list of entities
            entities.ToList().ForEach(entity =>
                shapedData.Add(FetchDataForEntity(entity, requiredProperties)));

            // and returning a collection of shaped entities as a result
            return shapedData;
        }
    }
}