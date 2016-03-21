using UnityEngine;
using System.Collections;

public class ReconnectStage : StageBase
{
    public ReconnectStage(GameStateType type)
        : base(type)
    {
    }
    public override void StartStage()
    {
        PlayerDataMode.Instance.isConnected = false;
        StageManager.Instance.ChangeState(GameStateType.WorldState);
    }

    public override void EndStage()
    {
    }
}