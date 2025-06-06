using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Persistence.Configurations
{
    public class FilmsConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasData(new Film { Id = 1, Title = "", Description = "", ImageUrl = "", ImageAlt = "" }
                );
        }
    }
}
