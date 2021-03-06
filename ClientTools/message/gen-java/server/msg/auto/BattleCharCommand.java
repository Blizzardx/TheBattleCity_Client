/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
package server.msg.auto;

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

public class BattleCharCommand implements org.apache.thrift.TBase<BattleCharCommand, BattleCharCommand._Fields>, java.io.Serializable, Cloneable, Comparable<BattleCharCommand> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("BattleCharCommand");

  private static final org.apache.thrift.protocol.TField CHAR_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("charId", org.apache.thrift.protocol.TType.I32, (short)10);
  private static final org.apache.thrift.protocol.TField COMMAND_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("commandList", org.apache.thrift.protocol.TType.LIST, (short)20);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new BattleCharCommandStandardSchemeFactory());
    schemes.put(TupleScheme.class, new BattleCharCommandTupleSchemeFactory());
  }

  public int charId; // required
  public List<BattleCommandData> commandList; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    CHAR_ID((short)10, "charId"),
    COMMAND_LIST((short)20, "commandList");

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
        case 10: // CHAR_ID
          return CHAR_ID;
        case 20: // COMMAND_LIST
          return COMMAND_LIST;
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
  private static final int __CHARID_ISSET_ID = 0;
  private byte __isset_bitfield = 0;
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.CHAR_ID, new org.apache.thrift.meta_data.FieldMetaData("charId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.COMMAND_LIST, new org.apache.thrift.meta_data.FieldMetaData("commandList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, BattleCommandData.class))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(BattleCharCommand.class, metaDataMap);
  }

  public BattleCharCommand() {
  }

  public BattleCharCommand(
    int charId,
    List<BattleCommandData> commandList)
  {
    this();
    this.charId = charId;
    setCharIdIsSet(true);
    this.commandList = commandList;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public BattleCharCommand(BattleCharCommand other) {
    __isset_bitfield = other.__isset_bitfield;
    this.charId = other.charId;
    if (other.isSetCommandList()) {
      List<BattleCommandData> __this__commandList = new ArrayList<BattleCommandData>(other.commandList.size());
      for (BattleCommandData other_element : other.commandList) {
        __this__commandList.add(new BattleCommandData(other_element));
      }
      this.commandList = __this__commandList;
    }
  }

  public BattleCharCommand deepCopy() {
    return new BattleCharCommand(this);
  }

  @Override
  public void clear() {
    setCharIdIsSet(false);
    this.charId = 0;
    this.commandList = null;
  }

  public int getCharId() {
    return this.charId;
  }

  public BattleCharCommand setCharId(int charId) {
    this.charId = charId;
    setCharIdIsSet(true);
    return this;
  }

  public void unsetCharId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __CHARID_ISSET_ID);
  }

  /** Returns true if field charId is set (has been assigned a value) and false otherwise */
  public boolean isSetCharId() {
    return EncodingUtils.testBit(__isset_bitfield, __CHARID_ISSET_ID);
  }

  public void setCharIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __CHARID_ISSET_ID, value);
  }

  public int getCommandListSize() {
    return (this.commandList == null) ? 0 : this.commandList.size();
  }

  public java.util.Iterator<BattleCommandData> getCommandListIterator() {
    return (this.commandList == null) ? null : this.commandList.iterator();
  }

  public void addToCommandList(BattleCommandData elem) {
    if (this.commandList == null) {
      this.commandList = new ArrayList<BattleCommandData>();
    }
    this.commandList.add(elem);
  }

  public List<BattleCommandData> getCommandList() {
    return this.commandList;
  }

  public BattleCharCommand setCommandList(List<BattleCommandData> commandList) {
    this.commandList = commandList;
    return this;
  }

  public void unsetCommandList() {
    this.commandList = null;
  }

  /** Returns true if field commandList is set (has been assigned a value) and false otherwise */
  public boolean isSetCommandList() {
    return this.commandList != null;
  }

  public void setCommandListIsSet(boolean value) {
    if (!value) {
      this.commandList = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case CHAR_ID:
      if (value == null) {
        unsetCharId();
      } else {
        setCharId((Integer)value);
      }
      break;

    case COMMAND_LIST:
      if (value == null) {
        unsetCommandList();
      } else {
        setCommandList((List<BattleCommandData>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case CHAR_ID:
      return Integer.valueOf(getCharId());

    case COMMAND_LIST:
      return getCommandList();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case CHAR_ID:
      return isSetCharId();
    case COMMAND_LIST:
      return isSetCommandList();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof BattleCharCommand)
      return this.equals((BattleCharCommand)that);
    return false;
  }

  public boolean equals(BattleCharCommand that) {
    if (that == null)
      return false;

    boolean this_present_charId = true;
    boolean that_present_charId = true;
    if (this_present_charId || that_present_charId) {
      if (!(this_present_charId && that_present_charId))
        return false;
      if (this.charId != that.charId)
        return false;
    }

    boolean this_present_commandList = true && this.isSetCommandList();
    boolean that_present_commandList = true && that.isSetCommandList();
    if (this_present_commandList || that_present_commandList) {
      if (!(this_present_commandList && that_present_commandList))
        return false;
      if (!this.commandList.equals(that.commandList))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(BattleCharCommand other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetCharId()).compareTo(other.isSetCharId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetCharId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.charId, other.charId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetCommandList()).compareTo(other.isSetCommandList());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetCommandList()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.commandList, other.commandList);
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
    StringBuilder sb = new StringBuilder("BattleCharCommand(");
    boolean first = true;

    sb.append("charId:");
    sb.append(this.charId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("commandList:");
    if (this.commandList == null) {
      sb.append("null");
    } else {
      sb.append(this.commandList);
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

  private static class BattleCharCommandStandardSchemeFactory implements SchemeFactory {
    public BattleCharCommandStandardScheme getScheme() {
      return new BattleCharCommandStandardScheme();
    }
  }

  private static class BattleCharCommandStandardScheme extends StandardScheme<BattleCharCommand> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, BattleCharCommand struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 10: // CHAR_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.charId = iprot.readI32();
              struct.setCharIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 20: // COMMAND_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list8 = iprot.readListBegin();
                struct.commandList = new ArrayList<BattleCommandData>(_list8.size);
                for (int _i9 = 0; _i9 < _list8.size; ++_i9)
                {
                  BattleCommandData _elem10;
                  _elem10 = new BattleCommandData();
                  _elem10.read(iprot);
                  struct.commandList.add(_elem10);
                }
                iprot.readListEnd();
              }
              struct.setCommandListIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, BattleCharCommand struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(CHAR_ID_FIELD_DESC);
      oprot.writeI32(struct.charId);
      oprot.writeFieldEnd();
      if (struct.commandList != null) {
        oprot.writeFieldBegin(COMMAND_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.commandList.size()));
          for (BattleCommandData _iter11 : struct.commandList)
          {
            _iter11.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class BattleCharCommandTupleSchemeFactory implements SchemeFactory {
    public BattleCharCommandTupleScheme getScheme() {
      return new BattleCharCommandTupleScheme();
    }
  }

  private static class BattleCharCommandTupleScheme extends TupleScheme<BattleCharCommand> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, BattleCharCommand struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetCharId()) {
        optionals.set(0);
      }
      if (struct.isSetCommandList()) {
        optionals.set(1);
      }
      oprot.writeBitSet(optionals, 2);
      if (struct.isSetCharId()) {
        oprot.writeI32(struct.charId);
      }
      if (struct.isSetCommandList()) {
        {
          oprot.writeI32(struct.commandList.size());
          for (BattleCommandData _iter12 : struct.commandList)
          {
            _iter12.write(oprot);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, BattleCharCommand struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(2);
      if (incoming.get(0)) {
        struct.charId = iprot.readI32();
        struct.setCharIdIsSet(true);
      }
      if (incoming.get(1)) {
        {
          org.apache.thrift.protocol.TList _list13 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.commandList = new ArrayList<BattleCommandData>(_list13.size);
          for (int _i14 = 0; _i14 < _list13.size; ++_i14)
          {
            BattleCommandData _elem15;
            _elem15 = new BattleCommandData();
            _elem15.read(iprot);
            struct.commandList.add(_elem15);
          }
        }
        struct.setCommandListIsSet(true);
      }
    }
  }

}

