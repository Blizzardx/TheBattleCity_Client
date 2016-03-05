//========================================================================
// Copyright(C): ***********************
//
// CLR Version : 4.0.30319.42000
// FileName : UIWindowAlert
//
// Created by : Baoxue at 2015/11/24 11:54:25
//
//
//========================================================================
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UIWindowAlert:WindowBase
{
    private UILabel         m_LabelTitle;
    private UILabel         m_LabelContent;
    private UILabel         m_LabelOk;
    private UILabel         m_LabelCancle;
    private GameObject      m_ButtonOk;
    private GameObject      m_ButtonCancle;
    private Action<bool>    m_CallBack;
    private UIGrid m_Grid;

    public override void OnInit()
    {
        base.OnInit();
        m_LabelTitle = FindChildComponent<UILabel>("UILabel_Title");
        m_LabelContent = FindChildComponent<UILabel>("UILabel_Content");
        m_LabelOk = FindChildComponent<UILabel>("Label_Ok");
        m_LabelCancle = FindChildComponent<UILabel>("Label_Cancle");
        m_ButtonOk = FindChild("UIButton_Ok");
        m_ButtonCancle = FindChild("UIButton_Cancle");
        m_Grid = FindChildComponent<UIGrid>("ButtonRoot");

        UIEventListener.Get(m_ButtonOk).onClick = OnClickOk;
        UIEventListener.Get(m_ButtonCancle).onClick = OnClickCancle;
    }
    private void OnClickOk(GameObject go)
    {
        Hide();
        if (null != m_CallBack)
        {
            m_CallBack(true);
        }
    }
    private void OnClickCancle(GameObject go)
    {
        Hide();
        if (null != m_CallBack)
        {
            m_CallBack(false);
        }
    }
    public override void OnOpen(object param)
    {
        base.OnOpen(param);
    }
    public void Alert(string content)
    {
        m_CallBack = null;
        m_LabelTitle.text = string.Empty;
        m_LabelContent.text = content;
        m_LabelOk.text = "OK";
        m_LabelCancle.text = string.Empty;

        m_ButtonCancle.SetActive(false);
        m_Grid.Reposition();
    }
    public void Alert(string title, string content)
    {
        m_CallBack = null;
        m_LabelTitle.text = title;
        m_LabelContent.text = content;
        m_LabelOk.text = "OK";
        m_LabelCancle.text = string.Empty;

        m_ButtonCancle.SetActive(false);
        m_Grid.Reposition();
    }
    public void Alert(string title, string content, string correct, Action<bool> callBack)
    {
        m_CallBack = callBack;
        m_LabelTitle.text = title;
        m_LabelContent.text = content;
        m_LabelOk.text = correct;
        m_LabelCancle.text = string.Empty;

        m_ButtonCancle.SetActive(false);
        m_Grid.Reposition();
    }
    public void Alert(string title, string content, string correct, string cancle, Action<bool> callBack)
    {
        m_CallBack = callBack;
        m_LabelTitle.text = title;
        m_LabelContent.text = content;
        m_LabelOk.text = correct;
        m_LabelCancle.text = cancle;

        m_ButtonCancle.SetActive(true);
        m_Grid.Reposition();
    }
}

