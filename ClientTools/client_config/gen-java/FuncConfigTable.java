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

public class FuncConfigTable implements org.apache.thrift.TBase<FuncConfigTable, FuncConfigTable._Fields>, java.io.Serializable, Cloneable, Comparable<FuncConfigTable> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("FuncConfigTable");

  private static final org.apache.thrift.protocol.TField FUNC_MAP_FIELD_DESC = new org.apache.thrift.protocol.TField("funcMap", org.apache.thrift.protocol.TType.MAP, (short)1);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new FuncConfigTableStandardSchemeFactory());
    schemes.put(TupleScheme.class, new FuncConfigTableTupleSchemeFactory());
  }

  public Map<Integer,FuncGroup> funcMap; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    FUNC_MAP((short)1, "funcMap");

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
        case 1: // FUNC_MAP
          return FUNC_MAP;
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
    tmpMap.put(_Fields.FUNC_MAP, new org.apache.thrift.meta_data.FieldMetaData("funcMap", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.MapMetaData(org.apache.thrift.protocol.TType.MAP, 
            new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32), 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, FuncGroup.class))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(FuncConfigTable.class, metaDataMap);
  }

  public FuncConfigTable() {
  }

  public FuncConfigTable(
    Map<Integer,FuncGroup> funcMap)
  {
    this();
    this.funcMap = funcMap;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public FuncConfigTable(FuncConfigTable other) {
    if (other.isSetFuncMap()) {
      Map<Integer,FuncGroup> __this__funcMap = new HashMap<Integer,FuncGroup>(other.funcMap.size());
      for (Map.Entry<Integer, FuncGroup> other_element : other.funcMap.entrySet()) {

        Integer other_element_key = other_element.getKey();
        FuncGroup other_element_value = other_element.getValue();

        Integer __this__funcMap_copy_key = other_element_key;

        FuncGroup __this__funcMap_copy_value = new FuncGroup(other_element_value);

        __this__funcMap.put(__this__funcMap_copy_key, __this__funcMap_copy_value);
      }
      this.funcMap = __this__funcMap;
    }
  }

  public FuncConfigTable deepCopy() {
    return new FuncConfigTable(this);
  }

  @Override
  public void clear() {
    this.funcMap = null;
  }

  public int getFuncMapSize() {
    return (this.funcMap == null) ? 0 : this.funcMap.size();
  }

  public void putToFuncMap(int key, FuncGroup val) {
    if (this.funcMap == null) {
      this.funcMap = new HashMap<Integer,FuncGroup>();
    }
    this.funcMap.put(key, val);
  }

  public Map<Integer,FuncGroup> getFuncMap() {
    return this.funcMap;
  }

  public FuncConfigTable setFuncMap(Map<Integer,FuncGroup> funcMap) {
    this.funcMap = funcMap;
    return this;
  }

  public void unsetFuncMap() {
    this.funcMap = null;
  }

  /** Returns true if field funcMap is set (has been assigned a value) and false otherwise */
  public boolean isSetFuncMap() {
    return this.funcMap != null;
  }

  public void setFuncMapIsSet(boolean value) {
    if (!value) {
      this.funcMap = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case FUNC_MAP:
      if (value == null) {
        unsetFuncMap();
      } else {
        setFuncMap((Map<Integer,FuncGroup>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case FUNC_MAP:
      return getFuncMap();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case FUNC_MAP:
      return isSetFuncMap();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof FuncConfigTable)
      return this.equals((FuncConfigTable)that);
    return false;
  }

  public boolean equals(FuncConfigTable that) {
    if (that == null)
      return false;

    boolean this_present_funcMap = true && this.isSetFuncMap();
    boolean that_present_funcMap = true && that.isSetFuncMap();
    if (this_present_funcMap || that_present_funcMap) {
      if (!(this_present_funcMap && that_present_funcMap))
        return false;
      if (!this.funcMap.equals(that.funcMap))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(FuncConfigTable other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetFuncMap()).compareTo(other.isSetFuncMap());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetFuncMap()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.funcMap, other.funcMap);
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
    StringBuilder sb = new StringBuilder("FuncConfigTable(");
    boolean first = true;

    sb.append("funcMap:");
    if (this.funcMap == null) {
      sb.append("null");
    } else {
      sb.append(this.funcMap);
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

  private static class FuncConfigTableStandardSchemeFactory implements SchemeFactory {
    public FuncConfigTableStandardScheme getScheme() {
      return new FuncConfigTableStandardScheme();
    }
  }

  private static class FuncConfigTableStandardScheme extends StandardScheme<FuncConfigTable> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, FuncConfigTable struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // FUNC_MAP
            if (schemeField.type == org.apache.thrift.protocol.TType.MAP) {
              {
                org.apache.thrift.protocol.TMap _map10 = iprot.readMapBegin();
                struct.funcMap = new HashMap<Integer,FuncGroup>(2*_map10.size);
                for (int _i11 = 0; _i11 < _map10.size; ++_i11)
                {
                  int _key12;
                  FuncGroup _val13;
                  _key12 = iprot.readI32();
                  _val13 = new FuncGroup();
                  _val13.read(iprot);
                  struct.funcMap.put(_key12, _val13);
                }
                iprot.readMapEnd();
              }
              struct.setFuncMapIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, FuncConfigTable struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      if (struct.funcMap != null) {
        oprot.writeFieldBegin(FUNC_MAP_FIELD_DESC);
        {
          oprot.writeMapBegin(new org.apache.thrift.protocol.TMap(org.apache.thrift.protocol.TType.I32, org.apache.thrift.protocol.TType.STRUCT, struct.funcMap.size()));
          for (Map.Entry<Integer, FuncGroup> _iter14 : struct.funcMap.entrySet())
          {
            oprot.writeI32(_iter14.getKey());
            _iter14.getValue().write(oprot);
          }
          oprot.writeMapEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class FuncConfigTableTupleSchemeFactory implements SchemeFactory {
    public FuncConfigTableTupleScheme getScheme() {
      return new FuncConfigTableTupleScheme();
    }
  }

  private static class FuncConfigTableTupleScheme extends TupleScheme<FuncConfigTable> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, FuncConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetFuncMap()) {
        optionals.set(0);
      }
      oprot.writeBitSet(optionals, 1);
      if (struct.isSetFuncMap()) {
        {
          oprot.writeI32(struct.funcMap.size());
          for (Map.Entry<Integer, FuncGroup> _iter15 : struct.funcMap.entrySet())
          {
            oprot.writeI32(_iter15.getKey());
            _iter15.getValue().write(oprot);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, FuncConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(1);
      if (incoming.get(0)) {
        {
          org.apache.thrift.protocol.TMap _map16 = new org.apache.thrift.protocol.TMap(org.apache.thrift.protocol.TType.I32, org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.funcMap = new HashMap<Integer,FuncGroup>(2*_map16.size);
          for (int _i17 = 0; _i17 < _map16.size; ++_i17)
          {
            int _key18;
            FuncGroup _val19;
            _key18 = iprot.readI32();
            _val19 = new FuncGroup();
            _val19.read(iprot);
            struct.funcMap.put(_key18, _val19);
          }
        }
        struct.setFuncMapIsSet(true);
      }
    }
  }

}

