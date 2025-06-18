using FilmMatch.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FilmMatch.Application.Contracts.Responses.GetRecommendations
{
    public class GetRecommendationsResponseItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string Category { get; set; } = default!;
    }
}