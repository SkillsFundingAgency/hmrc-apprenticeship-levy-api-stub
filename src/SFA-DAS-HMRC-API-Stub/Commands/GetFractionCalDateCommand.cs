using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetFractionCalDateCommand : ICommand<GetFractionCalDateRequest, GetFractionCalDateResponse>
    {
        private readonly IFractionCalDateRepository _fractionCalDateRepository;

        public GetFractionCalDateCommand(IFractionCalDateRepository fractionCalDateRepository)
        {
            _fractionCalDateRepository = fractionCalDateRepository ?? throw new ArgumentException("FractionCalDateRepository cannot be null");
        }

        public async Task<GetFractionCalDateResponse> Get(GetFractionCalDateRequest request)
        {
            var result = await _fractionCalDateRepository.GetByLastCalDate(request.LastCalculationDate);

            if (result == null)
            {
                return null;
            }

            var retVal = new GetFractionCalDateResponse();
            retVal.LastCalculationDate = result;

            return retVal;
        }
    }
}
