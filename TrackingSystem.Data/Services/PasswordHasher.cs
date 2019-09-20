using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TrackingSystem.Data.Interfaces;

namespace TrackingSystem.Data.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public bool CompareHashToPassword(string hash, string password)
        {
            return hash.Equals(Hash(password));
        }

        public string Hash(string password)
        {
            SHA512 hasher = SHA512.Create();
            byte[] result = hasher.ComputeHash(Encoding.UTF8.GetBytes(password));

            IEnumerable<string> convertToHexString = from @byte in result select @byte.ToString("X2");

            return string.Join("", convertToHexString);
        }
    }
}
