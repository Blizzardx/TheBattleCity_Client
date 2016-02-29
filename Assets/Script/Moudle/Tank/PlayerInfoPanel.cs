using UnityEngine;
using System.Collections;

public class PlayerInfoPanel
{
    private GameObject  m_ObjectRoot;
    private UISlider    m_SliderHP;
    private UILabel     m_LabelName;
    private Camera      m_SceneCamera;

    public void Initialize(Camera sceneCamera,string playerName)
    {
        m_SceneCamera = sceneCamera;

        // load object first
        LoadObject();
        
        m_LabelName.text = playerName;
        m_SliderHP.value = 1.0f;
    }
    public void Update(Vector3 tankPos)
    {
        //fix
        tankPos.y *= 2.0f;
        tankPos.z *= 1.3f;
        Vector3 tmpPos = m_SceneCamera.WorldToScreenPoint(tankPos);
        tmpPos = WindowManager.GetInstance.GetUICamera().ScreenToWorldPoint(tmpPos);
        
        m_ObjectRoot.transform.position = tmpPos;
    }
    public void SetActive(bool status)
    {
        m_ObjectRoot.SetActive(status);
    }
    public void SetHPPercent(float hpPercent)
    {
        m_SliderHP.value = hpPercent;
    }
    private void LoadObject()
    {
        m_ObjectRoot = GameObject.Instantiate(ResourceManager.LoadBuildInUIPrefab("Battle/Player_Info")) as GameObject;
        ComponentTool.Attach(WindowManager.GetInstance.GetUIWindowRoot().transform, m_ObjectRoot.transform);
        m_LabelName = ComponentTool.FindChildComponent<UILabel>("Player_Name", m_ObjectRoot);
        m_SliderHP = ComponentTool.FindChildComponent<UISlider>("Player_Hp", m_ObjectRoot);
    }
}
