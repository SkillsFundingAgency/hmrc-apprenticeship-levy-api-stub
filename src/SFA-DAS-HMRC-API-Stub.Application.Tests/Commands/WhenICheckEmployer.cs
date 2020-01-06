using NUnit.Framework;
using Moq;
using FluentAssertions;
using SFA.DAS.HMRC.API.Stub.Application.Queries;
using SFA.DAS.HMRC.API.Stub.Repositories;
using System;
using SFA.DAS.HMRC.API.Stub.Domain;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenICheckEmployer
    {
        private Mock<IEmployerChecksRepository> employerChecksRepository;

        [Test]
        public async Task ThenAValidResponseIsReturnedForAValidRequest()
        {
            string empRef = "000/AB00001";
            string nino = "AB123456A";
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(10);
            string id = "1";

            // Arrange
            employerChecksRepository = new Mock<IEmployerChecksRepository>(MockBehavior.Strict);
            employerChecksRepository
                .Setup(x => x.GetEmploymentStatus(empRef, nino, fromDate, toDate))
                .ReturnsAsync(new EmployerStatus()
                {
                    Employed = false,
                    EmpRef = empRef,
                    FromDate = fromDate.Date,
                    ToDate = toDate.Date,
                    Id = id,
                    Nino = nino
                });

            var sut = new GetEmployerChecksQuery(employerChecksRepository.Object);

            // Act
            var result = await sut.Get(new GetEmployerChecksRequest(empRef, nino, fromDate, toDate));

            // Assert
            result.Should().NotBeNull();
            result.Nino.Should().Be(nino);
            result.FromDate.Should().Be(fromDate);
            result.ToDate.Should().Be(toDate);
            result.Empref.Should().Be(empRef);
        }

        [Test]
        public async Task ThenANullResponseIsReturnedForAnInvalidRequest()
        {
            string empRef = "000/AB00001";
            string nino = "AB123456A";
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(10);
            string id = "1";

            // Arrange
            employerChecksRepository = new Mock<IEmployerChecksRepository>(MockBehavior.Strict);
            employerChecksRepository
                .Setup(x => x.GetEmploymentStatus(empRef, nino, fromDate, toDate))
                .ReturnsAsync(default(EmployerStatus));

            var sut = new GetEmployerChecksQuery(employerChecksRepository.Object);

            // Act
            var result = await sut.Get(new GetEmployerChecksRequest(empRef, nino, fromDate, toDate));

            // Assert
            result.Should().BeNull();
        }

        [Test]
        public async Task ThenTheResponseIndicatesEmployedForARequestWhereDatesOverlap()
        {
            string empRef = "000/AB00001";
            string nino = "AB123456A";
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(10);
            string id = "1";

            // Arrange
            employerChecksRepository = new Mock<IEmployerChecksRepository>(MockBehavior.Strict);
            employerChecksRepository
                .Setup(x => x.GetEmploymentStatus(empRef, nino, fromDate, toDate.AddDays(-1).Date))
                .ReturnsAsync(new EmployerStatus()
                {
                    EmpRef = empRef,
                    FromDate = fromDate.Date,
                    ToDate = toDate.Date,
                    Id = id,
                    Nino = nino
                });

            var sut = new GetEmployerChecksQuery(employerChecksRepository.Object);

            // Act
            var result = await sut.Get(new GetEmployerChecksRequest(empRef, nino, fromDate, toDate.AddDays(-1).Date));

            // Assert
            result.Should().NotBeNull();
            result.Employed.Should().BeTrue();
        }

        [Test]
        public async Task ThenTheResponseIndicatesNotEmployedForARequestWhereDatesDoNotOverlap()
        {
            string empRef = "000/AB00001";
            string nino = "AB123456A";
            DateTime fromDate = DateTime.Now;
            DateTime toDate = DateTime.Now.AddDays(10);
            string id = "1";

            // Arrange
            employerChecksRepository = new Mock<IEmployerChecksRepository>(MockBehavior.Strict);
            employerChecksRepository
                .Setup(x => x.GetEmploymentStatus(empRef, nino, fromDate, toDate))
                .ReturnsAsync(new EmployerStatus()
                {
                    EmpRef = empRef,
                    FromDate = fromDate.Date,
                    ToDate = toDate.Date,
                    Id = id,
                    Nino = nino
                });

            var sut = new GetEmployerChecksQuery(employerChecksRepository.Object);

            // Act
            var result = await sut.Get(new GetEmployerChecksRequest(empRef, nino, fromDate, toDate));

            // Assert
            result.Should().NotBeNull();
            result.Employed.Should().BeFalse();
        }
    }
}