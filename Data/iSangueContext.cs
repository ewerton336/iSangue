using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iSangue.Models;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;


namespace iSangue.Data
{
    public class iSangueContext : DbContext
    {
        public iSangueContext (DbContextOptions<iSangueContext> options)
            : base(options)
        {
        }

        public DbSet<Entidadecoletora> Entidadecoletora { get; set; }
        public DbSet<Doador> Doador { get; set; }
    }
}
