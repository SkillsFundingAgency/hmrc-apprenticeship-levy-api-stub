using System;
using System.Collections.Generic;
using System.Text;

namespace SFA.DAS.HMRC.API.Stub.Application.Utils
{
    public static class TimeUtils
    {
        public static long TimeSinceEpoc()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
