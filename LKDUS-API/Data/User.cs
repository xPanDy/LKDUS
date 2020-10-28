using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{
    [Table("Users")]
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string NfcCode { get; set; }
        public virtual IList<Role> Roles { get; set; }
        public int? roleId { get; set; }
        
    }
}