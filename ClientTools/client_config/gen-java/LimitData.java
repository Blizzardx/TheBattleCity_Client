/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
import org.apache.thrift.scheme.IScheme;
import org.apache.thrift.scheme.SchemeFactory;
import org.apache.thrift.scheme.StandardScheme;

import org.apache.thrift.scheme.TupleScheme;
import org.apache.thrift.protocol.TTupleProtocol;
import org.apache.thrift.protocol.TProtocolException;
import org.apache.thrift.EncodingUtils;
import org.apache.thrift.TException;
import org.apache.thrift.async.AsyncMethodCallback;
import org.apache.thrift.server.AbstractNonblockingServer.*;
import java.util.List;
import java.util.ArrayList;
import java.util.Map;
import java.util.HashMap;
import java.util.EnumMap;
import java.util.Set;
import java.util.HashSet;
import java.util.EnumSet;
import java.util.Collections;
import java.util.BitSet;
import java.nio.ByteBuffer;
import java.util.Arrays;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class LimitData implements org.apache.thrift.TBase<LimitData, LimitData._Fields>, java.io.Serializable, Cloneable, Comparable<LimitData> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("LimitData");

  private static final org.apache.thrift.protocol.TField ID_FIELD_DESC = new org.apache.thrift.protocol.TField("id", org.apache.thrift.protocol.TType.I32, (short)1);
  private static final org.apache.thrift.protocol.TField OPER_FIELD_DESC = new org.apache.thrift.protocol.TField("oper", org.apache.thrift.protocol.TType.BYTE, (short)2);
  private static final org.apache.thrift.protocol.TField MESSAGE_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("messageId", org.apache.thrift.protocol.TType.I32, (short)3);
  private static final org.apache.thrift.protocol.TField TARGET_FIELD_DESC = new org.apache.thrift.protocol.TField("target", org.apache.thrift.protocol.TType.BYTE, (short)4);
  private static final org.apache.thrift.protocol.TField PARAM_STRING_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("paramStringList", org.apache.thrift.protocol.TType.LIST, (short)5);
  private static final org.apache.thrift.protocol.TField PARAM_INT_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("paramIntList", org.apache.thrift.protocol.TType.LIST, (short)6);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new LimitDataStandardSchemeFactory());
    schemes.put(TupleScheme.class, new LimitDataTupleSchemeFactory());
  }

  public int id; // required
  public byte oper; // required
  public int messageId; // required
  public byte target; // required
  public List<String> paramStringList; // required
  public List<Integer> paramIntList; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    ID((short)1, "id"),
    OPER((short)2, "oper"),
    MESSAGE_ID((short)3, "messageId"),
    TARGET((short)4, "target"),
    PARAM_STRING_LIST((short)5, "paramStringList"),
    PARAM_INT_LIST((short)6, "paramIntList");

    private static final Map<String, _Fields> byName = new HashMap<String, _Fields>();

    static {
      for (_Fields field : EnumSet.allOf(_Fields.class)) {
        byName.put(field.getFieldName(), field);
      }
    }

    /**
     * Find the _Fields constant that matches fieldId, or null if its not found.
     */
    public static _Fields findByThriftId(int fieldId) {
      switch(fieldId) {
        case 1: // ID
          return ID;
        case 2: // OPER
          return OPER;
        case 3: // MESSAGE_ID
          return MESSAGE_ID;
        case 4: // TARGET
          return TARGET;
        case 5: // PARAM_STRING_LIST
          return PARAM_STRING_LIST;
        case 6: // PARAM_INT_LIST
          return PARAM_INT_LIST;
        default:
          return null;
      }
    }

    /**
     * Find the _Fields constant that matches fieldId, throwing an exception
     * if it is not found.
     */
    public static _Fields findByThriftIdOrThrow(int fieldId) {
      _Fields fields = findByThriftId(fieldId);
      if (fields == null) throw new IllegalArgumentException("Field " + fieldId + " doesn't exist!");
      return fields;
    }

    /**
     * Find the _Fields constant that matches name, or null if its not found.
     */
    public static _Fields findByName(String name) {
      return byName.get(name);
    }

    private final short _thriftId;
    private final String _fieldName;

    _Fields(short thriftId, String fieldName) {
      _thriftId = thriftId;
      _fieldName = fieldName;
    }

    public short getThriftFieldId() {
      return _thriftId;
    }

    public String getFieldName() {
      return _fieldName;
    }
  }

  // isset id assignments
  private static final int __ID_ISSET_ID = 0;
  private static final int __OPER_ISSET_ID = 1;
  private static final int __MESSAGEID_ISSET_ID = 2;
  private static final int __TARGET_ISSET_ID = 3;
  private byte __isset_bitfield = 0;
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.ID, new org.apache.thrift.meta_data.FieldMetaData("id", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32        , "LimitId")));
    tmpMap.put(_Fields.OPER, new org.apache.thrift.meta_data.FieldMetaData("oper", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.BYTE)));
    tmpMap.put(_Fields.MESSAGE_ID, new org.apache.thrift.meta_data.FieldMetaData("messageId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.TARGET, new org.apache.thrift.meta_data.FieldMetaData("target", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.BYTE)));
    tmpMap.put(_Fields.PARAM_STRING_LIST, new org.apache.thrift.meta_data.FieldMetaData("paramStringList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING))));
    tmpMap.put(_Fields.PARAM_INT_LIST, new org.apache.thrift.meta_data.FieldMetaData("paramIntList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(LimitData.class, metaDataMap);
  }

  public LimitData() {
  }

  public LimitData(
    int id,
    byte oper,
    int messageId,
    byte target,
    List<String> paramStringList,
    List<Integer> paramIntList)
  {
    this();
    this.id = id;
    setIdIsSet(true);
    this.oper = oper;
    setOperIsSet(true);
    this.messageId = messageId;
    setMessageIdIsSet(true);
    this.target = target;
    setTargetIsSet(true);
    this.paramStringList = paramStringList;
    this.paramIntList = paramIntList;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public LimitData(LimitData other) {
    __isset_bitfield = other.__isset_bitfield;
    this.id = other.id;
    this.oper = other.oper;
    this.messageId = other.messageId;
    this.target = other.target;
    if (other.isSetParamStringList()) {
      List<String> __this__paramStringList = new ArrayList<String>(other.paramStringList);
      this.paramStringList = __this__paramStringList;
    }
    if (other.isSetParamIntList()) {
      List<Integer> __this__paramIntList = new ArrayList<Integer>(other.paramIntList);
      this.paramIntList = __this__paramIntList;
    }
  }

  public LimitData deepCopy() {
    return new LimitData(this);
  }

  @Override
  public void clear() {
    setIdIsSet(false);
    this.id = 0;
    setOperIsSet(false);
    this.oper = 0;
    setMessageIdIsSet(false);
    this.messageId = 0;
    setTargetIsSet(false);
    this.target = 0;
    this.paramStringList = null;
    this.paramIntList = null;
  }

  public int getId() {
    return this.id;
  }

  public LimitData setId(int id) {
    this.id = id;
    setIdIsSet(true);
    return this;
  }

  public void unsetId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __ID_ISSET_ID);
  }

  /** Returns true if field id is set (has been assigned a value) and false otherwise */
  public boolean isSetId() {
    return EncodingUtils.testBit(__isset_bitfield, __ID_ISSET_ID);
  }

  public void setIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __ID_ISSET_ID, value);
  }

  public byte getOper() {
    return this.oper;
  }

  public LimitData setOper(byte oper) {
    this.oper = oper;
    setOperIsSet(true);
    return this;
  }

  public void unsetOper() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __OPER_ISSET_ID);
  }

  /** Returns true if field oper is set (has been assigned a value) and false otherwise */
  public boolean isSetOper() {
    return EncodingUtils.testBit(__isset_bitfield, __OPER_ISSET_ID);
  }

  public void setOperIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __OPER_ISSET_ID, value);
  }

  public int getMessageId() {
    return this.messageId;
  }

  public LimitData setMessageId(int messageId) {
    this.messageId = messageId;
    setMessageIdIsSet(true);
    return this;
  }

  public void unsetMessageId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __MESSAGEID_ISSET_ID);
  }

  /** Returns true if field messageId is set (has been assigned a value) and false otherwise */
  public boolean isSetMessageId() {
    return EncodingUtils.testBit(__isset_bitfield, __MESSAGEID_ISSET_ID);
  }

  public void setMessageIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __MESSAGEID_ISSET_ID, value);
  }

  public byte getTarget() {
    return this.target;
  }

  public LimitData setTarget(byte target) {
    this.target = target;
    setTargetIsSet(true);
    return this;
  }

  public void unsetTarget() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __TARGET_ISSET_ID);
  }

  /** Returns true if field target is set (has been assigned a value) and false otherwise */
  public boolean isSetTarget() {
    return EncodingUtils.testBit(__isset_bitfield, __TARGET_ISSET_ID);
  }

  public void setTargetIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __TARGET_ISSET_ID, value);
  }

  public int getParamStringListSize() {
    return (this.paramStringList == null) ? 0 : this.paramStringList.size();
  }

  public java.util.Iterator<String> getParamStringListIterator() {
    return (this.paramStringList == null) ? null : this.paramStringList.iterator();
  }

  public void addToParamStringList(String elem) {
    if (this.paramStringList == null) {
      this.paramStringList = new ArrayList<String>();
    }
    this.paramStringList.add(elem);
  }

  public List<String> getParamStringList() {
    return this.paramStringList;
  }

  public LimitData setParamStringList(List<String> paramStringList) {
    this.paramStringList = paramStringList;
    return this;
  }

  public void unsetParamStringList() {
    this.paramStringList = null;
  }

  /** Returns true if field paramStringList is set (has been assigned a value) and false otherwise */
  public boolean isSetParamStringList() {
    return this.paramStringList != null;
  }

  public void setParamStringListIsSet(boolean value) {
    if (!value) {
      this.paramStringList = null;
    }
  }

  public int getParamIntListSize() {
    return (this.paramIntList == null) ? 0 : this.paramIntList.size();
  }

  public java.util.Iterator<Integer> getParamIntListIterator() {
    return (this.paramIntList == null) ? null : this.paramIntList.iterator();
  }

  public void addToParamIntList(int elem) {
    if (this.paramIntList == null) {
      this.paramIntList = new ArrayList<Integer>();
    }
    this.paramIntList.add(elem);
  }

  public List<Integer> getParamIntList() {
    return this.paramIntList;
  }

  public LimitData setParamIntList(List<Integer> paramIntList) {
    this.paramIntList = paramIntList;
    return this;
  }

  public void unsetParamIntList() {
    this.paramIntList = null;
  }

  /** Returns true if field paramIntList is set (has been assigned a value) and false otherwise */
  public boolean isSetParamIntList() {
    return this.paramIntList != null;
  }

  public void setParamIntListIsSet(boolean value) {
    if (!value) {
      this.paramIntList = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case ID:
      if (value == null) {
        unsetId();
      } else {
        setId((Integer)value);
      }
      break;

    case OPER:
      if (value == null) {
        unsetOper();
      } else {
        setOper((Byte)value);
      }
      break;

    case MESSAGE_ID:
      if (value == null) {
        unsetMessageId();
      } else {
        setMessageId((Integer)value);
      }
      break;

    case TARGET:
      if (value == null) {
        unsetTarget();
      } else {
        setTarget((Byte)value);
      }
      break;

    case PARAM_STRING_LIST:
      if (value == null) {
        unsetParamStringList();
      } else {
        setParamStringList((List<String>)value);
      }
      break;

    case PARAM_INT_LIST:
      if (value == null) {
        unsetParamIntList();
      } else {
        setParamIntList((List<Integer>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case ID:
      return Integer.valueOf(getId());

    case OPER:
      return Byte.valueOf(getOper());

    case MESSAGE_ID:
      return Integer.valueOf(getMessageId());

    case TARGET:
      return Byte.valueOf(getTarget());

    case PARAM_STRING_LIST:
      return getParamStringList();

    case PARAM_INT_LIST:
      return getParamIntList();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case ID:
      return isSetId();
    case OPER:
      return isSetOper();
    case MESSAGE_ID:
      return isSetMessageId();
    case TARGET:
      return isSetTarget();
    case PARAM_STRING_LIST:
      return isSetParamStringList();
    case PARAM_INT_LIST:
      return isSetParamIntList();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof LimitData)
      return this.equals((LimitData)that);
    return false;
  }

  public boolean equals(LimitData that) {
    if (that == null)
      return false;

    boolean this_present_id = true;
    boolean that_present_id = true;
    if (this_present_id || that_present_id) {
      if (!(this_present_id && that_present_id))
        return false;
      if (this.id != that.id)
        return false;
    }

    boolean this_present_oper = true;
    boolean that_present_oper = true;
    if (this_present_oper || that_present_oper) {
      if (!(this_present_oper && that_present_oper))
        return false;
      if (this.oper != that.oper)
        return false;
    }

    boolean this_present_messageId = true;
    boolean that_present_messageId = true;
    if (this_present_messageId || that_present_messageId) {
      if (!(this_present_messageId && that_present_messageId))
        return false;
      if (this.messageId != that.messageId)
        return false;
    }

    boolean this_present_target = true;
    boolean that_present_target = true;
    if (this_present_target || that_present_target) {
      if (!(this_present_target && that_present_target))
        return false;
      if (this.target != that.target)
        return false;
    }

    boolean this_present_paramStringList = true && this.isSetParamStringList();
    boolean that_present_paramStringList = true && that.isSetParamStringList();
    if (this_present_paramStringList || that_present_paramStringList) {
      if (!(this_present_paramStringList && that_present_paramStringList))
        return false;
      if (!this.paramStringList.equals(that.paramStringList))
        return false;
    }

    boolean this_present_paramIntList = true && this.isSetParamIntList();
    boolean that_present_paramIntList = true && that.isSetParamIntList();
    if (this_present_paramIntList || that_present_paramIntList) {
      if (!(this_present_paramIntList && that_present_paramIntList))
        return false;
      if (!this.paramIntList.equals(that.paramIntList))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(LimitData other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetId()).compareTo(other.isSetId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.id, other.id);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetOper()).compareTo(other.isSetOper());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetOper()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.oper, other.oper);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetMessageId()).compareTo(other.isSetMessageId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetMessageId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.messageId, other.messageId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetTarget()).compareTo(other.isSetTarget());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetTarget()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.target, other.target);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetParamStringList()).compareTo(other.isSetParamStringList());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetParamStringList()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.paramStringList, other.paramStringList);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetParamIntList()).compareTo(other.isSetParamIntList());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetParamIntList()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.paramIntList, other.paramIntList);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    return 0;
  }

  public _Fields fieldForId(int fieldId) {
    return _Fields.findByThriftId(fieldId);
  }

  public void read(org.apache.thrift.protocol.TProtocol iprot) throws org.apache.thrift.TException {
    schemes.get(iprot.getScheme()).getScheme().read(iprot, this);
  }

  public void write(org.apache.thrift.protocol.TProtocol oprot) throws org.apache.thrift.TException {
    schemes.get(oprot.getScheme()).getScheme().write(oprot, this);
  }

  @Override
  public String toString() {
    StringBuilder sb = new StringBuilder("LimitData(");
    boolean first = true;

    sb.append("id:");
    sb.append(this.id);
    first = false;
    if (!first) sb.append(", ");
    sb.append("oper:");
    sb.append(this.oper);
    first = false;
    if (!first) sb.append(", ");
    sb.append("messageId:");
    sb.append(this.messageId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("target:");
    sb.append(this.target);
    first = false;
    if (!first) sb.append(", ");
    sb.append("paramStringList:");
    if (this.paramStringList == null) {
      sb.append("null");
    } else {
      sb.append(this.paramStringList);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("paramIntList:");
    if (this.paramIntList == null) {
      sb.append("null");
    } else {
      sb.append(this.paramIntList);
    }
    first = false;
    sb.append(")");
    return sb.toString();
  }

  public void validate() throws org.apache.thrift.TException {
    // check for required fields
    // check for sub-struct validity
  }

  private void writeObject(java.io.ObjectOutputStream out) throws java.io.IOException {
    try {
      write(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(out)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private void readObject(java.io.ObjectInputStream in) throws java.io.IOException, ClassNotFoundException {
    try {
      // it doesn't seem like you should have to do this, but java serialization is wacky, and doesn't call the default constructor.
      __isset_bitfield = 0;
      read(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(in)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private static class LimitDataStandardSchemeFactory implements SchemeFactory {
    public LimitDataStandardScheme getScheme() {
      return new LimitDataStandardScheme();
    }
  }

  private static class LimitDataStandardScheme extends StandardScheme<LimitData> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, LimitData struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.id = iprot.readI32();
              struct.setIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 2: // OPER
            if (schemeField.type == org.apache.thrift.protocol.TType.BYTE) {
              struct.oper = iprot.readByte();
              struct.setOperIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 3: // MESSAGE_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.messageId = iprot.readI32();
              struct.setMessageIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 4: // TARGET
            if (schemeField.type == org.apache.thrift.protocol.TType.BYTE) {
              struct.target = iprot.readByte();
              struct.setTargetIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 5: // PARAM_STRING_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list0 = iprot.readListBegin();
                struct.paramStringList = new ArrayList<String>(_list0.size);
                for (int _i1 = 0; _i1 < _list0.size; ++_i1)
                {
                  String _elem2;
                  _elem2 = iprot.readString();
                  struct.paramStringList.add(_elem2);
                }
                iprot.readListEnd();
              }
              struct.setParamStringListIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 6: // PARAM_INT_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list3 = iprot.readListBegin();
                struct.paramIntList = new ArrayList<Integer>(_list3.size);
                for (int _i4 = 0; _i4 < _list3.size; ++_i4)
                {
                  int _elem5;
                  _elem5 = iprot.readI32();
                  struct.paramIntList.add(_elem5);
                }
                iprot.readListEnd();
              }
              struct.setParamIntListIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          default:
            org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
        }
        iprot.readFieldEnd();
      }
      iprot.readStructEnd();

      // check for required fields of primitive type, which can't be checked in the validate method
      struct.validate();
    }

    public void write(org.apache.thrift.protocol.TProtocol oprot, LimitData struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(ID_FIELD_DESC);
      oprot.writeI32(struct.id);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(OPER_FIELD_DESC);
      oprot.writeByte(struct.oper);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(MESSAGE_ID_FIELD_DESC);
      oprot.writeI32(struct.messageId);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(TARGET_FIELD_DESC);
      oprot.writeByte(struct.target);
      oprot.writeFieldEnd();
      if (struct.paramStringList != null) {
        oprot.writeFieldBegin(PARAM_STRING_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRING, struct.paramStringList.size()));
          for (String _iter6 : struct.paramStringList)
          {
            oprot.writeString(_iter6);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.paramIntList != null) {
        oprot.writeFieldBegin(PARAM_INT_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.I32, struct.paramIntList.size()));
          for (int _iter7 : struct.paramIntList)
          {
            oprot.writeI32(_iter7);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class LimitDataTupleSchemeFactory implements SchemeFactory {
    public LimitDataTupleScheme getScheme() {
      return new LimitDataTupleScheme();
    }
  }

  private static class LimitDataTupleScheme extends TupleScheme<LimitData> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, LimitData struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetId()) {
        optionals.set(0);
      }
      if (struct.isSetOper()) {
        optionals.set(1);
      }
      if (struct.isSetMessageId()) {
        optionals.set(2);
      }
      if (struct.isSetTarget()) {
        optionals.set(3);
      }
      if (struct.isSetParamStringList()) {
        optionals.set(4);
      }
      if (struct.isSetParamIntList()) {
        optionals.set(5);
      }
      oprot.writeBitSet(optionals, 6);
      if (struct.isSetId()) {
        oprot.writeI32(struct.id);
      }
      if (struct.isSetOper()) {
        oprot.writeByte(struct.oper);
      }
      if (struct.isSetMessageId()) {
        oprot.writeI32(struct.messageId);
      }
      if (struct.isSetTarget()) {
        oprot.writeByte(struct.target);
      }
      if (struct.isSetParamStringList()) {
        {
          oprot.writeI32(struct.paramStringList.size());
          for (String _iter8 : struct.paramStringList)
          {
            oprot.writeString(_iter8);
          }
        }
      }
      if (struct.isSetParamIntList()) {
        {
          oprot.writeI32(struct.paramIntList.size());
          for (int _iter9 : struct.paramIntList)
          {
            oprot.writeI32(_iter9);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, LimitData struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(6);
      if (incoming.get(0)) {
        struct.id = iprot.readI32();
        struct.setIdIsSet(true);
      }
      if (incoming.get(1)) {
        struct.oper = iprot.readByte();
        struct.setOperIsSet(true);
      }
      if (incoming.get(2)) {
        struct.messageId = iprot.readI32();
        struct.setMessageIdIsSet(true);
      }
      if (incoming.get(3)) {
        struct.target = iprot.readByte();
        struct.setTargetIsSet(true);
      }
      if (incoming.get(4)) {
        {
          org.apache.thrift.protocol.TList _list10 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRING, iprot.readI32());
          struct.paramStringList = new ArrayList<String>(_list10.size);
          for (int _i11 = 0; _i11 < _list10.size; ++_i11)
          {
            String _elem12;
            _elem12 = iprot.readString();
            struct.paramStringList.add(_elem12);
          }
        }
        struct.setParamStringListIsSet(true);
      }
      if (incoming.get(5)) {
        {
          org.apache.thrift.protocol.TList _list13 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.I32, iprot.readI32());
          struct.paramIntList = new ArrayList<Integer>(_list13.size);
          for (int _i14 = 0; _i14 < _list13.size; ++_i14)
          {
            int _elem15;
            _elem15 = iprot.readI32();
            struct.paramIntList.add(_elem15);
          }
        }
        struct.setParamIntListIsSet(true);
      }
    }
  }

}

