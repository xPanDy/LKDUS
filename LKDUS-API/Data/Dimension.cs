using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    
    [Table("Dimensions")]
    public partial class Dimension
    {
        public int Id { get; set; }
        public string? RangeName { get; set; }
        public string? Name { get; set; }


        public decimal? FormatMin { get; set; }
        public decimal? FormatMax { get; set; }


    }


}