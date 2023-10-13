using System.Collections.Generic;
using System.Linq;
using Mapster;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Team.Extensions.Infrastrcture.Enums;

namespace Team.Extensions.Infrastrcture
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
        public ApiResultCode ResultCode { get; }

        /// <summary>
        /// List of error/service info messages
        /// </summary>
        public List<DetailedMessage> Messages { get; }

        /// <summary>
        /// Object to return in an operation
        /// </summary>
        public T Entity { get; }

        public ApiResult(ApiResultCode code, List<DetailedMessage> messages, T entity)
        {
            (ResultCode, Messages, Entity) = (code, messages, entity);
        }

        public bool IsSuccess => ResultCode == ApiResultCode.Ok;

        public ApiResult<U> ToApiResult<U>() where U : class
        {
            return new ApiResult<U>(ResultCode, Messages, Entity?.Adapt<U>());
        }

        public override string ToString()
        {
            return string.Join('\n', Messages.SelectMany(x => x.Messages.Select(xx => $"{x.Object}: {xx.Text}")));
        }

        #region OPERATORS

        public static bool operator true(ApiResult<T> r)
        {
            return r.ResultCode == ApiResultCode.Ok;
        }

        public static bool operator false(ApiResult<T> r)
        {
            return r.ResultCode != ApiResultCode.Ok;
        }

        public static bool operator !(ApiResult<T> r)
        {
            return r.ResultCode != ApiResultCode.Ok;
        }

        #endregion

        #region CREATION_METHODS

        public static readonly ApiResult<T> SuccessEmpty = new ApiResult<T>(ApiResultCode.Ok, null, null);

        public static ApiResult<T> Success(T entity)
        {
            return new ApiResult<T>(ApiResultCode.Ok, null, entity);
        }

        public static ApiResult<T> Success(string message)
        {
            return new ApiResult<T>(
                ApiResultCode.Ok,
                new List<DetailedMessage> {DetailedMessage.FromSuccess(message, typeof(T).Name)},
                null);
        }

        public static ApiResult<T> Error(string message, string objectName = null)
        {
            return new ApiResult<T>(
                ApiResultCode.Error,
                new List<DetailedMessage> {DetailedMessage.FromError(message, objectName ?? typeof(T).ToString())},
                null);
        }

        public static ApiResult<T> Error(DetailedMessage message)
        {
            return new ApiResult<T>(
                ApiResultCode.Error,
                new List<DetailedMessage> {message},
                null);
        }

        public static ApiResult<T> Error(List<DetailedMessage> messages)
        {
            return new ApiResult<T>(
                ApiResultCode.Error,
                messages,
                null);
        }

        public static ApiResult<T> NotFound(string message = "Record not found", string objectName = null)
        {
            return new ApiResult<T>(
                ApiResultCode.NotFound,
                new List<DetailedMessage> {DetailedMessage.FromError(message, objectName ?? typeof(T).Name)},
                null);
        }

        public static ApiResult<T> NotFound(DetailedMessage message)
        {
            return new ApiResult<T>(
                ApiResultCode.NotFound,
                new List<DetailedMessage> {message},
                null);
        }

        public static ApiResult<T> NotFound(List<DetailedMessage> messages)
        {
            return new ApiResult<T>(
                ApiResultCode.NotFound,
                messages,
                null);
        }

        public static ApiResult<T> FromModelState(ModelStateDictionary modelState)
        {
            return Error(
                modelState.SelectMany(kvp => kvp
                        .Value
                        .Errors
                        .Select(x => DetailedMessage.FromError(x.ErrorMessage, kvp.Key)))
                    .ToList());
        }

        #endregion
    }
}