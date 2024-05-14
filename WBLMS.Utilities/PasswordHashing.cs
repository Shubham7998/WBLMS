using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WBLMS.Utilities
{
    public class PasswordHashing
    {
        private const int _saltSize = 16; // 128 bits
        private const int _keySize = 32; // 256 bits
        private const int _iterations = 50000;
        private const string _key = "pw";

        private static readonly HashAlgorithmName _algorithm = HashAlgorithmName.SHA256;

        private const char segmentDelimiter = ':';

        public static string getHashPassword(string userPassword)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_saltSize);

            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
                userPassword,
                salt,
                _iterations,
                _algorithm,
                _keySize
            );

            Console.WriteLine(hash.ToString());
            var haxHash = Convert.ToHexString(hash);
            var haxSalt = Convert.ToHexString(salt);

            var result = string.Join(
                segmentDelimiter,
                Convert.ToHexString(hash),
                Convert.ToHexString(salt),
                _iterations,
                _algorithm
            );

            Console.WriteLine(result);
            return result;
        }

        public static bool Verify(string input, string hashString)
        {
            string[] segments = hashString.Split(segmentDelimiter);
            byte[] hash = Convert.FromHexString(segments[0]);
            byte[] salt = Convert.FromHexString(segments[1]);
            int iterations = int.Parse(segments[2]);
            HashAlgorithmName algorithm = new HashAlgorithmName(segments[3]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(
                input,
                salt,
                iterations,
                algorithm,
                hash.Length
            );
            Console.WriteLine(inputHash);
            var result = CryptographicOperations.FixedTimeEquals(inputHash, hash);

            return result;
        }

    }
}
