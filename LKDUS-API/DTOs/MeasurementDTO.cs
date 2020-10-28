using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class MeasurementDTO
    {
        public int Id { get; set; }
        public string DateCreated { get; set; }
        public int Shift { get; set; }

        public int? MachineId { get; set; }
        public virtual MachineDTO Machine { get; set; }

        public int? UserId { get; set; }
        public virtual UserDTO User { get; set; }

        public int? MeasurementPositionId { get; set; }
        public virtual MeasurementDTO MeasurementPosition { get; set; }
    }
}
