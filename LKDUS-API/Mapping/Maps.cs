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
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Machine, MachineDTO>().ReverseMap();
            CreateMap<Measurement, MeasurementDTO>().ReverseMap();
            CreateMap<MeasurementPosition, MeasurementPositionDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
       
    }
}
