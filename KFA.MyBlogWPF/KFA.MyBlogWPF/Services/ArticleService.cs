using KFA.MyBlogWPF.Models;
using KFA.MyBlogWPF.Services.DTOs;
using KFA.MyBlogWPF.Stores;
using System;
using System.Collections.Generic;
using System.Data;
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

        public async Task<bool> AddArticleAsync(Article article)
        {
            if (string.IsNullOrWhiteSpace(article.Title) || string.IsNullOrWhiteSpace(article.Content))
                Debug.WriteLine("❌ Заголовок и текст статьи не должно быть пустым");

            const string endpoint = "Article/AddArticle";
            try
            {
                var request = new AddArticleRequest()
                {
                    Title = article.Title.Trim(),
                    Content = article.Content.Trim(),
                    ArticleDate = DateTime.Now.ToString("O"),
                    Tags = article.Tags
                };

                var response = await _apiClient.PostAsync<AddArticleRequest, ArticleResponse>(endpoint, request);
                if (response.IsSuccess)
                {
                    Debug.WriteLine($"✅ Статья {request.Title} успешно добавлена");

                    await _articlesStore.Add(article);
                    return true;
                }
                Debug.WriteLine($"Ошибка при добавлении статьи {article.Title}");
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"❌ Исключение при добавлении статьи: {article.Title}");
            }
        }

        public Task<bool> DeleteArticleAsync(Article article)
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

        public Task<bool> UpdateArticleAsync(Article article)
        {
            throw new NotImplementedException();
        }
    }
}
