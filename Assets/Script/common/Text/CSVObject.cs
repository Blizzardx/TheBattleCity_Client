using System;
using System.Collections.Generic;
using UnityEngine;
    /// <summary>
    /// 混合的数据类型 joe
    /// </summary>
    public class MixType
    {
        private object Value = null;

        public MixType(object Value)
        {
            this.Value = Value;
        }

        public string toString()
        {
            return (string)Value;
        }

        public int toInt()
        {
            return int.Parse((string)Value);
        }

        public float toFloat()
        {
            return float.Parse((string)Value);
        }
    }

    //joe 

    /// <summary>
    ///  CSV 表对应的二维表数据类型,支持以[行号,列号] or [行号,列名] 查询; 支持以For迭代
    /// </summary> 	
    public class CSVObject
    {
        public string AssetPath = "";
        public bool IsLoaded = false;

        /// <summary>
        ///   读取CSV类型资源  ;  CSVObject :CSV 表对应的二维表数据类型,支持以[行号,列号] or [行号,列名] 查询; 支持以For迭代
        /// </summary> 	
        ///public CSVObject  GetCSVItem (string AssetKey)
        //{  
        //AssetFile AF = new AssetFile (AssetKey);
        ////	string result = FileUtil.LoadStringFile (AF.LocalPath);
        //CSVObject csvo = new CSVObject (result);
        //	return csvo;
        //} 
        /// <summary>
        /// 读取文本类型资源  ,转换CSV格式为数组
        /// </summary> 
        //public string[]  GetTextItemArray (string AssetPath)
        //	{  
        //	AssetFile AF = new AssetFile (AssetKey);
        //	string result = FileUtil.LoadStringFile (AF.LocalPath);
        //	string [] resultA = {};
        //	if (result != "") {
        //		resultA = result.Split (new char[]{'\r','\n'}, System.StringSplitOptions.RemoveEmptyEntries);
        //	}
        //	return resultA;
        //	}

        public Dictionary<string, int> columnHeaders = new Dictionary<string, int>();
        public List<string[]> Items = new List<string[]>();
        public int Length = 0;

        /// <summary>
        ///  CSV 表对应的二维表数据类型,支持以[行号,列号] or [行号,列名] 查询; 支持以For迭代
        /// </summary>  
        public CSVObject()
        {

        }
        public CSVObject LoadFile(string AssetPath)
        {
            this.AssetPath = AssetPath;
            TextAsset ta = (TextAsset)Resources.Load(this.AssetPath);
            if (ta != null && !String.IsNullOrEmpty(ta.text))
            {
                this.SetValue(ta.text);
                IsLoaded = true;
            }
            else
            {
                this.SetValue("");
                IsLoaded = false;
            }
            return this;
        }

        public CSVObject LoadText(string Text)
        {
            if (!String.IsNullOrEmpty(Text))
            {
                this.SetValue(Text);
                IsLoaded = true;
            }
            else
            {
                this.SetValue("");
                IsLoaded = false;
            }
            return this;
        }

        private bool IsValidParam(int index, string cloKey)
        {
            if (columnHeaders.ContainsKey(cloKey) && (index >= 0 && index < Length))
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Key:" + index + ";cloKey:" + cloKey + " is inValid.");
                return false;
            }
        }

        private bool IsValidParam(int index, int cloIndex)
        {
            if ((cloIndex >= 0 && cloIndex < columnHeaders.Count) && (index >= 0 && index < Length))
            {
                return true;
            }
            else
            {
                Debug.LogWarning("Key:" + index + ";cloIndex:" + cloIndex + " is inValid.");
                return false;
            }
        }
        /// <summary>
        /// 基于严格模式CSV,获取混合类型键值  index 行号  cloKey 列号
        /// </summary> 
        public MixType GetValue(int index, string cloKey)
        {
            if (!IsValidParam(index, cloKey))
                return null;
            return new MixType(Items[index][columnHeaders[cloKey]]);
        }
        /// <summary>
        /// 基于严格模式CSV,获取混合类型键值   index 行号  cloKey 列号
        /// </summary> 
        public MixType GetValue(int index, int cloIndex)
        {
            if (!IsValidParam(index, cloIndex))
                return null;
            return new MixType(Items[index][cloIndex]);
        }
        /// <summary>
        /// 基于严格模式CSV,获取String键值  index 行号  cloKey 列名
        /// </summary> 
        public string GetStringValue(int index, string cloKey)
        {
            if (!IsValidParam(index, cloKey))
                return null;
            return Items[index][columnHeaders[cloKey]];
        }
        /// <summary>
        /// 基于严格模式CSV,获取String键值  index 行号  cloKey 列号
        /// </summary> 
        public string GetStringValue(int index, int cloIndex)
        {
            if (!IsValidParam(index, cloIndex))
                return null;
            return Items[index][cloIndex];
        }

        /// <summary>
        /// 基于严格模式CSV,获取Int键值  index 行号  cloKey 列名
        /// </summary> 
        public int GetIntValue(int index, string cloKey)
        {
            if (!IsValidParam(index, cloKey))
                return 0;
            return int.Parse(Items[index][columnHeaders[cloKey]]);
        }
        /// <summary>
        /// 基于严格模式CSV,获取Int键值  index 行号  cloKey 列号
        /// </summary> 
        public int GetIntValue(int index, int cloIndex)
        {
            if (!IsValidParam(index, cloIndex))
                return 0;
            return int.Parse(Items[index][cloIndex]);
        }
        /// <summary>
        /// 基于严格模式CSV,获取Float键值  index 行号  cloKey 列名
        /// </summary> 
        public float GetFloatValue(int index, string cloKey)
        {
            if (!IsValidParam(index, cloKey))
                return 0;
            return float.Parse(Items[index][columnHeaders[cloKey]]);
        }
        /// <summary>
        /// 基于严格模式CSV,获取Float键值  index 行号  cloKey 列号
        /// </summary> 
        public float GetFloatValue(int index, int cloIndex)
        {
            if (!IsValidParam(index, cloIndex))
                return 0;
            return float.Parse(Items[index][cloIndex]);
        }

        /// <summary>
        /// 基于严格模式CSV,获取Float键值  index 行号  cloKey 列号
        /// </summary> 
        public double GetDoubleValue(int index, int cloIndex)
        {
            if (!IsValidParam(index, cloIndex))
                return 0;
            return double.Parse(Items[index][cloIndex]);
        }
        /// <summary>
        /// 基于严格模式CSV,获取Float键值  index 行号  cloKey 列号
        /// </summary> 
        public double GetDoubleValue(int index, string cloKey)
        {
            if (!IsValidParam(index, cloKey))
                return 0;
            return double.Parse(Items[index][columnHeaders[cloKey]]);
        }

        private void SetValue(string text)
        {
            try
            {
                string[] tempItemArray = text.Split(new char[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (tempItemArray.Length > 0)
                {
                    string[] tempcol = tempItemArray[0].ToString().Split(',');
                    for (int i = 0; i < tempcol.Length; i++)
                    {
                        columnHeaders.Add(tempcol[i], i);
                    }
                    Items.Clear();
                    for (int j = 1; j < tempItemArray.Length; j++)
                    {
                        Items.Add(tempItemArray[j].ToString().Split(','));
                    }
                    Length = Items.Count;
                    tempItemArray = tempcol = null;
                }
            }
            catch 
            {
                Debug.LogWarning("CSVObject Parse error " + text.ToString().Substring(0, 80));
            }
        }//end  

    } //end class 
