using KFA.MyBlogWPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticlesAsync();
        Task<bool> UpdateArticleAsync(User user);
        Task<bool> DeleteArticleAsync(User user);
    }
}
