
using BestPracticesDemo.Domain.Entities;

namespace BestPracticesDemo.Application.Contracts.Persistence
{
    public interface ICategoryRepository: IAsyncRepository<Category>
    {
        Task<List<Category>> GetCategoriesWithEvents(bool includePastEvents);

    }
}
