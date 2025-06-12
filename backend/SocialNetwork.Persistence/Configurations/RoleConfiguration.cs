using FilmMatch.Domain.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmMatch.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData(
            new IdentityRole
            {
                Id = "1",
                Name = RoleConstants.User,
                NormalizedName = RoleConstants.User.ToUpper()
            },
            new IdentityRole
            {
                Id = "2",
                Name = RoleConstants.Admin,
                NormalizedName = RoleConstants.Admin.ToUpper()
            },
            new IdentityRole
            {
                Id = "3",
                Name = RoleConstants.God,
                NormalizedName = RoleConstants.God.ToUpper()
            });
    }
}