using System.Collections.Generic;
using System.Linq;
using System.Net;
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Sample.Extensions.Infrastrcture
{
    /// <summary>
    /// API operation result.
    /// May contain object, list of detailed error/service info messages, operation status.
    /// Can be used as controller method return type, or in some service operations.
    /// </summary>
    public class ApiResult<T> where T : class
    {
        /// <summary>
        /// Operation status
        /// </summary>
        public HttpStatusCode ResultCode { get; }

        /// <summary>
        /// Error/service info message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Operation result
        /// </summary>
        public T Entity { get; }

        public bool IsSuccess => ResultCode == HttpStatusCode.OK;

        public ApiResult(HttpStatusCode code, string message, T entity)
        {
            (ResultCode, Message, Entity) = (code, message, entity);
        }

        public ApiResult<U> ToApiResult<U>() where U : class
        {
            return new ApiResult<U>(ResultCode, Message, Entity?.Adapt<U>());
        }

        #region OPERATORS

        public static bool operator true(ApiResult<T> r)
        {
            return r.IsSuccess;
        }

        public static bool operator false(ApiResult<T> r)
        {
            return !r.IsSuccess;
        }

        public static bool operator !(ApiResult<T> r)
        {
            return !r.IsSuccess;
        }

        #endregion

        #region CREATION_METHODS

        public static readonly ApiResult<T> SuccessEmpty = new ApiResult<T>(HttpStatusCode.OK, null, null);

        public static ApiResult<T> Success(T entity)
        {
            return new ApiResult<T>(HttpStatusCode.OK, null, entity);
        }

        public static ApiResult<T> Success(string message)
        {
            return new ApiResult<T>(
                HttpStatusCode.OK,
                message,
                null);
        }

        public static ApiResult<T> BadRequest(string message)
        {
            return new ApiResult<T>(
                HttpStatusCode.BadRequest,
                message,
                null);
        }

        public static ApiResult<T> NotFound(string message = "Record not found")
        {
            return new ApiResult<T>(
                HttpStatusCode.NotFound,
                message,
                null);
        }

        public static ApiResult<T> InternalError(string message)
        {
            return new ApiResult<T>(
                HttpStatusCode.InternalServerError,
                message,
                null);
        }


        #endregion
    }
}