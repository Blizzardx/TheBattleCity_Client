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

namespace Config
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class NpcConfig : TBase
  {
    private int _id;
    private int _nameMsgId;
    private int _sex;
    private string _ModelResource;
    private int _age;
    private int _aiId;
    private int _clickFuncId;
    private bool _isInGroup;

    public int Id
    {
      get
      {
        return _id;
      }
      set
      {
        __isset.id = true;
        this._id = value;
      }
    }

    public int NameMsgId
    {
      get
      {
        return _nameMsgId;
      }
      set
      {
        __isset.nameMsgId = true;
        this._nameMsgId = value;
      }
    }

    public int Sex
    {
      get
      {
        return _sex;
      }
      set
      {
        __isset.sex = true;
        this._sex = value;
      }
    }

    public string ModelResource
    {
      get
      {
        return _ModelResource;
      }
      set
      {
        __isset.ModelResource = true;
        this._ModelResource = value;
      }
    }

    public int Age
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

    public int AiId
    {
      get
      {
        return _aiId;
      }
      set
      {
        __isset.aiId = true;
        this._aiId = value;
      }
    }

    public int ClickFuncId
    {
      get
      {
        return _clickFuncId;
      }
      set
      {
        __isset.clickFuncId = true;
        this._clickFuncId = value;
      }
    }

    public bool IsInGroup
    {
      get
      {
        return _isInGroup;
      }
      set
      {
        __isset.isInGroup = true;
        this._isInGroup = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool id;
      public bool nameMsgId;
      public bool sex;
      public bool ModelResource;
      public bool age;
      public bool aiId;
      public bool clickFuncId;
      public bool isInGroup;
    }

    public NpcConfig() {
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
              Id = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 2:
            if (field.Type == TType.I32) {
              NameMsgId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 3:
            if (field.Type == TType.I32) {
              Sex = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 4:
            if (field.Type == TType.String) {
              ModelResource = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 5:
            if (field.Type == TType.I32) {
              Age = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 6:
            if (field.Type == TType.I32) {
              AiId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 7:
            if (field.Type == TType.I32) {
              ClickFuncId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 8:
            if (field.Type == TType.Bool) {
              IsInGroup = iprot.ReadBool();
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
      TStruct struc = new TStruct("NpcConfig");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.id) {
        field.Name = "id";
        field.Type = TType.I32;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Id);
        oprot.WriteFieldEnd();
      }
      if (__isset.nameMsgId) {
        field.Name = "nameMsgId";
        field.Type = TType.I32;
        field.ID = 2;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(NameMsgId);
        oprot.WriteFieldEnd();
      }
      if (__isset.sex) {
        field.Name = "sex";
        field.Type = TType.I32;
        field.ID = 3;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Sex);
        oprot.WriteFieldEnd();
      }
      if (ModelResource != null && __isset.ModelResource) {
        field.Name = "ModelResource";
        field.Type = TType.String;
        field.ID = 4;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(ModelResource);
        oprot.WriteFieldEnd();
      }
      if (__isset.age) {
        field.Name = "age";
        field.Type = TType.I32;
        field.ID = 5;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Age);
        oprot.WriteFieldEnd();
      }
      if (__isset.aiId) {
        field.Name = "aiId";
        field.Type = TType.I32;
        field.ID = 6;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(AiId);
        oprot.WriteFieldEnd();
      }
      if (__isset.clickFuncId) {
        field.Name = "clickFuncId";
        field.Type = TType.I32;
        field.ID = 7;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(ClickFuncId);
        oprot.WriteFieldEnd();
      }
      if (__isset.isInGroup) {
        field.Name = "isInGroup";
        field.Type = TType.Bool;
        field.ID = 8;
        oprot.WriteFieldBegin(field);
        oprot.WriteBool(IsInGroup);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("NpcConfig(");
      sb.Append("Id: ");
      sb.Append(Id);
      sb.Append(",NameMsgId: ");
      sb.Append(NameMsgId);
      sb.Append(",Sex: ");
      sb.Append(Sex);
      sb.Append(",ModelResource: ");
      sb.Append(ModelResource);
      sb.Append(",Age: ");
      sb.Append(Age);
      sb.Append(",AiId: ");
      sb.Append(AiId);
      sb.Append(",ClickFuncId: ");
      sb.Append(ClickFuncId);
      sb.Append(",IsInGroup: ");
      sb.Append(IsInGroup);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
