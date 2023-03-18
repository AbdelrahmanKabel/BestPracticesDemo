using BestPracticesDemo.Application.Contracts.Persistence;
using BestPracticesDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestPracticesDemo.Persistence.Repositories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(BestPracticesDbContext dbContext): base(dbContext)
        {
        }

        public async Task<List<Category>> GetCategoriesWithEvents(bool includePastEvents)
        {
            var categories = await _dbContext.Categories.Include(c => c.Events).ToListAsync();
            if (!includePastEvents)
            {
                categories.ForEach(c => c.Events.ToList().RemoveAll(e => e.Date < DateTime.Today));
            }
            return categories;
        }
    }
}
