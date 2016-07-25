using UnityEngine;


public class MoveInfo : BattleCmdInfo
{
    public Vector3 dir;
    public float speed;
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
