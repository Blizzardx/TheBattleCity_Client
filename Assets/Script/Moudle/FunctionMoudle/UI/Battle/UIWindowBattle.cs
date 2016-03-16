using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class UIPlayerInfo
{
    public UIPlayerInfo()
    {

    }
    public UIPlayerInfo(GameObject objRoot)
    {
        m_ObjRoot   = objRoot;
        m_Hp        = ComponentTool.FindChildComponent<UISlider>("Player_Hp", m_ObjRoot);
        m_LabelName = ComponentTool.FindChildComponent<UILabel>("Player_Name", m_ObjRoot);
        m_ObjRoot.SetActive(true);
    }
    public GameObject   m_ObjRoot;
    public UISlider     m_Hp;
    public UILabel      m_LabelName; 
}
public class UIBattleEndPanel
{
    public UIBattleEndPanel(GameObject obj)
    {
        m_ObjPanelEnd = obj;
        m_ObjPanelWin = ComponentTool.FindChild("WinRoot", obj);
        m_ObjPanelLose = ComponentTool.FindChild("LoseRoot", obj);
    }
    private GameObject m_ObjPanelEnd;
    private GameObject m_ObjPanelWin;
    private GameObject m_ObjPanelLose;

    public void ShowEnd(bool isWin)
    {
        m_ObjPanelEnd.SetActive(true);
        m_ObjPanelWin.SetActive(isWin);
        m_ObjPanelLose.SetActive(!isWin);
    }
    public void HideEnd()
    {
        m_ObjPanelEnd.SetActive(false);
    }
}
public class UIWindowBattle:WindowBase
{
    private GameObject m_ObjHandlerRoot;
    private GameObject m_ObjHandlerFollowPoint;
    private const float m_iFollowLimitSize = 77.0f;
    private Action<Vector2> m_HandlerResCallBack;
    private Action<Vector3> m_FireResCallBack;
    private Vector3 m_vInitPos;
    private Camera m_UICamera;
    private bool m_bIsActive;
    private Dictionary<int, UIPlayerInfo> m_PlayerInfoList;
    private GameObject m_ObjPlayerInfoTemplate;
    private UIBattleEndPanel m_EndPanel;

    public override void OnInit()
    {
        base.OnInit();

        m_ObjHandlerFollowPoint = FindChild("FollowPoint");
        m_ObjHandlerRoot = FindChild("HandlerRoot");
        m_ObjPlayerInfoTemplate = FindChild("Player_Info");

        m_EndPanel = new UIBattleEndPanel(FindChild("PanelEnd"));
        AddChildElementClickEvent(OnClickBack, "Button_Back");
        m_ObjPlayerInfoTemplate.SetActive(false);
        m_ObjHandlerRoot.SetActive(false);
        m_UICamera = WindowManager.Instance.GetUICamera();
        m_PlayerInfoList = new Dictionary<int, UIPlayerInfo>();
        m_EndPanel.HideEnd();
    }
    private void OnClickBack(GameObject go)
    {
        BattleLogic.Instance.OnExitBattle();
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
    public void SetFireCallBack(Action<Vector3> callBack)
    {
        m_FireResCallBack = callBack;
    }
    public void RegisterPlayerInfo(int uid,string name)
    {
        if(m_PlayerInfoList.ContainsKey(uid))
        {
            GameObject.Destroy(m_PlayerInfoList[uid].m_ObjRoot);
            m_PlayerInfoList.Remove(uid);
        }

        GameObject playerInfoRoot = GameObject.Instantiate(m_ObjPlayerInfoTemplate);
        playerInfoRoot.transform.parent = m_ObjPlayerInfoTemplate.transform.parent;
        playerInfoRoot.transform.localScale = Vector3.one;
        UIPlayerInfo elem = new UIPlayerInfo(playerInfoRoot);
        //set name
        elem.m_LabelName.text = name;
        
        //set hp
        elem.m_Hp.value = 1.0f;

        //add to store
        m_PlayerInfoList.Add(uid, elem);
    }
    public void SetPlayerInfoPos(int uid,Vector3 screenPos)
    {
        UIPlayerInfo info = null;
        if (m_PlayerInfoList.TryGetValue(uid,out info))
        {
            //change screen to world
            Vector3 uiPos = m_UICamera.ScreenToWorldPoint(screenPos);

            info.m_ObjRoot.transform.position = uiPos;
        }
    }
    public void SetPlayerInfoHp(int uid, float hp)
    {
        UIPlayerInfo info = null;
        if (m_PlayerInfoList.TryGetValue(uid, out info))
        {
            info.m_Hp.value = hp;
        }
    }
    public void ClearPlayerInfo()
    {
        foreach(var elem in m_PlayerInfoList)
        {
            GameObject.Destroy(elem.Value.m_ObjRoot);
        }
        m_PlayerInfoList.Clear();
    }
    public void ShowEndPanel(bool isWin)
    {
        m_EndPanel.ShowEnd(isWin);
    }
    private void Update()
    {
        if (null == m_HandlerResCallBack || m_FireResCallBack == null)
        {
            return;
        }

        if (!m_bIsActive)
        {
            return;
        }
        //check input
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.touches[i].phase == TouchPhase.Moved)
                {
                    HandlerRes(Input.touches[i].position);
                    break;
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            HandlerRes(Input.mousePosition);
        }
        else
        {
            m_HandlerResCallBack(Vector2.zero);
            //hide handler icon
            m_ObjHandlerRoot.SetActive(false);
        }

        //fire
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if (Input.touches[i].phase == TouchPhase.Began)
            {
                HandlerFire(Input.touches[i].position);
                break;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            HandlerFire(Input.mousePosition);
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
    private void HandlerFire(Vector3 touchPosition)
    {
        m_FireResCallBack(touchPosition);
    }
}