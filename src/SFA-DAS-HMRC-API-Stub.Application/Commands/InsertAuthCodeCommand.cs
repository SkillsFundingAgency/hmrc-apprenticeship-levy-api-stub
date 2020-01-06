using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class InsertAuthCodeCommand : ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse>
    {
        private readonly IAuthCodeRepository _authCodeRepository;

        public InsertAuthCodeCommand(IAuthCodeRepository authCodeRepository)
        {
            _authCodeRepository = authCodeRepository;
        }

        public async Task<InsertAuthCodeResponse> Execute(InsertAuthCodeRequest request)
        {
            await _authCodeRepository.Insert(request.AuthCode);

            return new InsertAuthCodeResponse()
            {
                Success = true
            };
        }
    }
}
