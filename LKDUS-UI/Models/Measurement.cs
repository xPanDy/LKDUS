using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Models
{
    public class Measurement
    {
        public int Id { get; set; }


        public string DateCreated { get; set; }
        public int Shift { get; set; }

        public int? MachineId { get; set; }
        public virtual Machine Machine { get; set; }

        public int? UserId { get; set; }
       

        public int? MeasurementPositionId { get; set; }
        public int? PackId { get; set; }
        public int? MeasurementTypeId { get; set; }



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
