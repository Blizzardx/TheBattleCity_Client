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
  public partial class FuncConfigTable : TBase
  {
    private Dictionary<int, Config.FuncGroup> _funcMap;

    public Dictionary<int, Config.FuncGroup> FuncMap
    {
      get
      {
        return _funcMap;
      }
      set
      {
        __isset.funcMap = true;
        this._funcMap = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool funcMap;
    }

    public FuncConfigTable() {
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
                FuncMap = new Dictionary<int, Config.FuncGroup>();
                TMap _map5 = iprot.ReadMapBegin();
                for( int _i6 = 0; _i6 < _map5.Count; ++_i6)
                {
                  int _key7;
                  Config.FuncGroup _val8;
                  _key7 = iprot.ReadI32();
                  _val8 = new Config.FuncGroup();
                  _val8.Read(iprot);
                  FuncMap[_key7] = _val8;
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
      TStruct struc = new TStruct("FuncConfigTable");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (FuncMap != null && __isset.funcMap) {
        field.Name = "funcMap";
        field.Type = TType.Map;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteMapBegin(new TMap(TType.I32, TType.Struct, FuncMap.Count));
          foreach (int _iter9 in FuncMap.Keys)
          {
            oprot.WriteI32(_iter9);
            FuncMap[_iter9].Write(oprot);
          }
          oprot.WriteMapEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("FuncConfigTable(");
      sb.Append("FuncMap: ");
      sb.Append(FuncMap);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
