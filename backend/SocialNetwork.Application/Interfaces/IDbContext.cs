using FilmMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FilmMatch.Application.Interfaces ;

    public interface IDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Category> Categories { get; }
        DbSet<Film> Films { get; }
        
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
        public DatabaseFacade Database { get; }
    }