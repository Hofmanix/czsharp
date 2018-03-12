using System;
using System.Collections.Generic;
using System.Linq;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public class EventsRepository: TaggableRepository<Event, EventTag>, IEventsRepository
    {
        public EventsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Event> GetRange(DateTime from, DateTime to)
        {
            return FindAll().Where(e => (e.From >= from && e.From <= to) || (e.To >= from && e.To <= to));
        }
    }
}