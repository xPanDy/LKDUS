using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    
    [Table("Packs")]
    public partial class Pack
    {
        public int Id { get; set; }
        public string DateCreated { get; set; }
        public string Machine { get; set; }

        public int Shift { get; set; }
        public string Author { get; set; }
        public string FusCode { get; set; }
        public string MeasurementTypeName { get; set; }
       
    }


}