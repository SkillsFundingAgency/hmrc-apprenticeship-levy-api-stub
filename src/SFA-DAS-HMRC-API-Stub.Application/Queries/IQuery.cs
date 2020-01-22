using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public interface IQuery<TIn, TOut>
    {
        Task<TOut> Get(TIn request);
    }
}