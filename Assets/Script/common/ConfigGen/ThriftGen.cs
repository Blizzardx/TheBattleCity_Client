using Assets.Scripts.Core.Utils;
using Communication;
using Thrift.Protocol;

public class ThriftGen
{
    public static void SaveData(TBase data, string savePath)
    {
        byte[] fileData = ThriftSerialize.Serialize(data);
        FileUtils.WriteByteFile(savePath,fileData);
    }
	
}
