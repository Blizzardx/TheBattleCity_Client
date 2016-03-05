namespace Cache
{
    public class CacheKeyInfo
    {
        private CacheKey cacheKey;

        private object param;

        public CacheKeyInfo(CacheKey cacheKey, object param)
        {
            this.cacheKey = cacheKey;
            this.param = param;
        }

        public CacheKey CacheKey
        {
            get
            {
                return cacheKey;
            }
        }

        public string GetRealKey()
        {
            return cacheKey.Flag + "_" + cacheKey.Version + "_" + param;
        }

        public override int GetHashCode()
        {
            int result = 17;
            result = 31 * result + GetRealKey().GetHashCode();
            return result;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is CacheKeyInfo))
            {
                return false;
            }
            CacheKeyInfo other = obj as CacheKeyInfo;
            if (!GetRealKey().Equals(other.GetRealKey()))
            {
                return false;
            }
            return true;
        }

    }
    public class CacheKey
    {
        private string flag;
        private int version;
        private int expireTime;
        private bool canCompress;
        private ITranscoder transcoder;
        private string saveDiskPath;
        private bool isNativeFile;
        
        public int ExpireTime
        {
            get
            {
                return expireTime;
            }
        }

        public bool CanCompress
        {
            get
            {
                return canCompress;
            }
        }

        public ITranscoder Transcoder
        {
            get
            {
                return transcoder;
            }
        }

        public string SaveDiskPath
        {
            get
            {
                return saveDiskPath;
            }
        }
        public string Flag
        {
            get
            {
                return flag;
            }
        }

        public int Version
        {
            get
            {
                return version;
            }
        }
        public bool IsNativeFile 
        {
            get
            {
                return isNativeFile;
            }
        }
        public CacheKey(string flag, int version) : this(flag, version, 0, false, null, null)
        {
           
        }

        public CacheKey(string flag, int version, int expireTime, bool canCompress, ITranscoder transcoder, string saveDiskPath)
        {
            this.flag = flag;
            this.version = version;
            this.expireTime = expireTime;
            this.canCompress = canCompress;
            this.transcoder = transcoder;
            this.saveDiskPath = saveDiskPath;
        }

        public CacheKeyInfo BuildCacheInfo(object param)
        {
            CacheKeyInfo cacheInfo = new CacheKeyInfo(this, param);
            return cacheInfo;
        }
    }
}
