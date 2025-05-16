using FilmMatch.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmMatch.Persistence
{
    public class FilmsConfiguration : IEntityTypeConfiguration<Film>
    {
        public void Configure(EntityTypeBuilder<Film> builder)
        {
            builder.HasData( new Film { Id= 1, Title= "", Description="", ImageUrl="",ImageAlt=""}
                );
        }
    }
}
