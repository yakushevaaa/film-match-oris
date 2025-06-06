using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Persistence.Configurations
{

    namespace FilmMatch.Persistence
    {
        public class ApplicationDBContext : DbContext
        {
            public DbSet<Category> Categories { get; set; }
            public DbSet<Film> Films { get; set; }

            public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

                modelBuilder.ApplyConfiguration(new CategoryConfiguration());
                modelBuilder.ApplyConfiguration(new FilmsConfiguration());
            }
        }
    }

}
