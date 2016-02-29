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
public partial class PkgGeneral_RoleInfo : TBase
{
  private string _PlayerName;
  private short _MeshId;
  private short _MaterialId;
  private List<int> _EquipmentStore;

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

  public List<int> EquipmentStore
  {
    get
    {
      return _EquipmentStore;
    }
    set
    {
      __isset.EquipmentStore = true;
      this._EquipmentStore = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool PlayerName;
    public bool MeshId;
    public bool MaterialId;
    public bool EquipmentStore;
  }

  public PkgGeneral_RoleInfo() {
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
          if (field.Type == TType.I16) {
            MeshId = iprot.ReadI16();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 3:
          if (field.Type == TType.I16) {
            MaterialId = iprot.ReadI16();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 4:
          if (field.Type == TType.List) {
            {
              EquipmentStore = new List<int>();
              TList _list4 = iprot.ReadListBegin();
              for( int _i5 = 0; _i5 < _list4.Count; ++_i5)
              {
                int _elem6 = 0;
                _elem6 = iprot.ReadI32();
                EquipmentStore.Add(_elem6);
              }
              iprot.ReadListEnd();
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
    TStruct struc = new TStruct("PkgGeneral_RoleInfo");
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
    if (__isset.MeshId) {
      field.Name = "MeshId";
      field.Type = TType.I16;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      oprot.WriteI16(MeshId);
      oprot.WriteFieldEnd();
    }
    if (__isset.MaterialId) {
      field.Name = "MaterialId";
      field.Type = TType.I16;
      field.ID = 3;
      oprot.WriteFieldBegin(field);
      oprot.WriteI16(MaterialId);
      oprot.WriteFieldEnd();
    }
    if (EquipmentStore != null && __isset.EquipmentStore) {
      field.Name = "EquipmentStore";
      field.Type = TType.List;
      field.ID = 4;
      oprot.WriteFieldBegin(field);
      {
        oprot.WriteListBegin(new TList(TType.I32, EquipmentStore.Count));
        foreach (int _iter7 in EquipmentStore)
        {
          oprot.WriteI32(_iter7);
        }
        oprot.WriteListEnd();
      }
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("PkgGeneral_RoleInfo(");
    sb.Append("PlayerName: ");
    sb.Append(PlayerName);
    sb.Append(",MeshId: ");
    sb.Append(MeshId);
    sb.Append(",MaterialId: ");
    sb.Append(MaterialId);
    sb.Append(",EquipmentStore: ");
    sb.Append(EquipmentStore);
    sb.Append(")");
    return sb.ToString();
  }

}

