using FilmMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FilmMatch.Application.Interfaces;

public interface IDbContext
{
    DbSet<User> Users { get; }
    DbSet<Category> Categories { get; }
    DbSet<Film> Films { get; }
    DbSet<UserLikedFilm> UserLikedFilm { get; }
    DbSet<UserDislikedFilm> UserDislikedFilm { get; }
    DbSet<UserBookmarkedFilm> UserBookmarkedFilm { get; }
    DbSet<UserFriend> UserFriends { get; }
    DbSet<FriendRequest> FriendRequests { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    DatabaseFacade Database { get; }

    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
}