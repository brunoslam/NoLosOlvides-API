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
    }
}
