using UnityEngine;
using System.Collections;

public class HandlerBase
{
    public virtual void OnCreate()
    {
        
    }
    protected void HandlerModelData<T>(int dataId, object param) where T : ModelBase
    {
        ModelManager.Instance.GetModel<T>().DataOperation(this, dataId, param);
    }
}
