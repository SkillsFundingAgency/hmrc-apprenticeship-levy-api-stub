using SFA.DAS.HMRC.API.Stub.Application.Extensions;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Commands
{
    public class InsertAuthRequestCommand : ICommand<InsertAuthRequestRequest, InsertAuthRequestResponse>
    {
        private readonly IAuthRequestRepository _authRequestRepository;

        public InsertAuthRequestCommand(IAuthRequestRepository authRequestRepository)
        {
            _authRequestRepository = authRequestRepository;
        }

        public async Task<InsertAuthRequestResponse> Execute(InsertAuthRequestRequest request)
        {
            var id = LongUtils.Rand();
            request.AuthRequest.Id = id;

            await _authRequestRepository.Insert(request.AuthRequest);

            return new InsertAuthRequestResponse()
            {
                Id = id
            };
        }
    }
}
