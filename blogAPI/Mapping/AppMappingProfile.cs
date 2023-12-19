using AutoMapper;
using blogAPI.Data.Entities;
using blogAPI.Dto;

namespace blogAPI.Mapping
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<CommentModel,  CommentDto>();
            CreateMap<TagModel, TagDto>();
            CreateMap<UserModel, AuthorDto>();
        }
    }
}
