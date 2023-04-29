using Application.Features.ReviewFeatures.Commands.CreateReview;
using Application.Features.ReviewFeatures.Commands.UpdateReview;
using Application.Features.ReviewFeatures.Queries.Common;
using Domain.Entities;
using Mapster;

namespace Application.Common.Mapping.Mapster.MapsterConfiguraions
{
    public static class RegisterReviewConfig
    {
        public static void Registration()
        {
            RegisterCreateReviewCommandConfig();
            RegisterUpdateReviewCommandConfig();
            RegisterReviewDTOConfig();
        }
        
        private static void RegisterCreateReviewCommandConfig()
        {
            TypeAdapterConfig<CreateReviewCommand, Review>
                .NewConfig()
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.ReviewText, src => src.ReviewText)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
        private static void RegisterUpdateReviewCommandConfig()
        {
            TypeAdapterConfig<UpdateReviewCommand, Review>
                .NewConfig()
                .Map(dest => dest.ReviewId, src => src.Id)
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.ReviewText, src => src.ReviewText)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }

        private static void RegisterReviewDTOConfig()
        {
            TypeAdapterConfig<Review, ReviewDTO>
                .NewConfig()
                .Map(dest => dest.ReviewId, src => src.ReviewId)
                .Map(dest => dest.Rating, src => src.Rating)
                .Map(dest => dest.ReviewText, src => src.ReviewText)
                .Map(dest => dest.UserId, src => src.UserId)
                .Map(dest => dest.BookId, src => src.BookId)
                .TwoWays();
        }
    }
}