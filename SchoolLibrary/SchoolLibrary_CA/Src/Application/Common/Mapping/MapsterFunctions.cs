using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping
{
    public class MapsterFunctions
    {
        public static TDestination MapSourceToDestination<TSource, TDestination>(TSource entity)
        {
            return entity!.Adapt<TDestination>();
        }
        public static IQueryable<TDestination> MapListSourceToDestination<TSource, TDestination>
            (IEnumerable<User> entities)
        {
            var entitiesQueryable = entities.AsQueryable();

            return entitiesQueryable.ProjectToType<TDestination>();
        }
    }
}