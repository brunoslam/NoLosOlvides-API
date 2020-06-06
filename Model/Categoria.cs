using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class Categoria
    {
        [Key]
        public int IdCategoria { get; set; }
        public string Titulo { get; set; }
    }
}
