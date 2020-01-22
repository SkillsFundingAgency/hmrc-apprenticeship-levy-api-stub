using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetFractionsQuery : IQuery<GetFractionsRequest, GetFractionsResponse>
    {
        private readonly IFractionsRepository _fractionsRepository;

        public GetFractionsQuery(IFractionsRepository fractionsRepository)
        {
            _fractionsRepository = fractionsRepository ?? throw new ArgumentException("fractionsRepository cannot be null");
        }

        public async Task<GetFractionsResponse> Get(GetFractionsRequest request)
        {
            var result = await _fractionsRepository.GetByEmpRef(request.EmpRef, request.FromDate, request.ToDate);

            if (result == null)
            {
                return null;
            }

            var retVal = new GetFractionsResponse();
            retVal.Fraction = result;

            return retVal;
        }
    }
}
