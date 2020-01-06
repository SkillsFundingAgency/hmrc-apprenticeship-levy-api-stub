using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Application.Utils;
using SFA.DAS.HMRC.API.Stub.Domain;
using SFA_DAS_TaxService_Stub.Configuration;

namespace SFA_DAS_TaxService_Stub.Pages
{
    public class GrantScopeModel : PageModel
    {
        private readonly IQuery<GetAuthRequestByIdRequest, GetAuthRequestByIdResponse> _authQuery;
        private readonly IQuery<GetScopeByNameRequest, GetScopeByNameResponse> _scopeQuery;
        private readonly ICommand<DeleteAuthRequestRequest, DeleteAuthRequestResponse> _deleteAuth;
        private readonly ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse> _insertAuthCode;

        [BindProperty(Name = "auth_id", SupportsGet = true)]
        public long AuthId { get; set; }
        [BindProperty(Name = "origin", SupportsGet = true)]
        public string Origin { get; set; }

        public GrantScopeModel(
            IQuery<GetAuthRequestByIdRequest, GetAuthRequestByIdResponse> authQuery,
            IQuery<GetScopeByNameRequest, GetScopeByNameResponse> scopeQuery,
            ICommand<DeleteAuthRequestRequest, DeleteAuthRequestResponse> deleteAuth,
            ICommand<InsertAuthCodeRequest, InsertAuthCodeResponse> insertAuthCode)
        {
            _authQuery = authQuery;
            _scopeQuery = scopeQuery;
            _deleteAuth = deleteAuth;
            _insertAuthCode = insertAuthCode;
        }

        public async Task<IActionResult> OnGet()
        {
            var auth = await _authQuery.Get(new GetAuthRequestByIdRequest
            {
                AuthId = AuthId
            });

            if (auth == null)
            {
                return BadRequest("unknown auth id");
            }

            var scope = await _scopeQuery.Get(new GetScopeByNameRequest
            {
                Name = auth.AuthRequest.Scope
            });

            if(scope == null)
            {
                return BadRequest("unknown scope");
            }

            if(scope.Scope.NeedsExplicitGrant)
            {
                ViewData["ScopeDescription"] = scope.Scope.Description;
            }

            // TODO: Should be in transaction (original code did not have transaction so no loss of function but still...)

            var result = await _deleteAuth.Execute(new DeleteAuthRequestRequest
            {
                AuthId = AuthId
            });

            if(!result.Success)
            {
                return BadRequest();
            }

            var token = TokenUtils.GenerateToken();
            await _insertAuthCode.Execute(new InsertAuthCodeRequest
            {
                AuthCode = new AuthCode
                {
                    AuthorizationCode = token,
                    ClientId = auth.AuthRequest.ClientId,
                    GatewayId = HttpContext.Session.GetString(Constants.GATEWAYIDSESSIONKEY),
                    expiresIn = CalcExpiresIn(),
                    RedirectUri = auth.AuthRequest.RedirectUri,
                    CreatedAt = DateTime.Now,
                    Scope = auth.AuthRequest.Scope
                }
            });

            if(!string.IsNullOrEmpty(auth.AuthRequest.State))
            {
                RemoveFromSession(Constants.GATEWAYIDSESSIONKEY);
                return Redirect($"{auth.AuthRequest.RedirectUri}?code={token}&state={auth.AuthRequest.State}");
            }

            else
            {
                RemoveFromSession(Constants.GATEWAYIDSESSIONKEY);
                return Redirect($"{auth.AuthRequest.RedirectUri}?code={token}");
            }
        }

        private void RemoveFromSession(string key)
        {
            HttpContext.Session.Remove(key);
        }

        private int CalcExpiresIn()
        {
            return 4 * 60 * 60;
        }

        public void OnPost()
        {
            return;
        }
    }
}