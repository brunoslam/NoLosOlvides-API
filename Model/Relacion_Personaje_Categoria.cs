using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class Relacion_Personaje_Categoria
    {
        [Key]
        public int IdRelacionPersonajeCategoria { get; set; }
        public int IdPersonaje { get; set; }
        public int IdCategoria { get; set; }
    }
}
