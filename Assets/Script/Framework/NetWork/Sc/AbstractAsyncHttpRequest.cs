using Framework.Async;
using NetWork.Auto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Thrift.Protocol;
using UnityEngine;

namespace NetWork
{
    public abstract class AbstractAsyncHttpRequest<REQ, RESP> : IAsyncTask
        where REQ : TBase
        where RESP : TBase
    {
        private bool running = false;

        public bool Running
        {
            get { return running; }
        }
        private Header header = new Header();
        protected TBase req;
        protected ResponseMessage responseMessage;
        public AbstractAsyncHttpRequest(REQ req)
        {
            this.req = req;
            this.header.OrderId = HttpManager.Instance.GetNextOrderId();
            this.header.Sk = HttpManager.Instance.Sk;
        }

        public AsyncState BeforeAsyncTask()
        {
            running = true;
            if (IsBlock())
            {
                //TODO 给界面蒙上loading
                WindowManager.Instance.OpenWindow(WindowID.Loading);

            }

            return AsyncState.DoAsync;
        }

        public AsyncState DoAsyncTask()
        {
            if(AppManager.Instance.m_bIsShowDebugMsg)
            {
                Debuger.Log("send msg: " + req.ToString());
            }
            responseMessage = HttpManager.Instance.PostMessage(header, req);
            
            return AsyncState.AfterAsync;
        }

        public AsyncState AfterAsyncTask()
        {
            running = false;
            if (IsBlock())
            {
                //TODO 去掉loading
                WindowManager.Instance.HideWindow(WindowID.Loading);

            }
            if (responseMessage.Ex != null)
            {
                MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_ENABLE_BLOCK,null));

                //TODO 异常处理
                TipManager.Instance.Alert("提示", "登录已过期，请重新登录", "确定", "退出", (res) =>
                {
                    MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_DISABLE_BLOCK, null));
                    if (res)
                    {
                        StageManager.Instance.ChangeState(GameStateType.LoginState);
                    }
                    else
                    {
                        UnityEngine.Application.Quit();
                    }
                });
                return AsyncState.Done;
            }
            if (responseMessage.StatusCode != System.Net.HttpStatusCode.OK)
            {
                if((int)(responseMessage.StatusCode) == 403)
                {
                    MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_ENABLE_BLOCK, null));
                    //TODO 状态码不正确
                    TipManager.Instance.Alert("提示", "登录已过期，请重新登录","重新登录", (res) =>
                    {
                        MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_DISABLE_BLOCK, null));
                        if (res)
                        {
                            StageManager.Instance.ChangeState(GameStateType.LoginState);
                        }
                    });
                }
                else
                {
//                     MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_ENABLE_BLOCK, null));
//                     //TODO 状态码不正确
//                     TipManager.Instance.Alert("提示", "网络异常，请重试", "确定", "重新登录", (res) =>
//                     {
//                         MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_DISABLE_BLOCK, null));
//                         if (!res)
//                         {
//                             StageManager.Instance.ChangeState(GameStateType.LoginState);
//                         }
//                     });
                }
                
                return AsyncState.Done;
            }

            if (AppManager.Instance.m_bIsShowDebugMsg)
            {
                Debuger.Log("rec msg: " + responseMessage.Message.ToString());
                for (int i = 0; responseMessage.EventList.Events != null && i < responseMessage.EventList.Events.Count; ++i)
                {
                    Debuger.Log("rec event: " + responseMessage.EventList.Events[i].ToString());
                }
            }

            if (responseMessage.EventList != null)
            {
                //统一事件处理
                MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_MESSAGE_EVENT_LIST, responseMessage.EventList));

            }

            AfterRequest((RESP)responseMessage.Message);

            return AsyncState.Done;
        }

        /// <summary>
        /// 尝试这次请求
        /// </summary>
        public void TryRequest()
        {
            if (running)
            {
                return;
            }
            AsyncManager.Instance.ExecuteAsyncTask(this);
        }

        protected abstract void AfterRequest(RESP resp);

        /// <summary>
        /// 是否阻塞请求
        /// </summary>
        /// <returns></returns>
        protected abstract bool IsBlock();
    }
}
