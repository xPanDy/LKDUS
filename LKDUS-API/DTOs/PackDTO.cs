using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class PackDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Author { get; set; }
        public string DateCreated { get; set; }
        public string Machine { get; set; }
    }

    public class PackCreateDTO
    {

        [Required]
        public string Code { get; set; }
        public string Author { get; set; }
        public string DateCreated { get; set; }
        public string Machine { get; set; }
    }

    public class PackUpdateDTO
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        public string Author { get; set; }
        public string DateCreated { get; set; }
        public string Machine { get; set; }
    }
}
