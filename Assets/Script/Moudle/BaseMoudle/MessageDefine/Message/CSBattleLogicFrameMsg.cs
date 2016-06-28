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
  public partial class CSBattleLogicFrameMsg : TBase
  {
    private BattleCommandData _commandDataList;

    public BattleCommandData CommandDataList
    {
      get
      {
        return _commandDataList;
      }
      set
      {
        __isset.commandDataList = true;
        this._commandDataList = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool commandDataList;
    }

    public CSBattleLogicFrameMsg() {
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
            if (field.Type == TType.Struct) {
              CommandDataList = new BattleCommandData();
              CommandDataList.Read(iprot);
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
      TStruct struc = new TStruct("CSBattleLogicFrameMsg");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (CommandDataList != null && __isset.commandDataList) {
        field.Name = "commandDataList";
        field.Type = TType.Struct;
        field.ID = 10;
        oprot.WriteFieldBegin(field);
        CommandDataList.Write(oprot);
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("CSBattleLogicFrameMsg(");
      sb.Append("CommandDataList: ");
      sb.Append(CommandDataList== null ? "<null>" : CommandDataList.ToString());
      sb.Append(")");
      return sb.ToString();
    }

  }

}
