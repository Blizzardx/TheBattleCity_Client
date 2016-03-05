//========================================================================
// Copyright(C): CYTX
//
// FileName : CharTransformData
// CLR Version : 4.0.30319.42000
// 
// Created by : LeoLi at 2015/11/25 17:21:15
//
// Purpose : Animator 控制接口
//========================================================================

using UnityEngine;
using System;
using System.Collections;

public class AnimatorAgent
{
    private Animator m_Animator = null;
    //Animator HashCode
    private int m_SpeedId = 0;
    private int m_AgularSpeedId = 0;
    private int m_DirectionId = 0;
    //Offsets
    public float m_SpeedDampTime = 0.1f;
    public float m_AnguarSpeedDampTime = 0.25f;
    public float m_DirectionResponseTime = 0.2f;

    public AnimatorAgent (GameObject loader)
    {
        m_Animator = loader.GetComponent<Animator>();

        m_SpeedId = Animator.StringToHash("Speed");
        m_AgularSpeedId = Animator.StringToHash("AngularSpeed");
        m_DirectionId = Animator.StringToHash("Direction");
    }
    
    #region Public Interface
    public void PlayAnimation()
    { 
    }
    public void DoLocomotion(float speed, float direction)
    {
        if (m_Animator == null)
        {
            return;
        }

        if (!m_Animator.HasState(0, Animator.StringToHash("Locomotion.Idle")))
        {
            return;
        }

        if (speed == 0 && m_Animator.GetFloat(m_SpeedId) == 0)
        {
            return;
        }

        AnimatorStateInfo state = m_Animator.GetCurrentAnimatorStateInfo(0);

        bool inTransition = m_Animator.IsInTransition(0);
        bool inIdle = state.IsName("Locomotion.Idle");
        bool inTurn = state.IsName("Locomotion.TurnOnSpot") || state.IsName("Locomotion.PlantNTurnLeft") || state.IsName("Locomotion.PlantNTurnRight");
        bool inWalkRun = state.IsName("Locomotion.WalkRun");

        //float speedDampTime = inIdle ? 0 : m_SpeedDampTime;
        float speedDampTime = m_SpeedDampTime;
        float angularSpeedDampTime = inWalkRun || inTransition ? m_AnguarSpeedDampTime : 0;
        float directionDampTime = inTurn || inTransition ? 1000000 : 0;

        float angularSpeed = direction / m_DirectionResponseTime;

        m_Animator.SetFloat(m_SpeedId, speed, speedDampTime, Time.deltaTime);
        m_Animator.SetFloat(m_AgularSpeedId, angularSpeed, angularSpeedDampTime, Time.deltaTime);
        m_Animator.SetFloat(m_DirectionId, direction, directionDampTime, Time.deltaTime);
    }
	#endregion
                                                                                                                                                                                                                            
}
