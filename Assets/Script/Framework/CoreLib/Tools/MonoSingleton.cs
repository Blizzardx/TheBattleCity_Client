using UnityEngine;

namespace Common.Tool
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance " + typeof (T) + " already destroyed on application quit." +
                                     "Won't create again - returning null.");
                    return null;
                }

                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = (T) FindObjectOfType(typeof (T));
                            if (_instance == null)
                            {
                                GameObject singleton = new GameObject();
                                _instance = singleton.AddComponent<T>();
                                if (singleton.name == "New Game Object")
                                    singleton.name = "(singleton) " + typeof (T).ToString();
                                DontDestroyOnLoad(singleton);
                            }
                            else
                            {
//                           behaviac.Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                                Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                            }
                        }
                    }
                }
                return _instance;
            }
        }

        private static bool applicationIsQuitting = false;

        public void OnDestroy()
        {
            applicationIsQuitting = true;
        }
    }
}