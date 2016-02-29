using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Core.Utils
{
    public class FileUtils
    {
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

        public static string LoadStringFile(string path, bool isEncrypt = false)
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
                UnityEngine.Debug.LogWarning("LoadStringFile. Path Inexistent: " + path);
            }
           
            return content;
        }

        public static void ensureFolder(string path)
        {
            string folder = Path.GetDirectoryName(path);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }

        public static void SaveStringFile(string path, string content, bool isEncrypt = false)
        {
            ensureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.SetLength(0);
            var sw = new StreamWriter(fs);
            sw.Write(content);
            sw.Dispose();
            fs.Dispose();
        }
        public static void SaveFileByteArray(string path, byte[] bytes)
        {
            ensureFolder(path);
            FileStream fs = File.OpenWrite(path);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            fs.Dispose();
        }
    }
}
