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
            CreateMap<ClassMaster, ClassMasterDtos>().ReverseMap();
            CreateMap<ClassMaster, ClassMasterViewModel>().ReverseMap();
        }
    }
}
