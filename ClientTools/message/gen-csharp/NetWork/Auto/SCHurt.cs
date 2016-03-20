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
  public partial class SCHurt : TBase
  {
    private int _playerUid;
    private int _hurtValue;

    public int PlayerUid
    {
      get
      {
        return _playerUid;
      }
      set
      {
        __isset.playerUid = true;
        this._playerUid = value;
      }
    }

    public int HurtValue
    {
      get
      {
        return _hurtValue;
      }
      set
      {
        __isset.hurtValue = true;
        this._hurtValue = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool playerUid;
      public bool hurtValue;
    }

    public SCHurt() {
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
            if (field.Type == TType.I32) {
              PlayerUid = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 10:
            if (field.Type == TType.I32) {
              HurtValue = iprot.ReadI32();
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
      TStruct struc = new TStruct("SCHurt");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.playerUid) {
        field.Name = "playerUid";
        field.Type = TType.I32;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(PlayerUid);
        oprot.WriteFieldEnd();
      }
      if (__isset.hurtValue) {
        field.Name = "hurtValue";
        field.Type = TType.I32;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(HurtValue);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("SCHurt(");
      sb.Append("PlayerUid: ");
      sb.Append(PlayerUid);
      sb.Append(",HurtValue: ");
      sb.Append(HurtValue);
      sb.Append(")");
      return sb.ToString();
    }

  }

}