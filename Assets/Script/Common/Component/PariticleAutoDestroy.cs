using System.Collections.Generic;
using UnityEngine;

namespace Common.Component
{
    public class PariticleAutoDestroy : MonoBehaviour
    {
        private static int m_iTickTime = 2;
        private float m_fStartTime;
        private List<ParticleSystem> particleList;

        void Start()
        {
            m_fStartTime = Time.time;
            particleList = new List<ParticleSystem>();
            ComponentTool.FindAllChildComponents<ParticleSystem>(transform, ref particleList);
        }
        void Update()
        {
            float duringtime = Time.time - m_fStartTime;
            if(duringtime < m_iTickTime)
            {
                return;
            }

            //reset time
            m_fStartTime = Time.time;


            for(int i=0;i<particleList.Count;)
            {
                if(particleList[i] != null && !particleList[i].IsAlive())
                {
                    GameObject.Destroy(particleList[i].gameObject);
                    particleList.RemoveAt(i);
                }
                else
                {
                    ++i;
                }
            }
        }
    }
}

