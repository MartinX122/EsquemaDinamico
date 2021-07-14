using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EsquemaDinamico.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Tabla> Tablas { set; get; }

        public DbSet<Campos> Campos { set; get;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

           

        }
    }
}
