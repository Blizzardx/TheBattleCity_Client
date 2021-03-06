﻿using Framework.Common;
using Framework.Event;
using Framework.Message;
using Framework.Network;
using UnityEngine;

partial class MainMenuHandler:HandlerBase
{
    protected override void OnCreate()
    {
        base.OnCreate();

        MessageDispatcher.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECTED, SocketConnected);
        MessageDispatcher.Instance.RegistMessage(ClientCustomMessageDefine.C_SOCKET_CONNECT_ERROR, SocketConnectError);
    }

    public bool ConnectToServer()
    {
        if (NetworkManager.Instance.GetNetworkStatus() == SocketClient.SocketStatus.Connecting)
        {
            return false;
        }
        Debug.Log("try connect to server");
        // try connet to server 
        //NetworkManager.Instance.Connect("120.25.176.42", 1234);
        //NetworkManager.Instance.Connect("192.168.127.128", 8000);
        //NetworkManager.Instance.Connect("127.0.0.1", 1234);
        NetworkManager.Instance.Connect("192.168.1.8", 1234);
        return true;
    }
    private void SocketConnectError(IMessage obj)
    {
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.ConnectError, null);
    }
    private void SocketConnected(IMessage obj)
    {
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.Connected, null);
    }
}