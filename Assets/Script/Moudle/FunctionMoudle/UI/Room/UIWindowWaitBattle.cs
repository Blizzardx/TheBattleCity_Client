using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetWork.Auto;
using UnityEngine;

public class WaitBattleUIElement
{
    public WaitBattleUIElement()
    {

    }
    public WaitBattleUIElement(GameObject root)
    {
        m_ObjRoot = root;
        m_LabelPlayerName = ComponentTool.FindChildComponent<UILabel>("Label_Name", root);
    }
    public GameObject m_ObjRoot;
    public UILabel m_LabelPlayerName;
}
public class UIWindowWaitBattle : WindowBase
{
    private List<WaitBattleUIElement> m_PlayerList;
    private UIGrid m_UIGrid;

    public override void OnInit()
    {
        base.OnInit();
        m_PlayerList = new List<WaitBattleUIElement>();
        GameObject playerroot = FindChild("PlayerRoot");
        m_UIGrid = playerroot.GetComponent<UIGrid>();

        for (int i=0;i<playerroot.transform.childCount;++i)
        {
            WaitBattleUIElement elem = new WaitBattleUIElement(playerroot.transform.GetChild(i).gameObject);
            m_PlayerList.Add(elem);
            elem.m_ObjRoot.SetActive(false);
        }
    }
    public void AddPlayer(string name)
    {
        for(int i=0;i<m_PlayerList.Count;++i)
        {
            if(!m_PlayerList[i].m_ObjRoot.activeSelf)
            {
                m_PlayerList[i].m_ObjRoot.SetActive(true);
                m_PlayerList[i].m_LabelPlayerName.text = name;
                break;
            }
        }
        Reposition();
    }
    public void RemovePlayer(string name)
    {
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            if (m_PlayerList[i].m_LabelPlayerName.text == name)
            {
                m_PlayerList[i].m_ObjRoot.SetActive(false);
                break;
            }
        }
        Reposition();
    }
    private void Reposition()
    {
        m_UIGrid.Reposition();
    }
    public void UpdatePlayer(List<PlayerInfo> playerList)
    {
        int i = 0;
        for (; i < m_PlayerList.Count && i< playerList.Count; ++i)
        {
            m_PlayerList[i].m_ObjRoot.SetActive(true);
            m_PlayerList[i].m_LabelPlayerName.text = playerList[i].Name;
        }
        for(;i<m_PlayerList.Count;++i)
        {
            m_PlayerList[i].m_ObjRoot.SetActive(false);
        }
        Reposition();
    }
}
