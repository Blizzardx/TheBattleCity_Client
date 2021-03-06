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

public class TargetData implements org.apache.thrift.TBase<TargetData, TargetData._Fields>, java.io.Serializable, Cloneable, Comparable<TargetData> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("TargetData");

  private static final org.apache.thrift.protocol.TField TARGET_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("targetId", org.apache.thrift.protocol.TType.I32, (short)1);
  private static final org.apache.thrift.protocol.TField PARAM_STRING_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("paramStringList", org.apache.thrift.protocol.TType.LIST, (short)2);
  private static final org.apache.thrift.protocol.TField PARAM_INT_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("paramIntList", org.apache.thrift.protocol.TType.LIST, (short)3);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new TargetDataStandardSchemeFactory());
    schemes.put(TupleScheme.class, new TargetDataTupleSchemeFactory());
  }

  public int targetId; // required
  public List<String> paramStringList; // required
  public List<Integer> paramIntList; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    TARGET_ID((short)1, "targetId"),
    PARAM_STRING_LIST((short)2, "paramStringList"),
    PARAM_INT_LIST((short)3, "paramIntList");

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
        case 1: // TARGET_ID
          return TARGET_ID;
        case 2: // PARAM_STRING_LIST
          return PARAM_STRING_LIST;
        case 3: // PARAM_INT_LIST
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
  private static final int __TARGETID_ISSET_ID = 0;
  private byte __isset_bitfield = 0;
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.TARGET_ID, new org.apache.thrift.meta_data.FieldMetaData("targetId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.PARAM_STRING_LIST, new org.apache.thrift.meta_data.FieldMetaData("paramStringList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING))));
    tmpMap.put(_Fields.PARAM_INT_LIST, new org.apache.thrift.meta_data.FieldMetaData("paramIntList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(TargetData.class, metaDataMap);
  }

  public TargetData() {
  }

  public TargetData(
    int targetId,
    List<String> paramStringList,
    List<Integer> paramIntList)
  {
    this();
    this.targetId = targetId;
    setTargetIdIsSet(true);
    this.paramStringList = paramStringList;
    this.paramIntList = paramIntList;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public TargetData(TargetData other) {
    __isset_bitfield = other.__isset_bitfield;
    this.targetId = other.targetId;
    if (other.isSetParamStringList()) {
      List<String> __this__paramStringList = new ArrayList<String>(other.paramStringList);
      this.paramStringList = __this__paramStringList;
    }
    if (other.isSetParamIntList()) {
      List<Integer> __this__paramIntList = new ArrayList<Integer>(other.paramIntList);
      this.paramIntList = __this__paramIntList;
    }
  }

  public TargetData deepCopy() {
    return new TargetData(this);
  }

  @Override
  public void clear() {
    setTargetIdIsSet(false);
    this.targetId = 0;
    this.paramStringList = null;
    this.paramIntList = null;
  }

  public int getTargetId() {
    return this.targetId;
  }

  public TargetData setTargetId(int targetId) {
    this.targetId = targetId;
    setTargetIdIsSet(true);
    return this;
  }

  public void unsetTargetId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __TARGETID_ISSET_ID);
  }

  /** Returns true if field targetId is set (has been assigned a value) and false otherwise */
  public boolean isSetTargetId() {
    return EncodingUtils.testBit(__isset_bitfield, __TARGETID_ISSET_ID);
  }

  public void setTargetIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __TARGETID_ISSET_ID, value);
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

  public TargetData setParamStringList(List<String> paramStringList) {
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

  public TargetData setParamIntList(List<Integer> paramIntList) {
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
    case TARGET_ID:
      if (value == null) {
        unsetTargetId();
      } else {
        setTargetId((Integer)value);
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
    case TARGET_ID:
      return Integer.valueOf(getTargetId());

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
    case TARGET_ID:
      return isSetTargetId();
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
    if (that instanceof TargetData)
      return this.equals((TargetData)that);
    return false;
  }

  public boolean equals(TargetData that) {
    if (that == null)
      return false;

    boolean this_present_targetId = true;
    boolean that_present_targetId = true;
    if (this_present_targetId || that_present_targetId) {
      if (!(this_present_targetId && that_present_targetId))
        return false;
      if (this.targetId != that.targetId)
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
  public int compareTo(TargetData other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetTargetId()).compareTo(other.isSetTargetId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetTargetId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.targetId, other.targetId);
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
    StringBuilder sb = new StringBuilder("TargetData(");
    boolean first = true;

    sb.append("targetId:");
    sb.append(this.targetId);
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

  private static class TargetDataStandardSchemeFactory implements SchemeFactory {
    public TargetDataStandardScheme getScheme() {
      return new TargetDataStandardScheme();
    }
  }

  private static class TargetDataStandardScheme extends StandardScheme<TargetData> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, TargetData struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // TARGET_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.targetId = iprot.readI32();
              struct.setTargetIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 2: // PARAM_STRING_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list48 = iprot.readListBegin();
                struct.paramStringList = new ArrayList<String>(_list48.size);
                for (int _i49 = 0; _i49 < _list48.size; ++_i49)
                {
                  String _elem50;
                  _elem50 = iprot.readString();
                  struct.paramStringList.add(_elem50);
                }
                iprot.readListEnd();
              }
              struct.setParamStringListIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 3: // PARAM_INT_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list51 = iprot.readListBegin();
                struct.paramIntList = new ArrayList<Integer>(_list51.size);
                for (int _i52 = 0; _i52 < _list51.size; ++_i52)
                {
                  int _elem53;
                  _elem53 = iprot.readI32();
                  struct.paramIntList.add(_elem53);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, TargetData struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(TARGET_ID_FIELD_DESC);
      oprot.writeI32(struct.targetId);
      oprot.writeFieldEnd();
      if (struct.paramStringList != null) {
        oprot.writeFieldBegin(PARAM_STRING_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRING, struct.paramStringList.size()));
          for (String _iter54 : struct.paramStringList)
          {
            oprot.writeString(_iter54);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.paramIntList != null) {
        oprot.writeFieldBegin(PARAM_INT_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.I32, struct.paramIntList.size()));
          for (int _iter55 : struct.paramIntList)
          {
            oprot.writeI32(_iter55);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class TargetDataTupleSchemeFactory implements SchemeFactory {
    public TargetDataTupleScheme getScheme() {
      return new TargetDataTupleScheme();
    }
  }

  private static class TargetDataTupleScheme extends TupleScheme<TargetData> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, TargetData struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetTargetId()) {
        optionals.set(0);
      }
      if (struct.isSetParamStringList()) {
        optionals.set(1);
      }
      if (struct.isSetParamIntList()) {
        optionals.set(2);
      }
      oprot.writeBitSet(optionals, 3);
      if (struct.isSetTargetId()) {
        oprot.writeI32(struct.targetId);
      }
      if (struct.isSetParamStringList()) {
        {
          oprot.writeI32(struct.paramStringList.size());
          for (String _iter56 : struct.paramStringList)
          {
            oprot.writeString(_iter56);
          }
        }
      }
      if (struct.isSetParamIntList()) {
        {
          oprot.writeI32(struct.paramIntList.size());
          for (int _iter57 : struct.paramIntList)
          {
            oprot.writeI32(_iter57);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, TargetData struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(3);
      if (incoming.get(0)) {
        struct.targetId = iprot.readI32();
        struct.setTargetIdIsSet(true);
      }
      if (incoming.get(1)) {
        {
          org.apache.thrift.protocol.TList _list58 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRING, iprot.readI32());
          struct.paramStringList = new ArrayList<String>(_list58.size);
          for (int _i59 = 0; _i59 < _list58.size; ++_i59)
          {
            String _elem60;
            _elem60 = iprot.readString();
            struct.paramStringList.add(_elem60);
          }
        }
        struct.setParamStringListIsSet(true);
      }
      if (incoming.get(2)) {
        {
          org.apache.thrift.protocol.TList _list61 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.I32, iprot.readI32());
          struct.paramIntList = new ArrayList<Integer>(_list61.size);
          for (int _i62 = 0; _i62 < _list61.size; ++_i62)
          {
            int _elem63;
            _elem63 = iprot.readI32();
            struct.paramIntList.add(_elem63);
          }
        }
        struct.setParamIntListIsSet(true);
      }
    }
  }

}

