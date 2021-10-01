﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LKDUS_UI.Models
{
    public class Pack
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public string Machine { get; set; }

        public int Shift { get; set; }
        public string Author { get; set; }
        public string FusCode { get; set; }
        public string MeasurementTypeName { get; set; }
       // public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();
        //public virtual IList<Measurement> Measurements { get; set; } 
        public int? DimensionId { get; set; }
        public string FusClass { get; set; }

    }





}
