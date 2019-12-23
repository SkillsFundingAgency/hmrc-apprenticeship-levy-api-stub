using NUnit.Framework;
using Moq;
using FluentAssertions;
using SFA.DAS.HMRC.API.Stub.Commands;
using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenIGetFractions
    {
        private Mock<IFractionsRepository> fractionsRepository;
        private readonly string empRef = "000/AB00001";

        [Test]
        public async Task ThenAValidResponseIsReturnedForAValidRequest()
        {
            string id = "1";
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;
            var fractions = new List<FractionCalculation>
            {
               new FractionCalculation()
               {
                   CalculatedAt = DateTime.Now,
                   Fractions = new List<Fractions>
                   {
                       new Fractions
                       {
                           Region = "region 1",
                           Value = "0.1"
                       },
                       new Fractions
                       {
                           Region = "region 2",
                           Value = "0.2"
                       }
                   }
               }
            };

            // Arrange
            fractionsRepository = new Mock<IFractionsRepository>(MockBehavior.Strict);
            fractionsRepository
                .Setup(x => x.GetByEmpRef(empRef, fromDate, toDate))
                .ReturnsAsync(new RootObject()
                {
                    Id = id,
                    EmpRef = empRef,
                    FractionCalculations = fractions 
                });

            var sut = new GetFractionsCommand(fractionsRepository.Object);
            
            // Act
            var result = await sut.Get(new GetFractionsRequest(empRef, fromDate, toDate));

            // Assert
            result.Should().NotBeNull();
            result.Fraction.Should().NotBeNull();
            result.Fraction.Id.Should().Be(id);
            result.Fraction.FractionCalculations.Should().NotBeNull();
            result.Fraction.FractionCalculations.Count.Should().Be(fractions.Count);
            result.Fraction.FractionCalculations.First().Fractions.Count.Should().Be(2);
            result.Fraction.FractionCalculations.First().Fractions[0].Region.Should().Be(fractions.First().Fractions[0].Region);
            result.Fraction.FractionCalculations.First().Fractions[0].Value.Should().Be(fractions.First().Fractions[0].Value);
            result.Fraction.FractionCalculations.First().Fractions[1].Region.Should().Be(fractions.First().Fractions[1].Region);
            result.Fraction.FractionCalculations.First().Fractions[1].Value.Should().Be(fractions.First().Fractions[1].Value);
        }

        [Test]
        public async Task ThenANullResponseIsReturnedForAnIvalidRequest()
        {
            // Arrange
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now;

            fractionsRepository = new Mock<IFractionsRepository>(MockBehavior.Strict);
            fractionsRepository
                .Setup(x => x.GetByEmpRef(empRef, fromDate, toDate))
                .ReturnsAsync(default(RootObject));

            var sut = new GetFractionsCommand(fractionsRepository.Object);
            
            // Act
            var result = await sut.Get(new GetFractionsRequest(empRef, fromDate, toDate));

            // Assert
            result.Should().BeNull();
        }
    }
}