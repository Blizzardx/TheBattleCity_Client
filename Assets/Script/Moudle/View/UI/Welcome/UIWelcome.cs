using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Event;
using UnityEngine;

class UIWelcome:UIBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetResourceName("BuildIn/UI/Prefab/MainMenu/Window_Welcom");
    }
}