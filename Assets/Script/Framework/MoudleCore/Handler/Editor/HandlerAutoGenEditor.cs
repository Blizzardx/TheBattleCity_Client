using UnityEditor;
using UnityEngine;

namespace Assets.Script.Framework.MoudleCore.Handler.Editor
{
    class HandlerAutoGenEditor : EditorWindow
    {
        private string m_strUserCodeOutputPath = "Assets/Script/Project/Handler/User";
        private string m_strAutoCodeOutputPath = "Assets/Script/Project/Handler/Auto";
        private string m_strUserCodeTemplatePath = "Assets/Script/Framework/MoudleCore/Handler/Editor/HanderUserCodeTemplate.txt";
        private string m_strAutoCodeTemplatePath = "Assets/Script/Framework/MoudleCore/Handler/Editor/HanderAutoCodeTemplate.txt";
        private bool m_bIsGenUserCode = false;

        private string m_strTmpClassName;
        private HandlerAutoGenTool m_Handler;
        [MenuItem("Editors/Common/Handler Editor")]
        public static void Open()
        {
            CreateInstance<HandlerAutoGenEditor>().Show();
        }

        public HandlerAutoGenEditor()
        {
            ReloadSetting();
        }
        private void ReloadSetting()
        {
            m_Handler = new HandlerAutoGenTool();
            m_Handler.Initialize(m_strUserCodeOutputPath,m_strAutoCodeOutputPath,m_strUserCodeTemplatePath,m_strAutoCodeTemplatePath,m_bIsGenUserCode);
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("用户代码模板路径");
                m_strUserCodeTemplatePath = EditorGUILayout.TextField(m_strUserCodeTemplatePath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("自动生成代码模板路径");
                m_strAutoCodeTemplatePath = EditorGUILayout.TextField(m_strAutoCodeTemplatePath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("用户代码输出路径");
                m_strUserCodeOutputPath = EditorGUILayout.TextField(m_strUserCodeOutputPath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("自动生成代码输出路径");
                m_strAutoCodeOutputPath = EditorGUILayout.TextField(m_strAutoCodeOutputPath);
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                m_bIsGenUserCode = EditorGUILayout.Toggle("是否生成用户代码",m_bIsGenUserCode);

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("设置变更", GUILayout.Width(100f)))
                    {
                        ReloadSetting();
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.TextArea("类名");
                    m_strTmpClassName = EditorGUILayout.TextField(m_strTmpClassName);
                    if (GUILayout.Button("添加", GUILayout.Width(100f)))
                    {
                        m_Handler.Add(m_strTmpClassName);
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    GUILayout.TextArea("类名");
                    m_strTmpClassName = EditorGUILayout.TextField(m_strTmpClassName);
                    if (GUILayout.Button("删除", GUILayout.Width(100f)))
                    {
                        m_Handler.Remove(m_strTmpClassName);
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("刷新", GUILayout.Width(100f)))
                    {
                        m_Handler.Refresh();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
    }
}
