using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class DeleteAuthRequestByIdCommand : ICommand<DeleteAuthRequestRequest, DeleteAuthRequestResponse>
    {
        private readonly IAuthRequestRepository _authRequestRepository;

        public DeleteAuthRequestByIdCommand(IAuthRequestRepository authRequestRepository)
        {
            _authRequestRepository = authRequestRepository;
        }

        public async Task<DeleteAuthRequestResponse> Execute(DeleteAuthRequestRequest request)
        {
            var authRequest = await _authRequestRepository.DeleteAuthRequestById(request.AuthId);

            return new DeleteAuthRequestResponse
            {
                Success = authRequest
            };
        }
    }
}
