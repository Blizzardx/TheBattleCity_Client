using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIWindowWelcome:WindowBase
{
    public override void OnInit()
    {
        base.OnInit();
        AddChildElementClickEvent(OnClickEnter, "Image_bg");
        
    }

    private void OnClickEnter(GameObject go)
    {
        Hide();
        WindowManager.Instance.OpenWindow(WindowID.SelectMode);
    }
}
