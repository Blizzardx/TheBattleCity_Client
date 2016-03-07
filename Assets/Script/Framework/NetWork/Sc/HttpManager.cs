using Communication;
using NetWork.Auto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Thrift.Protocol;
using UnityEngine;

namespace NetWork
{
    public class HttpManager : Singleton<HttpManager>
    {
        private const string URL = "http://dev.cytxcn.com/mmAdv/game/faced.do";
        //private const string URL = "http://192.168.1.200:8080/cgz-game-web/game/faced.do";
        public const string CHAT_DOWNLOAD_URL = "http://dev.cytxcn.com/voice";
        public const string CHAT_UPLOAD_URL = "http://dev.cytxcn.com/game-voice-upload-server/upload.do";
        public string Sk { get; set; }

        private long orderId = 0L;


        public long GetNextOrderId()
        {
            lock (this)
            {
                return orderId++;
            }
        }

        public ResponseMessage PostMessage(Header header, TBase message)
        {
            Stream requestStream = null;
            Stream responseStream = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(URL) as HttpWebRequest;
                request.Method = "POST";
                //request.ContentType = "application/octet-stream; charset=utf-8";
                request.Timeout = 5000;

                byte[] bodyBytes = Encode(header, message);
                request.ContentLength = bodyBytes.Length;

                requestStream = request.GetRequestStream();
                
                //request.ContentLength = bodyBytes.Length;

                requestStream.Write(bodyBytes, 0, bodyBytes.Length);
                requestStream.Flush();

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    ResponseMessage responseMessage = new ResponseMessage();
                    responseMessage.StatusCode = response.StatusCode;
                    return responseMessage;
                }

                responseStream = response.GetResponseStream();


                return Decode(responseStream);
            }
            catch (Exception e)
            {
                ResponseMessage responseMessage = new ResponseMessage();
                if (e is WebException)
                {
                    WebException webEx = e as WebException;
                    if (webEx.Response is HttpWebResponse)
                    {
                        HttpWebResponse response = webEx.Response as HttpWebResponse;
                        
                        responseMessage.StatusCode = response.StatusCode;
                        
                    }
                    Debug.LogException(e);
                }
                else
                {
                    responseMessage.Ex = e;
                }
                return responseMessage;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                }
                if (responseStream != null)
                {
                    responseStream.Close();
                }
            }
        }

        private byte[] Encode(Header header, TBase message)
        {
            byte[] headerBytes = ThriftSerialize.Serialize(header);

            byte[] messageBytes = ThriftSerialize.Serialize(message);

            ByteBuffer buffer = ByteBuffer.Allocate(512);

            buffer.WriteInt(ThriftMessageHelper.GetMessageId(message));
            buffer.WriteInt(headerBytes.Length);
            buffer.WriteBytes(headerBytes);
            buffer.WriteInt(messageBytes.Length);
            buffer.WriteBytes(messageBytes);

            return buffer.ToArray();
        }

        private ResponseMessage Decode(Stream responseStream)
        {
            ByteBuffer buffer = ByteBuffer.Allocate(512);
            byte[] tempBytes = new byte[256];
            while (true)
            {
                int readLen = responseStream.Read(tempBytes, 0, tempBytes.Length);
                if (readLen <= 0)
                {
                    break;
                }
                buffer.WriteBytes(tempBytes);
            }

            ResponseMessage responseMessage = new ResponseMessage();
            responseMessage.MessageId = buffer.ReadInt();
            responseMessage.StatusCode = HttpStatusCode.OK;

            TBase message = ThriftMessageHelper.GetResponseMessage(responseMessage.MessageId);
            if (message == null)
            {
                Debuger.LogError("don't support response messageId:" + responseMessage.MessageId);
                return null;
            }
            
            byte[] headerBytes = new byte[buffer.ReadInt()];
            buffer.ReadBytes(headerBytes, 0, headerBytes.Length);

            byte[] messageBytes = new byte[buffer.ReadInt()];
            buffer.ReadBytes(messageBytes, 0, messageBytes.Length);

            byte[] eventListBytes = new byte[buffer.ReadInt()];
            buffer.ReadBytes(eventListBytes, 0, eventListBytes.Length);

            responseMessage.Header = new Header();
            ThriftSerialize.DeSerialize(responseMessage.Header, headerBytes);

            responseMessage.Message = message;
            ThriftSerialize.DeSerialize(message, messageBytes);

            //responseMessage.EventList = new MEventList();
            //ThriftSerialize.DeSerialize(responseMessage.EventList, eventListBytes);


            return responseMessage;
        }

        #region donwload

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

            void OnProcess(byte[] buffer, object param);

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

                    requestState.downloadCallback.OnProcess(buffer, requestState.param);
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

        #endregion
    }
}
