using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KFA.MyBlogWPF.Services.DTOs
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public ApiError? Error { get; set; }

        public static ApiResponse<T> Success(T data) => new() { IsSuccess = true, Data = data };
        public static ApiResponse<T> Failure(ApiError error) => new() { IsSuccess = false, Error = error };
    }
}
