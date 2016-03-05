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
  public partial class CloseSaleEvent : TBase
  {
    private long _saleId;
    private MTMessageType _messageType;

    public long SaleId
    {
      get
      {
        return _saleId;
      }
      set
      {
        __isset.saleId = true;
        this._saleId = value;
      }
    }

    /// <summary>
    /// 
    /// <seealso cref="MTMessageType"/>
    /// </summary>
    public MTMessageType MessageType
    {
      get
      {
        return _messageType;
      }
      set
      {
        __isset.messageType = true;
        this._messageType = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool saleId;
      public bool messageType;
    }

    public CloseSaleEvent() {
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
            if (field.Type == TType.I64) {
              SaleId = iprot.ReadI64();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.I32) {
              MessageType = (MTMessageType)iprot.ReadI32();
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
      TStruct struc = new TStruct("CloseSaleEvent");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.saleId) {
        field.Name = "saleId";
        field.Type = TType.I64;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI64(SaleId);
        oprot.WriteFieldEnd();
      }
      if (__isset.messageType) {
        field.Name = "messageType";
        field.Type = TType.I32;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32((int)MessageType);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("CloseSaleEvent(");
      sb.Append("SaleId: ");
      sb.Append(SaleId);
      sb.Append(",MessageType: ");
      sb.Append(MessageType);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
