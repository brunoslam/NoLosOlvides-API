using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class CategoriaEvidencia
    {
        [Key]
        public int IdCategoriaEvidencia { get; set; }
        public string Titulo { get; set; }
    }
}
