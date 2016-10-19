using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace POS.HDT.Common.Core.Logic.Utils
{
    public static class Utilities
    {
        public static string ConvertStringToMD5(string pString)
        {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(pString);

            Byte[] hashedBytes = new MD5CryptoServiceProvider().ComputeHash(inputBytes);

            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
