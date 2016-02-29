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
public partial class PkgCToS_Login : TBase
{
  private string _LoginId;
  private string _Password;

  public string LoginId
  {
    get
    {
      return _LoginId;
    }
    set
    {
      __isset.LoginId = true;
      this._LoginId = value;
    }
  }

  public string Password
  {
    get
    {
      return _Password;
    }
    set
    {
      __isset.Password = true;
      this._Password = value;
    }
  }


  public Isset __isset;
  #if !SILVERLIGHT
  [Serializable]
  #endif
  public struct Isset {
    public bool LoginId;
    public bool Password;
  }

  public PkgCToS_Login() {
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
            LoginId = iprot.ReadString();
          } else { 
            TProtocolUtil.Skip(iprot, field.Type);
          }
          break;
        case 2:
          if (field.Type == TType.String) {
            Password = iprot.ReadString();
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
    TStruct struc = new TStruct("PkgCToS_Login");
    oprot.WriteStructBegin(struc);
    TField field = new TField();
    if (LoginId != null && __isset.LoginId) {
      field.Name = "LoginId";
      field.Type = TType.String;
      field.ID = 1;
      oprot.WriteFieldBegin(field);
      oprot.WriteString(LoginId);
      oprot.WriteFieldEnd();
    }
    if (Password != null && __isset.Password) {
      field.Name = "Password";
      field.Type = TType.String;
      field.ID = 2;
      oprot.WriteFieldBegin(field);
      oprot.WriteString(Password);
      oprot.WriteFieldEnd();
    }
    oprot.WriteFieldStop();
    oprot.WriteStructEnd();
  }

  public override string ToString() {
    StringBuilder sb = new StringBuilder("PkgCToS_Login(");
    sb.Append("LoginId: ");
    sb.Append(LoginId);
    sb.Append(",Password: ");
    sb.Append(Password);
    sb.Append(")");
    return sb.ToString();
  }

}

