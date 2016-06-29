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

public class MusicGameSpeedConfig implements org.apache.thrift.TBase<MusicGameSpeedConfig, MusicGameSpeedConfig._Fields>, java.io.Serializable, Cloneable, Comparable<MusicGameSpeedConfig> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("MusicGameSpeedConfig");

  private static final org.apache.thrift.protocol.TField DIFFICULTYID_FIELD_DESC = new org.apache.thrift.protocol.TField("difficultyid", org.apache.thrift.protocol.TType.DOUBLE, (short)10);
  private static final org.apache.thrift.protocol.TField SPEED_FIELD_DESC = new org.apache.thrift.protocol.TField("speed", org.apache.thrift.protocol.TType.DOUBLE, (short)20);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new MusicGameSpeedConfigStandardSchemeFactory());
    schemes.put(TupleScheme.class, new MusicGameSpeedConfigTupleSchemeFactory());
  }

  public double difficultyid; // required
  public double speed; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    DIFFICULTYID((short)10, "difficultyid"),
    SPEED((short)20, "speed");

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
        case 10: // DIFFICULTYID
          return DIFFICULTYID;
        case 20: // SPEED
          return SPEED;
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
  private static final int __DIFFICULTYID_ISSET_ID = 0;
  private static final int __SPEED_ISSET_ID = 1;
  private byte __isset_bitfield = 0;
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.DIFFICULTYID, new org.apache.thrift.meta_data.FieldMetaData("difficultyid", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.DOUBLE)));
    tmpMap.put(_Fields.SPEED, new org.apache.thrift.meta_data.FieldMetaData("speed", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.DOUBLE)));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(MusicGameSpeedConfig.class, metaDataMap);
  }

  public MusicGameSpeedConfig() {
  }

  public MusicGameSpeedConfig(
    double difficultyid,
    double speed)
  {
    this();
    this.difficultyid = difficultyid;
    setDifficultyidIsSet(true);
    this.speed = speed;
    setSpeedIsSet(true);
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public MusicGameSpeedConfig(MusicGameSpeedConfig other) {
    __isset_bitfield = other.__isset_bitfield;
    this.difficultyid = other.difficultyid;
    this.speed = other.speed;
  }

  public MusicGameSpeedConfig deepCopy() {
    return new MusicGameSpeedConfig(this);
  }

  @Override
  public void clear() {
    setDifficultyidIsSet(false);
    this.difficultyid = 0.0;
    setSpeedIsSet(false);
    this.speed = 0.0;
  }

  public double getDifficultyid() {
    return this.difficultyid;
  }

  public MusicGameSpeedConfig setDifficultyid(double difficultyid) {
    this.difficultyid = difficultyid;
    setDifficultyidIsSet(true);
    return this;
  }

  public void unsetDifficultyid() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __DIFFICULTYID_ISSET_ID);
  }

  /** Returns true if field difficultyid is set (has been assigned a value) and false otherwise */
  public boolean isSetDifficultyid() {
    return EncodingUtils.testBit(__isset_bitfield, __DIFFICULTYID_ISSET_ID);
  }

  public void setDifficultyidIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __DIFFICULTYID_ISSET_ID, value);
  }

  public double getSpeed() {
    return this.speed;
  }

  public MusicGameSpeedConfig setSpeed(double speed) {
    this.speed = speed;
    setSpeedIsSet(true);
    return this;
  }

  public void unsetSpeed() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __SPEED_ISSET_ID);
  }

  /** Returns true if field speed is set (has been assigned a value) and false otherwise */
  public boolean isSetSpeed() {
    return EncodingUtils.testBit(__isset_bitfield, __SPEED_ISSET_ID);
  }

  public void setSpeedIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __SPEED_ISSET_ID, value);
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case DIFFICULTYID:
      if (value == null) {
        unsetDifficultyid();
      } else {
        setDifficultyid((Double)value);
      }
      break;

    case SPEED:
      if (value == null) {
        unsetSpeed();
      } else {
        setSpeed((Double)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case DIFFICULTYID:
      return Double.valueOf(getDifficultyid());

    case SPEED:
      return Double.valueOf(getSpeed());

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case DIFFICULTYID:
      return isSetDifficultyid();
    case SPEED:
      return isSetSpeed();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof MusicGameSpeedConfig)
      return this.equals((MusicGameSpeedConfig)that);
    return false;
  }

  public boolean equals(MusicGameSpeedConfig that) {
    if (that == null)
      return false;

    boolean this_present_difficultyid = true;
    boolean that_present_difficultyid = true;
    if (this_present_difficultyid || that_present_difficultyid) {
      if (!(this_present_difficultyid && that_present_difficultyid))
        return false;
      if (this.difficultyid != that.difficultyid)
        return false;
    }

    boolean this_present_speed = true;
    boolean that_present_speed = true;
    if (this_present_speed || that_present_speed) {
      if (!(this_present_speed && that_present_speed))
        return false;
      if (this.speed != that.speed)
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(MusicGameSpeedConfig other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetDifficultyid()).compareTo(other.isSetDifficultyid());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetDifficultyid()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.difficultyid, other.difficultyid);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetSpeed()).compareTo(other.isSetSpeed());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetSpeed()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.speed, other.speed);
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
    StringBuilder sb = new StringBuilder("MusicGameSpeedConfig(");
    boolean first = true;

    sb.append("difficultyid:");
    sb.append(this.difficultyid);
    first = false;
    if (!first) sb.append(", ");
    sb.append("speed:");
    sb.append(this.speed);
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

  private static class MusicGameSpeedConfigStandardSchemeFactory implements SchemeFactory {
    public MusicGameSpeedConfigStandardScheme getScheme() {
      return new MusicGameSpeedConfigStandardScheme();
    }
  }

  private static class MusicGameSpeedConfigStandardScheme extends StandardScheme<MusicGameSpeedConfig> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, MusicGameSpeedConfig struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 10: // DIFFICULTYID
            if (schemeField.type == org.apache.thrift.protocol.TType.DOUBLE) {
              struct.difficultyid = iprot.readDouble();
              struct.setDifficultyidIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 20: // SPEED
            if (schemeField.type == org.apache.thrift.protocol.TType.DOUBLE) {
              struct.speed = iprot.readDouble();
              struct.setSpeedIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, MusicGameSpeedConfig struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(DIFFICULTYID_FIELD_DESC);
      oprot.writeDouble(struct.difficultyid);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(SPEED_FIELD_DESC);
      oprot.writeDouble(struct.speed);
      oprot.writeFieldEnd();
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class MusicGameSpeedConfigTupleSchemeFactory implements SchemeFactory {
    public MusicGameSpeedConfigTupleScheme getScheme() {
      return new MusicGameSpeedConfigTupleScheme();
    }
  }

  private static class MusicGameSpeedConfigTupleScheme extends TupleScheme<MusicGameSpeedConfig> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, MusicGameSpeedConfig struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetDifficultyid()) {
        optionals.set(0);
      }
      if (struct.isSetSpeed()) {
        optionals.set(1);
      }
      oprot.writeBitSet(optionals, 2);
      if (struct.isSetDifficultyid()) {
        oprot.writeDouble(struct.difficultyid);
      }
      if (struct.isSetSpeed()) {
        oprot.writeDouble(struct.speed);
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, MusicGameSpeedConfig struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(2);
      if (incoming.get(0)) {
        struct.difficultyid = iprot.readDouble();
        struct.setDifficultyidIsSet(true);
      }
      if (incoming.get(1)) {
        struct.speed = iprot.readDouble();
        struct.setSpeedIsSet(true);
      }
    }
  }

}

