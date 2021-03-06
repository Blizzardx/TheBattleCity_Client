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

public class MissionStepConfigTable implements org.apache.thrift.TBase<MissionStepConfigTable, MissionStepConfigTable._Fields>, java.io.Serializable, Cloneable, Comparable<MissionStepConfigTable> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("MissionStepConfigTable");

  private static final org.apache.thrift.protocol.TField MISSION_STEP_BY_MISSION_ID_CONFIG_MAP_FIELD_DESC = new org.apache.thrift.protocol.TField("missionStepByMissionIdConfigMap", org.apache.thrift.protocol.TType.MAP, (short)1);
  private static final org.apache.thrift.protocol.TField MISSION_STEP_BY_STEP_ID_CONFIG_MAP_FIELD_DESC = new org.apache.thrift.protocol.TField("missionStepByStepIdConfigMap", org.apache.thrift.protocol.TType.MAP, (short)2);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new MissionStepConfigTableStandardSchemeFactory());
    schemes.put(TupleScheme.class, new MissionStepConfigTableTupleSchemeFactory());
  }

  public Map<Integer,List<MissionStepConfig>> missionStepByMissionIdConfigMap; // required
  public Map<Integer,MissionStepConfig> missionStepByStepIdConfigMap; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    MISSION_STEP_BY_MISSION_ID_CONFIG_MAP((short)1, "missionStepByMissionIdConfigMap"),
    MISSION_STEP_BY_STEP_ID_CONFIG_MAP((short)2, "missionStepByStepIdConfigMap");

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
        case 1: // MISSION_STEP_BY_MISSION_ID_CONFIG_MAP
          return MISSION_STEP_BY_MISSION_ID_CONFIG_MAP;
        case 2: // MISSION_STEP_BY_STEP_ID_CONFIG_MAP
          return MISSION_STEP_BY_STEP_ID_CONFIG_MAP;
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
    tmpMap.put(_Fields.MISSION_STEP_BY_MISSION_ID_CONFIG_MAP, new org.apache.thrift.meta_data.FieldMetaData("missionStepByMissionIdConfigMap", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.MapMetaData(org.apache.thrift.protocol.TType.MAP, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32), 
            new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
                new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, MissionStepConfig.class)))));
    tmpMap.put(_Fields.MISSION_STEP_BY_STEP_ID_CONFIG_MAP, new org.apache.thrift.meta_data.FieldMetaData("missionStepByStepIdConfigMap", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.MapMetaData(org.apache.thrift.protocol.TType.MAP, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32), 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, MissionStepConfig.class))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(MissionStepConfigTable.class, metaDataMap);
  }

  public MissionStepConfigTable() {
  }

  public MissionStepConfigTable(
    Map<Integer,List<MissionStepConfig>> missionStepByMissionIdConfigMap,
    Map<Integer,MissionStepConfig> missionStepByStepIdConfigMap)
  {
    this();
    this.missionStepByMissionIdConfigMap = missionStepByMissionIdConfigMap;
    this.missionStepByStepIdConfigMap = missionStepByStepIdConfigMap;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public MissionStepConfigTable(MissionStepConfigTable other) {
    if (other.isSetMissionStepByMissionIdConfigMap()) {
      Map<Integer,List<MissionStepConfig>> __this__missionStepByMissionIdConfigMap = new HashMap<Integer,List<MissionStepConfig>>(other.missionStepByMissionIdConfigMap.size());
      for (Map.Entry<Integer, List<MissionStepConfig>> other_element : other.missionStepByMissionIdConfigMap.entrySet()) {

        Integer other_element_key = other_element.getKey();
        List<MissionStepConfig> other_element_value = other_element.getValue();

        Integer __this__missionStepByMissionIdConfigMap_copy_key = other_element_key;

        List<MissionStepConfig> __this__missionStepByMissionIdConfigMap_copy_value = new ArrayList<MissionStepConfig>(other_element_value.size());
        for (MissionStepConfig other_element_value_element : other_element_value) {
          __this__missionStepByMissionIdConfigMap_copy_value.add(new MissionStepConfig(other_element_value_element));
        }

        __this__missionStepByMissionIdConfigMap.put(__this__missionStepByMissionIdConfigMap_copy_key, __this__missionStepByMissionIdConfigMap_copy_value);
      }
      this.missionStepByMissionIdConfigMap = __this__missionStepByMissionIdConfigMap;
    }
    if (other.isSetMissionStepByStepIdConfigMap()) {
      Map<Integer,MissionStepConfig> __this__missionStepByStepIdConfigMap = new HashMap<Integer,MissionStepConfig>(other.missionStepByStepIdConfigMap.size());
      for (Map.Entry<Integer, MissionStepConfig> other_element : other.missionStepByStepIdConfigMap.entrySet()) {

        Integer other_element_key = other_element.getKey();
        MissionStepConfig other_element_value = other_element.getValue();

        Integer __this__missionStepByStepIdConfigMap_copy_key = other_element_key;

        MissionStepConfig __this__missionStepByStepIdConfigMap_copy_value = new MissionStepConfig(other_element_value);

        __this__missionStepByStepIdConfigMap.put(__this__missionStepByStepIdConfigMap_copy_key, __this__missionStepByStepIdConfigMap_copy_value);
      }
      this.missionStepByStepIdConfigMap = __this__missionStepByStepIdConfigMap;
    }
  }

  public MissionStepConfigTable deepCopy() {
    return new MissionStepConfigTable(this);
  }

  @Override
  public void clear() {
    this.missionStepByMissionIdConfigMap = null;
    this.missionStepByStepIdConfigMap = null;
  }

  public int getMissionStepByMissionIdConfigMapSize() {
    return (this.missionStepByMissionIdConfigMap == null) ? 0 : this.missionStepByMissionIdConfigMap.size();
  }

  public void putToMissionStepByMissionIdConfigMap(int key, List<MissionStepConfig> val) {
    if (this.missionStepByMissionIdConfigMap == null) {
      this.missionStepByMissionIdConfigMap = new HashMap<Integer,List<MissionStepConfig>>();
    }
    this.missionStepByMissionIdConfigMap.put(key, val);
  }

  public Map<Integer,List<MissionStepConfig>> getMissionStepByMissionIdConfigMap() {
    return this.missionStepByMissionIdConfigMap;
  }

  public MissionStepConfigTable setMissionStepByMissionIdConfigMap(Map<Integer,List<MissionStepConfig>> missionStepByMissionIdConfigMap) {
    this.missionStepByMissionIdConfigMap = missionStepByMissionIdConfigMap;
    return this;
  }

  public void unsetMissionStepByMissionIdConfigMap() {
    this.missionStepByMissionIdConfigMap = null;
  }

  /** Returns true if field missionStepByMissionIdConfigMap is set (has been assigned a value) and false otherwise */
  public boolean isSetMissionStepByMissionIdConfigMap() {
    return this.missionStepByMissionIdConfigMap != null;
  }

  public void setMissionStepByMissionIdConfigMapIsSet(boolean value) {
    if (!value) {
      this.missionStepByMissionIdConfigMap = null;
    }
  }

  public int getMissionStepByStepIdConfigMapSize() {
    return (this.missionStepByStepIdConfigMap == null) ? 0 : this.missionStepByStepIdConfigMap.size();
  }

  public void putToMissionStepByStepIdConfigMap(int key, MissionStepConfig val) {
    if (this.missionStepByStepIdConfigMap == null) {
      this.missionStepByStepIdConfigMap = new HashMap<Integer,MissionStepConfig>();
    }
    this.missionStepByStepIdConfigMap.put(key, val);
  }

  public Map<Integer,MissionStepConfig> getMissionStepByStepIdConfigMap() {
    return this.missionStepByStepIdConfigMap;
  }

  public MissionStepConfigTable setMissionStepByStepIdConfigMap(Map<Integer,MissionStepConfig> missionStepByStepIdConfigMap) {
    this.missionStepByStepIdConfigMap = missionStepByStepIdConfigMap;
    return this;
  }

  public void unsetMissionStepByStepIdConfigMap() {
    this.missionStepByStepIdConfigMap = null;
  }

  /** Returns true if field missionStepByStepIdConfigMap is set (has been assigned a value) and false otherwise */
  public boolean isSetMissionStepByStepIdConfigMap() {
    return this.missionStepByStepIdConfigMap != null;
  }

  public void setMissionStepByStepIdConfigMapIsSet(boolean value) {
    if (!value) {
      this.missionStepByStepIdConfigMap = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case MISSION_STEP_BY_MISSION_ID_CONFIG_MAP:
      if (value == null) {
        unsetMissionStepByMissionIdConfigMap();
      } else {
        setMissionStepByMissionIdConfigMap((Map<Integer,List<MissionStepConfig>>)value);
      }
      break;

    case MISSION_STEP_BY_STEP_ID_CONFIG_MAP:
      if (value == null) {
        unsetMissionStepByStepIdConfigMap();
      } else {
        setMissionStepByStepIdConfigMap((Map<Integer,MissionStepConfig>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case MISSION_STEP_BY_MISSION_ID_CONFIG_MAP:
      return getMissionStepByMissionIdConfigMap();

    case MISSION_STEP_BY_STEP_ID_CONFIG_MAP:
      return getMissionStepByStepIdConfigMap();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case MISSION_STEP_BY_MISSION_ID_CONFIG_MAP:
      return isSetMissionStepByMissionIdConfigMap();
    case MISSION_STEP_BY_STEP_ID_CONFIG_MAP:
      return isSetMissionStepByStepIdConfigMap();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof MissionStepConfigTable)
      return this.equals((MissionStepConfigTable)that);
    return false;
  }

  public boolean equals(MissionStepConfigTable that) {
    if (that == null)
      return false;

    boolean this_present_missionStepByMissionIdConfigMap = true && this.isSetMissionStepByMissionIdConfigMap();
    boolean that_present_missionStepByMissionIdConfigMap = true && that.isSetMissionStepByMissionIdConfigMap();
    if (this_present_missionStepByMissionIdConfigMap || that_present_missionStepByMissionIdConfigMap) {
      if (!(this_present_missionStepByMissionIdConfigMap && that_present_missionStepByMissionIdConfigMap))
        return false;
      if (!this.missionStepByMissionIdConfigMap.equals(that.missionStepByMissionIdConfigMap))
        return false;
    }

    boolean this_present_missionStepByStepIdConfigMap = true && this.isSetMissionStepByStepIdConfigMap();
    boolean that_present_missionStepByStepIdConfigMap = true && that.isSetMissionStepByStepIdConfigMap();
    if (this_present_missionStepByStepIdConfigMap || that_present_missionStepByStepIdConfigMap) {
      if (!(this_present_missionStepByStepIdConfigMap && that_present_missionStepByStepIdConfigMap))
        return false;
      if (!this.missionStepByStepIdConfigMap.equals(that.missionStepByStepIdConfigMap))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(MissionStepConfigTable other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetMissionStepByMissionIdConfigMap()).compareTo(other.isSetMissionStepByMissionIdConfigMap());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetMissionStepByMissionIdConfigMap()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.missionStepByMissionIdConfigMap, other.missionStepByMissionIdConfigMap);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetMissionStepByStepIdConfigMap()).compareTo(other.isSetMissionStepByStepIdConfigMap());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetMissionStepByStepIdConfigMap()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.missionStepByStepIdConfigMap, other.missionStepByStepIdConfigMap);
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
    StringBuilder sb = new StringBuilder("MissionStepConfigTable(");
    boolean first = true;

    sb.append("missionStepByMissionIdConfigMap:");
    if (this.missionStepByMissionIdConfigMap == null) {
      sb.append("null");
    } else {
      sb.append(this.missionStepByMissionIdConfigMap);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("missionStepByStepIdConfigMap:");
    if (this.missionStepByStepIdConfigMap == null) {
      sb.append("null");
    } else {
      sb.append(this.missionStepByStepIdConfigMap);
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

  private static class MissionStepConfigTableStandardSchemeFactory implements SchemeFactory {
    public MissionStepConfigTableStandardScheme getScheme() {
      return new MissionStepConfigTableStandardScheme();
    }
  }

  private static class MissionStepConfigTableStandardScheme extends StandardScheme<MissionStepConfigTable> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, MissionStepConfigTable struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // MISSION_STEP_BY_MISSION_ID_CONFIG_MAP
            if (schemeField.type == org.apache.thrift.protocol.TType.MAP) {
              {
                org.apache.thrift.protocol.TMap _map110 = iprot.readMapBegin();
                struct.missionStepByMissionIdConfigMap = new HashMap<Integer,List<MissionStepConfig>>(2*_map110.size);
                for (int _i111 = 0; _i111 < _map110.size; ++_i111)
                {
                  int _key112;
                  List<MissionStepConfig> _val113;
                  _key112 = iprot.readI32();
                  {
                    org.apache.thrift.protocol.TList _list114 = iprot.readListBegin();
                    _val113 = new ArrayList<MissionStepConfig>(_list114.size);
                    for (int _i115 = 0; _i115 < _list114.size; ++_i115)
                    {
                      MissionStepConfig _elem116;
                      _elem116 = new MissionStepConfig();
                      _elem116.read(iprot);
                      _val113.add(_elem116);
                    }
                    iprot.readListEnd();
                  }
                  struct.missionStepByMissionIdConfigMap.put(_key112, _val113);
                }
                iprot.readMapEnd();
              }
              struct.setMissionStepByMissionIdConfigMapIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 2: // MISSION_STEP_BY_STEP_ID_CONFIG_MAP
            if (schemeField.type == org.apache.thrift.protocol.TType.MAP) {
              {
                org.apache.thrift.protocol.TMap _map117 = iprot.readMapBegin();
                struct.missionStepByStepIdConfigMap = new HashMap<Integer,MissionStepConfig>(2*_map117.size);
                for (int _i118 = 0; _i118 < _map117.size; ++_i118)
                {
                  int _key119;
                  MissionStepConfig _val120;
                  _key119 = iprot.readI32();
                  _val120 = new MissionStepConfig();
                  _val120.read(iprot);
                  struct.missionStepByStepIdConfigMap.put(_key119, _val120);
                }
                iprot.readMapEnd();
              }
              struct.setMissionStepByStepIdConfigMapIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, MissionStepConfigTable struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      if (struct.missionStepByMissionIdConfigMap != null) {
        oprot.writeFieldBegin(MISSION_STEP_BY_MISSION_ID_CONFIG_MAP_FIELD_DESC);
        {
          oprot.writeMapBegin(new org.apache.thrift.protocol.TMap(org.apache.thrift.protocol.TType.I32, org.apache.thrift.protocol.TType.LIST, struct.missionStepByMissionIdConfigMap.size()));
          for (Map.Entry<Integer, List<MissionStepConfig>> _iter121 : struct.missionStepByMissionIdConfigMap.entrySet())
          {
            oprot.writeI32(_iter121.getKey());
            {
              oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, _iter121.getValue().size()));
              for (MissionStepConfig _iter122 : _iter121.getValue())
              {
                _iter122.write(oprot);
              }
              oprot.writeListEnd();
            }
          }
          oprot.writeMapEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.missionStepByStepIdConfigMap != null) {
        oprot.writeFieldBegin(MISSION_STEP_BY_STEP_ID_CONFIG_MAP_FIELD_DESC);
        {
          oprot.writeMapBegin(new org.apache.thrift.protocol.TMap(org.apache.thrift.protocol.TType.I32, org.apache.thrift.protocol.TType.STRUCT, struct.missionStepByStepIdConfigMap.size()));
          for (Map.Entry<Integer, MissionStepConfig> _iter123 : struct.missionStepByStepIdConfigMap.entrySet())
          {
            oprot.writeI32(_iter123.getKey());
            _iter123.getValue().write(oprot);
          }
          oprot.writeMapEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class MissionStepConfigTableTupleSchemeFactory implements SchemeFactory {
    public MissionStepConfigTableTupleScheme getScheme() {
      return new MissionStepConfigTableTupleScheme();
    }
  }

  private static class MissionStepConfigTableTupleScheme extends TupleScheme<MissionStepConfigTable> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, MissionStepConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetMissionStepByMissionIdConfigMap()) {
        optionals.set(0);
      }
      if (struct.isSetMissionStepByStepIdConfigMap()) {
        optionals.set(1);
      }
      oprot.writeBitSet(optionals, 2);
      if (struct.isSetMissionStepByMissionIdConfigMap()) {
        {
          oprot.writeI32(struct.missionStepByMissionIdConfigMap.size());
          for (Map.Entry<Integer, List<MissionStepConfig>> _iter124 : struct.missionStepByMissionIdConfigMap.entrySet())
          {
            oprot.writeI32(_iter124.getKey());
            {
              oprot.writeI32(_iter124.getValue().size());
              for (MissionStepConfig _iter125 : _iter124.getValue())
              {
                _iter125.write(oprot);
              }
            }
          }
        }
      }
      if (struct.isSetMissionStepByStepIdConfigMap()) {
        {
          oprot.writeI32(struct.missionStepByStepIdConfigMap.size());
          for (Map.Entry<Integer, MissionStepConfig> _iter126 : struct.missionStepByStepIdConfigMap.entrySet())
          {
            oprot.writeI32(_iter126.getKey());
            _iter126.getValue().write(oprot);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, MissionStepConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(2);
      if (incoming.get(0)) {
        {
          org.apache.thrift.protocol.TMap _map127 = new org.apache.thrift.protocol.TMap(org.apache.thrift.protocol.TType.I32, org.apache.thrift.protocol.TType.LIST, iprot.readI32());
          struct.missionStepByMissionIdConfigMap = new HashMap<Integer,List<MissionStepConfig>>(2*_map127.size);
          for (int _i128 = 0; _i128 < _map127.size; ++_i128)
          {
            int _key129;
            List<MissionStepConfig> _val130;
            _key129 = iprot.readI32();
            {
              org.apache.thrift.protocol.TList _list131 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
              _val130 = new ArrayList<MissionStepConfig>(_list131.size);
              for (int _i132 = 0; _i132 < _list131.size; ++_i132)
              {
                MissionStepConfig _elem133;
                _elem133 = new MissionStepConfig();
                _elem133.read(iprot);
                _val130.add(_elem133);
              }
            }
            struct.missionStepByMissionIdConfigMap.put(_key129, _val130);
          }
        }
        struct.setMissionStepByMissionIdConfigMapIsSet(true);
      }
      if (incoming.get(1)) {
        {
          org.apache.thrift.protocol.TMap _map134 = new org.apache.thrift.protocol.TMap(org.apache.thrift.protocol.TType.I32, org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.missionStepByStepIdConfigMap = new HashMap<Integer,MissionStepConfig>(2*_map134.size);
          for (int _i135 = 0; _i135 < _map134.size; ++_i135)
          {
            int _key136;
            MissionStepConfig _val137;
            _key136 = iprot.readI32();
            _val137 = new MissionStepConfig();
            _val137.read(iprot);
            struct.missionStepByStepIdConfigMap.put(_key136, _val137);
          }
        }
        struct.setMissionStepByStepIdConfigMapIsSet(true);
      }
    }
  }

}

