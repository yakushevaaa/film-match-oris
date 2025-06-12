using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Persistence.Configurations
{
    public class UserBookmarkedConfiguration : IEntityTypeConfiguration<UserBookmarkedFilm>
    {
        public void Configure(EntityTypeBuilder<UserBookmarkedFilm> builder)
        {
            builder.HasKey(ubf => new { ubf.UserId, ubf.FilmId });

            builder.HasOne(ubf => ubf.User)
                .WithMany(u => u.BookmarkedFilms)
                .HasForeignKey(ubf => ubf.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ubf => ubf.Film)
                .WithMany(f => f.BookmarkedByUsers)
                .HasForeignKey(ubf => ubf.FilmId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
