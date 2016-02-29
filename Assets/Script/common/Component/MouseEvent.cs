using UnityEngine;
using System.Collections;
using System;

public class MouseEvent : MonoBehaviour 
{
    public Action<UnityEngine.GameObject> OnClickCallBack;

    void OnClick()
    {
        if( null != OnClickCallBack)
        {
            OnClickCallBack(gameObject);
        }
    }
}
