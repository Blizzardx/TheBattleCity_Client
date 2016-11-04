using UnityEngine;

public class HandlerCommon : HandlerBase
{
    protected override void OnCreate()
    {
        Debug.Log("Init HandlerCommon");
    }

    public override int GetIndex()
    {
        return Index;
    }

    public const int Index = 2;
}
