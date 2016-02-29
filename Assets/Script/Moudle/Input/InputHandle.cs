using UnityEngine;
using System.Collections;
public enum InputType
{
    NetWork,
    Device,
}
public class InputHandleBase
{
    protected Vector3   m_MoveDirection;
    protected bool      m_bIsFiring;
    protected bool      m_bIsMoving;

    public Vector3 GetMoveDirection()
    {
        return m_MoveDirection;
    }
    public bool GetInputStatus_Fire()
    {
        if( m_bIsFiring )
        {
            m_bIsFiring = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetInputStatus_Moving()
    {
        return m_bIsMoving;
    }
    public virtual void Initialize()
    {
        m_bIsFiring = false;
        m_bIsMoving = false;
    }
    public virtual void Destructor()
    {

    }
    public virtual void FixedUpdate()
    {

    }
}
public class InputHandle_Device : InputHandleBase
{
    private Vector2             m_InitCenterPos;
    public  Vector2             m_FireTouchPos;
    private Vector3             m_MoveTouchPos;
    private Vector2             m_RealMoveTouchPos;
    private Vector3             m_MoveTouchCenterPos;
    private Camera              m_UICamera;
    private Ray                 m_Ray;
    private Transform           m_TouchIcon;
    private Transform           m_TouchIconBg;
    private bool                m_bIsInitMaxRadius;
    static private readonly float   m_fTriggerMoveRadius        = 10;
    static private readonly float   m_fLocalMoveRadius          = 28;
    static private  float           m_fMaxTriggerMoveRadius     = 10000;

    public override void Initialize()
    {
        base.Initialize();

        m_UICamera = GameObject.Find("Camera").GetComponent<Camera>();
        m_TouchIcon = GameObject.Find("MouseTouchIcon").GetComponent<Transform>();
        m_TouchIconBg = GameObject.Find("MouseTouchBg").GetComponent<Transform>(); 
        //m_TouchIcon.gameObject.SetActive(false);
        //m_TouchIconBg.gameObject.SetActive(false);
        m_bIsInitMaxRadius = false;
    }
    public override void FixedUpdate()
    {
        //reset status
        m_bIsFiring = false;

#if UNITY_EDITOR
        CheckPcDevice();
#else
        CheckTouchDevice();
#endif
        //rest icon pos
        ResetIconPos();
    }
    private void CheckPcDevice()
    {
        //test code
        if (Input.GetMouseButtonDown(0))
        {
            m_FireTouchPos = Input.mousePosition;

            m_bIsFiring = true;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2) // keep in left
            {
                if (m_bIsMoving)
                {
                    Vector2 tmpDelta = ((Vector2)(Input.mousePosition) - m_InitCenterPos);

                    if (tmpDelta.magnitude > m_fTriggerMoveRadius)
                    {
                        m_MoveDirection.x = tmpDelta.normalized.x;
                        m_MoveDirection.z = tmpDelta.normalized.y;
                        m_MoveDirection.y = 0.0f;
                    }
                    else
                    {
                        m_MoveDirection = Vector3.zero;
                    }
                    if (tmpDelta.magnitude > m_fMaxTriggerMoveRadius)
                    {
                        RestInitCenterPos(((Vector2)(Input.mousePosition) - m_fMaxTriggerMoveRadius * tmpDelta.normalized));
                    }
                }
                else
                {
                    RestInitCenterPos((Vector2)(Input.mousePosition));
                    m_MoveDirection = Vector3.zero;
                    m_bIsMoving = true;
                }

                RestMoveTouchPos((Vector2)(Input.mousePosition));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_bIsMoving = false;
        }
    }
    private void CheckTouchDevice()
    {
        //touch
        if (Input.touchCount > 0)
        {
            bool IsHandleMoving = false;
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.touches[i].phase == TouchPhase.Moved || Input.touches[i].phase == TouchPhase.Stationary)
                {
                    if (!IsHandleMoving && Input.touches[i].position.x < Screen.width / 2) // keep in left
                    {

                        if (m_bIsMoving)
                        {
                            Vector2 tmpDelta = (Input.touches[i].position - m_InitCenterPos);

                            if (tmpDelta.magnitude > m_fTriggerMoveRadius)
                            {
                                m_MoveDirection.x = tmpDelta.normalized.x;
                                m_MoveDirection.z = tmpDelta.normalized.y;
                                m_MoveDirection.y = 0.0f;
                            }
                            else
                            {
                                m_MoveDirection = Vector3.zero;
                            }
                            if (tmpDelta.magnitude > m_fMaxTriggerMoveRadius)
                            {
                                RestInitCenterPos((Input.touches[i].position - m_fMaxTriggerMoveRadius * tmpDelta.normalized));
                            }
                            //DebugLog.Log("Moving : " + m_MoveDirection.ToString());
                        }
                        else
                        {
                            RestInitCenterPos(Input.touches[i].position);
                            m_MoveDirection = Vector3.zero;
                            m_bIsMoving = true;
                        }
                        IsHandleMoving = true;

                        RestMoveTouchPos(Input.touches[i].position);
                    }
                }
                else if (Input.touches[i].phase == TouchPhase.Began)
                {
                    m_FireTouchPos = Input.touches[i].position;

                    m_bIsFiring = true;
                }
            }
        }
        else
        {
            m_bIsMoving = false;
            //DebugLog.Log("no fingers touch on screen " + " moving status: " + m_bIsMoving.ToString() + " fire status: " + m_bIsFiring.ToString());
        }
    }
    private void RestInitCenterPos(Vector2 pos)
    {
        m_InitCenterPos = pos;
        m_Ray = m_UICamera.ScreenPointToRay(m_InitCenterPos);
        m_MoveTouchCenterPos = m_Ray.GetPoint(0);
        m_MoveTouchCenterPos.z = 0;
    }
    private void RestMoveTouchPos(Vector2 pos)
    {
        m_RealMoveTouchPos = pos;
        m_Ray = m_UICamera.ScreenPointToRay(pos);
        m_MoveTouchPos = m_Ray.GetPoint(0);
        m_MoveTouchPos.z = 0;
    }
    private void ResetIconPos()
    {
        if (GetInputStatus_Moving())
        {
            m_TouchIcon.gameObject.SetActive(true);
            m_TouchIconBg.gameObject.SetActive(true);

            //update position
            m_TouchIconBg.position = m_MoveTouchCenterPos;

            m_TouchIcon.position = m_MoveTouchPos;
            if (!m_bIsInitMaxRadius && (m_TouchIcon.localPosition - m_TouchIconBg.localPosition).magnitude >= m_fLocalMoveRadius)
            {
                m_fMaxTriggerMoveRadius = (m_RealMoveTouchPos - m_InitCenterPos).magnitude;
                m_bIsInitMaxRadius = true;
            }
            DebugLog.Log(m_TouchIcon.localPosition.ToString() + " " + m_TouchIconBg.localPosition.ToString());
        }
        else
        {
            m_TouchIcon.gameObject.SetActive(false);
            m_TouchIconBg.gameObject.SetActive(false);
        }
    }
}
public class InputHandle_Network : InputHandleBase
{
    private Vector3 m_FireDirection;
    public PkgGeneral_Handle m_HandlePkg;
    public override void Initialize()
    {
        base.Initialize();

        //register message
        ClientMessageCenter.GetInstance.RegistMessage(MessageID.G_Handler, NetworkMsgProcess_Handle);
        ClientMessageCenter.GetInstance.RegistMessage(MessageID.CTS_BulletTrajectory, NetworkMsgProcess_Fire);
    }
    public override void FixedUpdate()
    {
        
    }
    public override void Destructor()
    {
        //unrigester message
        ClientMessageCenter.GetInstance.UnregistMessage(MessageID.G_Handler, NetworkMsgProcess_Handle);
   //     MessageCenter.GetInstance.UnregistMessage(MessageID.G_Fire, NetworkMsgProcess_Fire);
    }
    public Vector3 GetInitPosition()
    {
        return ComponentTool.ConvertSubVec3ToVector3(m_HandlePkg.InitPosition);
    }
    private void NetworkMsgProcess_Handle(MessageObject msg)
    {
        m_HandlePkg = (PkgGeneral_Handle)(msg.msgValue);
        m_MoveDirection = ComponentTool.ConvertSubVec3ToVector3(m_HandlePkg.Speed);

        if (m_MoveDirection == Vector3.zero)
        {
            m_bIsMoving = false;
        }
        else
        {
            m_bIsMoving = true;
        }
    }
    private void NetworkMsgProcess_Fire(MessageObject msg)
    {
        m_bIsFiring = true;
        PkgCToS_BulletTrajectory data = (PkgCToS_BulletTrajectory)(msg.msgValue);

        //fire

    }

}
