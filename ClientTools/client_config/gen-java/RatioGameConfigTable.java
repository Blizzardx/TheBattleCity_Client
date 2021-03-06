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

public class RatioGameConfigTable implements org.apache.thrift.TBase<RatioGameConfigTable, RatioGameConfigTable._Fields>, java.io.Serializable, Cloneable, Comparable<RatioGameConfigTable> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("RatioGameConfigTable");

  private static final org.apache.thrift.protocol.TField BALL_COUNT_FIELD_DESC = new org.apache.thrift.protocol.TField("ballCount", org.apache.thrift.protocol.TType.LIST, (short)1);
  private static final org.apache.thrift.protocol.TField BALL_COLOR_FIELD_DESC = new org.apache.thrift.protocol.TField("ballColor", org.apache.thrift.protocol.TType.LIST, (short)2);
  private static final org.apache.thrift.protocol.TField BALL_MATERIAL_FIELD_DESC = new org.apache.thrift.protocol.TField("ballMaterial", org.apache.thrift.protocol.TType.LIST, (short)3);
  private static final org.apache.thrift.protocol.TField BALL_SPEED_FIELD_DESC = new org.apache.thrift.protocol.TField("ballSpeed", org.apache.thrift.protocol.TType.LIST, (short)4);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new RatioGameConfigTableStandardSchemeFactory());
    schemes.put(TupleScheme.class, new RatioGameConfigTableTupleSchemeFactory());
  }

  public List<MiniGameHardConfig> ballCount; // required
  public List<MiniGameHardConfig> ballColor; // required
  public List<MiniGameHardConfig> ballMaterial; // required
  public List<MiniGameHardConfig> ballSpeed; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    BALL_COUNT((short)1, "ballCount"),
    BALL_COLOR((short)2, "ballColor"),
    BALL_MATERIAL((short)3, "ballMaterial"),
    BALL_SPEED((short)4, "ballSpeed");

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
        case 1: // BALL_COUNT
          return BALL_COUNT;
        case 2: // BALL_COLOR
          return BALL_COLOR;
        case 3: // BALL_MATERIAL
          return BALL_MATERIAL;
        case 4: // BALL_SPEED
          return BALL_SPEED;
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
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.BALL_COUNT, new org.apache.thrift.meta_data.FieldMetaData("ballCount", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, MiniGameHardConfig.class))));
    tmpMap.put(_Fields.BALL_COLOR, new org.apache.thrift.meta_data.FieldMetaData("ballColor", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, MiniGameHardConfig.class))));
    tmpMap.put(_Fields.BALL_MATERIAL, new org.apache.thrift.meta_data.FieldMetaData("ballMaterial", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, MiniGameHardConfig.class))));
    tmpMap.put(_Fields.BALL_SPEED, new org.apache.thrift.meta_data.FieldMetaData("ballSpeed", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, MiniGameHardConfig.class))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(RatioGameConfigTable.class, metaDataMap);
  }

  public RatioGameConfigTable() {
  }

  public RatioGameConfigTable(
    List<MiniGameHardConfig> ballCount,
    List<MiniGameHardConfig> ballColor,
    List<MiniGameHardConfig> ballMaterial,
    List<MiniGameHardConfig> ballSpeed)
  {
    this();
    this.ballCount = ballCount;
    this.ballColor = ballColor;
    this.ballMaterial = ballMaterial;
    this.ballSpeed = ballSpeed;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public RatioGameConfigTable(RatioGameConfigTable other) {
    if (other.isSetBallCount()) {
      List<MiniGameHardConfig> __this__ballCount = new ArrayList<MiniGameHardConfig>(other.ballCount.size());
      for (MiniGameHardConfig other_element : other.ballCount) {
        __this__ballCount.add(new MiniGameHardConfig(other_element));
      }
      this.ballCount = __this__ballCount;
    }
    if (other.isSetBallColor()) {
      List<MiniGameHardConfig> __this__ballColor = new ArrayList<MiniGameHardConfig>(other.ballColor.size());
      for (MiniGameHardConfig other_element : other.ballColor) {
        __this__ballColor.add(new MiniGameHardConfig(other_element));
      }
      this.ballColor = __this__ballColor;
    }
    if (other.isSetBallMaterial()) {
      List<MiniGameHardConfig> __this__ballMaterial = new ArrayList<MiniGameHardConfig>(other.ballMaterial.size());
      for (MiniGameHardConfig other_element : other.ballMaterial) {
        __this__ballMaterial.add(new MiniGameHardConfig(other_element));
      }
      this.ballMaterial = __this__ballMaterial;
    }
    if (other.isSetBallSpeed()) {
      List<MiniGameHardConfig> __this__ballSpeed = new ArrayList<MiniGameHardConfig>(other.ballSpeed.size());
      for (MiniGameHardConfig other_element : other.ballSpeed) {
        __this__ballSpeed.add(new MiniGameHardConfig(other_element));
      }
      this.ballSpeed = __this__ballSpeed;
    }
  }

  public RatioGameConfigTable deepCopy() {
    return new RatioGameConfigTable(this);
  }

  @Override
  public void clear() {
    this.ballCount = null;
    this.ballColor = null;
    this.ballMaterial = null;
    this.ballSpeed = null;
  }

  public int getBallCountSize() {
    return (this.ballCount == null) ? 0 : this.ballCount.size();
  }

  public java.util.Iterator<MiniGameHardConfig> getBallCountIterator() {
    return (this.ballCount == null) ? null : this.ballCount.iterator();
  }

  public void addToBallCount(MiniGameHardConfig elem) {
    if (this.ballCount == null) {
      this.ballCount = new ArrayList<MiniGameHardConfig>();
    }
    this.ballCount.add(elem);
  }

  public List<MiniGameHardConfig> getBallCount() {
    return this.ballCount;
  }

  public RatioGameConfigTable setBallCount(List<MiniGameHardConfig> ballCount) {
    this.ballCount = ballCount;
    return this;
  }

  public void unsetBallCount() {
    this.ballCount = null;
  }

  /** Returns true if field ballCount is set (has been assigned a value) and false otherwise */
  public boolean isSetBallCount() {
    return this.ballCount != null;
  }

  public void setBallCountIsSet(boolean value) {
    if (!value) {
      this.ballCount = null;
    }
  }

  public int getBallColorSize() {
    return (this.ballColor == null) ? 0 : this.ballColor.size();
  }

  public java.util.Iterator<MiniGameHardConfig> getBallColorIterator() {
    return (this.ballColor == null) ? null : this.ballColor.iterator();
  }

  public void addToBallColor(MiniGameHardConfig elem) {
    if (this.ballColor == null) {
      this.ballColor = new ArrayList<MiniGameHardConfig>();
    }
    this.ballColor.add(elem);
  }

  public List<MiniGameHardConfig> getBallColor() {
    return this.ballColor;
  }

  public RatioGameConfigTable setBallColor(List<MiniGameHardConfig> ballColor) {
    this.ballColor = ballColor;
    return this;
  }

  public void unsetBallColor() {
    this.ballColor = null;
  }

  /** Returns true if field ballColor is set (has been assigned a value) and false otherwise */
  public boolean isSetBallColor() {
    return this.ballColor != null;
  }

  public void setBallColorIsSet(boolean value) {
    if (!value) {
      this.ballColor = null;
    }
  }

  public int getBallMaterialSize() {
    return (this.ballMaterial == null) ? 0 : this.ballMaterial.size();
  }

  public java.util.Iterator<MiniGameHardConfig> getBallMaterialIterator() {
    return (this.ballMaterial == null) ? null : this.ballMaterial.iterator();
  }

  public void addToBallMaterial(MiniGameHardConfig elem) {
    if (this.ballMaterial == null) {
      this.ballMaterial = new ArrayList<MiniGameHardConfig>();
    }
    this.ballMaterial.add(elem);
  }

  public List<MiniGameHardConfig> getBallMaterial() {
    return this.ballMaterial;
  }

  public RatioGameConfigTable setBallMaterial(List<MiniGameHardConfig> ballMaterial) {
    this.ballMaterial = ballMaterial;
    return this;
  }

  public void unsetBallMaterial() {
    this.ballMaterial = null;
  }

  /** Returns true if field ballMaterial is set (has been assigned a value) and false otherwise */
  public boolean isSetBallMaterial() {
    return this.ballMaterial != null;
  }

  public void setBallMaterialIsSet(boolean value) {
    if (!value) {
      this.ballMaterial = null;
    }
  }

  public int getBallSpeedSize() {
    return (this.ballSpeed == null) ? 0 : this.ballSpeed.size();
  }

  public java.util.Iterator<MiniGameHardConfig> getBallSpeedIterator() {
    return (this.ballSpeed == null) ? null : this.ballSpeed.iterator();
  }

  public void addToBallSpeed(MiniGameHardConfig elem) {
    if (this.ballSpeed == null) {
      this.ballSpeed = new ArrayList<MiniGameHardConfig>();
    }
    this.ballSpeed.add(elem);
  }

  public List<MiniGameHardConfig> getBallSpeed() {
    return this.ballSpeed;
  }

  public RatioGameConfigTable setBallSpeed(List<MiniGameHardConfig> ballSpeed) {
    this.ballSpeed = ballSpeed;
    return this;
  }

  public void unsetBallSpeed() {
    this.ballSpeed = null;
  }

  /** Returns true if field ballSpeed is set (has been assigned a value) and false otherwise */
  public boolean isSetBallSpeed() {
    return this.ballSpeed != null;
  }

  public void setBallSpeedIsSet(boolean value) {
    if (!value) {
      this.ballSpeed = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case BALL_COUNT:
      if (value == null) {
        unsetBallCount();
      } else {
        setBallCount((List<MiniGameHardConfig>)value);
      }
      break;

    case BALL_COLOR:
      if (value == null) {
        unsetBallColor();
      } else {
        setBallColor((List<MiniGameHardConfig>)value);
      }
      break;

    case BALL_MATERIAL:
      if (value == null) {
        unsetBallMaterial();
      } else {
        setBallMaterial((List<MiniGameHardConfig>)value);
      }
      break;

    case BALL_SPEED:
      if (value == null) {
        unsetBallSpeed();
      } else {
        setBallSpeed((List<MiniGameHardConfig>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case BALL_COUNT:
      return getBallCount();

    case BALL_COLOR:
      return getBallColor();

    case BALL_MATERIAL:
      return getBallMaterial();

    case BALL_SPEED:
      return getBallSpeed();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case BALL_COUNT:
      return isSetBallCount();
    case BALL_COLOR:
      return isSetBallColor();
    case BALL_MATERIAL:
      return isSetBallMaterial();
    case BALL_SPEED:
      return isSetBallSpeed();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof RatioGameConfigTable)
      return this.equals((RatioGameConfigTable)that);
    return false;
  }

  public boolean equals(RatioGameConfigTable that) {
    if (that == null)
      return false;

    boolean this_present_ballCount = true && this.isSetBallCount();
    boolean that_present_ballCount = true && that.isSetBallCount();
    if (this_present_ballCount || that_present_ballCount) {
      if (!(this_present_ballCount && that_present_ballCount))
        return false;
      if (!this.ballCount.equals(that.ballCount))
        return false;
    }

    boolean this_present_ballColor = true && this.isSetBallColor();
    boolean that_present_ballColor = true && that.isSetBallColor();
    if (this_present_ballColor || that_present_ballColor) {
      if (!(this_present_ballColor && that_present_ballColor))
        return false;
      if (!this.ballColor.equals(that.ballColor))
        return false;
    }

    boolean this_present_ballMaterial = true && this.isSetBallMaterial();
    boolean that_present_ballMaterial = true && that.isSetBallMaterial();
    if (this_present_ballMaterial || that_present_ballMaterial) {
      if (!(this_present_ballMaterial && that_present_ballMaterial))
        return false;
      if (!this.ballMaterial.equals(that.ballMaterial))
        return false;
    }

    boolean this_present_ballSpeed = true && this.isSetBallSpeed();
    boolean that_present_ballSpeed = true && that.isSetBallSpeed();
    if (this_present_ballSpeed || that_present_ballSpeed) {
      if (!(this_present_ballSpeed && that_present_ballSpeed))
        return false;
      if (!this.ballSpeed.equals(that.ballSpeed))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(RatioGameConfigTable other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetBallCount()).compareTo(other.isSetBallCount());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetBallCount()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.ballCount, other.ballCount);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetBallColor()).compareTo(other.isSetBallColor());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetBallColor()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.ballColor, other.ballColor);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetBallMaterial()).compareTo(other.isSetBallMaterial());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetBallMaterial()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.ballMaterial, other.ballMaterial);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetBallSpeed()).compareTo(other.isSetBallSpeed());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetBallSpeed()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.ballSpeed, other.ballSpeed);
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
    StringBuilder sb = new StringBuilder("RatioGameConfigTable(");
    boolean first = true;

    sb.append("ballCount:");
    if (this.ballCount == null) {
      sb.append("null");
    } else {
      sb.append(this.ballCount);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("ballColor:");
    if (this.ballColor == null) {
      sb.append("null");
    } else {
      sb.append(this.ballColor);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("ballMaterial:");
    if (this.ballMaterial == null) {
      sb.append("null");
    } else {
      sb.append(this.ballMaterial);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("ballSpeed:");
    if (this.ballSpeed == null) {
      sb.append("null");
    } else {
      sb.append(this.ballSpeed);
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
      read(new org.apache.thrift.protocol.TCompactProtocol(new org.apache.thrift.transport.TIOStreamTransport(in)));
    } catch (org.apache.thrift.TException te) {
      throw new java.io.IOException(te);
    }
  }

  private static class RatioGameConfigTableStandardSchemeFactory implements SchemeFactory {
    public RatioGameConfigTableStandardScheme getScheme() {
      return new RatioGameConfigTableStandardScheme();
    }
  }

  private static class RatioGameConfigTableStandardScheme extends StandardScheme<RatioGameConfigTable> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, RatioGameConfigTable struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // BALL_COUNT
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list148 = iprot.readListBegin();
                struct.ballCount = new ArrayList<MiniGameHardConfig>(_list148.size);
                for (int _i149 = 0; _i149 < _list148.size; ++_i149)
                {
                  MiniGameHardConfig _elem150;
                  _elem150 = new MiniGameHardConfig();
                  _elem150.read(iprot);
                  struct.ballCount.add(_elem150);
                }
                iprot.readListEnd();
              }
              struct.setBallCountIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 2: // BALL_COLOR
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list151 = iprot.readListBegin();
                struct.ballColor = new ArrayList<MiniGameHardConfig>(_list151.size);
                for (int _i152 = 0; _i152 < _list151.size; ++_i152)
                {
                  MiniGameHardConfig _elem153;
                  _elem153 = new MiniGameHardConfig();
                  _elem153.read(iprot);
                  struct.ballColor.add(_elem153);
                }
                iprot.readListEnd();
              }
              struct.setBallColorIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 3: // BALL_MATERIAL
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list154 = iprot.readListBegin();
                struct.ballMaterial = new ArrayList<MiniGameHardConfig>(_list154.size);
                for (int _i155 = 0; _i155 < _list154.size; ++_i155)
                {
                  MiniGameHardConfig _elem156;
                  _elem156 = new MiniGameHardConfig();
                  _elem156.read(iprot);
                  struct.ballMaterial.add(_elem156);
                }
                iprot.readListEnd();
              }
              struct.setBallMaterialIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 4: // BALL_SPEED
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list157 = iprot.readListBegin();
                struct.ballSpeed = new ArrayList<MiniGameHardConfig>(_list157.size);
                for (int _i158 = 0; _i158 < _list157.size; ++_i158)
                {
                  MiniGameHardConfig _elem159;
                  _elem159 = new MiniGameHardConfig();
                  _elem159.read(iprot);
                  struct.ballSpeed.add(_elem159);
                }
                iprot.readListEnd();
              }
              struct.setBallSpeedIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, RatioGameConfigTable struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      if (struct.ballCount != null) {
        oprot.writeFieldBegin(BALL_COUNT_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.ballCount.size()));
          for (MiniGameHardConfig _iter160 : struct.ballCount)
          {
            _iter160.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.ballColor != null) {
        oprot.writeFieldBegin(BALL_COLOR_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.ballColor.size()));
          for (MiniGameHardConfig _iter161 : struct.ballColor)
          {
            _iter161.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.ballMaterial != null) {
        oprot.writeFieldBegin(BALL_MATERIAL_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.ballMaterial.size()));
          for (MiniGameHardConfig _iter162 : struct.ballMaterial)
          {
            _iter162.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.ballSpeed != null) {
        oprot.writeFieldBegin(BALL_SPEED_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.ballSpeed.size()));
          for (MiniGameHardConfig _iter163 : struct.ballSpeed)
          {
            _iter163.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class RatioGameConfigTableTupleSchemeFactory implements SchemeFactory {
    public RatioGameConfigTableTupleScheme getScheme() {
      return new RatioGameConfigTableTupleScheme();
    }
  }

  private static class RatioGameConfigTableTupleScheme extends TupleScheme<RatioGameConfigTable> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, RatioGameConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetBallCount()) {
        optionals.set(0);
      }
      if (struct.isSetBallColor()) {
        optionals.set(1);
      }
      if (struct.isSetBallMaterial()) {
        optionals.set(2);
      }
      if (struct.isSetBallSpeed()) {
        optionals.set(3);
      }
      oprot.writeBitSet(optionals, 4);
      if (struct.isSetBallCount()) {
        {
          oprot.writeI32(struct.ballCount.size());
          for (MiniGameHardConfig _iter164 : struct.ballCount)
          {
            _iter164.write(oprot);
          }
        }
      }
      if (struct.isSetBallColor()) {
        {
          oprot.writeI32(struct.ballColor.size());
          for (MiniGameHardConfig _iter165 : struct.ballColor)
          {
            _iter165.write(oprot);
          }
        }
      }
      if (struct.isSetBallMaterial()) {
        {
          oprot.writeI32(struct.ballMaterial.size());
          for (MiniGameHardConfig _iter166 : struct.ballMaterial)
          {
            _iter166.write(oprot);
          }
        }
      }
      if (struct.isSetBallSpeed()) {
        {
          oprot.writeI32(struct.ballSpeed.size());
          for (MiniGameHardConfig _iter167 : struct.ballSpeed)
          {
            _iter167.write(oprot);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, RatioGameConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(4);
      if (incoming.get(0)) {
        {
          org.apache.thrift.protocol.TList _list168 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.ballCount = new ArrayList<MiniGameHardConfig>(_list168.size);
          for (int _i169 = 0; _i169 < _list168.size; ++_i169)
          {
            MiniGameHardConfig _elem170;
            _elem170 = new MiniGameHardConfig();
            _elem170.read(iprot);
            struct.ballCount.add(_elem170);
          }
        }
        struct.setBallCountIsSet(true);
      }
      if (incoming.get(1)) {
        {
          org.apache.thrift.protocol.TList _list171 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.ballColor = new ArrayList<MiniGameHardConfig>(_list171.size);
          for (int _i172 = 0; _i172 < _list171.size; ++_i172)
          {
            MiniGameHardConfig _elem173;
            _elem173 = new MiniGameHardConfig();
            _elem173.read(iprot);
            struct.ballColor.add(_elem173);
          }
        }
        struct.setBallColorIsSet(true);
      }
      if (incoming.get(2)) {
        {
          org.apache.thrift.protocol.TList _list174 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.ballMaterial = new ArrayList<MiniGameHardConfig>(_list174.size);
          for (int _i175 = 0; _i175 < _list174.size; ++_i175)
          {
            MiniGameHardConfig _elem176;
            _elem176 = new MiniGameHardConfig();
            _elem176.read(iprot);
            struct.ballMaterial.add(_elem176);
          }
        }
        struct.setBallMaterialIsSet(true);
      }
      if (incoming.get(3)) {
        {
          org.apache.thrift.protocol.TList _list177 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.ballSpeed = new ArrayList<MiniGameHardConfig>(_list177.size);
          for (int _i178 = 0; _i178 < _list177.size; ++_i178)
          {
            MiniGameHardConfig _elem179;
            _elem179 = new MiniGameHardConfig();
            _elem179.read(iprot);
            struct.ballSpeed.add(_elem179);
          }
        }
        struct.setBallSpeedIsSet(true);
      }
    }
  }

}

