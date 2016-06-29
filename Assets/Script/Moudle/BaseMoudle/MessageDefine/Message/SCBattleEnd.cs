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

namespace NetWork.Auto
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class SCBattleEnd : TBase
  {
    private string _errorInfo;
    private bool _isWin;

    public string ErrorInfo
    {
      get
      {
        return _errorInfo;
      }
      set
      {
        __isset.errorInfo = true;
        this._errorInfo = value;
      }
    }

    public bool IsWin
    {
      get
      {
        return _isWin;
      }
      set
      {
        __isset.isWin = true;
        this._isWin = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool errorInfo;
      public bool isWin;
    }

    public SCBattleEnd() {
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
          case 10:
            if (field.Type == TType.String) {
              ErrorInfo = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.Bool) {
              IsWin = iprot.ReadBool();
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
      TStruct struc = new TStruct("SCBattleEnd");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (ErrorInfo != null && __isset.errorInfo) {
        field.Name = "errorInfo";
        field.Type = TType.String;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(ErrorInfo);
        oprot.WriteFieldEnd();
      }
      if (__isset.isWin) {
        field.Name = "isWin";
        field.Type = TType.Bool;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(IsWin);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("SCBattleEnd(");
      sb.Append("ErrorInfo: ");
      sb.Append(ErrorInfo);
      sb.Append(",IsWin: ");
      sb.Append(IsWin);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
