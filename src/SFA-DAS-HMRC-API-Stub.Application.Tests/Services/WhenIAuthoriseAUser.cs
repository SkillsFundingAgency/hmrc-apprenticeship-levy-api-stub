using NUnit.Framework;
using Moq;
using FluentAssertions;
using System;
using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System.Collections.Generic;
using SFA.DAS.HMRC.API.Stub.Services;
using System.Linq;

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenIAuthoriseAUser
    {
        private Mock<IAuthRecordRepository> authRepository;
        private Mock<IGatewayRepository> gatewayRepository;
        private readonly string gatewayId = "123456";
        private readonly string empRef = "000/AB00001";

        [Test]
        public async Task ThenAnAuthorisedResponseIsReturnedForAValidUser()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);
            GatewayUser gatewayUser = new GatewayUser()
            {
                Id = "1",
                EmpRef = empRef,
                GatewayId = gatewayId,
                Name = "Test",
                Password = string.Empty,
                Require2SV = true
            };

            gatewayRepository.Setup(x => x.GetGatewayRecordsForId(gatewayId))
                .ReturnsAsync(new List<GatewayUser>
                {
                    gatewayUser
                })
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthorized(gatewayId, empRef);

            result.Should().BeTrue();
        }            

        [Test]
        public async Task ThenAnAuthorisedResponseIsReturnedForAnIvalidUser()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);

            gatewayRepository.Setup(x => x.GetGatewayRecordsForId(gatewayId))
                .ReturnsAsync(new List<GatewayUser>())
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthorized(gatewayId, empRef);

            result.Should().BeFalse();
        }

        [Test]
        public async Task ThenAnAuthorisedResponseIsReturnedForAnInvalidGatewayId()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);

            gatewayRepository.Setup(x => x.GetGatewayRecordsForId(gatewayId))
                .ReturnsAsync(new List<GatewayUser>())
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthorized(string.Empty, empRef);

            result.Should().BeFalse();
        }

        [Test]
        public async Task ThenAnAuthorisedResponseIsReturnedForAnInvalidEmpRef()
        {
            authRepository = new Mock<IAuthRecordRepository>(MockBehavior.Strict);
            gatewayRepository = new Mock<IGatewayRepository>(MockBehavior.Strict);

            gatewayRepository.Setup(x => x.GetGatewayRecordsForId(gatewayId))
                .ReturnsAsync(new List<GatewayUser>())
            ;

            var sut = new AuthenticationService(authRepository.Object, gatewayRepository.Object);

            var result = await sut.IsAuthorized(string.Empty, empRef);

            result.Should().BeFalse();
        }
    }
}