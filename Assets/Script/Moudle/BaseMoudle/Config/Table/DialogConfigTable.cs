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
  public partial class DialogConfigTable : TBase
  {
    private Dictionary<int, Config.DialogConfig> _dialogConfigMap;

    public Dictionary<int, Config.DialogConfig> DialogConfigMap
    {
      get
      {
        return _dialogConfigMap;
      }
      set
      {
        __isset.dialogConfigMap = true;
        this._dialogConfigMap = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool dialogConfigMap;
    }

    public DialogConfigTable() {
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
                DialogConfigMap = new Dictionary<int, Config.DialogConfig>();
                TMap _map40 = iprot.ReadMapBegin();
                for( int _i41 = 0; _i41 < _map40.Count; ++_i41)
                {
                  int _key42;
                  Config.DialogConfig _val43;
                  _key42 = iprot.ReadI32();
                  _val43 = new Config.DialogConfig();
                  _val43.Read(iprot);
                  DialogConfigMap[_key42] = _val43;
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
      TStruct struc = new TStruct("DialogConfigTable");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (DialogConfigMap != null && __isset.dialogConfigMap) {
        field.Name = "dialogConfigMap";
        field.Type = TType.Map;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteMapBegin(new TMap(TType.I32, TType.Struct, DialogConfigMap.Count));
          foreach (int _iter44 in DialogConfigMap.Keys)
          {
            oprot.WriteI32(_iter44);
            DialogConfigMap[_iter44].Write(oprot);
          }
          oprot.WriteMapEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("DialogConfigTable(");
      sb.Append("DialogConfigMap: ");
      sb.Append(DialogConfigMap);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
