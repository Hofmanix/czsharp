using System;
using System.Collections.Generic;
using System.Linq;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public interface IEventsRepository: ITaggableRepository<Event, EventTag>
    {
        IQueryable<Event> GetRange(DateTime from, DateTime to);
    }
}