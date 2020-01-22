using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class InsertAuthRecordCommand : ICommand<InsertAuthRecordRequest, InsertAuthRecordResponse>
    {
        private readonly IAuthRecordRepository _authRecordRepository;

        public InsertAuthRecordCommand(IAuthRecordRepository authRecordRepository)
        {
            _authRecordRepository = authRecordRepository;
        }

        public async Task<InsertAuthRecordResponse> Execute(InsertAuthRecordRequest request)
        {
            await _authRecordRepository.Insert(request.AuthRecord);

            return new InsertAuthRecordResponse();
        }
    }
}
