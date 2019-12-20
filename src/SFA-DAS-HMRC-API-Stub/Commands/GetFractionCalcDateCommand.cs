using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetFractionCalcDateCommand : ICommand<GetFractionCalcDateRequest, GetFractionCalcDateResponse>
    {
        private readonly IFractionsCalcDateRepository _fractionsRepository;

        public GetFractionCalcDateCommand(IFractionsCalcDateRepository fractionsRepository)
        {
            _fractionsRepository = fractionsRepository ?? throw new ArgumentException("fractionsRepository cannot be null");
        }

        public async Task<GetFractionCalcDateResponse> Get(GetFractionCalcDateRequest request)
        {
            var result = await _fractionsRepository.GetLastCalcDate();

            if (result == null)
            {
                return null;
            }

            var retVal = new GetFractionCalcDateResponse();
            retVal.LastCalculationDate = result.LastCalculationDate;

            return retVal;
        }
    }
}
