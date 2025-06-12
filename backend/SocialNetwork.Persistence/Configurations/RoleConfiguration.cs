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
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = RoleConstants.User,
                NormalizedName = RoleConstants.User.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = RoleConstants.Admin,
                NormalizedName = RoleConstants.Admin.ToUpper()
            },
            new IdentityRole<Guid>
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = RoleConstants.God,
                NormalizedName = RoleConstants.God.ToUpper()
            }
        );
    }
}