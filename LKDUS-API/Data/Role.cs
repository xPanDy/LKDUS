using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    [Table("Roles")]
    public partial class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}