using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FilmMatch.Domain.Entities;

namespace FilmMatch.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    Id = Guid.Parse("d0bfe76e-0f12-4fcd-94aa-3be4f90d79e1"),
                    Name = "Фантастика",
                    ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x"
                },
                new Category
                {
                    Id = Guid.Parse("31f80f2a-9426-41e2-93f7-7f12180722a1"),
                    Name = "Триллер",
                    ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x"
                },
                new Category
                {
                    Id = Guid.Parse("5dbd7a97-f0a1-4f4e-91c7-244cbab17eec"),
                    Name = "Комедия",
                    ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x"
                },
                new Category
                {
                    Id = Guid.Parse("4b4974f1-8ea2-43f1-998f-d3a1cfb9d1c3"),
                    Name = "Драма",
                    ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x"
                },
                new Category
                {
                    Id = Guid.Parse("0b27972c-b3df-4ae4-9138-2e90c749d139"),
                    Name = "Боевик",
                    ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/4771096/2a0000017e39d1cfcb48b5f4fe5a81e8b9f4/1920x"
                }
            );
        }
    }

}
