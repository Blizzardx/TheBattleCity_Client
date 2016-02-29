using UnityEngine;
using System.Collections;

public class BulletRender : MonoBehaviour
{
    private Transform       m_Positon;

	public void Initialize(Material material)
    {
         //find component from tank mesh
        m_Positon = transform;
        MeshRenderer tmpMeshRender = ComponentTool.FindChildComponent<MeshRenderer>("Mesh", gameObject);
        tmpMeshRender.material = material;
    }
    public void SetPosition(Vector3 position)
    {
        m_Positon.position = position;
    }
    public void SetForward(Vector3 speed)
    {
        //set forword
        m_Positon.forward = speed.normalized;
    }
    public GameObject GetBulletObject()
    {
        return gameObject;
    }
}
