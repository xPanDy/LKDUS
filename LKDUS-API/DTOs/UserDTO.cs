using LKDUS_API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string NfcCode { get; set; }
        public virtual IList<RoleDTO> Roles { get; set; }
        public int? RoleId {get; set;}
    }

    public class UserCreateDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public string NfcCode { get; set; }
        public virtual IList<RoleDTO> Roles { get; set; }
        public int? RoleId { get; set; }
    }

    public class UserUpdateDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string NfcCode { get; set; }
       
        public int? RoleId { get; set; }
    
    }

    
}
