using System.Collections.Generic;
using System.Linq;
using NetWork;

namespace Cache
{
    public class CacheIndexManager
    {
        private Dictionary<string, CacheIndex> cacheIndexMap = new Dictionary<string, CacheIndex>();

        private string cacheIndexFilePath;

        public CacheIndexManager(string cacheIndexFilePath)
        {
            this.cacheIndexFilePath = cacheIndexFilePath;
        }

        /// <summary>
        /// 加载索引文件
        /// </summary>
        public void LoadCacheIndex()
        {
            ByteBuffer buffer = FileUtil.ReadFileToByteArray(cacheIndexFilePath);
            if(buffer != null)
            {
                while(buffer.ReadableBytes() > 0)
                {
                    CacheIndex cacheIndex = new CacheIndex();
                    cacheIndex.Key = buffer.ReadString();
                    cacheIndex.WriteTime = buffer.ReadInt();
                    cacheIndex.ExpireTime = buffer.ReadInt();
                    cacheIndex.CacheFilePath = buffer.ReadString();
                    cacheIndexMap.Add(cacheIndex.Key, cacheIndex);
                }
            }
        }

        public void AddCacheIndex(string key, string cacheFilePath, int expireTime)
        {
            lock (this)
            {
                if (cacheIndexMap.ContainsKey(key))
                {
                    CacheIndex cacheIndex = cacheIndexMap[key];
                    cacheIndex.ExpireTime = expireTime;
                    cacheIndex.WriteTime = (int)(TimeManager.Instance.Now / 1000);
                }
                else
                {
                    CacheIndex cacheIndex = new CacheIndex();
                    cacheIndex.Key = key;
                    cacheIndex.WriteTime = (int)(TimeManager.Instance.Now / 1000);
                    cacheIndex.ExpireTime = expireTime;
                    cacheIndex.CacheFilePath = cacheFilePath;
                    cacheIndexMap.Add(key, cacheIndex);
                }

                Flush();
            }
        }

        public void RemoveCacheIndex(string key)
        {
            lock (this)
            {
                if (!cacheIndexMap.ContainsKey(key))
                {
                    return;
                }
                cacheIndexMap.Remove(key);

                Flush();
            }
        }

        public CacheIndex GetCacheIndex(string key)
        {
            lock (this)
            {
                if (!cacheIndexMap.ContainsKey(key))
                {
                    return null;
                }
                return cacheIndexMap[key];
            }
        }

        /// <summary>
        /// 保存索引对象到磁盘
        /// </summary>
        public void Flush()
        {
            lock(this)
            {
                ByteBuffer buffer = ByteBuffer.Allocate(cacheIndexMap.Count() * 30);
                foreach (KeyValuePair<string, CacheIndex> kv in cacheIndexMap)
                {
                    CacheIndex cacheIndex = kv.Value;
                    buffer.WriteString(cacheIndex.Key);
                    buffer.WriteInt(cacheIndex.WriteTime);
                    buffer.WriteInt(cacheIndex.ExpireTime);
                    buffer.WriteString(cacheIndex.CacheFilePath);
                }
                FileUtil.WriteFile(cacheIndexFilePath, buffer.ToArray());
            }
        }
    }
}
