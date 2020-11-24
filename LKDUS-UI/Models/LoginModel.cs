using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }





    }

    public class LoginModelUserList
    {
        [Required]
        [Display(Name = "User Names")]
        public IList<LoginModel> UserNames { get; set; }





    }

    
     
}
