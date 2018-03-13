using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CzSharp.Model.Entities;

namespace CzSharp.Model.Repositories
{
    public interface ITaggableRepository<T, TR>: IRepository<T> where T: ITaggable<TR>
    {
        /// <summary>
        /// Task that was solving problem with manyToMany creations - resolved now
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tags"></param>
        /// <returns></returns>
        [Obsolete("CreateAsync works ok now")]
        Task CreateWithTagsAsync(T entity, List<Tag> tags);
    }
}