using UnityEngine;
using System.Collections;

public class TankRender : MonoBehaviour 
{
    private Animator m_AnimControl;
    private Transform m_Positon;
    private Transform m_GunPosition;
    private Transform m_FirePosition;
    private Transform m_FireLightPosition;


    //tag to find component
    private readonly string m_GunRootName       = "TankBody_GunRoot";
    private readonly string m_GunName           = "TankBody_Gun";
    private readonly string m_FirePositionName  = "FirePosition";
    private readonly string m_FireLightPositinName = "FireLightPosition";

    public void Initialize(int TextureId)
    {
        //find component from tank mesh
        m_AnimControl       = ComponentTool.FindChildComponent<Animator>(m_GunName,gameObject);
        m_Positon           = transform;
        m_GunPosition       = ComponentTool.FindChildComponent<Transform>(m_GunRootName, gameObject);
        m_FirePosition      = ComponentTool.FindChildComponent<Transform>(m_FirePositionName, gameObject);
        m_FireLightPosition = ComponentTool.FindChildComponent<Transform>(m_FireLightPositinName, gameObject);

        //load texture
        int AssetsId = ResourceManager.GetTankMaterialByType(TextureId);
        Material tmpTexture = ResourceManager.LoadBuildInAsset(AssetsId) as Material;

        MeshRenderer[] materials = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer elem in materials)
        {
            elem.material = tmpTexture;
        }
    }
    public void SetPosition(Vector3 positon)
    {        
        m_Positon.position = positon;
    }
    public void SetForword(Vector3 forward)
    {
        Vector3 tmpFoword = m_GunPosition.forward;
        m_Positon.forward = forward;
        m_GunPosition.forward = tmpFoword;
    }
    public Vector3 GetPosition()
    {
        return m_Positon.position;
    }
    public void Fire(Vector3 forward)
    {
        m_GunPosition.forward = forward;

        //animation play animation
        m_AnimControl.Play("Fire", 0, 0);

        //play sound
        AudioManager.GetInstance.PlayEffectSound(EffectAudioDefine.AudioEffectSoundType.Fire, gameObject.transform.position);

    }
    public Vector3 FirePosition
    {
        get
        {
            return m_FirePosition.position;
        }
    }
    public Vector3 FireLightPosition
    {
        get
        {
            return m_FireLightPosition.position;
        }
    }

}
