using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using SFA.DAS.HMRC.API.Stub.Application.Commands;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Application.Utils;
using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Infrastructure.OAuth
{
    public class TokenResponseGenerator : ITokenResponseGenerator
    {
        protected readonly ITokenService TokenService;
        protected readonly IRefreshTokenService RefreshTokenService;
        protected readonly IResourceStore Resources;
        protected readonly IClientStore Clients;
        protected readonly IQuery<GetAuthRecordtByAccessTokenRequest, GetAuthRecordtByAccessTokenResponse> AuthRecordQuery;
        protected readonly ICommand<UpdateAuthRecordRequest, UpdateAuthRecordResponse> AuthRecordUpdateCommand;
        protected ISystemClock Clock { get; }

        public TokenResponseGenerator(ITokenService tokenService, IRefreshTokenService refreshTokenService, 
            IResourceStore resources, IClientStore clients,
             IQuery<GetAuthRecordtByAccessTokenRequest, GetAuthRecordtByAccessTokenResponse> authRecordQuery,
             ICommand<UpdateAuthRecordRequest, UpdateAuthRecordResponse> authRecordUpdateCommand,
              ISystemClock clock)
        {
            TokenService = tokenService;
            RefreshTokenService = refreshTokenService;
            Resources = resources;
            Clients = clients;
            AuthRecordQuery = authRecordQuery;
            AuthRecordUpdateCommand = authRecordUpdateCommand;
            Clock = clock;
        }

        public async Task<TokenResponse> ProcessAsync(TokenRequestValidationResult request)
        {
            if (request.ValidatedRequest.GrantType == OidcConstants.GrantTypes.RefreshToken)
            {
                return await ProcessRefreshTokenRequestAsync(request);
            }
            else
            {
                return await ProcessAuthorizationCodeRequestAsync(request);
            }                                
        }

        protected virtual async Task<TokenResponse> ProcessRefreshTokenRequestAsync(TokenRequestValidationResult request)
        {

            var authRecord = new AuthRecord
            {
                AccessToken = TokenUtils.GenerateToken(),
                RefreshToken = TokenUtils.GenerateToken(),
                RefreshedAt = Clock.UtcNow.UtcDateTime,
            };

            var result = await AuthRecordUpdateCommand.Execute(new UpdateAuthRecordRequest
            {
                RefreshToken = request.ValidatedRequest.RefreshTokenHandle,
                AuthRecord = authRecord
            });

            var response = new TokenResponse
            {
                AccessToken = authRecord.AccessToken,
                AccessTokenLifetime = request.ValidatedRequest.AccessTokenLifetime,
                Custom = request.CustomResponse,
                Scope = string.Join(" ", request.ValidatedRequest.RefreshToken.Scopes),
                RefreshToken = authRecord.RefreshToken
            };

            return response;
        }

        protected virtual async Task<TokenResponse> ProcessAuthorizationCodeRequestAsync(TokenRequestValidationResult request)
        {
            //Logger.LogTrace("Creating response for authorization code request");

            //////////////////////////
            // access token
            /////////////////////////
            (var accessToken, var refreshToken) = await CreateAccessTokenAsync(request.ValidatedRequest);
            var response = new TokenResponse
            {
                AccessToken = accessToken,
                AccessTokenLifetime = request.ValidatedRequest.AccessTokenLifetime,
                Custom = request.CustomResponse,
                Scope = string.Join(" ", request.ValidatedRequest.AuthorizationCode.RequestedScopes),
                RefreshToken = refreshToken
            };
                      
            return response;
        }

        protected virtual async Task<(string accessToken, string refreshToken)> CreateAccessTokenAsync(ValidatedTokenRequest request)
        {
            TokenCreationRequest tokenRequest = null;
            bool createRefreshToken;

            if (request.AuthorizationCode != null)
            {

                // load the client that belongs to the authorization code
                Client client = null;
                if (request.AuthorizationCode.ClientId != null)
                {
                    client = await Clients.FindEnabledClientByIdAsync(request.AuthorizationCode.ClientId);
                }
                if (client == null)
                {
                    throw new InvalidOperationException("Client does not exist anymore.");
                }

                var resources = await Resources.FindEnabledResourcesByScopeAsync(request.AuthorizationCode.RequestedScopes);

                tokenRequest = new TokenCreationRequest
                {
                    Subject = request.AuthorizationCode.Subject,
                    Resources = resources,
                    ValidatedRequest = request
                }; 
            }

            var at = await TokenService.CreateAccessTokenAsync(tokenRequest);
            var accessToken = await TokenService.CreateSecurityTokenAsync(at);            
            
            var record = await AuthRecordQuery.Get(new GetAuthRecordtByAccessTokenRequest
            {
                AccessToken = accessToken
            });

            return (accessToken, record.AuthRecord.RefreshToken);
        }

    }
}
