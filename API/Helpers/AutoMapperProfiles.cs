using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, IdDto>();
            CreateMap<SetReviewDto, Review>();
            CreateMap<Review, IdDto>();
            CreateMap<RateDto, Rating>();
            CreateMap<Book, BasicInfoAboutBookDto>()
            .ForMember(m => m.ReviewsNumber, opt => opt.MapFrom(i => i.Reviews.Count()))
            .ForMember(m => m.Rating, opt => opt.MapFrom(i => i.Ratings.Select(x => x.Score).DefaultIfEmpty(0).Average()));

            CreateMap<Review, GetReviewDto>();
            CreateMap<Book, BasicInfoAboutBookWithReviewsDto>()
            .ForMember(m => m.Rating, opt => opt.MapFrom(i => i.Ratings.Select(x => x.Score).DefaultIfEmpty(0).Average()));
        }
    }
}