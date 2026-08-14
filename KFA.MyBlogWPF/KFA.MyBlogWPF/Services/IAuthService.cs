using KFA.MyBlogWPF.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services
{
    public interface IAuthService
    {
        Task<ApiResponse<bool>> LoginAsync(LoginRequest loginRequest);
        Task<bool> RegisterAsync(RegisterRequest registerRequest);
        Task<bool> LogoutAsync();
    }
}
