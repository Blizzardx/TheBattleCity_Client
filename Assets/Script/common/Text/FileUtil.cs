using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NetWork
{
    public class FileUtil
    {
        public static string GetFileMD5(string fileName)
        {
            try
            {
                FileStream file = new FileStream(fileName, System.IO.FileMode.Open);
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(file);
                file.Close();
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
        public static string GetStringMD5(string fileContent)
        {
            try
            {
                MD5 md5 = new MD5CryptoServiceProvider();

                byte[] bs2 = System.Text.Encoding.UTF8.GetBytes(fileContent);
                MemoryStream ms2 = new MemoryStream(bs2);

                byte[] retVal = md5.ComputeHash(ms2);
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
        public static void WriteFile(string filePath, byte[] bytes)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
        }

        public static void DeleteFile(string filePath)
        {
            Console.WriteLine("########   " + filePath);
            if (!File.Exists(filePath))
            {
                return;
            }
            File.Delete(filePath);
        }
        public static ByteBuffer ReadFileToByteArray(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            ByteBuffer buffer = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                int fileLen = (int)fs.Length;
                if (fileLen > 0)
                {
                    buffer = ByteBuffer.Allocate(fileLen);
                    byte[] block = new byte[256];
                    while (true)
                    {
                        int readLen = fs.Read(block, 0, block.Length);
                        if (readLen == 0)
                        {
                            break;
                        }
                        buffer.WriteBytes(block, 0, readLen);

                    }
                }
            }
            return buffer;
        }
        // md5(substr(md5($_G['config']['security']['authkey']), 8).$_G['uid'])
        public static string Md5Encode(string key, int uid)
        {
            string md5Str = GetStringMD5(key);
            string res = GetStringMD5(md5Str.Substring(8) + uid.ToString());
            return res;
        }
    }
}