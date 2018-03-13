using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CzSharp.Model.Entities;
using CzSharp.Model.Repositories;

namespace CzSharp.Services
{
    public class TagsService: ITagsService
    {
        private ITagsRepository tagsRepository;
        
        public TagsService(ITagsRepository tagsRepository)
        {
            this.tagsRepository = tagsRepository;
        }

        /// <summary>
        /// Parses tags given from tags input
        /// </summary>
        /// <param name="tagsStr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Tag>> ParseTags(string tagsStr)
        {
            Console.WriteLine(tagsStr);
            var tagsArr = tagsStr.Split(",").Select(t => t.Trim());
            var tags = new List<Tag>();

            foreach (var tagStr in tagsArr)
            {
                Console.WriteLine(tagStr);
                tags.Add(await FindOrCreate(tagStr));
            }

            return tags;
        }

        /// <summary>
        /// Finds already created tag or creates new one
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public async Task<Tag> FindOrCreate(string title)
        {
            var tag = await tagsRepository.FindByTitleAsync(title);
            Console.WriteLine($"Found tag: {tag}");
            if (tag == null)
            {
                Console.WriteLine("Tag is null");
                tag = new Tag
                {
                    Title = title
                };
                await tagsRepository.CreateAsync(tag);
            }
            
            Console.WriteLine($"Tag id is {tag.Id}");

            return tag;
        }
    }
}