using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public interface ICommand<TIn, TOut>
    {
        Task<TOut> Get(TIn request);
    }
}