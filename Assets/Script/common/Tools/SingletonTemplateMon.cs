using UnityEngine;

public class SingletonTemplateMon<T> : MonoBehaviour where T : MonoBehaviour
{
	protected static T _instance;
	
	/// <summary>
	/// 单列实列
	/// </summary>
	public static T Instance
	{
		get
		{
		    return _instance;
		}
	}
}