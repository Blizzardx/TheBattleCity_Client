using System;
using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform m_TransformGun;
    [SerializeField]
    private Transform m_TransformFirePos;
    [SerializeField]
    private Transform m_TransformFireLightPos;
    [SerializeField]
    private Animator m_GunAnimator;

    private Action m_OnDestroy;

    public void SetOnDestroyCallBack(Action ondestroy)
    {
        m_OnDestroy = ondestroy;
    }
    private void OnDestroy()
    {
        if (null != m_OnDestroy)
        {
            m_OnDestroy();
        }
    }
    
    public void Move(Vector2 dir)
    {
        //set dir
        Vector3 newDir = new Vector3(dir.x, transform.forward.y, dir.y);

        if (CheckDir(newDir))
        {
            transform.forward = newDir;
            Debuger.Log("send new speed " + newDir);
            Debuger.Log("Send new pos" + transform.position);
        }
    }

    private bool CheckDir(Vector3 dir)
    {
        if (dir == Vector3.zero)
        {
            return false;
        }
        return dir != transform.forward;
    }
}
