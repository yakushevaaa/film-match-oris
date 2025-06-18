using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FilmMatch.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>,
        IDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<UserLikedFilm> UserLikedFilm { get; set; }
        public DbSet<UserDislikedFilm> UserDislikedFilm { get; set; }
        public DbSet<UserBookmarkedFilm> UserBookmarkedFilm { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new FilmConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserBookmarkedConfiguration());
            modelBuilder.ApplyConfiguration(new UserDislikedFilmConfiguration());
            modelBuilder.ApplyConfiguration(new UserLikedFilmConfiguration());
            modelBuilder.ApplyConfiguration(new UserFriendConfiguration());
        }
        
    }
}