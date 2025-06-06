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
                        Id = 1,
                        Name = "Боевик",
                        ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x",
                        ImageAlt = "Боевик"
                    },
                    new Category
                    {
                        Id = 2,
                        Name = "Детектив",
                        ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x",
                        ImageAlt = "Детектив"
                    },
                    new Category
                    {
                        Id = 3,
                        Name = "Комедия",
                        ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x",
                        ImageAlt = "Комедия"
                    },
                    new Category
                    {
                        Id = 4,
                        Name = "Мелодрама",
                        ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x",
                        ImageAlt = "Мелодрама"
                    },
                    new Category
                    {
                        Id = 5,
                        Name = "Трагедия",
                        ImageUrl = "https://avatars.mds.yandex.net/get-kinopoisk-image/1773646/3e7efab5-e108-4de0-99b6-8196d6cf7c21/1920x",
                        ImageAlt = "Трагедия"
                    }
                );
            }
        }
   
}
