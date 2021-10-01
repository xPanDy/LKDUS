using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{

    [Table("Classes")]
    public partial class Classs
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int? ClassRank { get; set; }
        public int? IsOuter { get; set; }




        //  public virtual List<Measurement> MeasurementsList { get; set; }//;//= new List<Measurement>();
        //     public virtual ICollection<Measurement> Measurements { get; set; }//;//= new List<Measurement>();

    }


}