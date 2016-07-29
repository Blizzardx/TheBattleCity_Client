using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Common.Tool
{
    public class FileUtils
    {
        #region md5 option
        private static MD5CryptoServiceProvider md5Generator = new MD5CryptoServiceProvider();
        public static string GetMD5HashFromFile(string fileName)
        {
            try
            {
                FileStream inFile = new FileStream(fileName, FileMode.Open);
                byte[] retVal = md5Generator.ComputeHash(inFile);
                inFile.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        public static string GetMD5Str(string str)
        {
            byte[] encryptedBytes = md5Generator.ComputeHash(Encoding.ASCII.GetBytes(str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        public static string GetByteMD5(byte[] fileContent)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fileContent);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("GetMD5HashFromFile() fail,error:" + ex.Message);
            }
        }
        #endregion

        #region file option
        public static void DeleteFile(string filePath)
        {
            Console.WriteLine("########   " + filePath);
            if (!File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
        }
        public static void EnsureFolder(string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
        public static void ClearFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                
            }
            Directory.CreateDirectory(path);
        }
        #endregion

        #region read
        public static string ReadStringFile(string path, bool isEncrypt = false)
        {
            string content = "";
            if (File.Exists(path))
            {
                try
                {
                    StreamReader sr = File.OpenText(path);
                    content = sr.ReadToEnd();
                    sr.Dispose();
                }
                catch (Exception e)
                {
                    content = "";
                }
            }
            else
            {
                Debug.LogWarning("LoadStringFile. Path Inexistent: " + path);
            }

            return content;
        }
        public static byte[] ReadByteFile(string path)
        {
            byte[] res = null;
            if (File.Exists(path))
            {
                try
                {
                    FileStream sr = File.OpenRead(path);
                    res = new byte[sr.Length];
                    sr.Read(res, 0, res.Length);
                    sr.Dispose();
                }
                catch (Exception e)
                {
                    res = null;
                }
            }
            else
            {
                Debug.LogWarning("LoadStringFile. Path Inexistent: " + path);
            }

            return res;
        }
        #endregion

        #region write
        public static void WriteStringFile(string path, string content, bool isEncrypt = false)
        {
            EnsureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.SetLength(0);
            var sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Dispose();
            fs.Dispose();
        }
		public static void WriteStringFile(string path,List<string> contentList,bool isEncrypt = false)
		{
			EnsureFolder(path);
			FileStream fs = File.OpenWrite(path);
		    fs.Seek(fs.Length, 0);
			var sw = new StreamWriter(fs);
			foreach (string line in contentList) 
			{
				sw.WriteLine(line);
			}
			sw.Dispose();
			fs.Dispose();
		}
        public static void WriteByteFile(string path, byte[] bytes)
        {
            EnsureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }
        #endregion

        #region string

        public static bool IsEndof(string origin, string mark)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return false;
            }
            return origin.EndsWith(mark);
        }
        #endregion

        #region other

        public static long GetFileLength(string path)
        {
            if (!File.Exists(path))
            {
                return 0;
            }
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Length;
        }
        #endregion
    }
}
