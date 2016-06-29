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

public class ArithmeticConfigTable implements org.apache.thrift.TBase<ArithmeticConfigTable, ArithmeticConfigTable._Fields>, java.io.Serializable, Cloneable, Comparable<ArithmeticConfigTable> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("ArithmeticConfigTable");

  private static final org.apache.thrift.protocol.TField TIMER_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("timerList", org.apache.thrift.protocol.TType.LIST, (short)10);
  private static final org.apache.thrift.protocol.TField QUESTION_LIST_FIELD_DESC = new org.apache.thrift.protocol.TField("questionList", org.apache.thrift.protocol.TType.LIST, (short)20);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new ArithmeticConfigTableStandardSchemeFactory());
    schemes.put(TupleScheme.class, new ArithmeticConfigTableTupleSchemeFactory());
  }

  public List<ArithmeticTimer> timerList; // required
  public List<ArithmeticQuestion> questionList; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    TIMER_LIST((short)10, "timerList"),
    QUESTION_LIST((short)20, "questionList");

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
        case 10: // TIMER_LIST
          return TIMER_LIST;
        case 20: // QUESTION_LIST
          return QUESTION_LIST;
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
    tmpMap.put(_Fields.TIMER_LIST, new org.apache.thrift.meta_data.FieldMetaData("timerList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, ArithmeticTimer.class))));
    tmpMap.put(_Fields.QUESTION_LIST, new org.apache.thrift.meta_data.FieldMetaData("questionList", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.ListMetaData(org.apache.thrift.protocol.TType.LIST, 
            new org.apache.thrift.meta_data.StructMetaData(org.apache.thrift.protocol.TType.STRUCT, ArithmeticQuestion.class))));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(ArithmeticConfigTable.class, metaDataMap);
  }

  public ArithmeticConfigTable() {
  }

  public ArithmeticConfigTable(
    List<ArithmeticTimer> timerList,
    List<ArithmeticQuestion> questionList)
  {
    this();
    this.timerList = timerList;
    this.questionList = questionList;
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public ArithmeticConfigTable(ArithmeticConfigTable other) {
    if (other.isSetTimerList()) {
      List<ArithmeticTimer> __this__timerList = new ArrayList<ArithmeticTimer>(other.timerList.size());
      for (ArithmeticTimer other_element : other.timerList) {
        __this__timerList.add(new ArithmeticTimer(other_element));
      }
      this.timerList = __this__timerList;
    }
    if (other.isSetQuestionList()) {
      List<ArithmeticQuestion> __this__questionList = new ArrayList<ArithmeticQuestion>(other.questionList.size());
      for (ArithmeticQuestion other_element : other.questionList) {
        __this__questionList.add(new ArithmeticQuestion(other_element));
      }
      this.questionList = __this__questionList;
    }
  }

  public ArithmeticConfigTable deepCopy() {
    return new ArithmeticConfigTable(this);
  }

  @Override
  public void clear() {
    this.timerList = null;
    this.questionList = null;
  }

  public int getTimerListSize() {
    return (this.timerList == null) ? 0 : this.timerList.size();
  }

  public java.util.Iterator<ArithmeticTimer> getTimerListIterator() {
    return (this.timerList == null) ? null : this.timerList.iterator();
  }

  public void addToTimerList(ArithmeticTimer elem) {
    if (this.timerList == null) {
      this.timerList = new ArrayList<ArithmeticTimer>();
    }
    this.timerList.add(elem);
  }

  public List<ArithmeticTimer> getTimerList() {
    return this.timerList;
  }

  public ArithmeticConfigTable setTimerList(List<ArithmeticTimer> timerList) {
    this.timerList = timerList;
    return this;
  }

  public void unsetTimerList() {
    this.timerList = null;
  }

  /** Returns true if field timerList is set (has been assigned a value) and false otherwise */
  public boolean isSetTimerList() {
    return this.timerList != null;
  }

  public void setTimerListIsSet(boolean value) {
    if (!value) {
      this.timerList = null;
    }
  }

  public int getQuestionListSize() {
    return (this.questionList == null) ? 0 : this.questionList.size();
  }

  public java.util.Iterator<ArithmeticQuestion> getQuestionListIterator() {
    return (this.questionList == null) ? null : this.questionList.iterator();
  }

  public void addToQuestionList(ArithmeticQuestion elem) {
    if (this.questionList == null) {
      this.questionList = new ArrayList<ArithmeticQuestion>();
    }
    this.questionList.add(elem);
  }

  public List<ArithmeticQuestion> getQuestionList() {
    return this.questionList;
  }

  public ArithmeticConfigTable setQuestionList(List<ArithmeticQuestion> questionList) {
    this.questionList = questionList;
    return this;
  }

  public void unsetQuestionList() {
    this.questionList = null;
  }

  /** Returns true if field questionList is set (has been assigned a value) and false otherwise */
  public boolean isSetQuestionList() {
    return this.questionList != null;
  }

  public void setQuestionListIsSet(boolean value) {
    if (!value) {
      this.questionList = null;
    }
  }

  public void setFieldValue(_Fields field, Object value) {
    switch (field) {
    case TIMER_LIST:
      if (value == null) {
        unsetTimerList();
      } else {
        setTimerList((List<ArithmeticTimer>)value);
      }
      break;

    case QUESTION_LIST:
      if (value == null) {
        unsetQuestionList();
      } else {
        setQuestionList((List<ArithmeticQuestion>)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case TIMER_LIST:
      return getTimerList();

    case QUESTION_LIST:
      return getQuestionList();

    }
    throw new IllegalStateException();
  }

  /** Returns true if field corresponding to fieldID is set (has been assigned a value) and false otherwise */
  public boolean isSet(_Fields field) {
    if (field == null) {
      throw new IllegalArgumentException();
    }

    switch (field) {
    case TIMER_LIST:
      return isSetTimerList();
    case QUESTION_LIST:
      return isSetQuestionList();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof ArithmeticConfigTable)
      return this.equals((ArithmeticConfigTable)that);
    return false;
  }

  public boolean equals(ArithmeticConfigTable that) {
    if (that == null)
      return false;

    boolean this_present_timerList = true && this.isSetTimerList();
    boolean that_present_timerList = true && that.isSetTimerList();
    if (this_present_timerList || that_present_timerList) {
      if (!(this_present_timerList && that_present_timerList))
        return false;
      if (!this.timerList.equals(that.timerList))
        return false;
    }

    boolean this_present_questionList = true && this.isSetQuestionList();
    boolean that_present_questionList = true && that.isSetQuestionList();
    if (this_present_questionList || that_present_questionList) {
      if (!(this_present_questionList && that_present_questionList))
        return false;
      if (!this.questionList.equals(that.questionList))
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(ArithmeticConfigTable other) {
    if (!getClass().equals(other.getClass())) {
      return getClass().getName().compareTo(other.getClass().getName());
    }

    int lastComparison = 0;

    lastComparison = Boolean.valueOf(isSetTimerList()).compareTo(other.isSetTimerList());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetTimerList()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.timerList, other.timerList);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetQuestionList()).compareTo(other.isSetQuestionList());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetQuestionList()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.questionList, other.questionList);
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
    StringBuilder sb = new StringBuilder("ArithmeticConfigTable(");
    boolean first = true;

    sb.append("timerList:");
    if (this.timerList == null) {
      sb.append("null");
    } else {
      sb.append(this.timerList);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("questionList:");
    if (this.questionList == null) {
      sb.append("null");
    } else {
      sb.append(this.questionList);
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

  private static class ArithmeticConfigTableStandardSchemeFactory implements SchemeFactory {
    public ArithmeticConfigTableStandardScheme getScheme() {
      return new ArithmeticConfigTableStandardScheme();
    }
  }

  private static class ArithmeticConfigTableStandardScheme extends StandardScheme<ArithmeticConfigTable> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, ArithmeticConfigTable struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 10: // TIMER_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list190 = iprot.readListBegin();
                struct.timerList = new ArrayList<ArithmeticTimer>(_list190.size);
                for (int _i191 = 0; _i191 < _list190.size; ++_i191)
                {
                  ArithmeticTimer _elem192;
                  _elem192 = new ArithmeticTimer();
                  _elem192.read(iprot);
                  struct.timerList.add(_elem192);
                }
                iprot.readListEnd();
              }
              struct.setTimerListIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 20: // QUESTION_LIST
            if (schemeField.type == org.apache.thrift.protocol.TType.LIST) {
              {
                org.apache.thrift.protocol.TList _list193 = iprot.readListBegin();
                struct.questionList = new ArrayList<ArithmeticQuestion>(_list193.size);
                for (int _i194 = 0; _i194 < _list193.size; ++_i194)
                {
                  ArithmeticQuestion _elem195;
                  _elem195 = new ArithmeticQuestion();
                  _elem195.read(iprot);
                  struct.questionList.add(_elem195);
                }
                iprot.readListEnd();
              }
              struct.setQuestionListIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, ArithmeticConfigTable struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      if (struct.timerList != null) {
        oprot.writeFieldBegin(TIMER_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.timerList.size()));
          for (ArithmeticTimer _iter196 : struct.timerList)
          {
            _iter196.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      if (struct.questionList != null) {
        oprot.writeFieldBegin(QUESTION_LIST_FIELD_DESC);
        {
          oprot.writeListBegin(new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, struct.questionList.size()));
          for (ArithmeticQuestion _iter197 : struct.questionList)
          {
            _iter197.write(oprot);
          }
          oprot.writeListEnd();
        }
        oprot.writeFieldEnd();
      }
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class ArithmeticConfigTableTupleSchemeFactory implements SchemeFactory {
    public ArithmeticConfigTableTupleScheme getScheme() {
      return new ArithmeticConfigTableTupleScheme();
    }
  }

  private static class ArithmeticConfigTableTupleScheme extends TupleScheme<ArithmeticConfigTable> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, ArithmeticConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetTimerList()) {
        optionals.set(0);
      }
      if (struct.isSetQuestionList()) {
        optionals.set(1);
      }
      oprot.writeBitSet(optionals, 2);
      if (struct.isSetTimerList()) {
        {
          oprot.writeI32(struct.timerList.size());
          for (ArithmeticTimer _iter198 : struct.timerList)
          {
            _iter198.write(oprot);
          }
        }
      }
      if (struct.isSetQuestionList()) {
        {
          oprot.writeI32(struct.questionList.size());
          for (ArithmeticQuestion _iter199 : struct.questionList)
          {
            _iter199.write(oprot);
          }
        }
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, ArithmeticConfigTable struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(2);
      if (incoming.get(0)) {
        {
          org.apache.thrift.protocol.TList _list200 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.timerList = new ArrayList<ArithmeticTimer>(_list200.size);
          for (int _i201 = 0; _i201 < _list200.size; ++_i201)
          {
            ArithmeticTimer _elem202;
            _elem202 = new ArithmeticTimer();
            _elem202.read(iprot);
            struct.timerList.add(_elem202);
          }
        }
        struct.setTimerListIsSet(true);
      }
      if (incoming.get(1)) {
        {
          org.apache.thrift.protocol.TList _list203 = new org.apache.thrift.protocol.TList(org.apache.thrift.protocol.TType.STRUCT, iprot.readI32());
          struct.questionList = new ArrayList<ArithmeticQuestion>(_list203.size);
          for (int _i204 = 0; _i204 < _list203.size; ++_i204)
          {
            ArithmeticQuestion _elem205;
            _elem205 = new ArithmeticQuestion();
            _elem205.read(iprot);
            struct.questionList.add(_elem205);
          }
        }
        struct.setQuestionListIsSet(true);
      }
    }
  }

}

