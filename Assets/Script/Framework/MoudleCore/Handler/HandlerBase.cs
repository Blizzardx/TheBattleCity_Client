using UnityEngine;
using System.Collections;

public class HandlerBase
{
    #region public interface
    public void Create()
    {
        Debug.Log(this.GetType() + " hash code " + GetHashCode());
        OnCreate();
    }
    public void Destroy()
    {
        Debug.Log(this.GetType() + " destroy ");
        OnDestroy();
    }
    #endregion

    #region function for override
    protected void HandlerModelData<T>(int dataId, object param) where T : ModelBase
    {
        ModelManager.Instance.GetModel<T>().DataOperation(this, dataId, param);
    }
    protected virtual void OnDestroy()
    {
        
    }
    protected virtual void OnCreate()
    {
    }
    #endregion
}
