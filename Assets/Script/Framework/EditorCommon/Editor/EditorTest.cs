using NUnit.Framework;

public class EditorTest 
{
    [Test]
    public void Run()
    {
        ReflectionManager.Instance.CheckInit();
    }
}
