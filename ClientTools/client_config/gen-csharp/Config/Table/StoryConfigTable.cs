/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace Config.Table
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class StoryConfigTable : TBase
  {
    private Dictionary<int, List<Config.StoryConfig>> _storyConfigMap;

    public Dictionary<int, List<Config.StoryConfig>> StoryConfigMap
    {
      get
      {
        return _storyConfigMap;
      }
      set
      {
        __isset.storyConfigMap = true;
        this._storyConfigMap = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool storyConfigMap;
    }

    public StoryConfigTable() {
    }

    public void Read (TProtocol iprot)
    {
      TField field;
      iprot.ReadStructBegin();
      while (true)
      {
        field = iprot.ReadFieldBegin();
        if (field.Type == TType.Stop) { 
          break;
        }
        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.Map) {
              {
                StoryConfigMap = new Dictionary<int, List<Config.StoryConfig>>();
                TMap _map141 = iprot.ReadMapBegin();
                for( int _i142 = 0; _i142 < _map141.Count; ++_i142)
                {
                  int _key143;
                  List<Config.StoryConfig> _val144;
                  _key143 = iprot.ReadI32();
                  {
                    _val144 = new List<Config.StoryConfig>();
                    TList _list145 = iprot.ReadListBegin();
                    for( int _i146 = 0; _i146 < _list145.Count; ++_i146)
                    {
                      Config.StoryConfig _elem147 = new Config.StoryConfig();
                      _elem147 = new Config.StoryConfig();
                      _elem147.Read(iprot);
                      _val144.Add(_elem147);
                    }
                    iprot.ReadListEnd();
                  }
                  StoryConfigMap[_key143] = _val144;
                }
                iprot.ReadMapEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          default: 
            TProtocolUtil.Skip(iprot, field.Type);
            break;
        }
        iprot.ReadFieldEnd();
      }
      iprot.ReadStructEnd();
    }

    public void Write(TProtocol oprot) {
      TStruct struc = new TStruct("StoryConfigTable");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (StoryConfigMap != null && __isset.storyConfigMap) {
        field.Name = "storyConfigMap";
        field.Type = TType.Map;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteMapBegin(new TMap(TType.I32, TType.List, StoryConfigMap.Count));
          foreach (int _iter148 in StoryConfigMap.Keys)
          {
            oprot.WriteI32(_iter148);
            {
              oprot.WriteListBegin(new TList(TType.Struct, StoryConfigMap[_iter148].Count));
              foreach (Config.StoryConfig _iter149 in StoryConfigMap[_iter148])
              {
                _iter149.Write(oprot);
              }
              oprot.WriteListEnd();
            }
          }
          oprot.WriteMapEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("StoryConfigTable(");
      sb.Append("StoryConfigMap: ");
      sb.Append(StoryConfigMap);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
