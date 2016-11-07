using System;
using System.IO;
using System.Text;
using Common.Tool;
using UnityEngine;

namespace Assets.Script.Framework.MoudleCore.Handler.Editor
{
    class HandlerAutoGenTool
    {
        private string m_strUserCodeOutputPath;
        private string m_strAutoCodeOutputPath;
        private string m_strUserCodeTemplatePath;
        private string m_strAutoCodeTemplatePath;

        private string m_strAutoCodeTemplateContent;
        private string m_strUserCodeTemplateContent;
        private bool m_bIsGenUserCode;

        public void Initialize
            (   string userCodeOutputPath,
                string autoCodeOutputPath,
                string userCodeTemplatePath,
                string autoCodeTemplatePath,
                bool isGenUserCode
            )
        {
            m_strUserCodeOutputPath = Application.dataPath + "/" + userCodeOutputPath;
            m_strAutoCodeOutputPath = Application.dataPath + "/" + autoCodeOutputPath;
            m_strUserCodeTemplatePath = Application.dataPath + "/" + userCodeTemplatePath;
            m_strAutoCodeTemplatePath = Application.dataPath + "/" + autoCodeTemplatePath;
            m_bIsGenUserCode = isGenUserCode;

            // check path 
            FileUtils.EnsureFolder(m_strUserCodeOutputPath);
            FileUtils.EnsureFolder(m_strAutoCodeOutputPath);

            m_strAutoCodeTemplateContent = FileUtils.ReadStringFile(m_strAutoCodeTemplatePath);
            m_strUserCodeTemplateContent = FileUtils.ReadStringFile(m_strUserCodeTemplatePath);

            if (string.IsNullOrEmpty(m_strAutoCodeTemplateContent))
            {
                Debug.LogError("Can't load auto code template file form " + m_strAutoCodeTemplateContent);
            }
            if (string.IsNullOrEmpty(m_strUserCodeTemplateContent))
            {
                Debug.LogError("Can't load user code template file form " + m_strUserCodeTemplateContent);
            }

        }

        public void Refresh()
        {
            Directory.Delete(m_strAutoCodeOutputPath, true);
            FileUtils.EnsureFolder(m_strAutoCodeOutputPath);

            int index = 0;
            var list = ReflectionManager.Instance.GetTypeByBase(typeof(HandlerBase));
            for (int i = 0; i < list.Count; ++i)
            {
                GenCode(list[i].Name, index++);
            }
        }

        public void Add(string className)
        {
            int maxIndex = 0;
            var list = ReflectionManager.Instance.GetTypeByBase(typeof (HandlerBase));
            for (int i = 0; i < list.Count; ++i)
            {
                if (list[i].Name == className)
                {
                    Debug.LogError("Class already exist");
                    return;
                }
                HandlerBase modelInstance = Activator.CreateInstance(list[i]) as HandlerBase;
                // mark max index
                maxIndex = modelInstance.GetIndex() > maxIndex ? modelInstance.GetIndex() : maxIndex;
            }


            // get new index
            ++maxIndex;

            // do gen
            GenCode(className, maxIndex);
        }

        public void Remove(string className)
        {
            Directory.Delete(m_strAutoCodeOutputPath, true);
            FileUtils.EnsureFolder(m_strAutoCodeOutputPath);

            int index = 0;
            var list = ReflectionManager.Instance.GetTypeByBase(typeof(HandlerBase));
            for (int i = 0; i < list.Count; ++i)
            {
                if (className == list[i].Name)
                {
                    continue;
                }
                GenCode(list[i].Name, index++);
            }
            
            string outputUserCodePath = m_strUserCodeOutputPath + "/" + className + ".cs";
            
            if (File.Exists(outputUserCodePath))
            {
                File.Delete(outputUserCodePath);
            }
            else
            {
                Debug.LogWarning("Can't find class at path " + outputUserCodePath);
            }
        }
        private void GenCode(string className, int index)
        {
            if (m_bIsGenUserCode)
            {
                // create class
                StringBuilder tmpUser = new StringBuilder(m_strUserCodeTemplateContent);
                // add class name
                tmpUser = tmpUser.Replace("{0}", className);
                // build out put file name
                string outputUserCodePath = m_strUserCodeOutputPath + "/" + className + ".cs";

                // check file class already exist
                if (File.Exists(outputUserCodePath))
                {
                    Debug.LogWarning("File already exist " + outputUserCodePath);
                }
                else
                {
                    // save to file
                    FileUtils.WriteStringFile(outputUserCodePath, tmpUser.ToString());
                }
            }

            StringBuilder tmpAuto = new StringBuilder(m_strAutoCodeTemplateContent);
            // add class name
            tmpAuto = tmpAuto.Replace("{0}", className);
            // add index
            tmpAuto = tmpAuto.Replace("{1}", index.ToString());
            // build out put file name
            string outputAutoCodePath = m_strAutoCodeOutputPath + "/" + className + ".cs";
            // check file class already exist
            if (File.Exists(outputAutoCodePath))
            {
                Debug.LogWarning("File already exist ,delete & update : " + outputAutoCodePath);
                FileUtils.DeleteFile(outputAutoCodePath);
            }
            // save to file
            FileUtils.WriteStringFile(outputAutoCodePath, tmpAuto.ToString());
        }
    }
}
