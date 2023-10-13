using System;
using System.Threading.Tasks;
using Sample.DAL.Enums;
using Sample.DAL.Models;
using Sample.Extensions.Infrastrcture;

namespace Sample.Business.Services
{
    public interface IOperationService
    {
        Task<ApiResult<Operation>> AddOperationEntry(Guid balanceId, OperationType type, decimal balanceChange, string message);
        Task<ApiResult<CollectionOutputModel<OperationOutputDTO>>> GetPageAsync(PageModel pageModel, bool descending);
    }
}