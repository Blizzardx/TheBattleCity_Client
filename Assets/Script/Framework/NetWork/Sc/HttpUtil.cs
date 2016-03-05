using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;	
using System.Text;

namespace Core
{
    public class HttpUtil
    {
        private static readonly Encoding encoding = Encoding.UTF8;

        public static string HttpGet(string url,string tokenKey,string token)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.CookieContainer = new CookieContainer();

                request.Timeout = 10000;
                request.ReadWriteTimeout = 30000;

                request.Headers.Add(tokenKey,token);
                WebResponse response = request.GetResponse();

                string html = new StreamReader(response.GetResponseStream(), Encoding.UTF8).ReadToEnd();
                return html;
            }
            catch (System.UriFormatException ex)
            {
                
            }
            catch (System.Net.WebException ex)
            {

            }
            return null;
        }
        public static byte[] HttpDownloadFile(string url, string savePath = "")
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Timeout = 10000;
                request.ReadWriteTimeout = 300000;

                WebResponse response = request.GetResponse();

                HttpWebResponse resp = response as HttpWebResponse;
                if ((int)resp.StatusCode != 200)
                {
                    return null;//(int)resp.StatusCode;
                }

                Stream stream = response.GetResponseStream();
                byte[] buff = new byte[256];
                byte[] bytes = new byte[resp.ContentLength];
                int len = 0;
                int lenTotal = 0;
                do
                {
                    lenTotal += len;
                    len = stream.Read(buff, 0, buff.Length);
                    if (len == 0)
                    {
                        break;
                    }
                    Array.Copy(buff, 0, bytes, lenTotal, len);

                } while (len != 0);

                if( !string.IsNullOrEmpty(savePath))
                {
                    using (FileStream fs = new FileStream(savePath, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                        fs.Flush();
                        Console.WriteLine(" write to file " + savePath);
                    }
                }
                return bytes;
            }
            catch (System.UriFormatException ex)
            {

            }
            catch (System.Net.WebException ex)
            {

            }
            return null;
        }
        public static int HttpHead(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Credentials = CredentialCache.DefaultCredentials;
                request.Method = "HEAD";
                request.Timeout = 10000;
                request.ReadWriteTimeout = 10000;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                return (int)response.StatusCode;
            }
            catch (System.UriFormatException ex)
            {

            }
            catch (System.Net.WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    return (int)((HttpWebResponse)e.Response).StatusCode;
                }
            }
            return -1;
        }
        public static string HttpPost(string url, string data,string tokenKey,string token)
        {
            HttpWebRequest request = null;
            StreamReader streamReader = null;
            Stream responseStream = null;
            try
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                request.CookieContainer = new CookieContainer();
                request.Headers.Add(tokenKey,token);
                request.Timeout = 20000;
                request.ReadWriteTimeout = 30000;
                request.Accept = "application/json, text/javascript, */* q=0.01";
                //request.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                //request.Headers["Content-Type"] = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Method = "POST";
                byte[] postData = Encoding.UTF8.GetBytes(data);
                request.ContentLength = postData.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(postData, 0, postData.Length);
                //获取响应
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                responseStream = response.GetResponseStream();
                //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
                if (response.Headers["Content-Encoding"] != null && response.Headers["Content-Encoding"].ToLower().Contains("gzip"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }

                streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string retString = streamReader.ReadToEnd();
                return retString;
            }
            catch (Exception e)
            {
                return null;
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
            }
        }
        public static string UploadFile(string url, string fileName,byte[] data,List<KeyValuePair<string,object>> paramList,string tokenKey,string token)
        {            
			Stream responseStream = null;
            try
            {               
                // Generate post objects
                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                if( paramList != null )
                {
                    foreach(var elem in paramList)
                    {
                        postParameters.Add(elem.Key,elem.Value);
                    }
                }

                postParameters.Add("file", new HttpUtil.FileParameter(data, fileName, "application/octet-stream"));

                // Create request and receive response
                HttpWebResponse webResponse = HttpUtil.MultipartFormDataPost(url, postParameters,tokenKey,token);


                StreamReader streamReader = null;

                responseStream = webResponse.GetResponseStream();

                if (webResponse.Headers["Content-Encoding"] != null && webResponse.Headers["Content-Encoding"].ToLower().Contains("gzip"))
                {
                    responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
                }

                streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string retString = streamReader.ReadToEnd();

                if (webResponse.StatusCode != HttpStatusCode.OK)
                {
                    Console.Write("upload file error " );
                }

                return retString;
            }
            catch (WebException e)
            {
                //                if (e.Status == WebExceptionStatus.ProtocolError)
                //                {
                //                    return (int)((HttpWebResponse)e.Response).StatusCode;
                //                }
                //                return -1;
				StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
				string retString = streamReader.ReadToEnd();

                return "";
            }
        }
        private static HttpWebResponse MultipartFormDataPost(string postUrl, Dictionary<string, object> postParameters,string tokenKey,string token)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; charset=utf-8; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, contentType, formData,tokenKey,token);
        }
        private static HttpWebResponse PostForm(string postUrl, string contentType, byte[] formData,string tokenKey,string token)
        {
            HttpWebRequest request = WebRequest.Create(postUrl) as HttpWebRequest;
            request.CookieContainer = new CookieContainer();
            //request.Headers.Add(tokenKey, token);
            request.Timeout = 60000;
            if (request == null)
            {
                throw new NullReferenceException("request is not a http request");
            }

            // Set up the request properties.
            request.Method = "POST";
            request.ContentType = contentType;
            request.ContentLength = formData.Length;

            // You could add authentication here as well if needed:
            // request.PreAuthenticate = true;
            // request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
            // request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(System.Text.Encoding.Default.GetBytes("username" + ":" + "password")));

            // Send the form data to the request.
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(formData, 0, formData.Length);
                requestStream.Close();
            }

            return request.GetResponse() as HttpWebResponse;
        }
        private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
        {
            Stream formDataStream = new System.IO.MemoryStream();
            bool needsCLRF = false;

            foreach (var param in postParameters)
            {
                // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                // Skip it on the first parameter, add it to subsequent parameters.
                if (needsCLRF)
                    formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));

                needsCLRF = true;

                if (param.Value is FileParameter)
                {
                    FileParameter fileToUpload = (FileParameter)param.Value;

                    // Add just the first part of this param, since we will write the file data directly to the Stream
                    string header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                        boundary,
                        param.Key,
                        fileToUpload.FileName ?? param.Key,
                        fileToUpload.ContentType ?? "application/octet-stream");

                    formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                    // Write the file data directly to the Stream, rather than serializing it to a string.
                    formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                }
                else
                {
                    string postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                        boundary,
                        param.Key,
                        param.Value);
                    formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                }
            }

            // Add the end of the request.  Start with a newline
            string footer = "\r\n--" + boundary + "--\r\n";
            formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));

            // Dump the Stream into a byte[]
            formDataStream.Position = 0;
            byte[] formData = new byte[formDataStream.Length];
            formDataStream.Read(formData, 0, formData.Length);
            formDataStream.Close();

            return formData;
        }
        public class FileParameter
        {
            public byte[] File { get; set; }
            public string FileName { get; set; }
            public string ContentType { get; set; }
            public FileParameter(byte[] file) : this(file, null) { }
            public FileParameter(byte[] file, string filename) : this(file, filename, null) { }
            public FileParameter(byte[] file, string filename, string contenttype)
            {
                File = file;
                FileName = filename;
                ContentType = contenttype;
            }
        }
    }
}

