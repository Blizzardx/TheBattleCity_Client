//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : GameCamera
//
// Created by : LeoLi (742412055@qq.com) at 2015/11/4 15:01:25
//
//
//========================================================================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameCamera : MonoBehaviour
{
    [Range(0f, 20f)]
    public float Distance = 15f;
    [Range(0, 20f)]
    public float Height = 9f;
    [Range(0.1f, 10f)]
    public float OffsetHeight = 0.5f;
    [Range(0f, 359.9f)]
    public float Rotation = 0f;
    [Range(0f, 20f)]
    public float PositonDamping = 2f;
    [Range(0f, 20f)]
    public float RotationDamping = 2f;
    public Transform LookTarget = null;
    public bool IsOverObstacle = true;

    public static bool OpenClick
    {
        get;
        set;
    }
    public bool LockCam
    {
        set { m_bLock = value; }
        get { return m_bLock; }
    }

    private Vector3? m_EndPos = null;
    private Quaternion? m_EndRot = null;
    [SerializeField]
    private bool m_bLock = false;
    private float m_fCurrDistance;
    private float m_fCurrHeight;
    private float m_fCurrHeightOffset;
    private float m_fCurrRotation;
    private Vector3 m_CurrentPos;
    private Vector3 m_CurrentTargetPos;
    private Quaternion m_CurrentRot;
    //
    private Material m_TransformMatrial;
    private MeshRenderer m_LastMeshRender;
    private Material m_LastMeshMaterial;
    // Rotate
    private bool m_bRotating = false;
    private float m_fRotateAngle = 0f;
    private float m_fRotateSpeed = 0f;
    private float m_InitRotationDamping;
    private float m_InitPositonDamping;
    void Awake()
    {

    }

    #region MonoBehavior
    void Start()
    {
        //DontDestroyOnLoad(transform.parent);
        Distance = 8f;
        Height = 6f;
        OffsetHeight = 1f;
    }
    void OnDestroy()
    {
    }

    void FixedUpdate()
    {
        if (m_bLock && null != LookTarget)
        {
            CameraFollow();
        }
        else if (!m_bLock && null != m_EndPos && null != m_EndRot)
        {
            CameraMove();
        }
    }

    void Update()
    {
        // Change Rotate
        if (m_bRotating)
        {
            if (m_fRotateAngle - Rotation> 1f)
            {
                Rotation += m_fRotateSpeed * Time.deltaTime;
            }
            else if (m_fRotateAngle - Rotation < -1f)
            {
                Rotation -= m_fRotateSpeed * Time.deltaTime;
            }
            else
            {
                // Reset
                Rotation = m_fRotateAngle;
                StopRotate();
            }
        }
        // test
        if (Input.GetKey(KeyCode.Alpha1))
        {
            RotateWithTarget(90, 45f);
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            RotateWithTarget(-90, 45f);
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            RotateWithTarget(360, 90);
        }
        if (Input.GetKey(KeyCode.Alpha4))
        {
            RotateWithTarget(-360, 90);
        }
        if (Input.GetKey(KeyCode.Alpha5))
        {
            RotateWithTarget(-720, 180);
        }
        // Check Click
        if (OpenClick)
        {
            CheckClick();
        }

    }
    private void CameraMove()
    {
        // Current
        m_CurrentPos = transform.position;
        m_CurrentRot = transform.rotation;
        // Lerp
        m_CurrentPos = Vector3.Lerp(m_CurrentPos, (Vector3)m_EndPos, PositonDamping * Time.deltaTime);
        m_CurrentRot = Quaternion.Lerp(m_CurrentRot, (Quaternion)m_EndRot, RotationDamping * Time.deltaTime);
        // Set
        transform.position = m_CurrentPos;
        transform.rotation = m_CurrentRot;
    }
    private void CameraFollow()
    {
        // Current
        m_CurrentPos = transform.position;
        m_CurrentTargetPos = transform.position + transform.forward * Distance;
        // Expected
        float expectedHeight = LookTarget.transform.position.y + Height;
        Vector3 expectedPos = new Vector3(LookTarget.transform.position.x, expectedHeight, LookTarget.transform.position.z);
        Vector3 expectedForward = new Vector3(1 * Mathf.Sin(Rotation * 3.14159f / 180f), 0, 1 * Mathf.Cos(Rotation * 3.14159f / 180f));
        expectedPos -= expectedForward * Distance;
        Vector3 expectedTargetPos = new Vector3(LookTarget.transform.position.x,
                                                LookTarget.transform.position.y + OffsetHeight,
                                                LookTarget.transform.position.z);
        //CheckCollider(ref expectedPos);
        RaycastHit hit;
        if (Physics.Raycast(expectedTargetPos, expectedPos - expectedTargetPos, out hit, (expectedPos - expectedTargetPos).magnitude, 1 << LayerMask.NameToLayer("StaticEntity")))
        {
            if (IsOverObstacle)
            {
                expectedPos = hit.point;
            }
            //Debug.Log(hit.point.ToString());
        }
        if (Physics.Raycast(expectedTargetPos, expectedPos - expectedTargetPos, out hit, (expectedPos - expectedTargetPos).magnitude, 1 << LayerMask.NameToLayer("StaticEntity_Transparent")))
        {
            //change material
            MeshRenderer meshRender = hit.transform.gameObject.GetComponent<MeshRenderer>();
            if (null != meshRender && meshRender.material.shader != m_TransformMatrial.shader)
            {
                m_LastMeshRender = meshRender;
                m_LastMeshMaterial = meshRender.material;
                meshRender.material = m_TransformMatrial;
                Debuger.Log("material name : " + m_LastMeshMaterial.name);
            }
        }
        else if (Physics.Raycast(expectedTargetPos, expectedPos - expectedTargetPos, out hit, (expectedPos - expectedTargetPos).magnitude, 1 << LayerMask.NameToLayer("StaticEntity_MoveCamra")))
        {
            if (IsOverObstacle)
            {
                expectedPos = hit.point;
            }
        }
        else
        {
            if (m_LastMeshRender != null && m_LastMeshRender.material.shader == m_TransformMatrial.shader)
            {
                m_LastMeshRender.material = m_LastMeshMaterial;
                Debuger.Log("reset name : " + m_LastMeshRender.material.name);
                m_LastMeshRender = null;
            }
        }

        //Lerp
        m_CurrentPos = Vector3.Lerp(m_CurrentPos, expectedPos, PositonDamping * Time.deltaTime);
        //m_CurrentTargetPos = Vector3.MoveTowards(m_CurrentTargetPos, expectedTargetPos, RotationDamping * Time.deltaTime);
        m_CurrentTargetPos = Vector3.Lerp(m_CurrentTargetPos, expectedTargetPos, RotationDamping * Time.deltaTime);
        //Set
        transform.position = m_CurrentPos;
        transform.LookAt(m_CurrentTargetPos);
    }
    #endregion

    #region Public Interface
    public Vector3 GetCurrentPosition()
    {
        return transform.position;
    }
    public Vector3 GetCurrentEuler()
    {
        return transform.eulerAngles;
    }
    public Quaternion GetCurrentQuaterion()
    {
        return transform.rotation;
    }
    public void SetTarget(Transform trans, bool bRightNow = false)
    {
        LookTarget = trans;
        if (null != LookTarget)
        {
            if (bRightNow)
            {
                float expectedHeight = LookTarget.transform.position.y + Height;
                Vector3 expectedPos = new Vector3(LookTarget.transform.position.x, expectedHeight, LookTarget.transform.position.z);
                Vector3 expectedForward = new Vector3(1 * Mathf.Sin(Rotation * 3.14159f / 180f), 0, 1 * Mathf.Cos(Rotation * 3.14159f / 180f));
                expectedPos -= expectedForward * Distance;
                Vector3 expectedTargetPos = new Vector3(LookTarget.transform.position.x,
                                                        LookTarget.transform.position.y + OffsetHeight,
                                                        LookTarget.transform.position.z);
                transform.position = expectedPos;
                transform.LookAt(expectedTargetPos);
            }

            LockCam = true;
        }
    }
    public void SetCameraPos(Vector3 vPos, bool bRightNow = false)
    {
        if (bRightNow)
        {
            transform.position = vPos;
            m_EndPos = null;
        }
        else
        {
            m_EndPos = vPos;
        }
    }
    public void SetCameraRot(Quaternion rot, bool bRightNow = false)
    {
        if (bRightNow)
        {
            transform.rotation = rot;
            m_EndRot = null;
        }
        else
        {
            m_EndRot = rot;
        }
    }
    public void SetCameraRot(Vector3 euler, bool bRightNow = false)
    {
        if (bRightNow)
        {
            transform.rotation = Quaternion.Euler(euler);
            m_EndRot = null;
        }
        else
        {
            m_EndRot = Quaternion.Euler(euler);
        }
    }
    public void ResetCam()
    {
        m_EndPos = null;
        m_EndRot = null;
        LookTarget = null;
    }
    public void ShakeCamera(float fTime, Vector3 vAmount)
    {
        Hashtable hash = new Hashtable();
        hash.Add("time", fTime);
        hash.Add("amount", vAmount);
        hash.Add("islocal", true);
        iTween.ShakePosition(transform.parent.gameObject, hash);
    }
    public void RotateWithTarget(float angle, float speed)
    {
        if (null != LookTarget)
        {
            m_fRotateAngle = Rotation + angle;
            m_fRotateSpeed = speed;
            m_bRotating = true;
            m_InitRotationDamping = RotationDamping;
            m_InitPositonDamping = PositonDamping;
        }
    }
    public void StopRotate()
    {
        m_fRotateSpeed = 0f;
        m_fRotateAngle = Rotation;
        RotationDamping = m_InitRotationDamping;
        PositonDamping = m_InitPositonDamping;
        m_bRotating = false;
    }
    public bool CheckRoating ()
    {
        return m_bRotating;
    }
    #endregion

    #region System Functions
    private void CheckClick()
    {
        if (null == UICamera.hoveredObject && Input.GetMouseButtonDown(0))
        {
            if (null == UICamera.hoveredObject && Input.GetMouseButtonDown(0))
            {
                LayerMask Lifelayer = 1 << LayerMask.NameToLayer("Life");
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 100f, Lifelayer.value))
                {
                    MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_HIT_LIFE, hit.transform));
                    return;
                }
                LayerMask Terrainlayer = 1 << LayerMask.NameToLayer("Terrain");
                if (Physics.Raycast(ray, out hit, 100f, Terrainlayer.value))
                {
                    MessageManager.Instance.AddToMessageQueue(new MessageObject(ClientCustomMessageDefine.C_HIT_TERRAIN, hit.point));
                    return;
                }
            }
        }
    }
    #endregion
}

