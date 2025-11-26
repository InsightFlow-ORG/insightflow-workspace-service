using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Shared
{
    public class Result<T>
    {
        public bool IsSuccess { get; }
        public T? Data { get; }
        public string ErrorMessage { get; }
        public int StatusCode { get; }

        private Result(bool isSuccess, T? data, string errorMessage, int statusCode)
        {
            IsSuccess = isSuccess;
            Data = data;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        // Métodos estáticos para crear resultados
        public static Result<T> Success(T data) 
            => new(true, data, string.Empty, 200);

        public static Result<T> NotFound(string message) 
            => new(false, default, message, 404);

        public static Result<T> Conflict(string message) 
            => new(false, default, message, 409);

        public static Result<T> BadRequest(string message) 
            => new(false, default, message, 400);
    }
}