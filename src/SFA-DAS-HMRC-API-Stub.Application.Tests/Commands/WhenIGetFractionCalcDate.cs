using NUnit.Framework;
using Moq;
using FluentAssertions;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenIGetFractionCalcDate
    {
        private Mock<IFractionsCalcDateRepository> fractionsCalcDateRepository;

        [Test]
        public async Task ThenAValidResponseIsReturnedForAValidRequest()
        {
            string id = "1";
            DateTime fractionCalcDate = DateTime.Now;

            // Arrange
            fractionsCalcDateRepository = new Mock<IFractionsCalcDateRepository>(MockBehavior.Strict);
            fractionsCalcDateRepository
                .Setup(x => x.GetLastCalcDate())
                .ReturnsAsync(new FractionCalculationDate()
                {
                    Id = id,
                    LastCalculationDate = fractionCalcDate
                });

            var sut = new GetFractionCalcDateQuery(fractionsCalcDateRepository.Object);
            
            // Act
            var result = await sut.Get(new GetFractionCalcDateRequest());

            // Assert
            result.Should().NotBeNull();
            result.LastCalculationDate.Should().Be(fractionCalcDate);
        }

        [Test]
        public async Task ThenANullResponseIsReturnedForAnIvalidRequest()
        {
            // Arrange
            fractionsCalcDateRepository = new Mock<IFractionsCalcDateRepository>(MockBehavior.Strict);
            fractionsCalcDateRepository
                .Setup(x => x.GetLastCalcDate())
                .ReturnsAsync(default(FractionCalculationDate));

            var sut = new GetFractionCalcDateQuery(fractionsCalcDateRepository.Object);
            
            // Act
            var result = await sut.Get(new GetFractionCalcDateRequest());

            // Assert
            result.Should().BeNull();
        }
    }
}