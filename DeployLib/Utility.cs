using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Collections;

namespace DeployLib
{
    public static class Utility
    {
        /// <summary>
        /// 正则式判断，是否为合法文件格式,是则返回Ture,否则返回False;
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) { return false; }
            if (fileName.Length > 255) { return false; }
            else
            {
                Regex regex = new Regex(@"/|\\|<|>|\*|\?");
                var match = regex.IsMatch(fileName);
                return !match;
            }
        }
        static Utility()
        {
            _des = new DESCryptoServiceProvider();
            _encryptor = _des.CreateEncryptor();
            _decryptor = _des.CreateDecryptor();
        }

        static DESCryptoServiceProvider _des;
        static ICryptoTransform _encryptor;
        static ICryptoTransform _decryptor;

        public static string Encrypt(string plainText)
        {
            var inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedData = null;
            //using (var outputStream = new MemoryStream())
            //{
            //    using (var inputStream = new CryptoStream(outputStream, _encryptor, CryptoStreamMode.Write))
            //    {
            //        inputStream.Write(inputBytes, 0, inputBytes.Length);
            //        inputStream.FlushFinalBlock();
            //        encryptedData = outputStream.ToArray();
            //    }
            //}
            using (var outputStream = new MemoryStream())
            {
                var inputStream = new CryptoStream(outputStream, _encryptor, CryptoStreamMode.Write);
                {
                    inputStream.Write(inputBytes, 0, inputBytes.Length);
                    inputStream.FlushFinalBlock();
                    encryptedData = outputStream.ToArray();
                }
            }

            var result = Convert.ToBase64String(encryptedData);
            return result;
        }

        public static string Decrypt(string encryptedText)
        {
            byte[] inputBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = null;
            using (var outputStream = new MemoryStream())
            {
                var inputStream = new CryptoStream(outputStream, _decryptor, CryptoStreamMode.Write);
                {
                    inputStream.Write(inputBytes, 0, inputBytes.Length);
                    inputStream.FlushFinalBlock();
                    decryptedBytes = outputStream.ToArray();
                }
            }
            var result = Encoding.UTF8.GetString(decryptedBytes);
            return result;
        }

        const int tabspaceUnit = 4;
        public static string GetDetailedException(this Exception e, int prespace = 0)
        {
            if (e == null) { return ""; }
            var builder = new StringBuilder();
            builder.Tabspace(prespace); builder.AppendFormat("{0}:", DateTime.Now.ToString("yyyyMMdd-HHmmss")); builder.AppendLine();

            prespace += tabspaceUnit;
            builder.Tabspace(prespace); builder.AppendFormat("Message: {0}", e.Message); builder.AppendLine();

            builder.Tabspace(prespace); builder.AppendFormat("Source: {0}", e.Source); builder.AppendLine();

            builder.Tabspace(prespace); builder.AppendFormat("Target Site: {0}", e.TargetSite); builder.AppendLine();

            builder.Tabspace(prespace); builder.AppendFormat("Stack trace: {0}", e.StackTrace); builder.AppendLine();

            GetDetailedExceptionData(e, prespace, builder);

            builder.Tabspace(prespace); builder.AppendFormat("Help link: {0}", e.HelpLink); builder.AppendLine();

            if (e.InnerException != null)
            {
                builder.Append(e.InnerException.GetDetailedException(prespace));
            }
            prespace -= tabspaceUnit;

            return builder.ToString();
        }

        private static void GetDetailedExceptionData(Exception e, int tabspace, StringBuilder builder)
        {
            builder.Tabspace(tabspace); builder.AppendFormat("Data:"); builder.AppendLine();

            tabspace += tabspaceUnit;
            foreach (var item in e.Data)
            {
                var entry = (DictionaryEntry)item;
                builder.Tabspace(tabspace); builder.AppendFormat("{0}:{1}", entry.Key, entry.Value); builder.AppendLine();
            }
            tabspace -= tabspaceUnit;
        }

        private static void Tabspace(this StringBuilder builder, int tabspace)
        {
            if (builder == null) { return; }

            for (int i = 0; i < tabspace; i++)
            {
                builder.Append(" ");
            }
        }
    }
}
