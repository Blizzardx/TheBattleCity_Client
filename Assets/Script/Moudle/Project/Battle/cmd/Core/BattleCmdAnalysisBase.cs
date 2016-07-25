using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Tool;
using NetWork.Auto;
using UnityEngine;

public interface BattleCmdAnalysisBase
{
    void Init();
    BattleCmdInfo DecodeCmd(BattleCommandData cmd);
    BattleCommandData EncodeCmd(BattleCmdInfo info);
    Type GetDataInfoType();
    BattleCmdType GetMessageInfoType();
}