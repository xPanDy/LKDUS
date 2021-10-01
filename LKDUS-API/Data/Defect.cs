using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    
    [Table("Defects")]
    public partial class Defect
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Factory { get; set; }

        


      //  public virtual List<Measurement> MeasurementsList { get; set; }//;//= new List<Measurement>();
   //     public virtual ICollection<Measurement> Measurements { get; set; }//;//= new List<Measurement>();

}


}