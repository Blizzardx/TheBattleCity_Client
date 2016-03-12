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
  public partial class SCRoomList : TBase
  {
    private List<NetWork.Auto.RoomInfo> _roomList;

    public List<NetWork.Auto.RoomInfo> RoomList
    {
      get
      {
        return _roomList;
      }
      set
      {
        __isset.roomList = true;
        this._roomList = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool roomList;
    }

    public SCRoomList() {
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
            if (field.Type == TType.List) {
              {
                RoomList = new List<NetWork.Auto.RoomInfo>();
                TList _list0 = iprot.ReadListBegin();
                for( int _i1 = 0; _i1 < _list0.Count; ++_i1)
                {
                  NetWork.Auto.RoomInfo _elem2 = new NetWork.Auto.RoomInfo();
                  _elem2 = new NetWork.Auto.RoomInfo();
                  _elem2.Read(iprot);
                  RoomList.Add(_elem2);
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
      TStruct struc = new TStruct("SCRoomList");
      oprot.WriteStructBegin(struc);
      TField field = new TField();
      if (RoomList != null && __isset.roomList) {
        field.Name = "roomList";
        field.Type = TType.List;
        field.ID = 1;
        oprot.WriteFieldBegin(field);
        {
          oprot.WriteListBegin(new TList(TType.Struct, RoomList.Count));
          foreach (NetWork.Auto.RoomInfo _iter3 in RoomList)
          {
            _iter3.Write(oprot);
          }
          oprot.WriteListEnd();
        }
        oprot.WriteFieldEnd();
      }
      oprot.WriteFieldStop();
      oprot.WriteStructEnd();
    }

    public override string ToString() {
      StringBuilder sb = new StringBuilder("SCRoomList(");
      sb.Append("RoomList: ");
      sb.Append(RoomList);
      sb.Append(")");
      return sb.ToString();
    }

  }

}
