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
        Task<List<Tag>> GetAllTagAsync();
        Task<bool> UpdateTagAsync(Tag tag);
        Task<bool> DeleteTagAsync(Tag tag);
    }
}
