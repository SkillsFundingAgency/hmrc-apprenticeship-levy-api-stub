using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class UpdateAuthRecordCommand : ICommand<UpdateAuthRecordRequest, UpdateAuthRecordResponse>
    {
        private readonly IAuthRecordRepository _authRecordRepository;

        public UpdateAuthRecordCommand(IAuthRecordRepository authRecordRepository)
        {
            _authRecordRepository = authRecordRepository;
        }

        public async Task<UpdateAuthRecordResponse> Execute(UpdateAuthRecordRequest request)
        {
            await _authRecordRepository.Update(request.RefreshToken, request.AuthRecord);

            return new UpdateAuthRecordResponse();
        }
    }
}
