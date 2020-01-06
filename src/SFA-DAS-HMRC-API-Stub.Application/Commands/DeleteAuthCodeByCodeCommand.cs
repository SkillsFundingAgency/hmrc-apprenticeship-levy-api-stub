using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class DeleteAuthCodeByCodeCommand : ICommand<DeleteAuthCodeByCodeRequest, DeleteAuthCodeByCodeResponse>
    {
        private readonly IAuthCodeRepository _authCodeRepository;

        public DeleteAuthCodeByCodeCommand(IAuthCodeRepository authCodeRepository)
        {
            _authCodeRepository = authCodeRepository;
        }

        public async Task<DeleteAuthCodeByCodeResponse> Execute(DeleteAuthCodeByCodeRequest request)
        {
            var result = await _authCodeRepository.DeleteByCode(request.Code);

            return new DeleteAuthCodeByCodeResponse
            {
                Success = result
            };
        }
    }
}
