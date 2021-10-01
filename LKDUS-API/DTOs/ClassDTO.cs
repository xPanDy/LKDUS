using LKDUS_API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_API.DTOs
{
    public class ClassDTO
    {
         
        public int Id { get; set; }
        public string ClassName { get; set; }
        public int ClassRank { get; set; }
        public int IsOuter { get; set; }



    }

    public class ClassCreateDTO
    {




        public string ClassName { get; set; }
        public int ClassRank { get; set; }
        public int IsOuter { get; set; }


    }

    public class ClassUpdateDTO
    {
        public int Id { get; set; }

        public string ClassName { get; set; }
        public int ClassRank { get; set; }
        public int IsOuter { get; set; }

    }
}
