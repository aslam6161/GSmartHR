
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace GSmartHR.Services.Security
{
    public class EncryptionService : IEncryptionService
    {
        public  string GenerateSalt()
        {
             int nSalt = 70;

            var saltBytes = new byte[nSalt];

            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetNonZeroBytes(saltBytes);
            }

            return Convert.ToBase64String(saltBytes);
        }


        public  string GetHashPassword(string password, string salt)
        {
            int nIterations = 10101;
            int nHash = 70;

            var saltBytes = Convert.FromBase64String(salt);

            using (var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, nIterations))
            {
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(nHash));
            }
        }
    }
}
