using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace LKDUS_API.Data
{
    [Table("Measurements")]
    public class Measurement
    {
        public int Id { get; set; }
        
         
        public string  DateCreated { get; set ; }
        public int Shift { get; set; }

        public int? MachineId { get; set; }
        public virtual Machine  Machine { get; set; }

        public int? UserId { get; set; }
        public virtual User  User { get; set; }

        public int? MeasurementPositionId { get; set; }
        public virtual MeasurementPosition MeasurementPosition { get; set; }
        public int? PackId { get; set; }
        public virtual Pack Pack { get; set; }
         public int? MeasurementTypeId { get; set; }
        public virtual MeasurementType MeasurementType { get; set; }



        public string Measurement1 { get; set; }
        public string Measurement2 { get; set; }
        public string Measurement3 { get; set; }
        public string Measurement4 { get; set; }
        public string Measurement5 { get; set; }
        public string Measurement6 { get; set; }
        public string Measurement7 { get; set; }
        public string Measurement8 { get; set; }
        public string Measurement9 { get; set; }
       

    }
}