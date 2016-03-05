

using UnityEngine;
using System.Collections;

/// <summary>
///  If you want keep one component Activity all Time,use this,Do not destory.
/// </summary>
public class DoNotDestory : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad (this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}