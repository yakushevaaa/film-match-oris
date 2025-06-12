using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Persistence.Configurations;

public class UserLikedFilmConfiguration : IEntityTypeConfiguration<UserLikedFilm>
{
    public void Configure(EntityTypeBuilder<UserLikedFilm> builder)
    {
        builder.HasKey(ulf => new { ulf.UserId, ulf.FilmId });

        builder.HasOne(ulf => ulf.User)
            .WithMany(u => u.LikedFilms)
            .HasForeignKey(ulf => ulf.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ulf => ulf.Film)
            .WithMany(f => f.LikedByUsers)
            .HasForeignKey(ulf => ulf.FilmId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}