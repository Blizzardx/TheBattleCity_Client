namespace Cache
{
    public class CacheIndex
    {
        /// <summary>
        /// 缓存key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 该key的写入时间
        /// </summary>
        public int WriteTime { get; set; }
        /// <summary>
        /// 过期时间，单位秒
        /// </summary>
        public int ExpireTime { get; set; }
        /// <summary>
        /// 缓存文件所在路径
        /// </summary>
        public string CacheFilePath { get; set; }
    }
}
