
    /// <summary>
    /// JSON paraser. 
    /// 包装后的json解析类
    /// joe 
    /// </summary>
    public class JSONParser
    { 
        private static  Deserializer deserializer = new Deserializer ();

        /// <summary>
        /// 数据类对象转json串
        /// </summary>
        /// <returns>The JSON string.</returns>
        /// <param name="obj">Object.</param>
        /// <param name="includeTypeInfoForDerivedTypes">If set to <c>true</c> include type info for derived types.</param>
        public static string GetJSONString (object obj, bool includeTypeInfoForDerivedTypes = false)
        {
            return Serializer.Serialize (obj, includeTypeInfoForDerivedTypes);
        }  
        /// <summary>
        /// Json转对象(简单模型类)
        /// </summary>
        /// <returns>The object.</returns>
        /// <param name="json">Json.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static T GetObject<T> (string json)
        { 
            if (string.IsNullOrEmpty (json)) {
                json = "{}"; 
            }
            json = json.Replace ("'", "\"");
            return deserializer.Deserialize<T> (json);
        }
 	
    }
 