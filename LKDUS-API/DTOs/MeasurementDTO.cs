using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class MeasurementDTO
    {
        public int Id { get; set; }
        public string DateCreated { get; set; }
        public int Shift { get; set; }

        public int MachineId { get; set; }
        

        public string UserId { get; set; }
       

        public int MeasurementPositionId { get; set; }
        public int PackId { get; set; }
        public int MeasurementTypeId { get; set; }
        public decimal Measurement1 { get; set; }
        public decimal Measurement2 { get; set; }
        public decimal Measurement3 { get; set; }
        public decimal Measurement4 { get; set; }
        public decimal Measurement5 { get; set; }
        public decimal Measurement6 { get; set; }
        public decimal Measurement7 { get; set; }
        public decimal Measurement8 { get; set; }
        public decimal Measurement9 { get; set; }
    }

    public class MeasurementCreateDTO
    {

      
        public string DateCreated { get; set; }
        
        public int Shift { get; set; }

         public int MachineId { get; set; }
 
         public string UserId { get; set; }

         public int MeasurementPositionId { get; set; }
        public int PackId { get; set; }
        public int MeasurementTypeId { get; set; }
        public decimal Measurement1 { get; set; }
        public decimal Measurement2 { get; set; }
        public decimal Measurement3 { get; set; }
        public decimal Measurement4 { get; set; }
        public decimal Measurement5 { get; set; }
        public decimal Measurement6 { get; set; }
        public decimal Measurement7 { get; set; }
        public decimal Measurement8 { get; set; }
        public decimal Measurement9 { get; set; }



    }

    public class MeasurementUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string DateCreated { get; set; }

        [Required]
        public int Shift { get; set; }

        [Required]
        public int MachineId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MeasurementPositionId { get; set; }
        
    }

}
