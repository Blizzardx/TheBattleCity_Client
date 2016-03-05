

using System;
using UnityEngine;
using System.Collections;

/// <summary>
/// author:liao.baiyu
/// timer: 2014.12.1
/// 单件类，提供需要全局使用单件模版类
/// </summary>
/// 

public class Singleton<T> where T : new()  
{  
	protected Singleton()  
	{  
		if (Instance != null)  
		{  
			throw (new Exception("You have tried to create a new singleton class where you should have instanced it. Replace your \"new class()\" with \"class.Instance\""));  
		}  
	}  
	
	public static T Instance  
	{  
		get  
		{  
			return SingletonCreator.instance;  
		}  
	}  
	
	class SingletonCreator  
	{  
		static SingletonCreator()  
		{  
			
		}  
		internal static readonly T instance = new T();  
	}  
} 

 


