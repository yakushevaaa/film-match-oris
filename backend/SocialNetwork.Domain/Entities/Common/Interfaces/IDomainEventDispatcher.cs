using Core.Entities.Common;

namespace FilmMatch.Domain.Entities.Common.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<BaseEntity> entitiesWithEvents);
}