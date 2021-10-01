using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.PortableExecutable;

namespace LKDUS_API.Data
{
    [Table("MeasurementRanges")]
    public class MeasurementRange
    {
        public int Id { get; set; }
        
         
        public string? RangeName { get; set ; }
        public string? FormatName { get; set ; }
        public decimal? FormatMin { get; set ; }
        public decimal? FormatMax { get; set ; }
         
 
    }
}