using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using Common.Tool;
using UnityEngine;

namespace Framework.Network
{
    public class HttpManager:Singleton<HttpManager>
    {
        #region download

        private static readonly Encoding encoding = Encoding.UTF8;

        public class RequestState
        {
            // This class stores the State of the request.
            public const int BUFFER_SIZE = 1024 * 64;
            public byte[] BufferRead;
            public HttpWebRequest request;
            public HttpWebResponse response;
            public Stream streamResponse;
            public DownloadCallback downloadCallback;
            public FileStream continueFileStream;
            public object param;
            public RequestState()
            {
                BufferRead = new byte[BUFFER_SIZE];
                request = null;
                streamResponse = null;
            }
        }

        public interface DownloadCallback
        {
            void OnPrepare(long length, FileStream continueFileStream, object param);

            void OnProcess(byte[] buffer,object param);

            void OnComplate(object param);

            void OnError(object param, Exception e);
        }

        public  void BeginDownload(string url, DownloadCallback callback, object param)
        {
            BeginDownload(url, callback, null, param);
        }

        public  void BeginDownload(string url, DownloadCallback callback, FileStream continueFileStream, object param)
        {
            RequestState requestState = new RequestState();
            requestState.downloadCallback = callback;
            requestState.param = param;

            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.Timeout = 10000;
                request.ReadWriteTimeout = 10000;

                if (continueFileStream != null)
                {
                    requestState.continueFileStream = continueFileStream;
                    continueFileStream.Seek(continueFileStream.Length, SeekOrigin.Current);
                    request.AddRange((int)(continueFileStream.Length));
                }

                requestState.request = request;

                IAsyncResult result = request.BeginGetResponse(ResponseCallback, requestState);
            }
            catch (Exception e)
            {
                callback.OnError(param, e);
            }
        }
        public  void ResponseCallback(IAsyncResult ar)
        {

            RequestState requestState = ar.AsyncState as RequestState;
            try
            {
                HttpWebRequest request = requestState.request;

                requestState.response = request.EndGetResponse(ar) as HttpWebResponse;
                Stream stream = requestState.response.GetResponseStream();

                requestState.downloadCallback.OnPrepare(requestState.response.ContentLength, requestState.continueFileStream, requestState.param);

                requestState.streamResponse = stream;
                stream.BeginRead(requestState.BufferRead, 0, RequestState.BUFFER_SIZE, ReadCallBack, requestState);
            }
            catch (Exception e)
            {
                requestState.downloadCallback.OnError(requestState.param, e);
            }
        }
        private  void ReadCallBack(IAsyncResult ar)
        {
            RequestState requestState = ar.AsyncState as RequestState;
            try
            {
                int readLen = requestState.streamResponse.EndRead(ar);
                if (readLen > 0)
                {
                    byte[] buffer = new byte[readLen];
                    Array.Copy(requestState.BufferRead, 0, buffer, 0, readLen);

                    requestState.downloadCallback.OnProcess(buffer,requestState.param);
                    requestState.streamResponse.BeginRead(requestState.BufferRead, 0, RequestState.BUFFER_SIZE, ReadCallBack, requestState);
                }
                else
                {
                    requestState.streamResponse.Close();
                    requestState.response.Close();
                    requestState.downloadCallback.OnComplate(requestState.param);
                }
            }
            catch (Exception e)
            {
                requestState.downloadCallback.OnError(requestState.param, e);
            }
        }
        public bool Download(string url, out Exception error, out List<byte> byteBuffer, FileStream fileStream)
        {
            bool res = true;
            error = null;
            byteBuffer = new List<byte>();
            try
            {
                long bodyLength = 0;
                int recSize =0;
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                request.Timeout = 10000;
                request.ReadWriteTimeout = 10000;
                var response = request.GetResponse();

                bodyLength = int.Parse(response.Headers["Content-Length"]);
                Debug.Log(bodyLength);
                if (null == fileStream)
                {
                    byteBuffer.Capacity = (int)bodyLength;
                }
                using (Stream stream = response.GetResponseStream())
                {
                    int size = 0;
                    while (recSize < bodyLength)
                    {
                        byte[] tmpBytebuffer = new byte[1024];
                        size = stream.Read(tmpBytebuffer, 0, tmpBytebuffer.Length);
                        if (size == 0)
                        {
                            break;
                        }
                        recSize += size;
                        if (null != fileStream)
                        {
                            fileStream.Write(tmpBytebuffer, 0, size);
                        }
                        else
                        {
                            for (int i = 0; i < size; ++i)
                            {
                                byteBuffer.Add(tmpBytebuffer[i]);
                            }
                        }
                    }
                }
                if (recSize != bodyLength)
                {
                    res = false;
                    error = new Exception("not complete");
                }
            }
            catch (Exception e)
            {
                error = e;
                res = false;
            }
            finally
            {
                if (fileStream != null)
                {
                    fileStream.Close();
                }
        }
            return res;
        }
        #endregion
        #region upload
        public string UploadFile(string url, string fileName, byte[] data, List<KeyValuePair<string, object>> paramList)
        {
            Stream responseStream = null;
            try
            {
                // Generate post objects
                Dictionary<string, object> postParameters = new Dictionary<string, object>();
                if (paramList != null)
                {
                    foreach (var elem in paramList)
                    {
                        postParameters.Add(elem.Key, elem.Value);
                    }
                }

                postParameters.Add("file", new FileParameter(data, fileName, "application/octet-stream"));
                postParameters.Add("filename", fileName);
                postParameters.Add("fileformat", "bytes");
                postParameters.Add("path", "config");
                // Create request and receive response
                HttpWebResponse webResponse = MultipartFormDataPost(url, postParameters);


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
                    Console.Write("upload file error ");
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
        private HttpWebResponse MultipartFormDataPost(string postUrl, Dictionary<string, object> postParameters)
        {
            string formDataBoundary = String.Format("----------{0:N}", Guid.NewGuid());
            string contentType = "multipart/form-data; charset=utf-8; boundary=" + formDataBoundary;

            byte[] formData = GetMultipartFormData(postParameters, formDataBoundary);

            return PostForm(postUrl, contentType, formData);
        }
        private HttpWebResponse PostForm(string postUrl, string contentType, byte[] formData)
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
        private byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
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
        #endregion
    }
}
