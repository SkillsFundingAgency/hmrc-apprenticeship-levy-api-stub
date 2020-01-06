using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.HMRC.API.Stub.Application.Extensions
{
    public static class LongUtils
    {
        public static long Rand()
        {
            return ((long)((new Random()).NextDouble() * long.MaxValue));
        }
    }
}
