using System;
using System.Collections.Generic;
using System.IO;
using NetWork;

namespace Cache
{
    public class CacheManager
    {
        private static CacheManager Instance = null;

        private static CacheIndexManager cacheIndexManager = null;

        private const string CACHE_INDEX_FILE_NAME = "cache.index";

        private Dictionary<string, object> localCache = new Dictionary<string, object>();

        private Dictionary<string, byte[]> fileCache = new Dictionary<string, byte[]>();

        private string m_strCacheDir;

        private static string baseCacheDir;
        public static void Init(string cacheDir)
        {
            baseCacheDir = cacheDir;
            if (!Directory.Exists(cacheDir))
            {
                Directory.CreateDirectory(cacheDir);
            }

            cacheIndexManager = new CacheIndexManager(cacheDir + "/" + CACHE_INDEX_FILE_NAME);
            cacheIndexManager.LoadCacheIndex();
            Instance = new CacheManager();
            Instance.m_strCacheDir = cacheDir;
            //Console.WriteLine(cacheDir + "/" + CACHE_INDEX_FILE_NAME);
        }
        public static CacheManager GetInsance()
        {
            if(Instance == null || cacheIndexManager == null)
            {
                throw new ApplicationException("CacheManager is not Init.");
            }
            return Instance;
        }
        public void Set(CacheKeyInfo keyInfo, object value)
        {
            lock(this)
            {
                string realKey = string.Empty;    
                if (keyInfo.CacheKey.IsNativeFile)
                {
                    realKey = keyInfo.GetRealKey();
                }
                else
                {
                    realKey = keyInfo.GetRealKey() + ".cache";
                }
                bool saveDisk = keyInfo.CacheKey.SaveDiskPath != null;
                if (saveDisk)
                {
                    if(!Directory.Exists(baseCacheDir + keyInfo.CacheKey.SaveDiskPath))
                    {
                        Directory.CreateDirectory(baseCacheDir + keyInfo.CacheKey.SaveDiskPath);
                    }
                    byte[] bytes = null;
                    if (!keyInfo.CacheKey.IsNativeFile)
                    {
                        bytes = TranscoderManager.Instance.Encode(keyInfo.CacheKey.Transcoder, keyInfo.CacheKey.CanCompress, value);
                    }
                    else
                    {
                        bytes = (value) as byte[];
                    }
                    string cacheFile = baseCacheDir + keyInfo.CacheKey.SaveDiskPath + "/" + realKey;
                    using (FileStream fs = new FileStream(cacheFile, FileMode.Create))
                    {
                        fs.Write(bytes, 0, bytes.Length);
                    }
                    cacheIndexManager.AddCacheIndex(realKey, keyInfo.CacheKey.SaveDiskPath + "/" + realKey, keyInfo.CacheKey.ExpireTime);

                    if (fileCache.ContainsKey(realKey))
                    {
                        fileCache[realKey] = bytes;
                    }
                    else
                    {
                        fileCache.Add(realKey, bytes);
                    }

                }
                else
                {
                    if(localCache.ContainsKey(realKey))
                    {
                        localCache[realKey] = value;
                    }
                    else
                    {
                        localCache.Add(realKey, value);
                    }
                }
            }
        }
        public object Get(CacheKeyInfo keyInfo)
        {
            lock (this)
            {            
                string realKey = string.Empty;    
                if (keyInfo.CacheKey.IsNativeFile)
                {
                    realKey = keyInfo.GetRealKey();
                }
                else
                {
                    realKey = keyInfo.GetRealKey() + ".cache";
                }
                 
                bool saveDisk = keyInfo.CacheKey.SaveDiskPath != null;
                if (saveDisk)
                {
                    CacheIndex cacheIndex = cacheIndexManager.GetCacheIndex(realKey);
                    if(cacheIndex == null)
                    {
                        return null;
                    }

                    string filePath = baseCacheDir + cacheIndex.CacheFilePath;
                    if (cacheIndex.ExpireTime > 0)
                    {
                        int now = (int)(TimeManager.Instance.Now / 1000);
                        //过期
                        if (now - cacheIndex.WriteTime >= cacheIndex.ExpireTime)
                        {
                            FileUtil.DeleteFile(filePath);
                            cacheIndexManager.RemoveCacheIndex(realKey);
                            return null;
                        }
                    }
                   
                    if (!fileCache.ContainsKey(realKey))
                    {
                        if (!File.Exists(filePath))
                        {
                            FileUtil.DeleteFile(filePath);
                            cacheIndexManager.RemoveCacheIndex(realKey);
                            return null;
                        }

                        ByteBuffer buffer = FileUtil.ReadFileToByteArray(filePath);
                        if (buffer == null)
                        {
                            FileUtil.DeleteFile(filePath);
                            cacheIndexManager.RemoveCacheIndex(realKey);
                            return null;
                        }
                        fileCache.Add(realKey, buffer.ToArray());
                        buffer.ResetReaderIndex();
                    }
                    return TranscoderManager.Instance.Decode(keyInfo.CacheKey.Transcoder, fileCache[realKey], keyInfo.CacheKey.IsNativeFile);
                }
                else
                {
                    if (!localCache.ContainsKey(realKey))
                    {
                        return null;
                    }
                    return localCache[realKey];
                }
            }
        }
        public bool GetKeyExist(CacheKeyInfo keyInfo)
        {
            lock (this)
            {            
                string realKey = string.Empty;    
                if (keyInfo.CacheKey.IsNativeFile)
                {
                    realKey = keyInfo.GetRealKey();
                }
                else
                {
                    realKey = keyInfo.GetRealKey() + ".cache";
                }

                bool saveDisk = keyInfo.CacheKey.SaveDiskPath != null;
                if (saveDisk)
                {
                    CacheIndex cacheIndex = cacheIndexManager.GetCacheIndex(realKey);
                    if (cacheIndex == null)
                    {
                        return false;
                    }

                    string filePath = baseCacheDir + cacheIndex.CacheFilePath;
                    if (cacheIndex.ExpireTime > 0)
                    {
                        int now = (int)(TimeManager.Instance.Now / 1000);
                        //过期
                        if (now - cacheIndex.WriteTime >= cacheIndex.ExpireTime)
                        {
                            FileUtil.DeleteFile(filePath);
                            cacheIndexManager.RemoveCacheIndex(realKey);
                            return false;
                        }
                    }
                    
                    if (fileCache.ContainsKey(realKey))
                    {
                        return true;
                    }

                    if (!File.Exists(filePath))
                    {
                        FileUtil.DeleteFile(filePath);
                        cacheIndexManager.RemoveCacheIndex(realKey);
                        return false;
                    }
                    return true;
                }
                else
                {
                    return localCache.ContainsKey(realKey);
                }
            }
        }
        public void ClearCache()
        {
            if (Directory.Exists(m_strCacheDir))
            {
                Directory.Delete(m_strCacheDir,true);
            }
        }

        public void Del(CacheKeyInfo keyInfo)
        {
            lock (this)
            {
                string realKey = string.Empty;
                if (keyInfo.CacheKey.IsNativeFile)
                {
                    realKey = keyInfo.GetRealKey();
                }
                else
                {
                    realKey = keyInfo.GetRealKey() + ".cache";
                }
                bool saveDisk = keyInfo.CacheKey.SaveDiskPath != null;
                if (saveDisk)
                {
                    string cacheFile = baseCacheDir + keyInfo.CacheKey.SaveDiskPath + "/" + realKey;
                    if (File.Exists(cacheFile))
                    {
                        File.Delete(cacheFile);
                    }
                    cacheIndexManager.RemoveCacheIndex(cacheFile);
                    if (fileCache.ContainsKey(realKey))
                    {
                        fileCache.Remove(realKey);
                    }
                }
                else
                {
                    if (localCache.ContainsKey(realKey))
                    {
                        localCache.Remove(realKey);
                    }
                }
            }
        }
    }
}
