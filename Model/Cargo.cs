using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class Cargo
    {
        [Key]
        public int IdCargo { get; set; }
        public string Titulo { get; set; }
    }
}
