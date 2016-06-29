using UnityEngine;

using System.IO;

public class ProtoSerializer
{
    public static byte[] Serialize<T>(T t)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            ProtoBuf.Serializer.Serialize(ms, t);
            byte[] data = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(data, 0, data.Length);
            return data;
        }
    }

    public static T DeSerialize<T>(byte[] content)
    {
        using (MemoryStream ms = new MemoryStream(content))
        {
            T t = ProtoBuf.Serializer.Deserialize<T>(ms);
            return t;
        }
    }
}
