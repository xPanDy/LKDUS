using LKDUS_API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class DefectDTO
    {




        public int Id { get; set; }
        public string Name { get; set; }
        public int Factory { get; set; }



    }

    public class DefectCreateDTO
    {



      
        public string Name { get; set; }
        public int Factory { get; set; }

    }

    public class DefectUpdateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int Factory { get; set; }
    }
}
