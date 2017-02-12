using UnityEngine;

public class BattleCmdInfo_StopMove : BattleCmdInfo
{
    public int charId;

    public int GetCharId()
    {
        return charId;
    }
    public void SetCharId(int id)
    {
        charId = id;
    }
}

