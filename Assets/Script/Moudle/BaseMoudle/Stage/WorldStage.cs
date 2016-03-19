using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class WorldStage : StageBase
{
    public WorldStage(GameStateType type)
        : base(type)
    {
    }

    public override void InitStage()
    {
        base.InitStage();
    }

    public override void StartStage()
    {
        WorldLogic.Instance.StartLogic();
    }
    public override void EndStage()
    {
        WorldLogic.Instance.EndLogic();
    }
}