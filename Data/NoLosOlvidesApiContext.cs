using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoLosOlvidesApi.Model;

namespace NoLosOlvidesApi.Data
{
    public class NoLosOlvidesApiContext : DbContext
    {
        public NoLosOlvidesApiContext (DbContextOptions<NoLosOlvidesApiContext> options)
            : base(options)
        {
        }

        public DbSet<NoLosOlvidesApi.Model.Categoria> Categoria { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Cargo> Cargo { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Staff> Staff { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Personaje> Personaje { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Evidencia> Evidencia { get; set; }

        public DbSet<NoLosOlvidesApi.Model.CategoriaEvidencia> CategoriaEvidencia { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Relacion_Personaje_Cargo> Relacion_Personaje_Cargo { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Relacion_Personaje_Categoria> Relacion_Personaje_Categoria { get; set; }

        public DbSet<NoLosOlvidesApi.Model.Sugerencia> Sugerencia { get; set; }
    }
}
