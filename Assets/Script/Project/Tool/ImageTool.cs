using UnityEngine;
using System.Collections;
using System.IO;

public class ImageTool : MonoBehaviour
{
    public Texture2D[] m_Target;

	// Use this for initialization
	void Start ()
	{
	    for (int j = 0; j < m_Target.Length; ++j)
	    {
	        var target = m_Target[j];

            var colors = target.GetPixels();
            for (int i = 0; i < colors.Length; ++i)
            {
                if (colors[i] == Color.white)
                {
                    colors[i].a = 0.5f;
                }
            }
            target.SetPixels(colors);
            var bytes = target.EncodeToPNG();
            File.WriteAllBytes(target.name, bytes);
            Debug.Log("Done:"+ target.name);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
