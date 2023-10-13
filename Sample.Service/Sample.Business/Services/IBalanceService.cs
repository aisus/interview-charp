using System.Threading.Tasks;
using Sample.DAL.Models;
using Sample.Extensions.Infrastrcture;

namespace Sample.Business.Services
{
    public interface IBalanceService
    {
        Task<ApiResult<Balance>> GetBalance();
        Task<ApiResult<Balance>> Deposit(decimal amount);
        Task<ApiResult<Balance>> Withdraw(decimal amount);
        Task<ApiResult<Balance>> Take(decimal amount);
        Task<ApiResult<Balance>> Check(decimal amount);
        Task<ApiResult<Balance>> Add(decimal amount);
    }
}