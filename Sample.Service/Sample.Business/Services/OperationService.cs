using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sample.DAL.Enums;
using Sample.DAL.Models;
using Sample.Extensions;
using Sample.Extensions.DAL;
using Sample.Extensions.Infrastrcture;

namespace Sample.Business.Services
{
    public class OperationService : IOperationService
    {
        private readonly IAsyncCrudService<Operation> service;
        private readonly IAsyncOrderedQueryService<Operation> queryService;
        private readonly IHttpContextAccessor httpContextAccessor;
        
        public OperationService(IAsyncCrudService<Operation> service, IAsyncOrderedQueryService<Operation> queryService, IHttpContextAccessor httpContextAccessor)
        {
            this.service = service;
            this.queryService = queryService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<Operation>> AddOperationEntry(Guid balanceId, OperationType type, decimal balanceChange, string message)
        {
            var entry = new Operation
            {
                BalanceId = balanceId, 
                BalanceChange = balanceChange,
                Message = message,
                Type = type
            };

            return await service.CreateAsync(entry);
        }

        public async Task<ApiResult<CollectionOutputModel<OperationOutputDTO>>> GetPageAsync(PageModel pageModel, bool descending)
        {
            return await queryService.GetPageAsync<OperationOutputDTO, DateTimeOffset?>(pageModel, 
                x => x.Balance.UserId == httpContextAccessor.HttpContext.User.GetLoggedInUserId(), x => x.CreatedDate, descending);
        }
    }
}