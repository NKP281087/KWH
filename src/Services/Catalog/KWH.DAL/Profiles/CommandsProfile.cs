using AutoMapper;
using KWH.Common.ViewModel.Dtos;
using KWH.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KWH.Common.ViewModel.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            CreateMap<RFId, RFIdDtos>().ReverseMap();
            CreateMap<Section, SectionDtos>().ReverseMap();
            CreateMap<ClassMaster, ClassMasterDtos>().ReverseMap();
            CreateMap<ClassMaster, ClassMasterViewModel>().ReverseMap();
            CreateMap<CandidateInfo, CandidateInfoDtos>().ReverseMap();
            CreateMap<CandidateInfo, CandidateViewModel>().ReverseMap();
             
                                     
        }
    }
}
