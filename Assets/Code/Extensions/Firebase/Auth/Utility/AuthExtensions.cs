using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Firebase.Auth
{
    public struct NonceUtility
    {
        public string Nonce { get; private set; }
        public string RowNonce { get; private set; }

        public NonceUtility(int lenght)
        {
            RowNonce = AuthExtensions.RandomString(lenght);
            Nonce = AuthExtensions.GenerateSHA256Nonce(RowNonce);
        }
    }
    public static class AuthExtensions
    {
        public const string charset = "0123456789ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxyz-._";

        public static string RandomString(int length)
        {
            if (length <= 0) throw new Exception("Expected nonce to have positive length");

            RNGCryptoServiceProvider secureRandomNumber = new RNGCryptoServiceProvider();
            string result = string.Empty;
            int remainingLength = length;

            byte[] randomNumberHolder = new byte[1];
            while (remainingLength > 0)
            {
                List<int> randomNumbers = new List<int>(16);

                for (var randomNumberCount = 0; randomNumberCount < 16; randomNumberCount++)
                {
                    secureRandomNumber.GetBytes(randomNumberHolder);
                    randomNumbers.Add(randomNumberHolder[0]);
                }

                for (int i = 0; i < randomNumbers.Count; i++)
                {
                    if (remainingLength == 0) break;

                    int randomNumber = randomNumbers[i];
                    if (randomNumber < charset.Length)
                    {
                        result += charset[randomNumber];
                        remainingLength--;
                    }
                }
            }

            return result;
        }
        public static string GenerateSHA256Nonce(string rawNonce)
        {
            SHA256Managed sha = new SHA256Managed();
            byte[] utf8RawNonce = Encoding.UTF8.GetBytes(rawNonce);
            byte[] hash = sha.ComputeHash(utf8RawNonce);

            string result = string.Empty;
            for (int i = 0; i < hash.Length; i++)
            {
                result += hash[i].ToString("x2");
            }

            return result;
        }
    }
}