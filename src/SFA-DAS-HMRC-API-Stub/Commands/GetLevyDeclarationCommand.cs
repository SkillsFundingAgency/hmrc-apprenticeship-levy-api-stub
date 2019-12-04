using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Commands
{
    public class GetLevyDeclarationCommand : ICommand<GetLevyDeclarationRequest, GetLevyDeclarationResponse>
    {
        private readonly ILevyDeclarationRepository _levyDeclarationRepository;

        public GetLevyDeclarationCommand(ILevyDeclarationRepository levyDeclarationRepository)
        {
            _levyDeclarationRepository = levyDeclarationRepository ?? throw new ArgumentException("LevyDeclarationRepository cannot be null");
        }

        public async Task<GetLevyDeclarationResponse> Get(GetLevyDeclarationRequest request)
        {
            var result = await _levyDeclarationRepository.GetByEmpRef(request.EmpRef, request.FromDate, request.ToDate);

            if (result == null)
            {
                return null;
            }

            var retVal = new GetLevyDeclarationResponse();
            retVal.Declarations = result;

            return retVal;
        }
    }
}
