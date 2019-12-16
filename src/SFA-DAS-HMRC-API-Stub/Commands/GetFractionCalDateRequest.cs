using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetFractionCalDateRequest
    {
        public DateTime LastCalculationDate;

        public GetFractionCalDateRequest(DateTime lastCalculationDate)
        {
            this.LastCalculationDate = lastCalculationDate;
        }
    }
}
