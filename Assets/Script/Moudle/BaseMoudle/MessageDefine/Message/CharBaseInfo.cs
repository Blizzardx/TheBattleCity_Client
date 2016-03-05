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
  public partial class CharBaseInfo : TBase
  {
    private int _charId;
    private string _charName;
    private sbyte _gender;
    private sbyte _age;
    private int _fame;
    private short _level;
    private Dictionary<string, int> _charTalentMap;
    private sbyte _role;
    private byte[] _charDeail;

    public int CharId
    {
      get
      {
        return _charId;
      }
      set
      {
        __isset.charId = true;
        this._charId = value;
      }
    }

    public string CharName
    {
      get
      {
        return _charName;
      }
      set
      {
        __isset.charName = true;
        this._charName = value;
      }
    }

    public sbyte Gender
    {
      get
      {
        return _gender;
      }
      set
      {
        __isset.gender = true;
        this._gender = value;
      }
    }

    public sbyte Age
    {
      get
      {
        return _age;
      }
      set
      {
        __isset.age = true;
        this._age = value;
      }
    }

    public int Fame
    {
      get
      {
        return _fame;
      }
      set
      {
        __isset.fame = true;
        this._fame = value;
      }
    }

    public short Level
    {
      get
      {
        return _level;
      }
      set
      {
        __isset.level = true;
        this._level = value;
      }
    }

    public Dictionary<string, int> CharTalentMap
    {
      get
      {
        return _charTalentMap;
      }
      set
      {
        __isset.charTalentMap = true;
        this._charTalentMap = value;
      }
    }

    public sbyte Role
    {
      get
      {
        return _role;
      }
      set
      {
        __isset.role = true;
        this._role = value;
      }
    }

    public byte[] CharDeail
    {
      get
      {
        return _charDeail;
      }
      set
      {
        __isset.charDeail = true;
        this._charDeail = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool charId;
      public bool charName;
      public bool gender;
      public bool age;
      public bool fame;
      public bool level;
      public bool charTalentMap;
      public bool role;
      public bool charDeail;
    }

    public CharBaseInfo() {
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
            if (field.Type == TType.I32) {
              CharId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.String) {
              CharName = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 30:
            if (field.Type == TType.Byte) {
              Gender = iprot.ReadByte();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 40:
            if (field.Type == TType.Byte) {
              Age = iprot.ReadByte();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 50:
            if (field.Type == TType.I32) {
              Fame = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 60:
            if (field.Type == TType.I16) {
              Level = iprot.ReadI16();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 70:
            if (field.Type == TType.Map) {
              {
                CharTalentMap = new Dictionary<string, int>();
                TMap _map0 = iprot.ReadMapBegin();
                for( int _i1 = 0; _i1 < _map0.Count; ++_i1)
                {
                  string _key2;
                  int _val3;
                  _key2 = iprot.ReadString();
                  _val3 = iprot.ReadI32();
                  CharTalentMap[_key2] = _val3;
                }
                iprot.ReadMapEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 80:
            if (field.Type == TType.Byte) {
              Role = iprot.ReadByte();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 90:
            if (field.Type == TType.String) {
              CharDeail = iprot.ReadBinary();
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
      TStruct struc = new TStruct("CharBaseInfo");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.charId) {
        field.Name = "charId";
        field.Type = TType.I32;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(CharId);
        oprot.WriteFieldEnd();
      }
      if (CharName != null && __isset.charName) {
        field.Name = "charName";
        field.Type = TType.String;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(CharName);
        oprot.WriteFieldEnd();
      }
      if (__isset.gender) {
        field.Name = "gender";
        field.Type = TType.Byte;
        field.ID = 30;
        oprot.WriteFieldBegin(field);
        oprot.WriteByte(Gender);
        oprot.WriteFieldEnd();
      }
      if (__isset.age) {
        field.Name = "age";
        field.Type = TType.Byte;
        field.ID = 40;
        oprot.WriteFieldBegin(field);
        oprot.WriteByte(Age);
        oprot.WriteFieldEnd();
      }
      if (__isset.fame) {
        field.Name = "fame";
        field.Type = TType.I32;
        field.ID = 50;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Fame);
        oprot.WriteFieldEnd();
      }
      if (__isset.level) {
        field.Name = "level";
        field.Type = TType.I16;
        field.ID = 60;
        oprot.WriteFieldBegin(field);
        oprot.WriteI16(Level);
        oprot.WriteFieldEnd();
      }
      if (CharTalentMap != null && __isset.charTalentMap) {
        field.Name = "charTalentMap";
        field.Type = TType.Map;
        field.ID = 70;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteMapBegin(new TMap(TType.String, TType.I32, CharTalentMap.Count));
          foreach (string _iter4 in CharTalentMap.Keys)
          {
            oprot.WriteString(_iter4);
            oprot.WriteI32(CharTalentMap[_iter4]);
          }
          oprot.WriteMapEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (__isset.role) {
        field.Name = "role";
        field.Type = TType.Byte;
        field.ID = 80;
        oprot.WriteFieldBegin(field);
        oprot.WriteByte(Role);
        oprot.WriteFieldEnd();
      }
      if (CharDeail != null && __isset.charDeail) {
        field.Name = "charDeail";
        field.Type = TType.String;
        field.ID = 90;
        oprot.WriteFieldBegin(field);
        oprot.WriteBinary(CharDeail);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("CharBaseInfo(");
      sb.Append("CharId: ");
      sb.Append(CharId);
      sb.Append(",CharName: ");
      sb.Append(CharName);
      sb.Append(",Gender: ");
      sb.Append(Gender);
      sb.Append(",Age: ");
      sb.Append(Age);
      sb.Append(",Fame: ");
      sb.Append(Fame);
      sb.Append(",Level: ");
      sb.Append(Level);
      sb.Append(",CharTalentMap: ");
      sb.Append(CharTalentMap);
      sb.Append(",Role: ");
      sb.Append(Role);
      sb.Append(",CharDeail: ");
      sb.Append(CharDeail);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
