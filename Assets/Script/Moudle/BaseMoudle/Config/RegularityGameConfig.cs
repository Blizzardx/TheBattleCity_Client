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
  public partial class RegularityGameConfig : TBase
  {
    private double _difficultyid;
    private List<RegularityGameOption> _optionList;
    private List<string> _answerList;

    public double Difficultyid
    {
      get
      {
        return _difficultyid;
      }
      set
      {
        __isset.difficultyid = true;
        this._difficultyid = value;
      }
    }

    public List<RegularityGameOption> OptionList
    {
      get
      {
        return _optionList;
      }
      set
      {
        __isset.optionList = true;
        this._optionList = value;
      }
    }

    public List<string> AnswerList
    {
      get
      {
        return _answerList;
      }
      set
      {
        __isset.answerList = true;
        this._answerList = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool difficultyid;
      public bool optionList;
      public bool answerList;
    }

    public RegularityGameConfig() {
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
            if (field.Type == TType.Double) {
              Difficultyid = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.List) {
              {
                OptionList = new List<RegularityGameOption>();
                TList _list49 = iprot.ReadListBegin();
                for( int _i50 = 0; _i50 < _list49.Count; ++_i50)
                {
                  RegularityGameOption _elem51 = new RegularityGameOption();
                  _elem51 = new RegularityGameOption();
                  _elem51.Read(iprot);
                  OptionList.Add(_elem51);
                }
                iprot.ReadListEnd();
              }
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 30:
            if (field.Type == TType.List) {
              {
                AnswerList = new List<string>();
                TList _list52 = iprot.ReadListBegin();
                for( int _i53 = 0; _i53 < _list52.Count; ++_i53)
                {
                  string _elem54 = null;
                  _elem54 = iprot.ReadString();
                  AnswerList.Add(_elem54);
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
      TStruct struc = new TStruct("RegularityGameConfig");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.difficultyid) {
        field.Name = "difficultyid";
        field.Type = TType.Double;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Difficultyid);
        oprot.WriteFieldEnd();
      }
      if (OptionList != null && __isset.optionList) {
        field.Name = "optionList";
        field.Type = TType.List;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, OptionList.Count));
          foreach (RegularityGameOption _iter55 in OptionList)
          {
            _iter55.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      if (AnswerList != null && __isset.answerList) {
        field.Name = "answerList";
        field.Type = TType.List;
        field.ID = 30;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.String, AnswerList.Count));
          foreach (string _iter56 in AnswerList)
          {
            oprot.WriteString(_iter56);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("RegularityGameConfig(");
      sb.Append("Difficultyid: ");
      sb.Append(Difficultyid);
      sb.Append(",OptionList: ");
      sb.Append(OptionList);
      sb.Append(",AnswerList: ");
      sb.Append(AnswerList);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
