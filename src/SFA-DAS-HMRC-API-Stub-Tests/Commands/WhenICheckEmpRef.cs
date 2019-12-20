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

namespace SFA.DAS.HMRC.API.Stub.Application.Tests
{
    public class WhenICheckEmpRef
    {
        private Mock<IEmployerReferenceRepository> employerReferenceRepository;

        [Test]
        public async Task ThenAValidResponseIsReturnedForAValidRequest()
        {
            string empRef = "000/AB00001";
            string id = "1";

            Name name = new Name
            {
                NameLine1 = "Employer"
            };

            Employer employer = new Employer
            {
                Name = name
            };

            links =

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

            // Act


            // Assert

        }

    }
}