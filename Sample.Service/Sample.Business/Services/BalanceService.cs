using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sample.DAL.Models;
using Sample.Extensions;
using Sample.Extensions.DAL;
using Sample.Extensions.Infrastrcture;

namespace Sample.Business.Services
{
    public class BalanceService : IBalanceService
    {
        private readonly IAsyncCrudService<Balance> service;
        private readonly IOperationService operationService;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BalanceService(IAsyncCrudService<Balance> service, IOperationService operationService, IHttpContextAccessor httpContextAccessor)
        {
            this.service = service;
            this.operationService = operationService;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<ApiResult<Balance>> Deposit(decimal amount)
        {
            var result = await Add(amount);
            
            if (result)
            {
                await operationService.AddOperationEntry(result.Entity.Id, DAL.Enums.OperationType.Deposit, amount, "Deposit");
            }

            return result;
        }

        public async Task<ApiResult<Balance>> GetBalance()
        {
            var result = await service.FindAsync(x => x.UserId == httpContextAccessor.HttpContext.User.GetLoggedInUserId()); 

            if (!result)
            {
                return await CreateDefaultBalance();
            }

            return result;
        }

        public async Task<ApiResult<Balance>> Withdraw(decimal amount)
        {
            var result = await Take(amount);

            if (result)
            {
                await operationService.AddOperationEntry(result.Entity.Id, DAL.Enums.OperationType.Withdrawal, -amount, "Withdrawal");
            }

            return result;
        }

        public async Task<ApiResult<Balance>> Take(decimal amount)
        {
            var balance = (await GetBalance()).Entity;

            if (amount > balance.CurrentBalance)
            {
                return ApiResult<Balance>.BadRequest("Insufficient balance");
            }

            balance.CurrentBalance -= amount;

            return await service.UpdateAsync(balance.Id, balance);
        }

        public async Task<ApiResult<Balance>> Check(decimal amount)
        {
            var balance = (await GetBalance()).Entity;

            if (amount > balance.CurrentBalance)
            {
                return ApiResult<Balance>.BadRequest("Insufficient balance");
            }

            return ApiResult<Balance>.Success(balance);
        }

        public async Task<ApiResult<Balance>> Add(decimal amount)
        {
            var balance = (await GetBalance()).Entity;

            balance.CurrentBalance += amount;

            return await service.UpdateAsync(balance.Id, balance);
        }
        
        private async Task<ApiResult<Balance>> CreateDefaultBalance()
        {
            var startingBalance = 10000m;
            var defaultBalance = new Balance
            {
                UserId = httpContextAccessor.HttpContext.User.GetLoggedInUserId(),
                CurrentBalance = startingBalance
            };

            var result = await service.CreateAsync(defaultBalance);

            if (result)
            {
                await operationService.AddOperationEntry(result.Entity.Id, DAL.Enums.OperationType.Deposit, startingBalance, "Starting balance");
            }

            return result;
        }
    }
}