using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class FusPackDTO
    {
        public int Id { get; set; }
        public int OperatorId { get; set; }

        public string OperatorNameSurname { get; set; }

        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public DateTime DateCreated { get; set; }
        public string Master { get; set; }

        public string ItemName { get; set; }
    }

    
}
