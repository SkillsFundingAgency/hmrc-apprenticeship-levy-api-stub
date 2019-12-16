using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetFractionsCommand : ICommand<GetFractionsRequest, GetFractionsResponse>
    {
        private readonly IFractionsRepository _fractionsRepository;

        public GetFractionsCommand(IFractionsRepository fractionsRepository)
        {
            _fractionsRepository = fractionsRepository ?? throw new ArgumentException("FractionsRepository cannot be null");
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
