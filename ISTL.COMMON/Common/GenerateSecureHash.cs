using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ISTL.COMMON.Common
{
    public class GenerateSecureHash
    {
        /// <summary>
        /// Creates the sha1 hash.
        /// </summary>
        /// <param name="byteList">The byte list.</param>
        /// <returns></returns>
        public static string CreateSha1Hash(List<byte[]> byteList)
        {
            SHA1 hash = new SHA1CryptoServiceProvider();
            byte[] hashBytes = new byte[byteList.Sum(a => a != null ? a.Length : 0)];
            byte[] hashResult = null;

            int offset = 0;
            foreach (byte[] bytes in byteList)
            {
                if (bytes != null)
                {
                    System.Buffer.BlockCopy(bytes, 0, hashBytes, offset, bytes.Length);
                    offset += bytes.Length;
                }
            }

            hashResult = hash.ComputeHash(hashBytes);
            return Convert.ToBase64String(hashResult);
        }

        /// <summary>
        /// Creates the sha1 hash.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        public static string CreateSha1Hash(byte[] bytes)
        {
            SHA1 hash = new SHA1CryptoServiceProvider();
            byte[] hashBytes = new byte[bytes.Length];
            byte[] hashResult = null;

            System.Buffer.BlockCopy(bytes, 0, hashBytes, 0, bytes.Length);

            hashResult = hash.ComputeHash(hashBytes);
            return Convert.ToBase64String(hashResult);
        }
    }
}
