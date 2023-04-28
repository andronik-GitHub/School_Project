using System.Reflection;
using Application.Features.UserFeatures.Commands.CreateUser;
using Application.Features.UserFeatures.Commands.UpdateUser;
using Application.Features.UserFeatures.Queries.Common;
using Domain.Entities;
using Domain.ValueObjects;
using Mapster;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Mapping
{
    public static class MapsterConfigurations
    {
        public static void RegisterMapsterConfiguration(this IServiceCollection services)
        {
            RegisterCreateUserCommandConfig();
            RegisterUpdateUserCommandConfig();
            RegisterUserDTOConfig();
            
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }


        private static void RegisterCreateUserCommandConfig()
        {
            TypeAdapterConfig<CreateUserCommand, User>
                .NewConfig()
                .Map(dest => dest.UserName, src => new UserName
                {
                    FirstName = new Name(src.FirstName), 
                    LastName = new Name(src.LastName)
                })
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Address, src => new Address
                {
                    Street = new Location(src.Street),
                    City = new Location(src.City),
                    Country = new Location(src.Country)
                })
                .Map(dest => dest.Phone, src => src.Phone)
                .TwoWays();
        }
        private static void RegisterUpdateUserCommandConfig()
        {
            TypeAdapterConfig<UpdateUserCommand, User>
                .NewConfig()
                .Map(dest => dest.UserName, src => new UserName
                {
                    FirstName = new Name(src.FirstName), 
                    LastName = new Name(src.LastName)
                })
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Address, src => new Address
                {
                    Street = new Location(src.Street),
                    City = new Location(src.City),
                    Country = new Location(src.Country)
                })
                .Map(dest => dest.Phone, src => src.Phone)
                .TwoWays();
        }
        private static void RegisterUserDTOConfig()
        {
            TypeAdapterConfig<User, UserDTO>
                .NewConfig()
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.FirstName, src => src.UserName.FirstName.Value)
                .Map(dest => dest.LastName, src => src.UserName.LastName.Value)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.Password, src => src.Password)
                .Map(dest => dest.Street, src => src.Address.Street.Value)
                .Map(dest => dest.City, src => src.Address.City.Value)
                .Map(dest => dest.Country, src => src.Address.Country.Value)
                .Map(dest => dest.Phone, src => src.Phone)
                .TwoWays();
        }
    }
}