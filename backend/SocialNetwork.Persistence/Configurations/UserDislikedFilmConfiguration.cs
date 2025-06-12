using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Persistence.Configurations;

public class UserDislikedFilmConfiguration : IEntityTypeConfiguration<UserDislikedFilm>
{
    public void Configure(EntityTypeBuilder<UserDislikedFilm> builder)
    {
        builder.HasKey(udf => new { udf.UserId, udf.FilmId });

        builder.HasOne(udf => udf.User)
            .WithMany(u => u.DislikedFilms)
            .HasForeignKey(udf => udf.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(udf => udf.Film)
            .WithMany(f => f.DislikedByUsers)
            .HasForeignKey(udf => udf.FilmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}