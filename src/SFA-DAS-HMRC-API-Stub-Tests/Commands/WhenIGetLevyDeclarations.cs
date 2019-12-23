using NUnit.Framework;
using Moq;
using FluentAssertions;
using SFA.DAS.HMRC.API.Stub.Commands;
using System;
using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;
using SFA.DAS.HMRC.API.Stub.Data.Repositories;
using System.Collections.Generic;

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenIGetLevyDeclarations
    {
        private Mock<ILevyDeclarationRepository> levyDeclarationsRepository;
        private readonly string empRef = "000/AB00001";

        [Test]
        public async Task ThenAValidResponseIsReturnedForAValidRequest()
        {
            int id = 1;
            DateTime? fromDate = DateTime.Now;
            DateTime? toDate = DateTime.Now;
            var declarations = new List<Declaration>
            {
               new Declaration()
               {
                   Id = id.ToString(),
                   SubmissionTime = DateTime.Now,
                   LevyAllowanceForFullYear = 100,
                   LevyDueYTD = 1000,
                   PayrollPeriod = new PayrollPeriod
                   {
                       Month = 1,
                       Year = "2019"
                   }
               },
               new Declaration()
               {
                   Id = (++id).ToString(),
                   SubmissionTime = DateTime.Now.AddMonths(1),
                   LevyAllowanceForFullYear = 200,
                   LevyDueYTD = 2000,
                   PayrollPeriod = new PayrollPeriod
                   {
                       Month = 2,
                       Year = "2019"
                   }
               }
            };

            // Arrange
            levyDeclarationsRepository = new Mock<ILevyDeclarationRepository>(MockBehavior.Strict);
            levyDeclarationsRepository
                .Setup(x => x.GetByEmpRef(empRef, fromDate, toDate))
                .ReturnsAsync(new LevyDeclaration()
                {
                    EmpRef = empRef,
                    Declarations = declarations
                });

            var sut = new GetLevyDeclarationCommand(levyDeclarationsRepository.Object);

            // Act
            var result = await sut.Get(new GetLevyDeclarationRequest(empRef, fromDate, toDate));

            // Assert
            result.Should().NotBeNull();
            result.Declarations.Should().NotBeNull();
            result.Declarations.EmpRef.Should().Be(empRef);
            result.Declarations.Declarations.Count.Should().Be(declarations.Count);
            result.Declarations.Declarations[0].Id.Should().Be(declarations[0].Id);
            result.Declarations.Declarations[1].Id.Should().Be(declarations[1].Id);
            result.Declarations.Declarations[0].SubmissionTime.Should().Be(declarations[0].SubmissionTime);
            result.Declarations.Declarations[1].SubmissionTime.Should().Be(declarations[1].SubmissionTime);
            result.Declarations.Declarations[0].LevyDueYTD.Should().Be(declarations[0].LevyDueYTD);
            result.Declarations.Declarations[1].LevyDueYTD.Should().Be(declarations[1].LevyDueYTD);
            result.Declarations.Declarations[0].LevyAllowanceForFullYear.Should().Be(declarations[0].LevyAllowanceForFullYear);
            result.Declarations.Declarations[1].LevyAllowanceForFullYear.Should().Be(declarations[1].LevyAllowanceForFullYear);

            result.Declarations.Declarations[0].PayrollPeriod.Should().NotBeNull();
            result.Declarations.Declarations[1].PayrollPeriod.Should().NotBeNull();

            result.Declarations.Declarations[0].PayrollPeriod.Month.Should().Be(declarations[0].PayrollPeriod.Month);
            result.Declarations.Declarations[1].PayrollPeriod.Month.Should().Be(declarations[1].PayrollPeriod.Month);
            result.Declarations.Declarations[0].PayrollPeriod.Year.Should().Be(declarations[0].PayrollPeriod.Year);
            result.Declarations.Declarations[1].PayrollPeriod.Year.Should().Be(declarations[1].PayrollPeriod.Year);
        }

        [Test]
        public async Task ThenANullResponseIsReturnedForAnIvalidRequest()
        {
            // Arrange
            DateTime? fromDate = DateTime.Now;
            DateTime? toDate = DateTime.Now;

            levyDeclarationsRepository = new Mock<ILevyDeclarationRepository>(MockBehavior.Strict);
            levyDeclarationsRepository
                .Setup(x => x.GetByEmpRef(empRef, fromDate, toDate))
                .ReturnsAsync(default(LevyDeclaration));

            var sut = new GetLevyDeclarationCommand(levyDeclarationsRepository.Object);
            
            // Act
            var result = await sut.Get(new GetLevyDeclarationRequest(empRef, fromDate, toDate));

            // Assert
            result.Should().BeNull();
        }
    }
}