using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    
    [Table("Packs")]
    public partial class Pack
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Machine { get; set; }

        public int Shift { get; set; }
        public string Author { get; set; }
        public string FusCode { get; set; }
        public string MeasurementTypeName { get; set; }
       

        public int? DimensionId { get; set; }


      //  public virtual List<Measurement> MeasurementsList { get; set; }//;//= new List<Measurement>();
   //     public virtual ICollection<Measurement> Measurements { get; set; }//;//= new List<Measurement>();

}


}