using AutoMapper;
using libraries.domain;
using library.Services.Domain.Dtos;


namespace library.Services.Helpers.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDto, Book>();
            CreateMap<Book, BookDto>();
        }

    }
}
