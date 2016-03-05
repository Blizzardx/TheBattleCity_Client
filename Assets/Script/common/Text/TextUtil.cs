using System;
using System.Text;
using System.IO;
using System.Collections;
using UnityEngine;


    /// <summary>
    /// 文本处理类工具 : 用法顾名思义
    /// </summary>
    public class TextUtil
    {
        private const string TAG = "TextUtil";

        public static string ToBase64String(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                Debuger.LogWarning(TAG + ".ToBase64String, text is null.");
                return "";
            }

            Debuger.Log(TAG + ".ToBase64String, before text: " + text);

            byte[] url = Encoding.UTF8.GetBytes(text);
            string result = Convert.ToBase64String(url);

            Debuger.Log(TAG + ".ToBase64String, after text: " + result);

            return result;
        }
        public static string ToSHA1String(string text)
        {
            //		if (string.IsNullOrEmpty (text)) {
            //			Debuger.LogWarning (TAG + ".ToSHA1String, text is null.");
            //			return "";
            //		}
            //		
            //		Debuger.Log (TAG + ".ToSHA1String, before text: " + text);
            //		byte[] StrRes = Encoding.Default.GetBytes (text);
            //		HashAlgorithm iSHA = new SHA1CryptoServiceProvider ();
            //		StrRes = iSHA.ComputeHash (StrRes);
            //		StringBuilder EnText = new StringBuilder ();
            //		foreach (byte iByte in StrRes) {
            //			EnText.AppendFormat ("{0:x2}", iByte);
            //		}
            //		
            //		string result = EnText.ToString ();
            //		Debuger.Log (TAG + ".ToSHA1String, after text: " + result);
            //		return result;
            return "";
        }
        public static string GetStringArray(params object[] parameters)
        {
            string resultParam = "";
            string tempParam = "";
            foreach (var item in parameters)
            {
                tempParam = item as string;
                if (tempParam != null)
                {
                    resultParam += tempParam + ",";
                }
            }

            Debuger.Log("GetStringArray, resultParam: " + resultParam);
            return resultParam;
        }
        /// <summary>
        /// 判断对象数组是否为空.
        /// </summary>
        public static bool IsArrayEmpty(params object[] parameters)
        {
            if (parameters == null || parameters.Length == 0)
            {
                Debuger.LogError("IsArrayEmpty, parameters is null...");
                return true;
            }

            return false;
        }
        public static ArrayList ReadFile(string path, string name)
        {
            StreamReader sr = null;
            try
            {
                sr = File.OpenText(path + "//" + name);
            }
            catch
            {
                //路径与名称未找到文件则直接返回空
                return null;
            }
            string line;
            ArrayList arrlist = new ArrayList();
            while ((line = sr.ReadLine()) != null)
            {
                //一行一行的读取
                //将每一行的内容存入数组链表容器中
                arrlist.Add(line);
            }

            //close stream
            sr.Close();
            sr.Dispose();
            
            return arrlist;
        }
        public static void WriteToLine(string path, string name, string line)
        {
            //文件流信息
            StreamWriter sw;
            FileInfo t = new FileInfo(path + "//" + name);
            if (!t.Exists)
            {
                //如果此文件不存在则创建
                sw = t.CreateText();
            }
            else
            {
                //如果此文件存在则打开
                sw = t.AppendText();
            }

            //以行的形式写入信息
            sw.WriteLine(line);
            
            sw.Close();
            sw.Dispose();
        }
        public static void WriteFile(string path, string name, ArrayList lineArray)
        {
            //文件流信息
            FileInfo t = new FileInfo(path + "//" + name);
            StreamWriter sw = t.CreateText();

            //写入信息
            foreach (string elem in lineArray)
            {
                sw.WriteLine(elem);
            }
            sw.Close();
            sw.Dispose();
        }
        public static bool EditoFileLine(string path,string name,int editoLineId,string newLine)
        {
            ArrayList content = ReadFile(path, name);

            //convert to array index
            editoLineId -= 1;
            
            if( null == content )
            {
                return false;
            }
            if( editoLineId < 0 || editoLineId >= content.Count)
            {
                return false;
            }
            content[editoLineId] = newLine;
            WriteFile(path,name,content);
            return true;
        }
        public static bool DeleteFile(string path, string name)
        {
            try
            {
                File.Delete(path + "//" + name);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
