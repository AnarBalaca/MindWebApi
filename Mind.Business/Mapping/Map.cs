using AutoMapper;
using Mind.Entity.Dto.Psychologist;
using Mind.Entity.Entities;

namespace Mind.Business.Mapping;

public class Map : Profile
{
    public Map()
    {
        CreateMap<Psychologist, PsychologistGetDto>();
        CreateMap<PsychologistCreateDto,Psychologist>();
    }

}
