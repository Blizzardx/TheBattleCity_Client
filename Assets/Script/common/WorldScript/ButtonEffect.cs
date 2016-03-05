using UnityEngine;
using UnityEngine.UI;
namespace QQ
{
    public class ButtonEffect : MonoBehaviour
    {
        [SerializeField, Range(0, .999999f)]
        float value = 0f;
        [SerializeField]
        float maxSpeed = 5;
        public float Value { get { return this.value; } set { this.value =value>=1?.999999f:value; Change(); } }
        [SerializeField]
        Image im;
        [SerializeField]
        Animator ani;
        [SerializeField]
        Color[] cols = new Color[0];
        public bool isSmooth = true;
        void Change()
        {
            if (isSmooth)
            {
                float rate = 1f / (cols.Length - 1);
                int id = (int)(value / rate);
                im.color = Color.Lerp(cols[id], cols[id + 1], value % rate / rate);
            }
            else
                im.color =cols[(int)(value / (1f / cols.Length))];
            ani.speed = value / 1 * maxSpeed;
            im.fillAmount = value;
        }
#if UNITY_EDITOR
        void Update()
        {
            Change();
        }
#endif
    }
}
