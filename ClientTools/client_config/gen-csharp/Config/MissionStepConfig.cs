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
  public partial class MissionStepConfig : TBase
  {
    private int _id;
    private int _missionId;
    private int _sceneId;
    private int _sceneLimitId;
    private int _sceneFuncId;
    private int _completeLimitId;
    private int _completeFuncId;
    private int _guideMessageId;
    private string _guideAudioId;

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

    public int MissionId
    {
      get
      {
        return _missionId;
      }
      set
      {
        __isset.missionId = true;
        this._missionId = value;
      }
    }

    public int SceneId
    {
      get
      {
        return _sceneId;
      }
      set
      {
        __isset.sceneId = true;
        this._sceneId = value;
      }
    }

    public int SceneLimitId
    {
      get
      {
        return _sceneLimitId;
      }
      set
      {
        __isset.sceneLimitId = true;
        this._sceneLimitId = value;
      }
    }

    public int SceneFuncId
    {
      get
      {
        return _sceneFuncId;
      }
      set
      {
        __isset.sceneFuncId = true;
        this._sceneFuncId = value;
      }
    }

    public int CompleteLimitId
    {
      get
      {
        return _completeLimitId;
      }
      set
      {
        __isset.completeLimitId = true;
        this._completeLimitId = value;
      }
    }

    public int CompleteFuncId
    {
      get
      {
        return _completeFuncId;
      }
      set
      {
        __isset.completeFuncId = true;
        this._completeFuncId = value;
      }
    }

    public int GuideMessageId
    {
      get
      {
        return _guideMessageId;
      }
      set
      {
        __isset.guideMessageId = true;
        this._guideMessageId = value;
      }
    }

    public string GuideAudioId
    {
      get
      {
        return _guideAudioId;
      }
      set
      {
        __isset.guideAudioId = true;
        this._guideAudioId = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool id;
      public bool missionId;
      public bool sceneId;
      public bool sceneLimitId;
      public bool sceneFuncId;
      public bool completeLimitId;
      public bool completeFuncId;
      public bool guideMessageId;
      public bool guideAudioId;
    }

    public MissionStepConfig() {
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
          case 10:
            if (field.Type == TType.I32) {
              MissionId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.I32) {
              SceneId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 30:
            if (field.Type == TType.I32) {
              SceneLimitId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 40:
            if (field.Type == TType.I32) {
              SceneFuncId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 50:
            if (field.Type == TType.I32) {
              CompleteLimitId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 60:
            if (field.Type == TType.I32) {
              CompleteFuncId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 61:
            if (field.Type == TType.I32) {
              GuideMessageId = iprot.ReadI32();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 62:
            if (field.Type == TType.String) {
              GuideAudioId = iprot.ReadString();
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
      TStruct struc = new TStruct("MissionStepConfig");
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
      if (__isset.missionId) {
        field.Name = "missionId";
        field.Type = TType.I32;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(MissionId);
        oprot.WriteFieldEnd();
      }
      if (__isset.sceneId) {
        field.Name = "sceneId";
        field.Type = TType.I32;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(SceneId);
        oprot.WriteFieldEnd();
      }
      if (__isset.sceneLimitId) {
        field.Name = "sceneLimitId";
        field.Type = TType.I32;
        field.ID = 30;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(SceneLimitId);
        oprot.WriteFieldEnd();
      }
      if (__isset.sceneFuncId) {
        field.Name = "sceneFuncId";
        field.Type = TType.I32;
        field.ID = 40;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(SceneFuncId);
        oprot.WriteFieldEnd();
      }
      if (__isset.completeLimitId) {
        field.Name = "completeLimitId";
        field.Type = TType.I32;
        field.ID = 50;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(CompleteLimitId);
        oprot.WriteFieldEnd();
      }
      if (__isset.completeFuncId) {
        field.Name = "completeFuncId";
        field.Type = TType.I32;
        field.ID = 60;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(CompleteFuncId);
        oprot.WriteFieldEnd();
      }
      if (__isset.guideMessageId) {
        field.Name = "guideMessageId";
        field.Type = TType.I32;
        field.ID = 61;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(GuideMessageId);
        oprot.WriteFieldEnd();
      }
      if (GuideAudioId != null && __isset.guideAudioId) {
        field.Name = "guideAudioId";
        field.Type = TType.String;
        field.ID = 62;
        oprot.WriteFieldBegin(field);
        oprot.WriteString(GuideAudioId);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("MissionStepConfig(");
      sb.Append("Id: ");
      sb.Append(Id);
      sb.Append(",MissionId: ");
      sb.Append(MissionId);
      sb.Append(",SceneId: ");
      sb.Append(SceneId);
      sb.Append(",SceneLimitId: ");
      sb.Append(SceneLimitId);
      sb.Append(",SceneFuncId: ");
      sb.Append(SceneFuncId);
      sb.Append(",CompleteLimitId: ");
      sb.Append(CompleteLimitId);
      sb.Append(",CompleteFuncId: ");
      sb.Append(CompleteFuncId);
      sb.Append(",GuideMessageId: ");
      sb.Append(GuideMessageId);
      sb.Append(",GuideAudioId: ");
      sb.Append(GuideAudioId);
      sb.Append(")");
      return sb.ToString();
    }

  }

}