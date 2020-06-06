using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class Personaje
    {
        [Key]
        public int IdPersonaje { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public Cargo IdCargo { get; set; }
        public string Rut { get; set; }
        public string Nacionalidad { get; set; }
        public string ImgUrl { get; set; }
    }
}
