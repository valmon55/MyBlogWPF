using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public interface IApiClient
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<HttpResponseMessage> PostAsync<T>(string endpoint, T data);
        Task<HttpResponseMessage> PutAsync<T>(string endpoint, T data);
        Task<HttpResponseMessage> DeleteAsync<T>(string endpoint);
    }
}
