using FilmMatch.Application.Contracts.Responses.GetRecommendations;
using FilmMatch.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FilmMatch.Application.Features.Films.GetRecommendations
{
    public class GetRecommendationsQueryHandler : IRequestHandler<GetRecommendationsQuery, GetRecommendationsResponse>
    {
        private readonly IUserContext _userContext;
        private readonly IDbContext _dbContext;

        public GetRecommendationsQueryHandler(IUserContext userContext, IDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }

        public async Task<GetRecommendationsResponse> Handle(GetRecommendationsQuery request, CancellationToken cancellationToken)
        {
            var userId = _userContext.GetUserId();
            var likedCategories = _dbContext.UserLikedFilm.Where(x => x.UserId == userId)
                .Select(x => x.Film!.CategoryId).Distinct().Take(3);

            if (!likedCategories.Any())
            {
                return new GetRecommendationsResponse
                {
                    IsEverythingReacted = false
                };
            }

            var recommendations = await _dbContext.Films
                .Where(x => likedCategories.Contains(x.CategoryId)
                && !_dbContext.UserLikedFilm
                    .Where(z => z.UserId == userId)
                    .Select(y => y.FilmId)
                    .Contains(x.Id)
                && !_dbContext.UserDislikedFilm
                    .Where(z => z.UserId == userId)
                    .Select(y => y.FilmId)
                    .Contains(x.Id))
                .Select(x => new GetRecommendationsResponseItem
                {
                    Id = x.Id,
                    Description = x.LongDescription,
                    Title = x.Title,
                    Image = x.ImageUrl,
                    Category = x.Category!.Name,
                })
                .ToListAsync(cancellationToken: cancellationToken);
            
            var response = new GetRecommendationsResponse(recommendations);
            if(!recommendations.Any())
                response.IsEverythingReacted = true;
            
            return response;
        }
    }
}