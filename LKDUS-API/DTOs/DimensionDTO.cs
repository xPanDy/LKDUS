using LKDUS_API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class DimensionDTO
    {




        public int Id { get; set; }
        public string Name { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }


    }

    public class DimensionCreateDTO
    {



     
        public string Name { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }


    }

     
}
