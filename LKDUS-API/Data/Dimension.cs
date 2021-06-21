using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    
    [Table("Dimensions")]
    public partial class Dimension
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
       

    }


}