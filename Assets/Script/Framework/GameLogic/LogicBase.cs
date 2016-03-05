using UnityEngine;
using System.Collections;

public abstract class  LogicBase<T> : Singleton<T> where T : new()
{
    abstract public void StartLogic();
    abstract public void EndLogic();
}
