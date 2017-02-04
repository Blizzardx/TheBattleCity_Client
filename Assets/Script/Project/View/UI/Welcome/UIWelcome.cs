using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Event;
using UnityEngine;

class UIWelcome:UIBase
{
    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/MainMenu/Window_Welcom";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
    }

    protected override void OnInit()
    {
        base.OnInit();
        UIEventListener.Get(FindChild("Image_bg")).onClick = OnClickEnter;
        AudioPlayer.Instance.PlayAudio("BuildIn/Audio/music_default", Vector3.zero,true);
    }

    private void OnClickEnter(GameObject go)
    {
        UIManager.Instance.OpenWindow<UIMainMenu>(UIManager.WindowLayer.Window);
        Hide();
    }
}