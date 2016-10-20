using Common.Tool;
using Framework.Common;
using Framework.Event;
using Framework.Log;
using Framework.Queue;
using Framework.Task;
using Framework.Tick;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region public interface
    public void Initialize()
    {
        AdaptiveUI();
        ClearTmpCache();

        LogManager.Instance.Initialize(AppManager.Instance.m_bIsShowDebugMsg);
        EventDispatcher.Instance.BroadcastAsync(EventIdDefine.AppInit);
    }
    public void Update()
    {
        TickTaskManager.Instance.Update();
    }
    public void OnAppQuit()
    {
        EventDispatcher.Instance.Broadcast(EventIdDefine.AppQuit);
        LogManager.Instance.Distructor();
        TaskManager.Instance.QuickFinishedAllTask();
    }
    #endregion

    #region system function

    private void ClearTmpCache()
    {
        TaskQueue_Block.Instance.Enqueue(new TaskElement(8,null, Application.persistentDataPath + "/tmp/"));
    }
    private void AdaptiveUI()
    {
        int ManualWidth = 1920;
        int ManualHeight = 1080;
        UIRoot uiRoot = GameObject.FindObjectOfType<UIRoot>();
        if (uiRoot != null)
        {
            if (System.Convert.ToSingle(Screen.height) / Screen.width > System.Convert.ToSingle(ManualHeight) / ManualWidth)
                uiRoot.manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
            else
                uiRoot.manualHeight = ManualHeight;
        }
    }
    #endregion

}
