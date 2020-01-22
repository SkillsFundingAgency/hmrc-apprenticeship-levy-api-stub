using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SFA.DAS.HMRC.API.Stub.Application.Utils
{
    public static class TokenUtils
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public static string GenerateToken()
        {
            var buf = new Byte[12];
            rngCsp.GetBytes(buf);

            return BitConverter.ToString(buf).Replace("-", string.Empty);
        }
    }
}
