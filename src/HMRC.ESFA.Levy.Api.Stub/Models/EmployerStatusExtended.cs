using System;
using HMRC.ESFA.Levy.Api.Types;

namespace HMRC.ESFA.Levy.Api.Stub.Models
{
    public class EmployerStatusExtended : EmploymentStatus
    {
        public Exception Exception { get; set; }

        public int? HttpStatusCode { get; set; }
    }
}