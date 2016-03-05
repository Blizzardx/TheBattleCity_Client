using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor class used to view panels.
/// </summary>

[CustomEditor(typeof (UILabelPlus))]
public class UILabelEditor : UIRectEditor
{
    public UILabelPlus m_UIElement = null;

    protected override void OnEnable()
    {

        try
        {
            m_UIElement = target as UILabelPlus;
            m_UIElement.Init();
            base.OnEnable();
        }
        catch
        {
        }

    }

    protected override void DrawFinalProperties()
    {
        NGUIEditorTools.DrawHeader("样式");

        m_UIElement.m_LabelStyle = (CSS.Styles)EditorGUILayout.EnumPopup("样式:", m_UIElement.m_LabelStyle);

        if (GUILayout.Button(@"应用样式"))
        {
            m_UIElement.ResetStyle(m_UIElement.m_LabelStyle);

            EditorUtility.SetDirty(m_UIElement);
        }

        NGUIEditorTools.DrawSeparator();
    }
}