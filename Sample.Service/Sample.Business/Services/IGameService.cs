using System.Threading.Tasks;
using Sample.Business.DTO;
using Sample.Extensions.Infrastrcture;

namespace Sample.Business.Services
{
    public interface IGameService
    {
        Task<ApiResult<GameOutputDTO>> Play(GameInputDTO input);
    }
}