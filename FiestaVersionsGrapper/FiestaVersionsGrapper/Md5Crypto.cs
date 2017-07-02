using System;
using System.IO;
using System.Security.Cryptography;

namespace FiestaVersionsGrapper
{
    public class Md5Crypto
    {
        public static string GetMD5HashFromFile(string fileName)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(fileName))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                }
            }
        }
    }
}
