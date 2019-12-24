using NUnit.Framework;
using Moq;
using FluentAssertions;
using SFA.DAS.HMRC.API.Stub.Commands;
using System;
using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System.Collections.Generic;
using SFA.DAS.HMRC.API.Stub.Services;
using System.Linq;

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenIAuthenticateAUser
    {
        private Mock<IAuthRecordRepository> authRepository;
        private Mock<IGatewayRepository> gatewayRepository;
        private readonly string token = "123456";

        [Test]
        public async Task ThenAnAuthenticatedResponseIsReturnedForAValidUser()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);
            List<AuthRecord> authRecords = CreateAuthRecord(false);

            authRepository.Setup(x => x.GetAuthRecords(token))
                .ReturnsAsync(authRecords)
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthenticated(token);

            result.Should().NotBeNull();
            result.IsAuthenticated.Should().BeTrue();
            result.IsPrivileged.Should().BeFalse();
        }    

        [Test]
        public async Task ThenAPrivilegedResponseIsReturnedForAPrivilegedUser()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);
            List<AuthRecord> authRecords = CreateAuthRecord(true);

            authRepository.Setup(x => x.GetAuthRecords(token))
              .ReturnsAsync(authRecords)
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthenticated(token);

            result.Should().NotBeNull();
            result.IsPrivileged.Should().BeTrue();
            result.GatewayId = authRecords.First().GatewayId;
        }

        [Test]
        public async Task ThenAnUnauthenticatedResponseIsReturnedForAnIvalidUser()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);
            List<AuthRecord> authRecords = new List<AuthRecord>();

            authRepository.Setup(x => x.GetAuthRecords(token))
              .ReturnsAsync(authRecords)
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthenticated(token);

            result.Should().NotBeNull();
            result.IsAuthenticated.Should().BeFalse();
            result.IsPrivileged.Should().BeFalse();
        }

        private static List<AuthRecord> CreateAuthRecord(bool privileged)
        {
            return new List<AuthRecord>
            {
                new AuthRecord
                {
                    Id = "1",
                    AccessToken = "123456",
                    ClientId = "Test",
                    CreatedAt = DateTime.Now,
                    ExpiresIn = 14440,
                    GatewayId = "Test",
                    IsPrivileged = privileged,
                    RefreshedAt = DateTime.Now,
                    RefreshToken = string.Empty,
                    Scope = "test.scope"
                }
            };
        }
    }
}