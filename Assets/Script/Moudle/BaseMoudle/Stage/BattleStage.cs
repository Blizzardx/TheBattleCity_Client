using UnityEngine;
using System.Collections;

public class BattleStage : StageBase
{
    public BattleStage(GameStateType type)
        : base(type)
    {
    }

    public override void InitStage()
    {
        base.InitStage();
        BattleLogic.Instance.StartLogic();
    }

    public override void StartStage()
    {
        WindowManager.Instance.CloseAllWindow();
        BattleLogic.Instance.OnStart();
    }
    public override void EndStage()
    {
        BattleLogic.Instance.EndLogic();
    }
}
