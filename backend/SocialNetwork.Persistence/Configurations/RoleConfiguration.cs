using FilmMatch.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmMatch.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<Guid>> builder)
    {
        builder.HasData(
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("1a1a1a1a-1a1a-1a1a-1a1a-1a1a1a1a1a1a"),
                Name = RoleConstants.God,
                NormalizedName = RoleConstants.God.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("2b2b2b2b-2b2b-2b2b-2b2b-2b2b2b2b2b2b"),
                Name = RoleConstants.Admin,
                NormalizedName = RoleConstants.Admin.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"),
                Name = RoleConstants.User,
                NormalizedName = RoleConstants.User.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("4d4d4d4d-4d4d-4d4d-4d4d-4d4d4d4d4d4d"),
                Name = RoleConstants.Blocked,
                NormalizedName = RoleConstants.Blocked.ToUpper()
            }
        );
    }
}