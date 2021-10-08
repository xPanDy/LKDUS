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
        public string? RangeName { get; set; }
        public string? Name { get; set; }


        public decimal? FormatMin { get; set; }
        public decimal? FormatMax { get; set; }


    }

    public class DimensionCreateDTO
    {




        public string? RangeName { get; set; }
        public string? Name { get; set; }


        public decimal? FormatMin { get; set; }
        public decimal? FormatMax { get; set; }


    }

     
}
