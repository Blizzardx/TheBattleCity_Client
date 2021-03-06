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

public class TrunkConfigTable implements org.apache.thrift.TBase<TrunkConfigTable, TrunkConfigTable._Fields>, java.io.Serializable, Cloneable, Comparable<TrunkConfigTable> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("TrunkConfigTable");

  private static final org.apache.thrift.protocol.TField TRUNK_CONFIG_XML_FIELD_DESC = new org.apache.thrift.protocol.TField("trunkConfigXml", org.apache.thrift.protocol.TType.STRING, (short)1);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new TrunkConfigTableStandardSchemeFactory());
    schemes.put(TupleScheme.class, new TrunkConfigTableTupleSchemeFactory());
  }

  public String trunkConfigXml; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    TRUNK_CONFIG_XML((short)1, "trunkConfigXml");

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
        case 1: // TRUNK_CONFIG_XML
          return TRUNK_CONFIG_XML;
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
    tmpMap.put(_Fields.TRUNK_CONFIG_XML, new org.apache.thrift.meta_data.FieldMetaData("trunkConfigXml", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING)));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(TrunkConfigTable.class, metaDataMap);
  }

  public TrunkConfigTable() {
  }

  public TrunkConfigTable(
    String trunkConfigXml)
  {
    this();
    this.trunkConfigXml = trunkConfigXml;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public TrunkConfigTable(TrunkConfigTable other) {
    if (other.isSetTrunkConfigXml()) {
      this.trunkConfigXml = other.trunkConfigXml;
    }
  }

  public TrunkConfigTable deepCopy() {
    return new TrunkConfigTable(this);
  }

  @Override
  public void clear() {
    this.trunkConfigXml = null;
  }

  public String getTrunkConfigXml() {
    return this.trunkConfigXml;
  }

  public TrunkConfigTable setTrunkConfigXml(String trunkConfigXml) {
    this.trunkConfigXml = trunkConfigXml;
    return this;
  }

  public void unsetTrunkConfigXml() {
    this.trunkConfigXml = null;
  }

  /** Returns true if field trunkConfigXml is set (has been assigned a value) and false otherwise */
  public boolean isSetTrunkConfigXml() {
    return this.trunkConfigXml != null;
  }

  public void setTrunkConfigXmlIsSet(boolean value) {
    if (!value) {
      this.trunkConfigXml = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case TRUNK_CONFIG_XML:
      if (value == null) {
        unsetTrunkConfigXml();
      } else {
        setTrunkConfigXml((String)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case TRUNK_CONFIG_XML:
      return getTrunkConfigXml();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case TRUNK_CONFIG_XML:
      return isSetTrunkConfigXml();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof TrunkConfigTable)
      return this.equals((TrunkConfigTable)that);
    return false;
  }

  public boolean equals(TrunkConfigTable that) {
    if (that == null)
      return false;

    boolean this_present_trunkConfigXml = true && this.isSetTrunkConfigXml();
    boolean that_present_trunkConfigXml = true && that.isSetTrunkConfigXml();
    if (this_present_trunkConfigXml || that_present_trunkConfigXml) {
      if (!(this_present_trunkConfigXml && that_present_trunkConfigXml))
        return false;
      if (!this.trunkConfigXml.equals(that.trunkConfigXml))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(TrunkConfigTable other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetTrunkConfigXml()).compareTo(other.isSetTrunkConfigXml());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetTrunkConfigXml()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.trunkConfigXml, other.trunkConfigXml);
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
    StringBuilder sb = new StringBuilder("TrunkConfigTable(");
    boolean first = true;

    sb.append("trunkConfigXml:");
    if (this.trunkConfigXml == null) {
      sb.append("null");
    } else {
      sb.append(this.trunkConfigXml);
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

  private static class TrunkConfigTableStandardSchemeFactory implements SchemeFactory {
    public TrunkConfigTableStandardScheme getScheme() {
      return new TrunkConfigTableStandardScheme();
    }
  }

  private static class TrunkConfigTableStandardScheme extends StandardScheme<TrunkConfigTable> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, TrunkConfigTable struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 1: // TRUNK_CONFIG_XML
            if (schemeField.type == org.apache.thrift.protocol.TType.STRING) {
              struct.trunkConfigXml = iprot.readString();
              struct.setTrunkConfigXmlIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, TrunkConfigTable struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      if (struct.trunkConfigXml != null) {
        oprot.writeFieldBegin(TRUNK_CONFIG_XML_FIELD_DESC);
        oprot.writeString(struct.trunkConfigXml);
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class TrunkConfigTableTupleSchemeFactory implements SchemeFactory {
    public TrunkConfigTableTupleScheme getScheme() {
      return new TrunkConfigTableTupleScheme();
    }
  }

  private static class TrunkConfigTableTupleScheme extends TupleScheme<TrunkConfigTable> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, TrunkConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetTrunkConfigXml()) {
        optionals.set(0);
      }
      oprot.writeBitSet(optionals, 1);
      if (struct.isSetTrunkConfigXml()) {
        oprot.writeString(struct.trunkConfigXml);
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, TrunkConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(1);
      if (incoming.get(0)) {
        struct.trunkConfigXml = iprot.readString();
        struct.setTrunkConfigXmlIsSet(true);
      }
    }
  }

}

