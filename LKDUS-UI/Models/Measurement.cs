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
        public string MachineName { get; set; }
        public int Shift { get; set; }

        //public int? MachineId { get; set; }
        public virtual Machine Machine { get; set; }

        public string UserId { get; set; }
       

        public int? MeasurementPositionId { get; set; }
        public int? PackId { get; set; }
        public int? MeasurementTypeId { get; set; }



        public decimal Measurement1 { get; set; }
        public decimal Measurement2 { get; set; }
        public decimal Measurement3 { get; set; }
        public decimal Measurement4 { get; set; }
        public decimal Measurement5 { get; set; }
        public decimal Measurement6 { get; set; }
        public decimal Measurement7 { get; set; }
        public decimal Measurement8 { get; set; }
        public decimal Measurement9 { get; set; }
        public decimal Measurement10 { get; set; }




    }





}
