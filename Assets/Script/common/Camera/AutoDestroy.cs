using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Destroy", 5f);
	}

    void Destroy()
    {
        GameObject.Destroy(this.gameObject);
    }
}
