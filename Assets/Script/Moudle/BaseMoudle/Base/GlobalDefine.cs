using UnityEngine;
using System.Collections;
public enum GameStateType
{
    //logic
    none,
    LoginState,
    BattleState,
    ReConnectState,
}
public class WindowID
{
    public const int Loading = 0;
    public const int Login = 1;
    public const int Alert = 2;
    public const int AssetUpdate = 3;
    public const int Battle = 4;
}

public enum WindowLayer
{
    Window,
    Tip ,
    Max,
}
public enum GameLogicSceneType
{
    SceneEnd = 1,
    TalkEnd = 2,
    ActionEnd = 3,
    Login = 4,
    Register = 5,
    CreateChar = 6,
}
public class Definer
{
    public static void RegisterWindow()
    {
        WindowManager.Instance.RegisterWindow(WindowID.Alert, "Tip/UIWindow_Alert", WindowLayer.Tip, typeof(UIWindowAlert));
        WindowManager.Instance.RegisterWindow(WindowID.Loading, "Loading/UIWindow_Loading", WindowLayer.Window, typeof(UIWindowLoading));
        WindowManager.Instance.RegisterWindow(WindowID.Battle, "Battle/UIWindow_Battle", WindowLayer.Window, typeof(UIWindowBattle));
    }
    public static void RegisterStage()
    {
        //logic scene
        StageManager.Instance.RegisterStage(GameStateType.LoginState, "Login", typeof(LoginStage));
        StageManager.Instance.RegisterStage(GameStateType.ReConnectState, "Login", typeof(ReconnectStage));
        StageManager.Instance.RegisterStage(GameStateType.BattleState, "Battle", typeof(BattleStage));

        //node game scene
    }
    public static  void DoCollection()
    {
        if (StageManager.Instance.GetCurrentGameStage() == GameStateType.LoginState)
        {
            return;
            // do nothing
        }
        
        //release logic 
        
        
    }
}