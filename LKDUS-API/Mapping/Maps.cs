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
           
            CreateMap<Pack, PackDTO>().ReverseMap();

            CreateMap<Measurement, MeasurementDTO>().ReverseMap();
            CreateMap<Measurement, MeasurementCreateDTO>().ReverseMap();
            CreateMap<Measurement, MeasurementUpdateDTO>().ReverseMap();
          
            
            CreateMap<MeasurementType, MeasurementTypeDTO>().ReverseMap();
            CreateMap<MeasurementType, MeasurementTypeUpdateDTO>().ReverseMap();
            CreateMap<MeasurementType, MeasurementTypeCreateDTO>().ReverseMap();
            
            CreateMap<MeasurementPosition, MeasurementPositionDTO>().ReverseMap();
            CreateMap<MeasurementPosition, MeasurementPositionCreateDTO>().ReverseMap();
            CreateMap<MeasurementPosition, MeasurementPositionUpdateDTO>().ReverseMap();


            CreateMap<Role, RoleDTO>().ReverseMap();





        }
       
    }
}
