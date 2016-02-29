using UnityEngine;
using System.Collections;
using System;
public enum SceneStatus
{
    Loading,
    Initializing,
    Runing,
}
public class SceneBase : MonoBehaviour 
{
    protected SceneStatus m_Status;
    virtual public void OpenScene(Action OnFinishedCallBack )
    {

    }
    virtual public void CloseScene()
    {

    }
    virtual public void BasicUpdate()
    {

    }
}
