using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Script.Framework.Assets.NewAssetTest.Editor
{
    class AssetbundlePackerEditor:EditorWindow
    {
        private string m_strUGUIAtlasPath = Application.dataPath + "/UGUI/";
        private string m_strNGUIAtlasPath = Application.dataPath + "/NGUI/";
        private string m_strDependentAssetRootPath = Application.dataPath + "/Data/";
        private string m_strOutputPath = Application.dataPath + "/../output/";
        private AssetbundlePacker m_Handler;

        [MenuItem("Editors/ResourcePacker/ResourcePacker Editor")]
        public static void Open()
        {
            CreateInstance<AssetbundlePackerEditor>().Show();
        }

        public AssetbundlePackerEditor()
        {
            m_Handler = new AssetbundlePacker();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("UGUI Pack path");
                m_strUGUIAtlasPath = EditorGUILayout.TextField(m_strUGUIAtlasPath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("NGUI Pack path");
                m_strNGUIAtlasPath = EditorGUILayout.TextField(m_strNGUIAtlasPath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("Asset Pack path");
                m_strDependentAssetRootPath = EditorGUILayout.TextField(m_strDependentAssetRootPath);
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(20f);

                EditorGUILayout.BeginHorizontal();
                GUILayout.TextArea("Output path");
                m_strOutputPath = EditorGUILayout.TextField(m_strOutputPath);
                EditorGUILayout.EndHorizontal();
                
                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Auto Set bundle Name", GUILayout.Width(300f)))
                    {
                        AutosetBundle();
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Reset bundle Name", GUILayout.Width(300f)))
                    {
                        ResetBundleName();
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Begin build Bundle", GUILayout.Width(300f)))
                    {
                        BeginBuildBundle();
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Gen Asset To bundle Find Map", GUILayout.Width(300f)))
                    {
                        GenAssetToBundleFindMap();
                    }
                }
                EditorGUILayout.EndHorizontal();

                GUILayout.Space(20f);
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("test", GUILayout.Width(300f)))
                    {
                        Test();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }

        private void GenAssetToBundleFindMap()
        {
            m_Handler.GenAssetToBundleFindMap();
        }

        private void BeginBuildBundle()
        {
            m_Handler.BeginBuild();
        }

        private void ResetBundleName()
        {
            m_Handler.ResetAllBundleName(Application.dataPath);
        }

        private void AutosetBundle()
        {
            m_Handler.BeginSetBundleName(m_strUGUIAtlasPath,m_strNGUIAtlasPath,m_strDependentAssetRootPath,m_strOutputPath,TextureImporterType.Advanced,TextureImporterFormat.ARGB16);
        }

        private void Test()
        {
            var dps = AssetDatabase.GetDependencies("Assets/ArtCQQ/camRoot.prefab");
            for (int i = 0; i < dps.Length; ++i)
            {
                Debug.Log(dps[i]);
            }
        }
    }
}
