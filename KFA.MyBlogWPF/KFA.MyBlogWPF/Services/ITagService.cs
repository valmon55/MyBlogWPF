using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public interface ITagService
    {
        Task<bool> AddTagAsync(string name);
        Task<IReadOnlyList<Tag>> GetAllTagAsync();
        Task<bool> UpdateTagAsync(int id, string name);
        Task<bool> DeleteTagAsync(string endpoint, Tag tag);
    }
}
