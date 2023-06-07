using AutoMapper;
using Library.Application.Command.PostBooks;
using Library.Domain.Entities;

namespace Library.Domain.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostBooksCommand, Books>();
        }
    }
}
