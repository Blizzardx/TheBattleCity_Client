using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIWindowSelectMode : WindowBase
{
    public override void OnInit()
    {
        base.OnInit();

        AddChildElementClickEvent(OnClickOnlineMode, "Button_OnLine");
        AddChildElementClickEvent(OnClickLANMode, "Button_LAN");
        AddChildElementClickEvent(OnClickLocaleMode, "Button_Console");
    }
    public override void OnOpen(object param)
    {
        base.OnOpen(param);

        AudioPlayer.Instance.PlayAudio("music_menu", Vector3.zero, true);
    }
    private void OnClickLocaleMode(GameObject go)
    {
        TipManager.Instance.Alert("", "功能暂未开放，敬请期待", "OK", (res) => { });
    }

    private void OnClickLANMode(GameObject go)
    {
        TipManager.Instance.Alert("", "功能暂未开放，敬请期待", "OK", (res) => { });
    }

    private void OnClickOnlineMode(GameObject go)
    {
        StageManager.Instance.ChangeState(GameStateType.WorldState);
    }
}

