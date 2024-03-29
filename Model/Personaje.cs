﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [NotMapped]
        public int IdCargo { get; set; }
        [NotMapped]
        public Cargo Cargo { get; set; }
        public string Descripcion { get; set; }
        public string Rut { get; set; }
        public string Nacionalidad { get; set; }
        public string ImagenUrl { get; set; }
        public int IdEstadoAprobacion { get; set; }
        [NotMapped]
        public List<Evidencia> ArrEvidencias { get; set; }
        [NotMapped]
        public List<Cargo> ArrCargo { get; set; }
        [NotMapped]
        public List<Categoria> ArrCategoria { get; set; }
        [NotMapped]
        public List<Relacion_Personaje_Cargo> ArrRelacionCargo { get; set; }
        [NotMapped]
        public List<Relacion_Personaje_Categoria> ArrRelacionCategoria { get; set; }



    }
}
