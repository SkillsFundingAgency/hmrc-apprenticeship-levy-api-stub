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
    public class WhenICheckEmpRef
    {
        private Mock<IEmployerReferenceRepository> employerReferenceRepository;
        private readonly string empRef = "000/AB00001";

        [Test]
        public async Task ThenAValidResponseIsReturnedForAValidRequest()
        {
            string id = "1";

            Name name = new Name
            {
                NameLine1 = "Employer"
            };

            Employer employer = new Employer
            {
                Name = name
            };

            var links = new Links()
            {
                Declarations = new Declarations()
                {
                    Href = "testing/declarations/href"
                },
                Self = new Self()
                {
                    Href = "testing/self/href"
                },
                EmploymentCheck = new EmploymentCheck()
                {
                    Href = "testing/employmentcheck.href"
                },
                Fraction = new Fraction()
                {
                    Href = "testing/fraction/href"
                }
            };

            // Arrange
            employerReferenceRepository = new Mock<IEmployerReferenceRepository>(MockBehavior.Strict);
            employerReferenceRepository
                .Setup(x => x.GetEmployerReference(empRef))
                .ReturnsAsync(new EmployerReference()
                {
                    Id = id,
                    Links = links,
                    EmpRef = empRef,
                    Employer = employer
                });

            var sut = new GetEmployerReferenceQuery(employerReferenceRepository.Object);
            
            // Act
            var result = await sut.Get(new GetEmployerReferenceRequest(empRef));

            // Assert
            result.Should().NotBeNull();
            result.EmployerReference.Should().NotBeNull();
            result.EmployerReference.Id.Should().Be(id);
            result.EmployerReference.EmpRef.Should().Be(empRef);
            result.EmployerReference.Employer.Should().NotBeNull();
            result.EmployerReference.Employer.Name.Should().Be(employer.Name);
            result.EmployerReference.Links.Should().NotBeNull();
            result.EmployerReference.Links.Self.Should().NotBeNull();
            result.EmployerReference.Links.Self.Href.Should().Be(links.Self.Href);
            result.EmployerReference.Links.Fraction.Should().NotBeNull();
            result.EmployerReference.Links.Fraction.Href.Should().Be(links.Fraction.Href);
            result.EmployerReference.Links.Declarations.Should().NotBeNull();
            result.EmployerReference.Links.Declarations.Href.Should().Be(links.Declarations.Href);
            result.EmployerReference.Links.EmploymentCheck.Should().NotBeNull();
            result.EmployerReference.Links.EmploymentCheck.Href.Should().Be(links.EmploymentCheck.Href);
        }

        [Test]
        public async Task ThenANullResponseIsReturnedForAnIvalidRequest()
        {
            // Arrange
            employerReferenceRepository = new Mock<IEmployerReferenceRepository>(MockBehavior.Strict);
            employerReferenceRepository
                .Setup(x => x.GetEmployerReference(empRef))
                .ReturnsAsync(default(EmployerReference));

            var sut = new GetEmployerReferenceQuery(employerReferenceRepository.Object);
            
            // Act
            var result = await sut.Get(new GetEmployerReferenceRequest(empRef));

            // Assert
            result.Should().BeNull();
        }
    }
}