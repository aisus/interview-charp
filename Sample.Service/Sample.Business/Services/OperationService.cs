using System;
using System.Threading.Tasks;
using Sample.DAL.Enums;
using Sample.DAL.Models;
using Sample.Extensions.DAL;
using Sample.Extensions.Infrastrcture;

namespace Sample.Business.Services
{
    public class OperationService : IOperationService
    {
        private readonly IAsyncCrudService<Operation> service;

        public OperationService(IAsyncCrudService<Operation> service)
        {
            this.service = service;
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
    }
}