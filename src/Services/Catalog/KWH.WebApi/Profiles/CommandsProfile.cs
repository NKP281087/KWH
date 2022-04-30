using AutoMapper;
using KWH.DAL.Entities;
using KWH.WebApi.Dtos;

namespace KWH.WebApi.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<RFId, RFIdDtos>().ReverseMap();   
            CreateMap<Section, SectionDtos>().ReverseMap();
          //  CreateMap<ClassMaster, ClassMasterDtos>().ReverseMap(); 
        }
    }
}
