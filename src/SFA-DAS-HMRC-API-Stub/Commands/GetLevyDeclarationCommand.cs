using SFA.DAS.HMRC.API.Stub.Data.Repositories;
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
            _levyDeclarationRepository = levyDeclarationRepository ?? throw new ArgumentException("LevyDeclaration cannot be null");

        }

        public Task<GetLevyDeclarationResponse> Get(GetLevyDeclarationRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
