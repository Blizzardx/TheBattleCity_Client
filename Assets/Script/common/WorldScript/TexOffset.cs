using UnityEngine;
using System.Collections;

    public class TexOffset : MonoBehaviour
    {
        public Vector2 speed = Vector2.zero;
        [SerializeField]
        bool isShare = true;
        Material mat;
        void Start()
        {
            if (isShare)
                mat = GetComponent<MeshRenderer>().sharedMaterial;
            else
                mat = GetComponent<MeshRenderer>().material;
        }
        void Update()
        {
            mat.mainTextureOffset += speed * Time.deltaTime;
        }
    }
