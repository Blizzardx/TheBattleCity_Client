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
        float l = 10;
        float r = 100;
        float a = 0.8f;
        float v = l * (1 - a) + r * a;
        Debug.Log(v);
        v = Mathf.Lerp(l, r, a);
        Debug.Log(v);
    }
}
