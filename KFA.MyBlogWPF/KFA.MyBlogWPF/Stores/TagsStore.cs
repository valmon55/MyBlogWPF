using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Stores
{
    public class TagsStore
    {
        public event Action<Tag> TagAdded;
        public event Action<Tag> TagUpdated;
        public event Action<int> TagDeleted;
        public async Task Add(Tag tag)
        {
            TagAdded?.Invoke(tag);
        }
        public async Task Update(Tag tag)
        {
            TagUpdated?.Invoke(tag);
        }

        public async Task Delete(int id)
        {
            TagDeleted?.Invoke(id);
        }
    }
}
