using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class MeasurementPositionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class MeasurementPositionCreateDTO
    {

        [Required]
        public string   Name { get; set; } 
         
    }

    public class MeasurementPositionUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

    }
}
