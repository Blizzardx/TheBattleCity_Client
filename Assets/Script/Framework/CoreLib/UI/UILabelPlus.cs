using System.Reflection;
using UnityEngine;
using System.Collections.Generic;

public class UILabelPlus : MonoBehaviour
{
    public CSS.Styles       m_LabelStyle;
    public UILabel          m_Label;
    private bool            m_bIsAviliable = false;

    #region key define
    public static readonly string m_strColorTint    = "ColorTint";
    public static readonly string m_strColorTop     = "ColorTop";
    public static readonly string m_strColorBottom  = "ColorBottom";
    public static readonly string m_strColorEffect  = "ColorEffect";
    public static readonly string m_strIsGradient   = "IsGradient";
    public static readonly string m_strEffectStyle  = "EffectStyle";
    public static readonly string m_strEffectDistance = "EffectDistance";
    #endregion

    #region public interface
    public void Init()
    {
        m_Label = this.GetComponent<UILabel>();
        m_bIsAviliable = m_Label != null;
    }
    public void ResetStyle(CSS.Styles style,LabelFontSizeDefine size = LabelFontSizeDefine.LABEL_SIZE_NONE)
    {
        if (! m_bIsAviliable)
        {
            return;
        }
        m_LabelStyle = style;

        // get style information
        SetLabelValueByStyle(style);

        // set font size
        if (size != LabelFontSizeDefine.LABEL_SIZE_NONE)
        {
            m_Label.fontSize = (int)(size);
        }
    }
    #endregion

    #region system function
    private void Start()
    {
        Init();
    }
    private void SetLabelValueByStyle(CSS.Styles style)
    {
        CSS SSL = new CSS();
        FieldInfo myFields = SSL.GetType()
            .GetField(m_LabelStyle.ToString(), BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance);
        if (myFields != null)
        {
            Deserialize(ref m_Label, (string)myFields.GetValue(SSL));
        }
    }
    #endregion

    #region static function
    static public string Serialize(UILabel label)
    {
        Dictionary<string, object> root = new Dictionary<string, object>();
        Dictionary<string, string> elemColor;
        Dictionary<string, string> elemVector;
        
        // set color
        elemColor = new Dictionary<string, string>();
        elemColor.Add("r", label.color.r.ToString());
        elemColor.Add("g", label.color.g.ToString());
        elemColor.Add("b", label.color.b.ToString());
        elemColor.Add("a", label.color.a.ToString());
        root.Add(m_strColorTint,elemColor);

        //set effect
        root.Add(m_strEffectStyle,((int)(label.effectStyle)).ToString());
        if (label.effectStyle != UILabel.Effect.None)
        {
            elemVector = new Dictionary<string, string>();
            elemVector.Add("x", label.effectDistance.x.ToString());
            elemVector.Add("y", label.effectDistance.y.ToString());
            root.Add(m_strEffectDistance, elemVector);

            elemColor = new Dictionary<string, string>();
            elemColor.Add("r", label.effectColor.r.ToString());
            elemColor.Add("g", label.effectColor.g.ToString());
            elemColor.Add("b", label.effectColor.b.ToString());
            elemColor.Add("a", label.effectColor.a.ToString());
            root.Add(m_strColorEffect,elemColor);
        }

        // set gradient
        root.Add(m_strIsGradient, label.applyGradient.ToString());
        if (label.applyGradient)
        {
            elemColor = new Dictionary<string, string>();
            elemColor.Add("r", label.gradientTop.r.ToString());
            elemColor.Add("g", label.gradientTop.g.ToString());
            elemColor.Add("b", label.gradientTop.b.ToString());
            elemColor.Add("a", label.gradientTop.a.ToString());
            root.Add(m_strColorTop, elemColor);

            elemColor = new Dictionary<string, string>();
            elemColor.Add("r", label.gradientBottom.r.ToString());
            elemColor.Add("g", label.gradientBottom.g.ToString());
            elemColor.Add("b", label.gradientBottom.b.ToString());
            elemColor.Add("a", label.gradientBottom.a.ToString());
            root.Add(m_strColorBottom, elemColor);
        }

        return Json.Serialize(root);
    }
    static public void Deserialize(ref UILabel label, string jsonContent)
    {
        Dictionary<string, object> root = Json.Deserialize(jsonContent) as Dictionary<string, object>;

        if (null == root)
        {
            return;
        }

        // set color
        label.color = GetColor(root[m_strColorTint]);

        //set effect
        label.effectStyle = (UILabel.Effect) ((int.Parse(root[m_strEffectStyle] as string)));
        if (label.effectStyle != UILabel.Effect.None)
        {
            label.effectDistance = GetVector(root[m_strEffectDistance]); ;
            label.effectColor = GetColor(root[m_strColorEffect]); 
        }

        // set gradient
        label.applyGradient = ((string) (root[m_strIsGradient])) == "True";
        if (label.applyGradient)
        {
            label.gradientTop = GetColor(root[m_strColorTop]);
            label.gradientBottom = GetColor(root[m_strColorBottom]);
        }
    }
    static public Color GetColor(object source)
    {
        Color target = new Color();
        Dictionary<string, object> tmpSource = source as Dictionary<string, object>;
        target.r = float.Parse(tmpSource["r"] as string);
        target.g = float.Parse(tmpSource["g"] as string);
        target.b = float.Parse(tmpSource["b"] as string);
        target.a = float.Parse(tmpSource["a"] as string);
        return target;
    }
    static public Vector2 GetVector(object source)
    {
        Vector2 target = new Vector2();
        Dictionary<string, object> tmpSource = source as Dictionary<string, object>;
        target.x = float.Parse(tmpSource["x"] as string);
        target.y = float.Parse(tmpSource["y"] as string);
        return target;
    }
    #endregion
}
