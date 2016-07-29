class BattleCmdInfo_Speed : BattleCmdInfo
{
    public float Speed;
    private int CharId;
    public int GetCharId()
    {
        return CharId;
    }

    public void SetCharId(int id)
    {
        CharId = id;
    }
}