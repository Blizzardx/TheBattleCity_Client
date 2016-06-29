using UnityEngine;
using System.Collections;

public class SceneMenu : SceneBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        SetSceneName("SceneMenu");
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
    }

    protected override void OnProcess(float process)
    {
        base.OnProcess(process);
        Debug.Log("Process " + process);
    }

    protected override void OnExit()
    {
        base.OnExit();
    }
}
