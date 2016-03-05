//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : CameraMovement
//
// Created by : LeoLi (742412055@qq.com) at 2015/10/30 15:49:28
//
//
//========================================================================
using UnityEngine;
//using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EditorCameraMovement : SingletonTemplateMon<EditorCameraMovement>
{
    #region Property
    [Range(0.1f,0.3f)]
    public float m_fSpeed = 0.2f;
    [Range(0.1f, 0.3f)]
    public float m_fRotate = 0.2f;
    public Camera m_SceneCamera;
    #endregion

    #region Field
    private Vector3 m_vDelta;
    private Vector3 m_vTmp;
    private Vector3 m_vInitPos;
    private Vector3 m_vCamInitRotate;
    #endregion

    #region MonoBehavior
    void Awake()
    {
        _instance = this;
        MessageManager.Instance.Initialize();
        AudioPlayer.Instance.Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (null != m_SceneCamera)
        {
            HandlerSceneCamera();
        }
    }

    #endregion

    #region Public Interface
    public void SetSceneCamera(Camera sceneCamera)
    {
        m_SceneCamera = sceneCamera;
    }
    #endregion

    #region System Functions
    private void HandlerSceneCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_vInitPos = Input.mousePosition;
            m_vCamInitRotate = m_SceneCamera.transform.eulerAngles;
        }
        if (Input.GetMouseButton(1))
        {
            if (Input.GetKey(KeyCode.W))
            {
                m_SceneCamera.transform.position += m_fSpeed * m_SceneCamera.transform.forward;
            }
            if (Input.GetKey(KeyCode.S))
            {
                m_SceneCamera.transform.position -= m_fSpeed * m_SceneCamera.transform.forward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                m_SceneCamera.transform.position -= m_fSpeed * m_SceneCamera.transform.right;
            }
            if (Input.GetKey(KeyCode.D))
            {
                m_SceneCamera.transform.position += m_fSpeed * m_SceneCamera.transform.right;
            }
            if (Input.GetKey(KeyCode.E))
            {
                m_SceneCamera.transform.position += m_fSpeed * m_SceneCamera.transform.up;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                m_SceneCamera.transform.position -= m_fSpeed * m_SceneCamera.transform.up;
            }
            if (Input.GetKey(KeyCode.R))
            {
                //GlobalScripts.Instance.mGameCamera.ShakeCamera(1f, new Vector3(1, 1, 1));
            }


            m_vDelta = Input.mousePosition - m_vInitPos;
            m_vTmp.x = -m_vDelta.y;
            m_vTmp.y = m_vDelta.x;
            m_vTmp *= m_fRotate;
            m_vTmp.z = m_SceneCamera.transform.eulerAngles.z;
            m_vCamInitRotate.z = 0.0F;
            m_SceneCamera.transform.eulerAngles = m_vCamInitRotate + m_vTmp;
        }
        if (Input.GetMouseButtonUp(1))
        {

        }
    }
    #endregion
}

