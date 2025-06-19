using System.Linq.Expressions;
using FilmMatch.Application.Interfaces;
using FilmMatch.Domain.Entities;
using FilmMatch.Domain.Entities.Common;
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
        public DbSet<FriendRequest> FriendRequests { get; set; }

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
            
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType);
                    var body = Expression.Equal(
                        Expression.Property(parameter, nameof(BaseAuditableEntity.IsDeleted)),
                        Expression.Constant(false)
                        );

                    var lambda = Expression.Lambda(body, parameter);

                    modelBuilder.Entity(entityType.ClrType)
                        .HasQueryFilter(lambda);
                }
            }
        }
        
    }
}