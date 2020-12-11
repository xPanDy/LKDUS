using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace LKDUS_API.Data
{
    [Table("MeasurementType")]
    public class MeasurementType
    {
        public int Id { get; set; }
        
         
        public string  Name { get; set ; }
         
 
    }
}