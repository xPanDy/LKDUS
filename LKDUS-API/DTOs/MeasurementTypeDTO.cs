using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class MeasurementTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
         
    }

    public class MeasurementTypeCreateDTO
    {

        [Required]
        public string Name { get; set; }
         
    }

    public class MeasurementTypeUpdateDTO
    {
        [Required]
        public int Id { get; set; }

         
        public string Name { get; set; }

        
        
    }

}
