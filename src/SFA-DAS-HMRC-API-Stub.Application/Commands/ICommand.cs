using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public interface ICommand<TIn, TOut>
    {
        Task<TOut> Execute(TIn request);
    }
}