using System;
using System.Security.Cryptography;
using Shurly.Core.Helpers;

namespace Shurly.Core.Security
{
    public class RandomPassword
    {
        private const int DefaultPasswordLength = 8;

        /// <summary>
        /// Generates 8 alphanumeric character password.
        /// </summary>
        public static string GeneratePassword(string characters)
        {
            return GeneratePassword(DefaultPasswordLength, characters);
        }

        /// <summary>
        /// Generates the password.
        /// </summary>
        public static string GeneratePassword(int passwordLength,
                                               string passwordCharacters)
        {
            if (passwordLength < 0)
                throw new ArgumentOutOfRangeException("Password Length");

            if (string.IsNullOrEmpty(passwordCharacters))
                throw new ArgumentOutOfRangeException("Password Characters");

            var password = new char[passwordLength];

            var random = GetRandom();

            for (int i = 0; i < passwordLength; i++)
            {
                if (i == 0)
                {
                    password[i] = Characters.AlphaSmall[random.Next(Characters.AlphaSmall.Length)];
                }
                else if (i == 1)
                {
                    password[i] = Characters.AlphaCaps[random.Next(Characters.AlphaCaps.Length)];
                }
                else if (i == 2)
                {
                    password[i] = Characters.Numeric[random.Next(Characters.Numeric.Length)];
                }
                else
                {
                    password[i] = passwordCharacters[random.Next(passwordCharacters.Length)];
                }
            }

            return new string(password);
        }

        /// <summary>
        /// Gets a random object with a real random seed
        /// </summary>
        /// <returns></returns>
        private static Random GetRandom()
        {
            // Use a 4-byte array to fill it with random bytes and convert it then
            // to an integer value.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            new RNGCryptoServiceProvider().GetBytes(randomBytes);

            // Convert 4 bytes into a 32-bit integer value.
            int seed = (randomBytes[0] & 0x7f) << 24 |
                          randomBytes[1] << 16 |
                            randomBytes[2] << 8 |
                              randomBytes[3];

            // Now, this is real randomization.
            return new Random(seed);
        }
    }
}