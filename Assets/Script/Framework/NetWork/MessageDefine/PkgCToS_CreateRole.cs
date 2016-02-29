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
public partial class PkgCToS_CreateRole : TBase
{
  private string _PlayerName;
  private string _RoleName;
  private short _MeshId;
  private short _MaterialId;

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

  public string RoleName
  {
    get
    {
      return _RoleName;
    }
    set
    {
      __isset.RoleName = true;
      this._RoleName = value;
    }
  }

  public short MeshId
  {
    get
    {
      return _MeshId;
    }
    set
    {
      __isset.MeshId = true;
      this._MeshId = value;
    }
  }

  public short MaterialId
  {
    get
    {
      return _MaterialId;
    }
    set
    {
      __isset.MaterialId = true;
      this._MaterialId = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool PlayerName;
    public bool RoleName;
    public bool MeshId;
    public bool MaterialId;
  }

  public PkgCToS_CreateRole() {
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
          if (field.Type == TType.String) {
            RoleName = iprot.ReadString();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 3:
          if (field.Type == TType.I16) {
            MeshId = iprot.ReadI16();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 4:
          if (field.Type == TType.I16) {
            MaterialId = iprot.ReadI16();
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
    TStruct struc = new TStruct("PkgCToS_CreateRole");
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
    if (RoleName != null && __isset.RoleName) {
      field.Name = "RoleName";
      field.Type = TType.String;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      oprot.WriteString(RoleName);
      oprot.WriteFieldEnd();
    }
    if (__isset.MeshId) {
      field.Name = "MeshId";
      field.Type = TType.I16;
      field.ID = 3;
      oprot.WriteFieldBegin(field);
      oprot.WriteI16(MeshId);
      oprot.WriteFieldEnd();
    }
    if (__isset.MaterialId) {
      field.Name = "MaterialId";
      field.Type = TType.I16;
      field.ID = 4;
      oprot.WriteFieldBegin(field);
      oprot.WriteI16(MaterialId);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("PkgCToS_CreateRole(");
    sb.Append("PlayerName: ");
    sb.Append(PlayerName);
    sb.Append(",RoleName: ");
    sb.Append(RoleName);
    sb.Append(",MeshId: ");
    sb.Append(MeshId);
    sb.Append(",MaterialId: ");
    sb.Append(MaterialId);
    sb.Append(")");
    return sb.ToString();
  }

}

