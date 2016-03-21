using UnityEngine;
using System.Collections;

public class LoginStage : StageBase
{
    public LoginStage(GameStateType type) : base(type)
    {
    }
    public override void StartStage()
    {
        Debuger.Log("StartStage LoginStage");

        WindowManager.Instance.HideAllWindow();
        if(!PlayerDataMode.Instance.isShowWelcom)
        {
            PlayerDataMode.Instance.isShowWelcom = true;
            WindowManager.Instance.OpenWindow(WindowID.Welcome);
        }
        else
        {
            WindowManager.Instance.OpenWindow(WindowID.SelectMode);
        }
    }
    public override void EndStage()
    {
        Debuger.Log("EndStage LoginStage");
    }
}
