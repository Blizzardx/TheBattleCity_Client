using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TankHandler 
{
    private List<MessageObject> m_NetworkSendingList;
    private PkgGeneral_Handle   m_NetworkSubHandlePkg;
    private PkgCToS_BulletTrajectory m_NetworkSubFirePkg;
    private TankRender          m_TankRender;
    private TankControl         m_TankData;
    private InputHandleBase     m_InputMgr;
    private InputType           m_InputType;
    private float               m_fMovingSpeed  = 0.001f;
    private Camera              m_SceneCamera;
    private PlayerInfoPanel     m_PlayerInfoPanel;
    
    public void Initialize(string playerName,GameObject tank,InputType type,int TextureId)
    {
        m_NetworkSendingList    = new List<MessageObject>();
        m_NetworkSubHandlePkg   = new PkgGeneral_Handle();
        m_NetworkSubFirePkg     = new PkgCToS_BulletTrajectory();
                
        //initialize sub pkg
        m_NetworkSubHandlePkg.Speed     = new SubPkg_Vector3();
        m_NetworkSubHandlePkg.InitPosition = new SubPkg_Vector3();

        m_SceneCamera           = GameObject.Find("SceneCamera").GetComponent<Camera>();
        
        m_InputType     = type;
        m_TankData      = new TankControl();
        m_TankRender    = tank.GetComponent<TankRender>();

        switch (m_InputType)
        {
            case InputType.NetWork:
                m_InputMgr = new InputHandle_Network();
                break;
            case InputType.Device:
                m_InputMgr = new InputHandle_Device();
                break;
        }

        m_TankRender.Initialize(TextureId);
        m_TankData.Position = m_TankRender.GetPosition();
        m_TankData.InitPosition = m_TankRender.GetPosition();
        m_InputMgr.Initialize();
        m_PlayerInfoPanel = new PlayerInfoPanel();
        m_PlayerInfoPanel.Initialize(m_SceneCamera,playerName);
        m_PlayerInfoPanel.SetActive(true);
        // udpate player information panel
        m_PlayerInfoPanel.Update(m_TankData.Position);
    }
    public void Destructor()
    {
        m_NetworkSendingList = null;
        m_InputMgr.Destructor();
    }
    public void FixedUpdate()
    {
        switch( m_InputType )
        {
            case InputType.Device:
                DeviceInputUpdate();
                break;
            case InputType.NetWork:
                NetworkInputUpdate();
                break;
        }
        // udpate player information panel
        m_PlayerInfoPanel.Update(m_TankData.Position);
    }
    public void DeviceInputUpdate()
    {
        m_InputMgr.FixedUpdate();

        if (m_InputMgr.GetInputStatus_Moving())
        {
            m_TankData.Speed = m_InputMgr.GetMoveDirection() * m_fMovingSpeed;
            m_TankRender.SetForword(m_InputMgr.GetMoveDirection());
        }
        else
        {
            m_TankData.Speed = Vector3.zero;
        }
        if (m_InputMgr.GetInputStatus_Fire())
        {
            Vector3 firePosition = ((InputHandle_Device)(m_InputMgr)).m_FireTouchPos;
            //send network msg --- fire 
            Ray tmpRay = m_SceneCamera.ScreenPointToRay(firePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(tmpRay, out hitInfo))
            {
                Vector3 fireDir = hitInfo.point - m_TankData.Position;
                fireDir.y = 0;
                fireDir.Normalize();

                //play animation
                m_TankRender.Fire(fireDir);

                //push bullent to store
                m_NetworkSubFirePkg.BulletTrajectory = BulletManager.GetInstance.PushBullet
                    (0,
                    1,
                    0,
                    m_TankRender.FirePosition,
                    fireDir * m_fMovingSpeed * 2.0f,
                    0);
                m_NetworkSubFirePkg.PlayerName = PlayerData.GetInstance.PlayerName;

                //create fire light
                BulletManager.GetInstance.CreateBulletLight(0, 1, m_TankRender.FireLightPosition);

                m_NetworkSendingList.Add(new MessageObject(MessageID.CTS_BulletTrajectory, m_NetworkSubFirePkg));
            }
            else
            {
                //play animation
                m_TankRender.Fire(firePosition);
            }
        }

        if (m_TankData.GetIsNeedRefreshHandler())
        {
            //reset init position
            m_TankData.InitPosition = m_TankData.Position;
            ComponentTool.SetSubPkgVector3(m_NetworkSubHandlePkg.InitPosition, m_TankData.InitPosition);

            //reset time
            m_TankData.m_ResetSpeedTime = TimeManager.GetDuringTimeStartFire() - Time.fixedDeltaTime * 1000.0F;

            if (m_TankData.Speed == Vector3.zero)
            {
                //send network msg --- move
                ComponentTool.SetSubPkgVector3(m_NetworkSubHandlePkg.Speed, Vector3.zero);
            }
            else
            {
                //send network msg --- move
                ComponentTool.SetSubPkgVector3(m_NetworkSubHandlePkg.Speed, m_InputMgr.GetMoveDirection() * m_fMovingSpeed);
            }
            m_NetworkSubHandlePkg.StartTime = m_TankData.m_ResetSpeedTime;

            m_NetworkSendingList.Add(new MessageObject(MessageID.G_Handler, m_NetworkSubHandlePkg));
        }

        //fixed update
        m_TankData.FixedUpdate();
        m_TankRender.SetPosition(m_TankData.Position);

        TrySendingMsg();
    }
    public void NetworkInputUpdate()
    {
        m_InputMgr.FixedUpdate();

        if (m_InputMgr.GetInputStatus_Moving())
        {
            m_TankData.Speed = m_InputMgr.GetMoveDirection();
            m_TankRender.SetForword(m_InputMgr.GetMoveDirection());
        }
        else
        {
            m_TankData.Speed = Vector3.zero;
        }

        if (m_TankData.GetIsNeedRefreshHandler())
        {
            m_TankData.SetNetworkSpeed(((InputHandle_Network)(m_InputMgr)).GetInitPosition(),m_InputMgr.GetMoveDirection(), ((InputHandle_Network)(m_InputMgr)).m_HandlePkg.StartTime);            
        }

        //fixed update
        m_TankData.FixedUpdate();
        m_TankRender.SetPosition(m_TankData.Position);

    }
    private void TrySendingMsg()
    {
        if (m_NetworkSendingList.Count > 0)
        {
            ClientSocketManager.GetInstance.SendMsgToRemoteServer(m_NetworkSendingList.ToArray());
            DebugLog.Log("Send pkg");
            m_NetworkSendingList.Clear();
        }
    }
}
