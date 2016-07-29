using UnityEngine;

namespace Common.Tool
{
    public class SingletonTemplateMon<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T _instance;
        
        public static T Instance
        {
            get { return _instance; }
        }
    }

}