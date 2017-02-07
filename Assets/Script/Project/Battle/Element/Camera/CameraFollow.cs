using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private float m_fHigh;
    [SerializeField]
    private Vector2 m_vLimitMin;
    [SerializeField]
    private Vector2 m_vLimitMax;
    [SerializeField]
    private Vector3 m_RelativePos;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (null != m_Target)
        {
            transform.position = m_Target.position + m_RelativePos;
            transform.LookAt(m_Target);
            LimitPos();
        }
    }

    private void LimitPos()
    {
        if (transform.position.x < m_vLimitMin.x)
        {
            transform.position = new Vector3(m_vLimitMin.x, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > m_vLimitMax.x)
        {
            transform.position = new Vector3(m_vLimitMax.x, transform.position.y, transform.position.z);
        }

        if (transform.position.z < m_vLimitMin.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, m_vLimitMin.y);
        }
        else if (transform.position.z > m_vLimitMax.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, m_vLimitMax.y);
        }
    }

    public void SetTarget(Transform target)
    {
        m_Target = target;
    }
}
