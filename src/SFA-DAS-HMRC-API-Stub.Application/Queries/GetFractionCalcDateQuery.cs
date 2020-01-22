using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetFractionCalcDateQuery : IQuery<GetFractionCalcDateRequest, GetFractionCalcDateResponse>
    {
        private readonly IFractionsCalcDateRepository _fractionsRepository;

        public GetFractionCalcDateQuery(IFractionsCalcDateRepository fractionsRepository)
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
