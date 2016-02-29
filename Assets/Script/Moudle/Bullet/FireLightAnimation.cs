using UnityEngine;
using System.Collections;

public class FireLightAnimation : MonoBehaviour 
{
    private float           m_fDestroyScale = 0.001f;
    private float           m_fAnimSpeed    = 0.01f;
    private float           m_fCurrentScale = 1f;
    	
    public void Initialize(Material material)
    {
        MeshRenderer mesh = ComponentTool.FindChildComponent<MeshRenderer>("Mesh", gameObject);
        mesh.material = material;
    }
	// Update is called once per frame
	void Update () 
    {
        m_fCurrentScale -= m_fAnimSpeed;
        transform.localScale = new Vector3(m_fCurrentScale, m_fCurrentScale, m_fCurrentScale);
        CheckDestroy();
	}
    void CheckDestroy()
    {
        if (m_fCurrentScale <= m_fDestroyScale)
        {
            Destroy(gameObject);
        }
    }
}
