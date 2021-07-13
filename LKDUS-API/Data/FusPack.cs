using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LKDUS_API.Data
{

    [Table("vw_lkdus_packs")]
    public partial class FusPack
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