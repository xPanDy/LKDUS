using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace LKDUS_API.Data
{
    [Table("Measurements")]
    public class Measurement
    {
        public int Id { get; set; }
        public string DateCreated { get; set; }
        public int Shift { get; set; }

        public int? MachineId { get; set; }
        public virtual Machine  Machine { get; set; }

        public int? UserId { get; set; }
        public virtual User  User { get; set; }

        public int? MeasurementPositionId { get; set; }
        public virtual MeasurementPosition MeasurementPosition { get; set; }

    }
}