using System;
using System.Collections.Generic;
using Framework.Common;
using Framework.Message;
using Framework.Network;
using NetWork.Auto;
using UnityEngine;

public class UIRoom:UIBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetResourceName("BuildIn/UI/Prefab/Room/UIWindow_Room", PerloadAssetType.BuildInAsset);
    }
    protected override void OnInit()
    {
        base.OnInit();
        //UIEventListener.Get(FindChild("Button_OnLine")).onClick = OnClickOnLineMode;
        //UIEventListener.Get(FindChild("Button_LAN")).onClick = OnClickLANMode;
        //UIEventListener.Get(FindChild("Button_Console")).onClick = OnClickConsoleMode;
        
    }
}