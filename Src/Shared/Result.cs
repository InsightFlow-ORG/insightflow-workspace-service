using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace insightflow_workspace_service.Src.Shared
{
    /// <summary>
    /// Clase genérica para representar el resultado de una operación
    /// </summary>
    /// <typeparam name="T">Tipo de dato que representa el resultado de la operación.</typeparam>
    public class Result<T>
    {
        /// <summary>
        /// Indica si la operación fue exitosa.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Datos resultantes de la operación.
        /// </summary>
        public T? Data { get; }

        /// <summary>
        /// Mensaje de error en caso de que la operación falle.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Código de estado HTTP asociado al resultado.
        /// </summary>
        public int StatusCode { get; }

        /// <summary>
        /// Constructor privado para inicializar una instancia de Result.
        /// </summary>
        /// <param name="isSuccess">Indica si la operación fue exitosa.</param>
        /// <param name="data">Datos resultantes de la operación.</param>
        /// <param name="errorMessage">Mensaje de error en caso de que la operación falle.</param>
        /// <param name="statusCode">Código de estado HTTP asociado al resultado.</param>
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