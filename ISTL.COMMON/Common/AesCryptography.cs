using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using NLog;
using System.IO;
using GlobalsCommon;

namespace ISTL.COMMON.Common
{
    /// <summary>
    /// http://code.google.com/p/aes-encryption-samples/wiki/CipherModes
    /// </summary>
    public class AesCryptography
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /* blocked
        private const string SECRETKEY = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        private const string INITVECTOR = "@1B2c3D4e5F6g7H8"; // must be 16 bytes        
        private const int GlobalsCommon.AesCryptography.KEYSIZE = 128; // Can be 192 or 256
        */

        private const PaddingMode PADDING = PaddingMode.PKCS7; // Set Padding Mode It May be ISO10126, ANSIX923, Zeros etc. Default use PKCS7 

        /// <summary>
        /// Gets the AESCBC cipher.
        /// </summary>
        /// <param name="keyBytes">The key bytes.</param>
        /// <param name="iVBytes">The i V bytes.</param>
        /// <returns></returns>
        private static RijndaelManaged GetAESCBCCipher(byte[] keyBytes, byte[] iVBytes)
        {
            RijndaelManaged cipher = new RijndaelManaged();
            cipher.KeySize = Convert.ToInt32(AesCryptographyConstant.KEYSIZE);
            cipher.BlockSize = Convert.ToInt32(AesCryptographyConstant.KEYSIZE);
            cipher.Mode = CipherMode.CBC;
            cipher.Padding = PADDING;
            cipher.IV = iVBytes;
            cipher.Key = keyBytes;

            return cipher;
        }

        /// <summary>
        /// Encrypts the specified key bytes.
        /// </summary>
        /// <param name="keyBytes">The key bytes.</param>
        /// <param name="iVBytes">The i V bytes.</param>
        /// <param name="messageBytes">The message bytes.</param>
        /// <returns></returns>
        private static byte[] Encrypt(byte[] keyBytes, byte[] iVBytes, byte[] messageBytes)
        {
            RijndaelManaged cipher = GetAESCBCCipher(keyBytes, iVBytes);

            //Get an encryptor.
            ICryptoTransform encryptor = cipher.CreateEncryptor();
            //Encrypt the data.
            MemoryStream msEncrypt = new MemoryStream();
            CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            //Write all data to the crypto stream and flush it.
            csEncrypt.Write(messageBytes, 0, messageBytes.Length);
            csEncrypt.FlushFinalBlock();

            byte[] encryptedText = msEncrypt.ToArray();
            csEncrypt.Close();
            msEncrypt.Close();

            return encryptedText;
        }

        /// <summary>
        /// Encrypts to string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string EncryptToString(object obj)
        {
            if (obj == null) { return null; }

            UTF8Encoding utf8Encoding = new UTF8Encoding();
            byte[] keyBytes = utf8Encoding.GetBytes(AesCryptographyConstant.SECRETKEY);
            byte[] iVBytes = utf8Encoding.GetBytes(AesCryptographyConstant.INITVECTOR);
            byte[] mesageBytes = null;

            try
            {
                if (obj.GetType().Equals(typeof(string)))
                {
                    mesageBytes = utf8Encoding.GetBytes(((string)obj));
                    return Convert.ToBase64String(Encrypt(keyBytes, iVBytes, mesageBytes));
                }
                else if (obj.GetType().Equals(typeof(byte[])))
                {
                    mesageBytes = ((byte[])obj);
                    return Convert.ToBase64String(Encrypt(keyBytes, iVBytes, mesageBytes));
                }
                else 
                {
                    mesageBytes = utf8Encoding.GetBytes((obj.ToString()));
                    return Convert.ToBase64String(Encrypt(keyBytes, iVBytes, mesageBytes));
                }
            }
            catch (Exception x)
            {
                logger.Error("Error during Encrypt Data.\n" + x);
            }
            return null;
        }

