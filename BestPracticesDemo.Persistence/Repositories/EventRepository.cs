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
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(BestPracticesDbContext dbContext): base(dbContext)
        {
        }

        public Task<bool> IsEventNameAndDateUnique(string name, DateTime date)
        {
            var matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(date.Date));
            return Task.FromResult(matches);
        }
    }
}
