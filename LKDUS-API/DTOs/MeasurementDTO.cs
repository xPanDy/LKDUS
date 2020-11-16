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
        public virtual MachineDTO Machine { get; set; }

        public int UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public int MeasurementPositionId { get; set; }
        public virtual MeasurementDTO MeasurementPosition { get; set; }
    }

    public class MeasurementCreateDTO
    {

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
