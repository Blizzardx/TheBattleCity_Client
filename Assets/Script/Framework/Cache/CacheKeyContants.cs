using Cache.Transcoder;
using System;

namespace Cache
{
    public class CacheKeyContants
    {
        public static readonly CacheKey IMAGE_KEY = new CacheKey("Image", 0, 3600 * 240, false, new BytesTranscoder(), "/Image");
        public static readonly CacheKey STRING_KEY = new CacheKey("String", 0, 3600 * 240, true, new StringTranscoder(), "/String");
    }
}
