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

public class LimitGroup implements org.apache.thrift.TBase<LimitGroup, LimitGroup._Fields>, java.io.Serializable, Cloneable, Comparable<LimitGroup> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("LimitGroup");

  private static final org.apache.thrift.protocol.TField ID_FIELD_DESC = new org.apache.thrift.protocol.TField("id", org.apache.thrift.protocol.TType.I32, (short)1);
  private static final org.apache.thrift.protocol.TField LOGIC_FIELD_DESC = new org.apache.thrift.protocol.TField("logic", org.apache.thrift.protocol.TType.BYTE, (short)2);
  private static final org.apache.thrift.protocol.TField LIMIT_DATA_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("limitDataList", org.apache.thrift.protocol.TType.LIST, (short)3);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new LimitGroupStandardSchemeFactory());
    schemes.put(TupleScheme.class, new LimitGroupTupleSchemeFactory());
  }

  public int id; // required
  public byte logic; // required
  public List<LimitData> limitDataList; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    ID((short)1, "id"),
    LOGIC((short)2, "logic"),
    LIMIT_DATA_LIST((short)3, "limitDataList");

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
        case 2: // LOGIC
          return LOGIC;
        case 3: // LIMIT_DATA_LIST
          return LIMIT_DATA_LIST;
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
  private static final int __LOGIC_ISSET_ID = 1;
  private byte __isset_bitfield = 0;
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.ID, new org.apache.thrift.meta_data.FieldMetaData("id", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32        , "LimitGroupId")));
    tmpMap.put(_Fields.LOGIC, new org.apache.thrift.meta_data.FieldMetaData("logic", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.BYTE)));
    tmpMap.put(_Fields.LIMIT_DATA_LIST, new org.apache.thrift.meta_data.FieldMetaData("limitDataList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, LimitData.class))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(LimitGroup.class, metaDataMap);
  }

  public LimitGroup() {
  }

  public LimitGroup(
    int id,
    byte logic,
    List<LimitData> limitDataList)
  {
    this();
    this.id = id;
    setIdIsSet(true);
    this.logic = logic;
    setLogicIsSet(true);
    this.limitDataList = limitDataList;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public LimitGroup(LimitGroup other) {
    __isset_bitfield = other.__isset_bitfield;
    this.id = other.id;
    this.logic = other.logic;
    if (other.isSetLimitDataList()) {
      List<LimitData> __this__limitDataList = new ArrayList<LimitData>(other.limitDataList.size());
      for (LimitData other_element : other.limitDataList) {
        __this__limitDataList.add(new LimitData(other_element));
      }
      this.limitDataList = __this__limitDataList;
    }
  }

  public LimitGroup deepCopy() {
    return new LimitGroup(this);
  }

  @Override
  public void clear() {
    setIdIsSet(false);
    this.id = 0;
    setLogicIsSet(false);
    this.logic = 0;
    this.limitDataList = null;
  }

  public int getId() {
    return this.id;
  }

  public LimitGroup setId(int id) {
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

  public byte getLogic() {
    return this.logic;
  }

  public LimitGroup setLogic(byte logic) {
    this.logic = logic;
    setLogicIsSet(true);
    return this;
  }

  public void unsetLogic() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __LOGIC_ISSET_ID);
  }

  /** Returns true if field logic is set (has been assigned a value) and false otherwise */
  public boolean isSetLogic() {
    return EncodingUtils.testBit(__isset_bitfield, __LOGIC_ISSET_ID);
  }

  public void setLogicIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __LOGIC_ISSET_ID, value);
  }

  public int getLimitDataListSize() {
    return (this.limitDataList == null) ? 0 : this.limitDataList.size();
  }

  public java.util.Iterator<LimitData> getLimitDataListIterator() {
    return (this.limitDataList == null) ? null : this.limitDataList.iterator();
  }

  public void addToLimitDataList(LimitData elem) {
    if (this.limitDataList == null) {
      this.limitDataList = new ArrayList<LimitData>();
    }
    this.limitDataList.add(elem);
  }

  public List<LimitData> getLimitDataList() {
    return this.limitDataList;
  }

  public LimitGroup setLimitDataList(List<LimitData> limitDataList) {
    this.limitDataList = limitDataList;
    return this;
  }

  public void unsetLimitDataList() {
    this.limitDataList = null;
  }

  /** Returns true if field limitDataList is set (has been assigned a value) and false otherwise */
  public boolean isSetLimitDataList() {
    return this.limitDataList != null;
  }

  public void setLimitDataListIsSet(boolean value) {
    if (!value) {
      this.limitDataList = null;
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

    case LOGIC:
      if (value == null) {
        unsetLogic();
      } else {
        setLogic((Byte)value);
      }
      break;

    case LIMIT_DATA_LIST:
      if (value == null) {
        unsetLimitDataList();
      } else {
        setLimitDataList((List<LimitData>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case ID:
      return Integer.valueOf(getId());

    case LOGIC:
      return Byte.valueOf(getLogic());

    case LIMIT_DATA_LIST:
      return getLimitDataList();

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
    case LOGIC:
      return isSetLogic();
    case LIMIT_DATA_LIST:
      return isSetLimitDataList();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof LimitGroup)
      return this.equals((LimitGroup)that);
    return false;
  }

  public boolean equals(LimitGroup that) {
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

    boolean this_present_logic = true;
    boolean that_present_logic = true;
    if (this_present_logic || that_present_logic) {
      if (!(this_present_logic && that_present_logic))
        return false;
      if (this.logic != that.logic)
        return false;
    }

    boolean this_present_limitDataList = true && this.isSetLimitDataList();
    boolean that_present_limitDataList = true && that.isSetLimitDataList();
    if (this_present_limitDataList || that_present_limitDataList) {
      if (!(this_present_limitDataList && that_present_limitDataList))
        return false;
      if (!this.limitDataList.equals(that.limitDataList))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(LimitGroup other) {
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
    lastComparison = Boolean.valueOf(isSetLogic()).compareTo(other.isSetLogic());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetLogic()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.logic, other.logic);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetLimitDataList()).compareTo(other.isSetLimitDataList());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetLimitDataList()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.limitDataList, other.limitDataList);
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
    StringBuilder sb = new StringBuilder("LimitGroup(");
    boolean first = true;

    sb.append("id:");
    sb.append(this.id);
    first = false;
    if (!first) sb.append(", ");
    sb.append("logic:");
    sb.append(this.logic);
    first = false;
    if (!first) sb.append(", ");
    sb.append("limitDataList:");
    if (this.limitDataList == null) {
      sb.append("null");
    } else {
      sb.append(this.limitDataList);
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

  private static class LimitGroupStandardSchemeFactory implements SchemeFactory {
    public LimitGroupStandardScheme getScheme() {
      return new LimitGroupStandardScheme();
    }
  }

  private static class LimitGroupStandardScheme extends StandardScheme<LimitGroup> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, LimitGroup struct) throws org.apache.thrift.TException {
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
          case 2: // LOGIC
            if (schemeField.type == org.apache.thrift.protocol.TType.BYTE) {
              struct.logic = iprot.readByte();
              struct.setLogicIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 3: // LIMIT_DATA_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list16 = iprot.readListBegin();
                struct.limitDataList = new ArrayList<LimitData>(_list16.size);
                for (int _i17 = 0; _i17 < _list16.size; ++_i17)
                {
                  LimitData _elem18;
                  _elem18 = new LimitData();
                  _elem18.read(iprot);
                  struct.limitDataList.add(_elem18);
                }
                iprot.readListEnd();
              }
              struct.setLimitDataListIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, LimitGroup struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(ID_FIELD_DESC);
      oprot.writeI32(struct.id);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(LOGIC_FIELD_DESC);
      oprot.writeByte(struct.logic);
      oprot.writeFieldEnd();
      if (struct.limitDataList != null) {
        oprot.writeFieldBegin(LIMIT_DATA_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.limitDataList.size()));
          for (LimitData _iter19 : struct.limitDataList)
          {
            _iter19.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class LimitGroupTupleSchemeFactory implements SchemeFactory {
    public LimitGroupTupleScheme getScheme() {
      return new LimitGroupTupleScheme();
    }
  }

  private static class LimitGroupTupleScheme extends TupleScheme<LimitGroup> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, LimitGroup struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetId()) {
        optionals.set(0);
      }
      if (struct.isSetLogic()) {
        optionals.set(1);
      }
      if (struct.isSetLimitDataList()) {
        optionals.set(2);
      }
      oprot.writeBitSet(optionals, 3);
      if (struct.isSetId()) {
        oprot.writeI32(struct.id);
      }
      if (struct.isSetLogic()) {
        oprot.writeByte(struct.logic);
      }
      if (struct.isSetLimitDataList()) {
        {
          oprot.writeI32(struct.limitDataList.size());
          for (LimitData _iter20 : struct.limitDataList)
          {
            _iter20.write(oprot);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, LimitGroup struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(3);
      if (incoming.get(0)) {
        struct.id = iprot.readI32();
        struct.setIdIsSet(true);
      }
      if (incoming.get(1)) {
        struct.logic = iprot.readByte();
        struct.setLogicIsSet(true);
      }
      if (incoming.get(2)) {
        {
          org.apache.thrift.protocol.TList _list21 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.limitDataList = new ArrayList<LimitData>(_list21.size);
          for (int _i22 = 0; _i22 < _list21.size; ++_i22)
          {
            LimitData _elem23;
            _elem23 = new LimitData();
            _elem23.read(iprot);
            struct.limitDataList.add(_elem23);
          }
        }
        struct.setLimitDataListIsSet(true);
      }
    }
  }

}

