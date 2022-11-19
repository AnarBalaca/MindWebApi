using AutoMapper;
using Mind.Business.Dto.Blog;
using Mind.Business.Dto.User;
using Mind.Entity.Dto.Psychologist;
using Mind.Entity.Entities;
using Mind.Entity.Identity;

namespace Mind.Business.Mapping;

public class Map : Profile
{
    public Map()
    {
        CreateMap<Psychologist, PsychologistGetDto>();
        CreateMap<PsychologistCreateDto,Psychologist>();
        CreateMap<Blog, BlogGetDto>();
        CreateMap<BlogCreateDto, Blog>();
        CreateMap<AppUser, UserGetDto>();
        CreateMap<AppUser, UserUpdateDto>();

    }

}
