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


        public DateTime DateCreated { get; set; }
        public int Shift { get; set; }

        public string MachineName { get; set; }
        public string UserId { get; set; }
        public int MeasurementPositionId { get; set; }

        public int FusPackId { get; set; }
      //  public Pack Pack { get; set; }
        public int? PackId { get; set; }

        public int? DefectId { get; set; }
        public int? ClassId { get; set; }
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
        public decimal Measurement10 { get; set; }
        public decimal Measurement11 { get; set; }
        public decimal Measurement12 { get; set; }
        public decimal Measurement13 { get; set; }
        public decimal Measurement14 { get; set; }
        public decimal Measurement15 { get; set; }
        public decimal Measurement16 { get; set; }
        public decimal Measurement17 { get; set; }
        public decimal Measurement18 { get; set; }
        public decimal Measurement19 { get; set; }
        public decimal Measurement20 { get; set; }
        public decimal Measurement21 { get; set; }
        public decimal Measurement22 { get; set; }
        public decimal Measurement23 { get; set; }
        public decimal Measurement24 { get; set; }
        public decimal Measurement25 { get; set; }
        public decimal Measurement26 { get; set; }
        public decimal Measurement27 { get; set; }
        public decimal Measurement28 { get; set; }
        public decimal Measurement29 { get; set; }
        public decimal Measurement30 { get; set; }
        public decimal Measurement31 { get; set; }
        public decimal Measurement32 { get; set; }
        public decimal Measurement33 { get; set; }

    }
}