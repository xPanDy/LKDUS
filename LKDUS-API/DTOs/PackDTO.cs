using LKDUS_API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class PackDTO
    {
        



        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Machine { get; set; }

        public int Shift { get; set; }
        public string Author { get; set; }
        public string FusCode { get; set; }
        public string MeasurementTypeName { get; set; }
        public int? DimensionId { get; set; }
        //  public virtual IList<Measurement> MeasurementsList { get; set; }
        public string FusClass { get; set; }
    }

    public class PackCreateDTO
    {

        
        
        public DateTime DateCreated { get; set; }
        public string Machine { get; set; }

        public int Shift { get; set; }
        public string Author { get; set; }
        public string FusCode { get; set; }
        public string MeasurementTypeName { get; set; }
          public int? DimensionId { get; set; }
        public string FusClass { get; set; }
    }

    public class PackUpdateDTO
    {
        public int Id { get; set; }
        
        public DateTime DateCreated { get; set; }
        public string Machine { get; set; }

        public int Shift { get; set; }
        public string Author { get; set; }
        public string FusCode { get; set; }
        public string MeasurementTypeName { get; set; }
        public int? DimensionId { get; set; }
        public string FusClass { get; set; }
    }
}
