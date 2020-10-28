using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{

    [Table("Machines")]
    public partial class Machine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}