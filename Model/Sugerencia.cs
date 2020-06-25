using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class Sugerencia
    {
        [Key]
        public int IdSugerencia { get; set; }
        public string Titulo { get; set; }
        public DateTime FechaIngreso { get; set; }
    }
}
