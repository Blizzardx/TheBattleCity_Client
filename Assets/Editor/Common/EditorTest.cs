using NUnit.Framework;
using UnityEngine;

public class EditorTest 
{
    [Test]
    public void Run()
    {
        ReflectionManager.Instance.CheckInit();
    }
    [Test]
    public void b()
    {
        Vector3 a = Vector3.one;
        Vector3 b = new Vector3(1,0,0);
        Debug.Log(Vector3.Dot(a.normalized, a.normalized));
        Debug.Log(Vector3.Dot(a.normalized, b.normalized));
        Debug.Log(Vector3.Dot(new Vector3(1, 0, 0).normalized, new Vector3(1, 1, 0).normalized));
        Debug.Log(Vector3.Dot(new Vector3(1, 0, 0).normalized, new Vector3(0, 1, 0).normalized));
        Debug.Log(Vector3.Dot(new Vector3(1, 0, 0).normalized, new Vector3(0, 0.5f, 0).normalized));
    }
}
