using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Queries
{
    public class GetLevyDeclarationQuery : IQuery<GetLevyDeclarationRequest, GetLevyDeclarationResponse>
    {
        private readonly ILevyDeclarationRepository _levyDeclarationRepository;

        public GetLevyDeclarationQuery(ILevyDeclarationRepository levyDeclarationRepository)
        {
            _levyDeclarationRepository = levyDeclarationRepository ?? throw new ArgumentException("LevyDeclarationRepository cannot be null");
        }

        public async Task<GetLevyDeclarationResponse> Get(GetLevyDeclarationRequest request)
        {
            var result = await _levyDeclarationRepository.GetByEmpRef(request.EmpRef, request.FromDate, request.ToDate);

            if (result == null || !result.Declarations.Any())
            {
                return null;
            }

            var retVal = new GetLevyDeclarationResponse();
            retVal.Declarations = result;

            return retVal;
        }
    }
}
