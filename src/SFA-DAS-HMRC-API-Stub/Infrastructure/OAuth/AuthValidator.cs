using IdentityServer4.Validation;
using MongoDB.Bson;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class AuthValidator : ICustomAuthorizeRequestValidator
    {
        private readonly IQuery<GetApplicationByIdRequest, GetApplicationByIdResponse> _query;
        private readonly IQuery<GetScopeByNameRequest, GetScopeByNameResponse> _scopeQuery;
        private readonly ICommand<InsertAuthRequestRequest, InsertAuthRequestResponse> _authCommand;

        public AuthValidator(
            IQuery<GetApplicationByIdRequest, GetApplicationByIdResponse> query,
            IQuery<GetScopeByNameRequest, GetScopeByNameResponse> scope,
            ICommand<InsertAuthRequestRequest, InsertAuthRequestResponse> authCommand)
        {
            _query = query;
            _scopeQuery = scope;
            _authCommand = authCommand;
        }

        public async Task ValidateAsync(CustomAuthorizeRequestValidationContext context)
        {
            var scopeTask = _scopeQuery.Get(new GetScopeByNameRequest
            {
                Name = context.Result.ValidatedRequest.RequestedScopes.First()
            });
            var applicationTask = _query.Get(new GetApplicationByIdRequest()
            {
                ClientId = context.Result.ValidatedRequest.ClientId
            });

            Task.WaitAll(scopeTask, applicationTask);

            if(scopeTask.Result == null || applicationTask.Result == null)
            {
                context.Result.IsError = true;
                context.Result.Error = scopeTask.Result == null ? "unknown scope" : "unknown client id";
            }

            var result = await _authCommand.Execute(new InsertAuthRequestRequest
            {
                AuthRequest = new Domain.AuthRequest
                {
                    ClientId = context.Result.ValidatedRequest.ClientId,
                    Scope = context.Result.ValidatedRequest.RequestedScopes.First(),
                    RedirectUri = context.Result.ValidatedRequest.RedirectUri,
                    CreationDate = DateTime.Now
                }
            });

            context.Result.ValidatedRequest.Options.UserInteraction.LoginUrl = context.Result.ValidatedRequest.Options.UserInteraction.LoginUrl.Replace("auth_id=0", $"auth_id={result.Id}");
        }
    }
}
