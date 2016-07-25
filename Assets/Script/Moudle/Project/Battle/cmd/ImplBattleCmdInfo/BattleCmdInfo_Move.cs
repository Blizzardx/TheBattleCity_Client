using UnityEngine;


public class BattleCmdInfo_Move : BattleCmdInfo
{
    public Vector3 dir;
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
