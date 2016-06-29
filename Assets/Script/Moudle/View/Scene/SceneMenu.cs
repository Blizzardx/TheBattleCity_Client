using UnityEngine;
using System.Collections;
using Framework.Event;

public class SceneMenu : SceneBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetSceneName("SceneMenu");
        AddPreloadResource("BuildIn/UI/Prefab/MainMenu/Window_Welcom", PerloadAssetType.BuildIn);

        UIManager.Instance.OpenWindow<UILoading>(UIManager.WindowLayer.Tip);
    }

    protected override void OnInit()
    {
        base.OnInit();
        Debug.Log("OnInit  SceneMenu");
    }

    protected override void OnCompleted()
    {
        base.OnCompleted();
        Debug.Log("loaded  SceneMenu");
        UIManager.Instance.CloseWindow<UILoading>();
        UIManager.Instance.OpenWindow<UIWelcome>(UIManager.WindowLayer.Window);
    }

    protected override void OnProcess(float process)
    {
        base.OnProcess(process);
        Debug.Log("Process " + process);
        EventDispatcher.Instance.BroadcastAsync(0, process);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}
