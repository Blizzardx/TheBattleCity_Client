//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : GlobalScripts
//
// Created by : LeoLi (742412055@qq.com) at 2015/11/4 15:36:15
//
// 定义全局可使用的变量，例如：GameCamera
//========================================================================
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GlobalScripts : Singleton<GlobalScripts>
{
    #region Property
    public GameCamera mGameCamera;
    #endregion

    #region Field

    #endregion

    #region Public Interface
    public void Initialize()
    {
        Reset();
    }
    public void Reset()
    {
        GameObject tempObj = GameObject.Find("MainCamera");
        if (null != tempObj)
        {
            mGameCamera = tempObj.GetComponent<GameCamera>();
            if (!mGameCamera) {
                Debuger.LogError("GameCamera not found");
            }
        }
        //tempObj = GameObject.Find("MainRoot/Font");
        //if (null != tempObj)
        //{
        //    GUIText tex = tempObj.GetComponent<GUIText>();
        //    if (null != tex)
        //        mGlobalTrueFont = tex.font;
        //}
        //tempObj = GameObject.Find("MainRoot/EffectRoot");
        //if (null != tempObj)
        //{
        //    mEffectRoot = tempObj.transform;
        //}
    }
    #endregion

    #region System Functions

    #endregion
}

