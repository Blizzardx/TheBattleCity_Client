
using System.Collections.Generic;
using Common.Component;
using Framework.Event;
using UnityEngine;

class UIWaitBattle : UIBase
{
    public class PlayerInfo
    {
        private GameObject m_ObjRoot;
        private UILabel m_LabelName;

        public PlayerInfo(GameObject root)
        {
            m_ObjRoot = root;
            m_LabelName = ComponentTool.FindChildComponent<UILabel>("Label_Name", m_ObjRoot);
        }
        public void SetActive(bool status)
        {
            m_ObjRoot.SetActive(status);
        }
        public void SetName(string name)
        {
            m_LabelName.text = name;
        }
    }
    private List<PlayerInfo> m_PlayerList;
    
    protected override PreloadAssetInfo SetSourceName()
    {
        var info = new PreloadAssetInfo();
        info.assetName = "BuildIn/UI/Prefab/Room/UIWindow_WaitBattle";
        info.assetType = PerloadAssetType.BuildInAsset;
        return info;
    }
    protected override void OnInit()
    {
        base.OnInit();
        int count = 5;
        m_PlayerList = new List<PlayerInfo>(count);
        for (int i = 0; i < count; ++i)
        {
            PlayerInfo elem = new PlayerInfo(FindChild("PlayerElement_" + i));
            m_PlayerList.Add(elem);
            elem.SetActive(false);
        }
        RegisterModelEvent(RoomModel.KeyPlayerList,OnPlayerListUpdate,ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index));
        UpdatePlayerList();
    }
    private void OnPlayerListUpdate(EventElement obj)
    {
        UpdatePlayerList();
    }
    private void UpdatePlayerList()
    {
        var list = ModelManager.Instance.GetModel<RoomModel>(RoomModel.Index).GetPlayerInfoList();
        for (int i = 0; i < m_PlayerList.Count; ++i)
        {
            if (list != null && i < list.Count)
            {
                m_PlayerList[i].SetActive(true);
                m_PlayerList[i].SetName(list[i].name);
            }
            else
            {
                m_PlayerList[i].SetActive(false);
            }
        }

    }
}