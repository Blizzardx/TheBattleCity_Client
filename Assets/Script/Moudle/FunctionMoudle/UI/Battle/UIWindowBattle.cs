using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class UIWindowBattle:WindowBase
{
    private GameObject m_ObjHandlerRoot;
    private GameObject m_ObjHandlerFollowPoint;
    private const float m_iFollowLimitSize = 77.0f;
    private Action<Vector2> m_HandlerResCallBack;
    private Vector3 m_vInitPos;
    private Camera m_UICamera;
    private bool m_bIsActive;

    public override void OnInit()
    {
        base.OnInit();

        m_ObjHandlerFollowPoint = FindChild("FollowPoint");
        m_ObjHandlerRoot = FindChild("HandlerRoot");

        m_ObjHandlerRoot.SetActive(false);
        m_UICamera = WindowManager.Instance.GetUICamera();
    }

    public override void OnOpen(object param)
    {
        base.OnOpen(param);
        UITickTask.Instance.RegisterToUpdateList(Update);

    }
    public override void OnClose()
    {
        base.OnClose();
        UITickTask.Instance.UnRegisterFromUpdateList(Update);
    }

    public void SetActive(bool status)
    {
        m_bIsActive = status;
    }
    public void SetInputCallBack(Action<Vector2> callback)
    {
        m_HandlerResCallBack = callback;
    }
    private void Update()
    {
        if (!m_bIsActive)
        {
            return;
        }
        //check input
        if (Input.touchCount > 0)
        {
            HandlerRes(Input.GetTouch(0).position);
        }
        else if (Input.GetMouseButton(0))
        {
            HandlerRes(Input.mousePosition);
        }
        else
        {
            //hide handler icon
            m_ObjHandlerRoot.SetActive(false);
        }
    }

    private bool CheckFirstTouch(Vector3 touchposition)
    {
        if (m_ObjHandlerRoot.activeSelf)
        {
            return false;
        }

        m_ObjHandlerRoot.SetActive(true);
        m_vInitPos = touchposition;
        m_vInitPos = m_UICamera.ScreenToWorldPoint(m_vInitPos);
        //set icon position
        m_ObjHandlerRoot.transform.position = m_vInitPos;
        m_ObjHandlerFollowPoint.transform.localPosition = Vector3.zero;
        return true;
    }
    private void HandlerRes(Vector3 touchPosition)
    {
        if (null == m_HandlerResCallBack)
        {
            return;
        }

        if (CheckFirstTouch(touchPosition))
        {
            return;
        }

        //check
        Vector3 point = touchPosition;
        point = m_UICamera.ScreenToWorldPoint(point);
        m_ObjHandlerFollowPoint.transform.position = point;

        // check limit
        Vector3 localp = m_ObjHandlerFollowPoint.transform.localPosition;

        if (localp.x > m_iFollowLimitSize)
        {
            //reset root x
            float offset = localp.x - m_iFollowLimitSize;
            m_ObjHandlerFollowPoint.transform.localPosition = 
                new Vector3(m_iFollowLimitSize, m_ObjHandlerFollowPoint.transform.localPosition.y, m_ObjHandlerFollowPoint.transform.localPosition.z);
            
            m_ObjHandlerRoot.transform.localPosition =
              new Vector3(m_ObjHandlerRoot.transform.localPosition.x + offset, m_ObjHandlerRoot.transform.localPosition.y, m_ObjHandlerRoot.transform.localPosition.z);
        }
        else if (localp.x < -m_iFollowLimitSize)
        {
            //reset root x
            float offset = -m_iFollowLimitSize - localp.x;
            m_ObjHandlerFollowPoint.transform.localPosition = 
                new Vector3(-m_iFollowLimitSize, m_ObjHandlerFollowPoint.transform.localPosition.y, m_ObjHandlerFollowPoint.transform.localPosition.z);

            m_ObjHandlerRoot.transform.localPosition =
                new Vector3(m_ObjHandlerRoot.transform.localPosition.x - offset, m_ObjHandlerRoot.transform.localPosition.y, m_ObjHandlerRoot.transform.localPosition.z);
        }

        if (localp.y > m_iFollowLimitSize)
        {
            //reset root x
            float offset = localp.y - m_iFollowLimitSize;
            m_ObjHandlerFollowPoint.transform.localPosition = 
                new Vector3(m_ObjHandlerFollowPoint.transform.localPosition.x, m_iFollowLimitSize, m_ObjHandlerFollowPoint.transform.localPosition.z);

            m_ObjHandlerRoot.transform.localPosition =
                new Vector3(m_ObjHandlerRoot.transform.localPosition.x, m_ObjHandlerRoot.transform.localPosition.y + offset, m_ObjHandlerRoot.transform.localPosition.z);
        }
        else if (localp.y < -m_iFollowLimitSize)
        {
            //reset root x
            float offset = -m_iFollowLimitSize - localp.y;
            m_ObjHandlerFollowPoint.transform.localPosition = 
                new Vector3(m_ObjHandlerFollowPoint.transform.localPosition.x, -m_iFollowLimitSize, m_ObjHandlerFollowPoint.transform.localPosition.z);

            m_ObjHandlerRoot.transform.localPosition =
                new Vector3(m_ObjHandlerRoot.transform.localPosition.x, m_ObjHandlerRoot.transform.localPosition.y - offset, m_ObjHandlerRoot.transform.localPosition.z);
        }

        //caculate dir
        Vector3 dir = m_ObjHandlerFollowPoint.transform.position - m_ObjHandlerRoot.transform.position;
        dir.Normalize();

        m_HandlerResCallBack(dir);
    }
}