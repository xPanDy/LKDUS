using AutoMapper;
using LKDUS_API.Data;
using LKDUS_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.Mapping
{
    public class Maps : Profile
    {
        public Maps()
        {
          //  CreateMap<AspNetUsers, AspNetUserDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();

            CreateMap<Machine, MachineDTO>().ReverseMap();
            CreateMap<Machine, MachineCreateDTO>().ReverseMap();
            CreateMap<Machine, MachineUpdateDTO>().ReverseMap();
           
            CreateMap<Pack, PackDTO>().ReverseMap();
            CreateMap<Pack, PackCreateDTO>().ReverseMap();
            CreateMap<Pack, PackUpdateDTO>().ReverseMap();
            
            
            CreateMap<Classs, ClassDTO>().ReverseMap();
            CreateMap<Classs, ClassCreateDTO>().ReverseMap();
            CreateMap<Classs, ClassUpdateDTO>().ReverseMap();


            CreateMap<Defect, DefectDTO>().ReverseMap();
            CreateMap<Defect, DefectCreateDTO>().ReverseMap();
            CreateMap<Defect, DefectUpdateDTO>().ReverseMap();

            CreateMap<Measurement, MeasurementDTO>().ReverseMap();
            CreateMap<Measurement, MeasurementCreateDTO>().ReverseMap();
            CreateMap<Measurement, MeasurementUpdateDTO>().ReverseMap();
          
            
            CreateMap<MeasurementType, MeasurementTypeDTO>().ReverseMap();
            CreateMap<MeasurementType, MeasurementTypeUpdateDTO>().ReverseMap();
            CreateMap<MeasurementType, MeasurementTypeCreateDTO>().ReverseMap();
            
            CreateMap<Dimension, DimensionDTO>().ReverseMap();
            CreateMap<Dimension, DimensionCreateDTO>().ReverseMap();
            
            
            CreateMap<MeasurementPosition, MeasurementPositionDTO>().ReverseMap();
            CreateMap<MeasurementPosition, MeasurementPositionCreateDTO>().ReverseMap();
            CreateMap<MeasurementPosition, MeasurementPositionUpdateDTO>().ReverseMap();
          
         CreateMap<MeasurementRange, MeasurementRangeDTO>().ReverseMap();
            CreateMap<MeasurementRange, MeasurementRangeCreateDTO>().ReverseMap();
            CreateMap<MeasurementRange, MeasurementRangeUpdateDTO>().ReverseMap();
          
            CreateMap<FusPack, FusPackDTO>().ReverseMap();


            CreateMap<Role, RoleDTO>().ReverseMap();
           
            
            //CreateMap<AspUser, AspUserDTO>().ReverseMap();
            //CreateMap<AspUser, AspCreateUserDTO>().ReverseMap();




        }
       
    }
}
