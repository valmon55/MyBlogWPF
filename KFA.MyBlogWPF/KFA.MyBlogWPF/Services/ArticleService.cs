using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IApiClient _apiClient;
        private readonly ArticlesStore _articlesStore;

        public ArticleService(IApiClient apiClient, ArticlesStore articlesStore)
        {
            _apiClient = apiClient;
            _articlesStore = articlesStore;
        }

        public Task<bool> DeleteArticleAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Article>> GetAllArticlesAsync()
        {
            Debug.WriteLine($"⏱️ Запрос всех статей");
            var articlesRequest = await _apiClient.GetAsync<List<ArticlesRequest>>("Article/AllArticles");
            Debug.WriteLine($"⏱️ Получено {articlesRequest?.Count ?? 0} статей");
            var articles = articlesRequest.Select(x => new Article()
            {
                Id = x.Id,
                UserId = x.AuthorId,
                ArticleDate = x.ArticleDate,
                Title = x.Title,
                Content = x.Content,
                Tags = x.Tags
            }).ToList();
            return articles;
        }

        public Task<bool> UpdateArticleAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
