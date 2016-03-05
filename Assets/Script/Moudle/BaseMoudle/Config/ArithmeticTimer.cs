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
  public partial class ArithmeticTimer : TBase
  {
    private double _difficulty;
    private int _timer;

    public double Difficulty
    {
      get
      {
        return _difficulty;
      }
      set
      {
        __isset.difficulty = true;
        this._difficulty = value;
      }
    }

    public int Timer
    {
      get
      {
        return _timer;
      }
      set
      {
        __isset.timer = true;
        this._timer = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool difficulty;
      public bool timer;
    }

    public ArithmeticTimer() {
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
              Difficulty = iprot.ReadDouble();
            } else { 
              TProtocolUtil.Skip(iprot, field.Type);
            }
            break;
          case 20:
            if (field.Type == TType.I32) {
              Timer = iprot.ReadI32();
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
      TStruct struc = new TStruct("ArithmeticTimer");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (__isset.difficulty) {
        field.Name = "difficulty";
        field.Type = TType.Double;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        oprot.WriteDouble(Difficulty);
        oprot.WriteFieldEnd();
      }
      if (__isset.timer) {
        field.Name = "timer";
        field.Type = TType.I32;
        field.ID = 20;
        oprot.WriteFieldBegin(field);
        oprot.WriteI32(Timer);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("ArithmeticTimer(");
      sb.Append("Difficulty: ");
      sb.Append(Difficulty);
      sb.Append(",Timer: ");
      sb.Append(Timer);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