        /// <summary>
        /// Encrypts to byte.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static byte[] EncryptToByte(object obj)
        {
            if (obj == null) { return null; }

            UTF8Encoding utf8Encoding = new UTF8Encoding();
            byte[] keyBytes = utf8Encoding.GetBytes(AesCryptographyConstant.SECRETKEY);
            byte[] iVBytes = utf8Encoding.GetBytes(AesCryptographyConstant.INITVECTOR);
            byte[] mesageBytes = null;

            try
            {
                if (obj.GetType().Equals(typeof(string)))
                {
                    mesageBytes = utf8Encoding.GetBytes(((string)obj));
                    return Encrypt(keyBytes, iVBytes, mesageBytes);

                }
                else if (obj.GetType().Equals(typeof(byte[])))
                {
                    mesageBytes = ((byte[])obj);
                    return Encrypt(keyBytes, iVBytes, mesageBytes);
                }
            }
            catch (Exception x)
            {
                logger.Error("Error during Encrypt Data.\n" + x);
            }
            return null;
        }

        /// <summary>
        /// Decrypts to string.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static string DecryptToString(object obj)
        {
            if (obj == null) { return null; }

            UTF8Encoding utf8Encoding = new UTF8Encoding();
            byte[] keyBytes = utf8Encoding.GetBytes(AesCryptographyConstant.SECRETKEY);
            byte[] iVBytes = utf8Encoding.GetBytes(AesCryptographyConstant.INITVECTOR);
            byte[] mesageBytes = null;

            try
            {
                if (obj.GetType().Equals(typeof(string)))
                {
                    mesageBytes = Convert.FromBase64String(((string)obj));
                }
                else if (obj.GetType().Equals(typeof(byte[])))
                {
                    mesageBytes = ((byte[])obj);
                }

                RijndaelManaged cipher = GetAESCBCCipher(keyBytes, iVBytes);

                //Get an encryptor.
                ICryptoTransform decryptor = cipher.CreateDecryptor();
                //Now decrypt the previously encrypted message using the decryptor
                MemoryStream msDecrypt = new MemoryStream(mesageBytes);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[mesageBytes.Length];

                //Read the data out of the crypto stream.
                int decryptedByteCount = csDecrypt.Read(plainTextBytes, 0, plainTextBytes.Length);
                csDecrypt.Close();
                msDecrypt.Close();

                // Convert decrypted data into a string. 
                return utf8Encoding.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (Exception x)
            {
                logger.Error("Error during Decrypt Data.\n" + x);
            }
            return null;
        }

        /// <summary>
        /// Decrypts to byte.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        public static byte[] DecryptToByte(object obj)
        {
            if (obj == null) { return null; }

            UTF8Encoding utf8Encoding = new UTF8Encoding();
            byte[] keyBytes = utf8Encoding.GetBytes(AesCryptographyConstant.SECRETKEY);
            byte[] iVBytes = utf8Encoding.GetBytes(AesCryptographyConstant.INITVECTOR);
            byte[] mesageBytes = null;

            try
            {
                if (obj.GetType().Equals(typeof(string)))
                {
                    mesageBytes = Convert.FromBase64String(((string)obj));
                }
                else if (obj.GetType().Equals(typeof(byte[])))
                {
                    mesageBytes = ((byte[])obj);
                }

                RijndaelManaged cipher = GetAESCBCCipher(keyBytes, iVBytes);

                //Get an encryptor.
                ICryptoTransform decryptor = cipher.CreateDecryptor();
                //Now decrypt the previously encrypted message using the decryptor
                MemoryStream msDecrypt = new MemoryStream(mesageBytes);
                CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[mesageBytes.Length];

                //Read the data out of the crypto stream.
                int decryptedByteCount = csDecrypt.Read(plainTextBytes, 0, plainTextBytes.Length);

                csDecrypt.Close();
                msDecrypt.Close();

                return plainTextBytes;
            }
            catch (Exception x)
            {
                logger.Error("Error during Decrypt Data.\n" + x);
            }
            return null;
        }
    }
}
