using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class MainMenu : SceneBase
{

    override public void OpenScene(Action OnFinishedCallBack)
    {
        WindowManager.GetInstance.OpenWindow(WindowType.Welcom);
        OnFinishedCallBack();
    }
    override public void CloseScene()
    {
       
    }
    public override void BasicUpdate()
    {
        
    }
}
