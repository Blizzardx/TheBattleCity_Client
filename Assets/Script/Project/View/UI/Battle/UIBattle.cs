using UnityEngine;
using System.Collections;
using Framework.Common;
using Framework.Event;

public class UIBattle : UIBase
{
    private Vector3 m_vHandlerInitPos;
    private GameObject m_ObjHandler;
    private GameObject m_ObjEndRoot;
    private GameObject m_ObjWinRoot;
    private GameObject m_ObjLoseRoot;

    protected override PreloadAssetInfo SetSourceName()
    {
        PreloadAssetInfo res = new PreloadAssetInfo();
        res.assetName = "BuildIn/UI/Prefab/Battle/UIWindow_Battle";
        res.assetType = PerloadAssetType.BuildInAsset;
        return res;
    }

    protected override void OnInit()
    {
        base.OnInit();
        m_ObjHandler = FindChild("FollowPoint");
        m_vHandlerInitPos = m_ObjHandler.transform.position;
        MyUIDragDropItem comp = m_ObjHandler.GetComponent<MyUIDragDropItem>();
        comp.RegisterDragAction(OnDragHandler);
        comp.RegisterDragEndAction(OnDragEndHandler);
        m_ObjEndRoot = FindChild("PanelEnd");
        m_ObjWinRoot = FindChild("WinRoot");
        m_ObjLoseRoot = FindChild("LoseRoot");
        m_ObjEndRoot.SetActive(false);
        UIEventListener.Get(FindChild("Button_Back")).onClick = OnClickBack;
    }
    // move
    private void OnDragHandler(Vector3 pos)
    {
        var dir = m_ObjHandler.transform.position - m_vHandlerInitPos;
        EventDispatcher.Instance.Broadcast(EventIdDefine.BattleSelfMove, dir.normalized);
    }
    // stop
    private void OnDragEndHandler(MyUIDragDropItem pos)
    {
        m_ObjHandler.transform.position = m_vHandlerInitPos;
        EventDispatcher.Instance.Broadcast(EventIdDefine.BattleSelfStopMove);
    }
    private void OnClickBack(GameObject go)
    {
        HandlerManager.Instance.GetHandler<RoomHandler>(RoomHandler.Index).EndBattle();
    }
}
