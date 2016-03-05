using System;
using NetWork.Auto;
using Thrift.Protocol;
using UnityEngine;
using System.Collections;

public class SystemMsgHandler:Singleton<SystemMsgHandler>
{
    #region public interface

    public void RegisterSystemMsg()
    {
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CLOSE, SocketClosed);
        MessageManager.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_TIMEOUT, SocketConnetTimeOut);
        //MessageManager.Instance.RegistMessage(MessageIdConstants.SC_SYSTEM_INFO, SystemInfo);
    }
    #endregion

    #region system message

    private void SocketClosed(MessageObject msg)
    {
        Debuger.Log("Socket closed");
        CollectionManager.Instance.SocketClosed();
    }
    private void SocketConnetTimeOut(MessageObject msg)
    {
        Debuger.Log("connet time out");
        CollectionManager.Instance.TimeOut();
    }
    private void SystemInfo(MessageObject msg)
    {
        TBase msgBody = (TBase) (msg.msgValue);
        Debuger.Log(msgBody.ToString());
    }
    #endregion
}
