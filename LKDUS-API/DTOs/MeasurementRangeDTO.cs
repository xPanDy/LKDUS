using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class MeasurementRangeDTO
    {
        public int Id { get; set; }
        public string RangeName { get; set; }
        public string FormatName { get; set; }
        public decimal FormatMin { get; set; }
        public decimal FormatMax { get; set; }

    }

    public class MeasurementRangeCreateDTO
    {

        public string RangeName { get; set; }
        public string FormatName { get; set; }
        public decimal FormatMin { get; set; }
        public decimal FormatMax { get; set; }
    }

    public class MeasurementRangeUpdateDTO
    {
        [Required]
        public int Id { get; set; }


        public string RangeName { get; set; }
        public string FormatName { get; set; }
        public decimal FormatMin { get; set; }
        public decimal FormatMax { get; set; }



    }

}
