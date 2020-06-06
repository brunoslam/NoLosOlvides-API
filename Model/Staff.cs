using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoLosOlvidesApi.Model
{
    public class Staff
    {
        [Key]
        public int IdStaff { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public Byte[] Password { get; set; }
        public int IdTipoStaff{ get; set; }
    }
}
