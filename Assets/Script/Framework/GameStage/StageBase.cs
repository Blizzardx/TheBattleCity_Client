using UnityEngine;
using System.Collections;

public abstract class StageBase
{
    public GameStateType StageType;

    public StageBase(GameStateType type)
    {
        StageType = type;
    }
    abstract public void StartStage();
    abstract public void EndStage();
    virtual public void PreLoadScene()
    {
        WindowManager.Instance.OpenWindow(WindowID.Loading);
    }

    virtual public void InitStage()
    {
        
    }
}
