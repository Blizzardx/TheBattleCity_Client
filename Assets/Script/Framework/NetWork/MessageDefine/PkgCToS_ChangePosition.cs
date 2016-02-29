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


#if !SILVERLIGHT
[Serializable]
#endif
public partial class PkgCToS_ChangePosition : TBase
{
  private string _PlayerName;
  private sbyte _Position;

  public string PlayerName
  {
    get
    {
      return _PlayerName;
    }
    set
    {
      __isset.PlayerName = true;
      this._PlayerName = value;
    }
  }

  public sbyte Position
  {
    get
    {
      return _Position;
    }
    set
    {
      __isset.Position = true;
      this._Position = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool PlayerName;
    public bool Position;
  }

  public PkgCToS_ChangePosition() {
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
          if (field.Type == TType.String) {
            PlayerName = iprot.ReadString();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 2:
          if (field.Type == TType.Byte) {
            Position = iprot.ReadByte();
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
    TStruct struc = new TStruct("PkgCToS_ChangePosition");
    oprot.WriteStructBegin(struc);
    TField field = new TField();
    if (PlayerName != null && __isset.PlayerName) {
      field.Name = "PlayerName";
      field.Type = TType.String;
      field.ID = 1;
      oprot.WriteFieldBegin(field);
      oprot.WriteString(PlayerName);
      oprot.WriteFieldEnd();
    }
    if (__isset.Position) {
      field.Name = "Position";
      field.Type = TType.Byte;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      oprot.WriteByte(Position);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("PkgCToS_ChangePosition(");
    sb.Append("PlayerName: ");
    sb.Append(PlayerName);
    sb.Append(",Position: ");
    sb.Append(Position);
    sb.Append(")");
    return sb.ToString();
  }

}

