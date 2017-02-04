class BattleCmdInfo_Speed : BattleCmdInfo
{
    public float Speed;
    public int CharId;
    public int GetCharId()
    {
        return CharId;
    }

    public void SetCharId(int id)
    {
        CharId = id;
    }
}