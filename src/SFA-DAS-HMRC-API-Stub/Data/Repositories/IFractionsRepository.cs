﻿using SFA.DAS.HMRC.API.Stub.Domain;
using System;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public interface IFractionsRepository
    {
        Task<Fractions> GetByEmpRef(
            string empRef,
            DateTime fromDate,
            DateTime toDate)
        ;

        Task<FractionCalculationDate> GetLastCalcDate();
    }
}
