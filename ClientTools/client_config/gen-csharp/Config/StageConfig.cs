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
  public partial class StageConfig : TBase
  {
    private int _id;
    private int _nextStageId;
    private int _nameMessageId;
    private string _nameAudioId;
    private int _descMessageId;
    private string _descAudioId;
    private int _helpMessageId;
    private string _helpAudioId;
    private int _stageMapId;
    private short _recommendLevel;
    private string _stageIcon;
    private int _enterLimitId;
    private int _enterFuncId;
    private int _winLimitId;
    private int _winFuncId;
    private int _loseFuncId;

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

    public int NextStageId
    {
      get
      {
        return _nextStageId;
      }
      set
      {
        __isset.nextStageId = true;
        this._nextStageId = value;
      }
    }

    public int NameMessageId
    {
      get
      {
        return _nameMessageId;
      }
      set
      {
        __isset.nameMessageId = true;
        this._nameMessageId = value;
      }
    }

    public string NameAudioId
    {
      get
      {
        return _nameAudioId;
      }
      set
      {
        __isset.nameAudioId = true;
        this._nameAudioId = value;
      }
    }

    public int DescMessageId
    {
      get
      {
        return _descMessageId;
      }
      set
      {
        __isset.descMessageId = true;
        this._descMessageId = value;
      }
    }

    public string DescAudioId
    {
      get
      {
        return _descAudioId;
      }
      set
      {
        __isset.descAudioId = true;
        this._descAudioId = value;
      }
    }

    public int HelpMessageId
    {
      get
      {
        return _helpMessageId;
      }
      set
      {
        __isset.helpMessageId = true;
        this._helpMessageId = value;
      }
    }

    public string HelpAudioId
    {
      get
      {
        return _helpAudioId;
      }
      set
      {
        __isset.helpAudioId = true;
        this._helpAudioId = value;
      }
    }

    public int StageMapId
    {
      get
      {
        return _stageMapId;
      }
      set
      {
        __isset.stageMapId = true;
        this._stageMapId = value;
      }
    }

    public short RecommendLevel
    {
      get
      {
        return _recommendLevel;
      }
      set
      {
        __isset.recommendLevel = true;
        this._recommendLevel = value;
      }
    }

    public string StageIcon
    {
      get
      {
        return _stageIcon;
      }
      set
      {
        __isset.stageIcon = true;
        this._stageIcon = value;
      }
    }

    public int EnterLimitId
    {
      get
      {
        return _enterLimitId;
      }
      set
      {
        __isset.enterLimitId = true;
        this._enterLimitId = value;
      }
    }

    public int EnterFuncId
    {
      get
      {
        return _enterFuncId;
      }
      set
      {
        __isset.enterFuncId = true;
        this._enterFuncId = value;
      }
    }

    public int WinLimitId
    {
      get
      {
        return _winLimitId;
      }
      set
      {
        __isset.winLimitId = true;
        this._winLimitId = value;
      }
    }

    public int WinFuncId
    {
      get
      {
        return _winFuncId;
      }
      set
      {
        __isset.winFuncId = true;
        this._winFuncId = value;
      }
    }

    public int LoseFuncId
    {
      get
      {
        return _loseFuncId;
      }
      set
      {
        __isset.loseFuncId = true;
        this._loseFuncId = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool id;
      public bool nextStageId;
      public bool nameMessageId;
      public bool nameAudioId;
      public bool descMessageId;
      public bool descAudioId;
      public bool helpMessageId;
      public bool helpAudioId;
      public bool stageMapId;
      public bool recommendLevel;
      public bool stageIcon;
      public bool enterLimitId;
      public bool enterFuncId;
      public bool winLimitId;
      public bool winFuncId;
      public bool loseFuncId;
    }

    public StageConfig() {
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
              Id = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 11:
            if (field.Type == TType.I32) {
              NextStageId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.I32) {
              NameMessageId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 21:
            if (field.Type == TType.String) {
              NameAudioId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 30:
            if (field.Type == TType.I32) {
              DescMessageId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 31:
            if (field.Type == TType.String) {
              DescAudioId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 40:
            if (field.Type == TType.I32) {
              HelpMessageId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 41:
            if (field.Type == TType.String) {
              HelpAudioId = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 50:
            if (field.Type == TType.I32) {
              StageMapId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 51:
            if (field.Type == TType.I16) {
              RecommendLevel = iprot.ReadI16();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 60:
            if (field.Type == TType.String) {
              StageIcon = iprot.ReadString();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 90:
            if (field.Type == TType.I32) {
              EnterLimitId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 100:
            if (field.Type == TType.I32) {
              EnterFuncId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 130:
            if (field.Type == TType.I32) {
              WinLimitId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 140:
            if (field.Type == TType.I32) {
              WinFuncId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 141:
            if (field.Type == TType.I32) {
              LoseFuncId = iprot.ReadI32();
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
      TStruct struc = new TStruct("StageConfig");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.id) {
        field.Name = "id";
        field.Type = TType.I32;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Id);
        oprot.WriteFieldEnd();
      }
      if (__isset.nextStageId) {
        field.Name = "nextStageId";
        field.Type = TType.I32;
        field.ID = 11;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(NextStageId);
        oprot.WriteFieldEnd();
      }
      if (__isset.nameMessageId) {
        field.Name = "nameMessageId";
        field.Type = TType.I32;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(NameMessageId);
        oprot.WriteFieldEnd();
      }
      if (NameAudioId != null && __isset.nameAudioId) {
        field.Name = "nameAudioId";
        field.Type = TType.String;
        field.ID = 21;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(NameAudioId);
        oprot.WriteFieldEnd();
      }
      if (__isset.descMessageId) {
        field.Name = "descMessageId";
        field.Type = TType.I32;
        field.ID = 30;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(DescMessageId);
        oprot.WriteFieldEnd();
      }
      if (DescAudioId != null && __isset.descAudioId) {
        field.Name = "descAudioId";
        field.Type = TType.String;
        field.ID = 31;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(DescAudioId);
        oprot.WriteFieldEnd();
      }
      if (__isset.helpMessageId) {
        field.Name = "helpMessageId";
        field.Type = TType.I32;
        field.ID = 40;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(HelpMessageId);
        oprot.WriteFieldEnd();
      }
      if (HelpAudioId != null && __isset.helpAudioId) {
        field.Name = "helpAudioId";
        field.Type = TType.String;
        field.ID = 41;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(HelpAudioId);
        oprot.WriteFieldEnd();
      }
      if (__isset.stageMapId) {
        field.Name = "stageMapId";
        field.Type = TType.I32;
        field.ID = 50;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(StageMapId);
        oprot.WriteFieldEnd();
      }
      if (__isset.recommendLevel) {
        field.Name = "recommendLevel";
        field.Type = TType.I16;
        field.ID = 51;
        oprot.WriteFieldBegin(field);
        oprot.WriteI16(RecommendLevel);
        oprot.WriteFieldEnd();
      }
      if (StageIcon != null && __isset.stageIcon) {
        field.Name = "stageIcon";
        field.Type = TType.String;
        field.ID = 60;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(StageIcon);
        oprot.WriteFieldEnd();
      }
      if (__isset.enterLimitId) {
        field.Name = "enterLimitId";
        field.Type = TType.I32;
        field.ID = 90;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(EnterLimitId);
        oprot.WriteFieldEnd();
      }
      if (__isset.enterFuncId) {
        field.Name = "enterFuncId";
        field.Type = TType.I32;
        field.ID = 100;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(EnterFuncId);
        oprot.WriteFieldEnd();
      }
      if (__isset.winLimitId) {
        field.Name = "winLimitId";
        field.Type = TType.I32;
        field.ID = 130;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(WinLimitId);
        oprot.WriteFieldEnd();
      }
      if (__isset.winFuncId) {
        field.Name = "winFuncId";
        field.Type = TType.I32;
        field.ID = 140;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(WinFuncId);
        oprot.WriteFieldEnd();
      }
      if (__isset.loseFuncId) {
        field.Name = "loseFuncId";
        field.Type = TType.I32;
        field.ID = 141;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(LoseFuncId);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("StageConfig(");
      sb.Append("Id: ");
      sb.Append(Id);
      sb.Append(",NextStageId: ");
      sb.Append(NextStageId);
      sb.Append(",NameMessageId: ");
      sb.Append(NameMessageId);
      sb.Append(",NameAudioId: ");
      sb.Append(NameAudioId);
      sb.Append(",DescMessageId: ");
      sb.Append(DescMessageId);
      sb.Append(",DescAudioId: ");
      sb.Append(DescAudioId);
      sb.Append(",HelpMessageId: ");
      sb.Append(HelpMessageId);
      sb.Append(",HelpAudioId: ");
      sb.Append(HelpAudioId);
      sb.Append(",StageMapId: ");
      sb.Append(StageMapId);
      sb.Append(",RecommendLevel: ");
      sb.Append(RecommendLevel);
      sb.Append(",StageIcon: ");
      sb.Append(StageIcon);
      sb.Append(",EnterLimitId: ");
      sb.Append(EnterLimitId);
      sb.Append(",EnterFuncId: ");
      sb.Append(EnterFuncId);
      sb.Append(",WinLimitId: ");
      sb.Append(WinLimitId);
      sb.Append(",WinFuncId: ");
      sb.Append(WinFuncId);
      sb.Append(",LoseFuncId: ");
      sb.Append(LoseFuncId);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
