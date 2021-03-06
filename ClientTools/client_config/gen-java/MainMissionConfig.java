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

public class MainMissionConfig implements org.apache.thrift.TBase<MainMissionConfig, MainMissionConfig._Fields>, java.io.Serializable, Cloneable, Comparable<MainMissionConfig> {
  private static final org.apache.thrift.protocol.TStruct STRUCT_DESC = new org.apache.thrift.protocol.TStruct("MainMissionConfig");

  private static final org.apache.thrift.protocol.TField ID_FIELD_DESC = new org.apache.thrift.protocol.TField("id", org.apache.thrift.protocol.TType.I32, (short)10);
  private static final org.apache.thrift.protocol.TField NAME_MESSAGE_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("nameMessageId", org.apache.thrift.protocol.TType.I32, (short)20);
  private static final org.apache.thrift.protocol.TField NAME_AUDIO_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("nameAudioId", org.apache.thrift.protocol.TType.STRING, (short)30);
  private static final org.apache.thrift.protocol.TField DES_MESSAGE_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("desMessageId", org.apache.thrift.protocol.TType.I32, (short)40);
  private static final org.apache.thrift.protocol.TField DES_AUDIO_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("desAudioId", org.apache.thrift.protocol.TType.STRING, (short)50);
  private static final org.apache.thrift.protocol.TField ACCEPT_LIMIT_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("acceptLimitId", org.apache.thrift.protocol.TType.I32, (short)60);
  private static final org.apache.thrift.protocol.TField COMPLETE_LIMIT_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("completeLimitId", org.apache.thrift.protocol.TType.I32, (short)70);
  private static final org.apache.thrift.protocol.TField COMPLETE_FUNC_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("completeFuncId", org.apache.thrift.protocol.TType.I32, (short)80);
  private static final org.apache.thrift.protocol.TField NEXT_MISSION_ID_FIELD_DESC = new org.apache.thrift.protocol.TField("nextMissionId", org.apache.thrift.protocol.TType.I32, (short)90);

  private static final Map<Class<? extends IScheme>, SchemeFactory> schemes = new HashMap<Class<? extends IScheme>, SchemeFactory>();
  static {
    schemes.put(StandardScheme.class, new MainMissionConfigStandardSchemeFactory());
    schemes.put(TupleScheme.class, new MainMissionConfigTupleSchemeFactory());
  }

  public int id; // required
  public int nameMessageId; // required
  public String nameAudioId; // required
  public int desMessageId; // required
  public String desAudioId; // required
  public int acceptLimitId; // required
  public int completeLimitId; // required
  public int completeFuncId; // required
  public int nextMissionId; // required

  /** The set of fields this struct contains, along with convenience methods for finding and manipulating them. */
  public enum _Fields implements org.apache.thrift.TFieldIdEnum {
    ID((short)10, "id"),
    NAME_MESSAGE_ID((short)20, "nameMessageId"),
    NAME_AUDIO_ID((short)30, "nameAudioId"),
    DES_MESSAGE_ID((short)40, "desMessageId"),
    DES_AUDIO_ID((short)50, "desAudioId"),
    ACCEPT_LIMIT_ID((short)60, "acceptLimitId"),
    COMPLETE_LIMIT_ID((short)70, "completeLimitId"),
    COMPLETE_FUNC_ID((short)80, "completeFuncId"),
    NEXT_MISSION_ID((short)90, "nextMissionId");

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
        case 10: // ID
          return ID;
        case 20: // NAME_MESSAGE_ID
          return NAME_MESSAGE_ID;
        case 30: // NAME_AUDIO_ID
          return NAME_AUDIO_ID;
        case 40: // DES_MESSAGE_ID
          return DES_MESSAGE_ID;
        case 50: // DES_AUDIO_ID
          return DES_AUDIO_ID;
        case 60: // ACCEPT_LIMIT_ID
          return ACCEPT_LIMIT_ID;
        case 70: // COMPLETE_LIMIT_ID
          return COMPLETE_LIMIT_ID;
        case 80: // COMPLETE_FUNC_ID
          return COMPLETE_FUNC_ID;
        case 90: // NEXT_MISSION_ID
          return NEXT_MISSION_ID;
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
  private static final int __NAMEMESSAGEID_ISSET_ID = 1;
  private static final int __DESMESSAGEID_ISSET_ID = 2;
  private static final int __ACCEPTLIMITID_ISSET_ID = 3;
  private static final int __COMPLETELIMITID_ISSET_ID = 4;
  private static final int __COMPLETEFUNCID_ISSET_ID = 5;
  private static final int __NEXTMISSIONID_ISSET_ID = 6;
  private byte __isset_bitfield = 0;
  public static final Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> metaDataMap;
  static {
    Map<_Fields, org.apache.thrift.meta_data.FieldMetaData> tmpMap = new EnumMap<_Fields, org.apache.thrift.meta_data.FieldMetaData>(_Fields.class);
    tmpMap.put(_Fields.ID, new org.apache.thrift.meta_data.FieldMetaData("id", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.NAME_MESSAGE_ID, new org.apache.thrift.meta_data.FieldMetaData("nameMessageId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.NAME_AUDIO_ID, new org.apache.thrift.meta_data.FieldMetaData("nameAudioId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING)));
    tmpMap.put(_Fields.DES_MESSAGE_ID, new org.apache.thrift.meta_data.FieldMetaData("desMessageId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.DES_AUDIO_ID, new org.apache.thrift.meta_data.FieldMetaData("desAudioId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.STRING)));
    tmpMap.put(_Fields.ACCEPT_LIMIT_ID, new org.apache.thrift.meta_data.FieldMetaData("acceptLimitId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.COMPLETE_LIMIT_ID, new org.apache.thrift.meta_data.FieldMetaData("completeLimitId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.COMPLETE_FUNC_ID, new org.apache.thrift.meta_data.FieldMetaData("completeFuncId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    tmpMap.put(_Fields.NEXT_MISSION_ID, new org.apache.thrift.meta_data.FieldMetaData("nextMissionId", org.apache.thrift.TFieldRequirementType.DEFAULT, 
        new org.apache.thrift.meta_data.FieldValueMetaData(org.apache.thrift.protocol.TType.I32)));
    metaDataMap = Collections.unmodifiableMap(tmpMap);
    org.apache.thrift.meta_data.FieldMetaData.addStructMetaDataMap(MainMissionConfig.class, metaDataMap);
  }

  public MainMissionConfig() {
  }

  public MainMissionConfig(
    int id,
    int nameMessageId,
    String nameAudioId,
    int desMessageId,
    String desAudioId,
    int acceptLimitId,
    int completeLimitId,
    int completeFuncId,
    int nextMissionId)
  {
    this();
    this.id = id;
    setIdIsSet(true);
    this.nameMessageId = nameMessageId;
    setNameMessageIdIsSet(true);
    this.nameAudioId = nameAudioId;
    this.desMessageId = desMessageId;
    setDesMessageIdIsSet(true);
    this.desAudioId = desAudioId;
    this.acceptLimitId = acceptLimitId;
    setAcceptLimitIdIsSet(true);
    this.completeLimitId = completeLimitId;
    setCompleteLimitIdIsSet(true);
    this.completeFuncId = completeFuncId;
    setCompleteFuncIdIsSet(true);
    this.nextMissionId = nextMissionId;
    setNextMissionIdIsSet(true);
  }

  /**
   * Performs a deep copy on <i>other</i>.
   */
  public MainMissionConfig(MainMissionConfig other) {
    __isset_bitfield = other.__isset_bitfield;
    this.id = other.id;
    this.nameMessageId = other.nameMessageId;
    if (other.isSetNameAudioId()) {
      this.nameAudioId = other.nameAudioId;
    }
    this.desMessageId = other.desMessageId;
    if (other.isSetDesAudioId()) {
      this.desAudioId = other.desAudioId;
    }
    this.acceptLimitId = other.acceptLimitId;
    this.completeLimitId = other.completeLimitId;
    this.completeFuncId = other.completeFuncId;
    this.nextMissionId = other.nextMissionId;
  }

  public MainMissionConfig deepCopy() {
    return new MainMissionConfig(this);
  }

  @Override
  public void clear() {
    setIdIsSet(false);
    this.id = 0;
    setNameMessageIdIsSet(false);
    this.nameMessageId = 0;
    this.nameAudioId = null;
    setDesMessageIdIsSet(false);
    this.desMessageId = 0;
    this.desAudioId = null;
    setAcceptLimitIdIsSet(false);
    this.acceptLimitId = 0;
    setCompleteLimitIdIsSet(false);
    this.completeLimitId = 0;
    setCompleteFuncIdIsSet(false);
    this.completeFuncId = 0;
    setNextMissionIdIsSet(false);
    this.nextMissionId = 0;
  }

  public int getId() {
    return this.id;
  }

  public MainMissionConfig setId(int id) {
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

  public int getNameMessageId() {
    return this.nameMessageId;
  }

  public MainMissionConfig setNameMessageId(int nameMessageId) {
    this.nameMessageId = nameMessageId;
    setNameMessageIdIsSet(true);
    return this;
  }

  public void unsetNameMessageId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __NAMEMESSAGEID_ISSET_ID);
  }

  /** Returns true if field nameMessageId is set (has been assigned a value) and false otherwise */
  public boolean isSetNameMessageId() {
    return EncodingUtils.testBit(__isset_bitfield, __NAMEMESSAGEID_ISSET_ID);
  }

  public void setNameMessageIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __NAMEMESSAGEID_ISSET_ID, value);
  }

  public String getNameAudioId() {
    return this.nameAudioId;
  }

  public MainMissionConfig setNameAudioId(String nameAudioId) {
    this.nameAudioId = nameAudioId;
    return this;
  }

  public void unsetNameAudioId() {
    this.nameAudioId = null;
  }

  /** Returns true if field nameAudioId is set (has been assigned a value) and false otherwise */
  public boolean isSetNameAudioId() {
    return this.nameAudioId != null;
  }

  public void setNameAudioIdIsSet(boolean value) {
    if (!value) {
      this.nameAudioId = null;
    }
  }

  public int getDesMessageId() {
    return this.desMessageId;
  }

  public MainMissionConfig setDesMessageId(int desMessageId) {
    this.desMessageId = desMessageId;
    setDesMessageIdIsSet(true);
    return this;
  }

  public void unsetDesMessageId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __DESMESSAGEID_ISSET_ID);
  }

  /** Returns true if field desMessageId is set (has been assigned a value) and false otherwise */
  public boolean isSetDesMessageId() {
    return EncodingUtils.testBit(__isset_bitfield, __DESMESSAGEID_ISSET_ID);
  }

  public void setDesMessageIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __DESMESSAGEID_ISSET_ID, value);
  }

  public String getDesAudioId() {
    return this.desAudioId;
  }

  public MainMissionConfig setDesAudioId(String desAudioId) {
    this.desAudioId = desAudioId;
    return this;
  }

  public void unsetDesAudioId() {
    this.desAudioId = null;
  }

  /** Returns true if field desAudioId is set (has been assigned a value) and false otherwise */
  public boolean isSetDesAudioId() {
    return this.desAudioId != null;
  }

  public void setDesAudioIdIsSet(boolean value) {
    if (!value) {
      this.desAudioId = null;
    }
  }

  public int getAcceptLimitId() {
    return this.acceptLimitId;
  }

  public MainMissionConfig setAcceptLimitId(int acceptLimitId) {
    this.acceptLimitId = acceptLimitId;
    setAcceptLimitIdIsSet(true);
    return this;
  }

  public void unsetAcceptLimitId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __ACCEPTLIMITID_ISSET_ID);
  }

  /** Returns true if field acceptLimitId is set (has been assigned a value) and false otherwise */
  public boolean isSetAcceptLimitId() {
    return EncodingUtils.testBit(__isset_bitfield, __ACCEPTLIMITID_ISSET_ID);
  }

  public void setAcceptLimitIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __ACCEPTLIMITID_ISSET_ID, value);
  }

  public int getCompleteLimitId() {
    return this.completeLimitId;
  }

  public MainMissionConfig setCompleteLimitId(int completeLimitId) {
    this.completeLimitId = completeLimitId;
    setCompleteLimitIdIsSet(true);
    return this;
  }

  public void unsetCompleteLimitId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __COMPLETELIMITID_ISSET_ID);
  }

  /** Returns true if field completeLimitId is set (has been assigned a value) and false otherwise */
  public boolean isSetCompleteLimitId() {
    return EncodingUtils.testBit(__isset_bitfield, __COMPLETELIMITID_ISSET_ID);
  }

  public void setCompleteLimitIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __COMPLETELIMITID_ISSET_ID, value);
  }

  public int getCompleteFuncId() {
    return this.completeFuncId;
  }

  public MainMissionConfig setCompleteFuncId(int completeFuncId) {
    this.completeFuncId = completeFuncId;
    setCompleteFuncIdIsSet(true);
    return this;
  }

  public void unsetCompleteFuncId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __COMPLETEFUNCID_ISSET_ID);
  }

  /** Returns true if field completeFuncId is set (has been assigned a value) and false otherwise */
  public boolean isSetCompleteFuncId() {
    return EncodingUtils.testBit(__isset_bitfield, __COMPLETEFUNCID_ISSET_ID);
  }

  public void setCompleteFuncIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __COMPLETEFUNCID_ISSET_ID, value);
  }

  public int getNextMissionId() {
    return this.nextMissionId;
  }

  public MainMissionConfig setNextMissionId(int nextMissionId) {
    this.nextMissionId = nextMissionId;
    setNextMissionIdIsSet(true);
    return this;
  }

  public void unsetNextMissionId() {
    __isset_bitfield = EncodingUtils.clearBit(__isset_bitfield, __NEXTMISSIONID_ISSET_ID);
  }

  /** Returns true if field nextMissionId is set (has been assigned a value) and false otherwise */
  public boolean isSetNextMissionId() {
    return EncodingUtils.testBit(__isset_bitfield, __NEXTMISSIONID_ISSET_ID);
  }

  public void setNextMissionIdIsSet(boolean value) {
    __isset_bitfield = EncodingUtils.setBit(__isset_bitfield, __NEXTMISSIONID_ISSET_ID, value);
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

    case NAME_MESSAGE_ID:
      if (value == null) {
        unsetNameMessageId();
      } else {
        setNameMessageId((Integer)value);
      }
      break;

    case NAME_AUDIO_ID:
      if (value == null) {
        unsetNameAudioId();
      } else {
        setNameAudioId((String)value);
      }
      break;

    case DES_MESSAGE_ID:
      if (value == null) {
        unsetDesMessageId();
      } else {
        setDesMessageId((Integer)value);
      }
      break;

    case DES_AUDIO_ID:
      if (value == null) {
        unsetDesAudioId();
      } else {
        setDesAudioId((String)value);
      }
      break;

    case ACCEPT_LIMIT_ID:
      if (value == null) {
        unsetAcceptLimitId();
      } else {
        setAcceptLimitId((Integer)value);
      }
      break;

    case COMPLETE_LIMIT_ID:
      if (value == null) {
        unsetCompleteLimitId();
      } else {
        setCompleteLimitId((Integer)value);
      }
      break;

    case COMPLETE_FUNC_ID:
      if (value == null) {
        unsetCompleteFuncId();
      } else {
        setCompleteFuncId((Integer)value);
      }
      break;

    case NEXT_MISSION_ID:
      if (value == null) {
        unsetNextMissionId();
      } else {
        setNextMissionId((Integer)value);
      }
      break;

    }
  }

  public Object getFieldValue(_Fields field) {
    switch (field) {
    case ID:
      return Integer.valueOf(getId());

    case NAME_MESSAGE_ID:
      return Integer.valueOf(getNameMessageId());

    case NAME_AUDIO_ID:
      return getNameAudioId();

    case DES_MESSAGE_ID:
      return Integer.valueOf(getDesMessageId());

    case DES_AUDIO_ID:
      return getDesAudioId();

    case ACCEPT_LIMIT_ID:
      return Integer.valueOf(getAcceptLimitId());

    case COMPLETE_LIMIT_ID:
      return Integer.valueOf(getCompleteLimitId());

    case COMPLETE_FUNC_ID:
      return Integer.valueOf(getCompleteFuncId());

    case NEXT_MISSION_ID:
      return Integer.valueOf(getNextMissionId());

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
    case NAME_MESSAGE_ID:
      return isSetNameMessageId();
    case NAME_AUDIO_ID:
      return isSetNameAudioId();
    case DES_MESSAGE_ID:
      return isSetDesMessageId();
    case DES_AUDIO_ID:
      return isSetDesAudioId();
    case ACCEPT_LIMIT_ID:
      return isSetAcceptLimitId();
    case COMPLETE_LIMIT_ID:
      return isSetCompleteLimitId();
    case COMPLETE_FUNC_ID:
      return isSetCompleteFuncId();
    case NEXT_MISSION_ID:
      return isSetNextMissionId();
    }
    throw new IllegalStateException();
  }

  @Override
  public boolean equals(Object that) {
    if (that == null)
      return false;
    if (that instanceof MainMissionConfig)
      return this.equals((MainMissionConfig)that);
    return false;
  }

  public boolean equals(MainMissionConfig that) {
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

    boolean this_present_nameMessageId = true;
    boolean that_present_nameMessageId = true;
    if (this_present_nameMessageId || that_present_nameMessageId) {
      if (!(this_present_nameMessageId && that_present_nameMessageId))
        return false;
      if (this.nameMessageId != that.nameMessageId)
        return false;
    }

    boolean this_present_nameAudioId = true && this.isSetNameAudioId();
    boolean that_present_nameAudioId = true && that.isSetNameAudioId();
    if (this_present_nameAudioId || that_present_nameAudioId) {
      if (!(this_present_nameAudioId && that_present_nameAudioId))
        return false;
      if (!this.nameAudioId.equals(that.nameAudioId))
        return false;
    }

    boolean this_present_desMessageId = true;
    boolean that_present_desMessageId = true;
    if (this_present_desMessageId || that_present_desMessageId) {
      if (!(this_present_desMessageId && that_present_desMessageId))
        return false;
      if (this.desMessageId != that.desMessageId)
        return false;
    }

    boolean this_present_desAudioId = true && this.isSetDesAudioId();
    boolean that_present_desAudioId = true && that.isSetDesAudioId();
    if (this_present_desAudioId || that_present_desAudioId) {
      if (!(this_present_desAudioId && that_present_desAudioId))
        return false;
      if (!this.desAudioId.equals(that.desAudioId))
        return false;
    }

    boolean this_present_acceptLimitId = true;
    boolean that_present_acceptLimitId = true;
    if (this_present_acceptLimitId || that_present_acceptLimitId) {
      if (!(this_present_acceptLimitId && that_present_acceptLimitId))
        return false;
      if (this.acceptLimitId != that.acceptLimitId)
        return false;
    }

    boolean this_present_completeLimitId = true;
    boolean that_present_completeLimitId = true;
    if (this_present_completeLimitId || that_present_completeLimitId) {
      if (!(this_present_completeLimitId && that_present_completeLimitId))
        return false;
      if (this.completeLimitId != that.completeLimitId)
        return false;
    }

    boolean this_present_completeFuncId = true;
    boolean that_present_completeFuncId = true;
    if (this_present_completeFuncId || that_present_completeFuncId) {
      if (!(this_present_completeFuncId && that_present_completeFuncId))
        return false;
      if (this.completeFuncId != that.completeFuncId)
        return false;
    }

    boolean this_present_nextMissionId = true;
    boolean that_present_nextMissionId = true;
    if (this_present_nextMissionId || that_present_nextMissionId) {
      if (!(this_present_nextMissionId && that_present_nextMissionId))
        return false;
      if (this.nextMissionId != that.nextMissionId)
        return false;
    }

    return true;
  }

  @Override
  public int hashCode() {
    return 0;
  }

  @Override
  public int compareTo(MainMissionConfig other) {
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
    lastComparison = Boolean.valueOf(isSetNameMessageId()).compareTo(other.isSetNameMessageId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetNameMessageId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.nameMessageId, other.nameMessageId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetNameAudioId()).compareTo(other.isSetNameAudioId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetNameAudioId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.nameAudioId, other.nameAudioId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetDesMessageId()).compareTo(other.isSetDesMessageId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetDesMessageId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.desMessageId, other.desMessageId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetDesAudioId()).compareTo(other.isSetDesAudioId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetDesAudioId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.desAudioId, other.desAudioId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetAcceptLimitId()).compareTo(other.isSetAcceptLimitId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetAcceptLimitId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.acceptLimitId, other.acceptLimitId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetCompleteLimitId()).compareTo(other.isSetCompleteLimitId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetCompleteLimitId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.completeLimitId, other.completeLimitId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetCompleteFuncId()).compareTo(other.isSetCompleteFuncId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetCompleteFuncId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.completeFuncId, other.completeFuncId);
      if (lastComparison != 0) {
        return lastComparison;
      }
    }
    lastComparison = Boolean.valueOf(isSetNextMissionId()).compareTo(other.isSetNextMissionId());
    if (lastComparison != 0) {
      return lastComparison;
    }
    if (isSetNextMissionId()) {
      lastComparison = org.apache.thrift.TBaseHelper.compareTo(this.nextMissionId, other.nextMissionId);
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
    StringBuilder sb = new StringBuilder("MainMissionConfig(");
    boolean first = true;

    sb.append("id:");
    sb.append(this.id);
    first = false;
    if (!first) sb.append(", ");
    sb.append("nameMessageId:");
    sb.append(this.nameMessageId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("nameAudioId:");
    if (this.nameAudioId == null) {
      sb.append("null");
    } else {
      sb.append(this.nameAudioId);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("desMessageId:");
    sb.append(this.desMessageId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("desAudioId:");
    if (this.desAudioId == null) {
      sb.append("null");
    } else {
      sb.append(this.desAudioId);
    }
    first = false;
    if (!first) sb.append(", ");
    sb.append("acceptLimitId:");
    sb.append(this.acceptLimitId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("completeLimitId:");
    sb.append(this.completeLimitId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("completeFuncId:");
    sb.append(this.completeFuncId);
    first = false;
    if (!first) sb.append(", ");
    sb.append("nextMissionId:");
    sb.append(this.nextMissionId);
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

  private static class MainMissionConfigStandardSchemeFactory implements SchemeFactory {
    public MainMissionConfigStandardScheme getScheme() {
      return new MainMissionConfigStandardScheme();
    }
  }

  private static class MainMissionConfigStandardScheme extends StandardScheme<MainMissionConfig> {

    public void read(org.apache.thrift.protocol.TProtocol iprot, MainMissionConfig struct) throws org.apache.thrift.TException {
      org.apache.thrift.protocol.TField schemeField;
      iprot.readStructBegin();
      while (true)
      {
        schemeField = iprot.readFieldBegin();
        if (schemeField.type == org.apache.thrift.protocol.TType.STOP) { 
          break;
        }
        switch (schemeField.id) {
          case 10: // ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.id = iprot.readI32();
              struct.setIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 20: // NAME_MESSAGE_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.nameMessageId = iprot.readI32();
              struct.setNameMessageIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 30: // NAME_AUDIO_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.STRING) {
              struct.nameAudioId = iprot.readString();
              struct.setNameAudioIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 40: // DES_MESSAGE_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.desMessageId = iprot.readI32();
              struct.setDesMessageIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 50: // DES_AUDIO_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.STRING) {
              struct.desAudioId = iprot.readString();
              struct.setDesAudioIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 60: // ACCEPT_LIMIT_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.acceptLimitId = iprot.readI32();
              struct.setAcceptLimitIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 70: // COMPLETE_LIMIT_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.completeLimitId = iprot.readI32();
              struct.setCompleteLimitIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 80: // COMPLETE_FUNC_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.completeFuncId = iprot.readI32();
              struct.setCompleteFuncIdIsSet(true);
            } else { 
              org.apache.thrift.protocol.TProtocolUtil.skip(iprot, schemeField.type);
            }
            break;
          case 90: // NEXT_MISSION_ID
            if (schemeField.type == org.apache.thrift.protocol.TType.I32) {
              struct.nextMissionId = iprot.readI32();
              struct.setNextMissionIdIsSet(true);
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

    public void write(org.apache.thrift.protocol.TProtocol oprot, MainMissionConfig struct) throws org.apache.thrift.TException {
      struct.validate();

      oprot.writeStructBegin(STRUCT_DESC);
      oprot.writeFieldBegin(ID_FIELD_DESC);
      oprot.writeI32(struct.id);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(NAME_MESSAGE_ID_FIELD_DESC);
      oprot.writeI32(struct.nameMessageId);
      oprot.writeFieldEnd();
      if (struct.nameAudioId != null) {
        oprot.writeFieldBegin(NAME_AUDIO_ID_FIELD_DESC);
        oprot.writeString(struct.nameAudioId);
        oprot.writeFieldEnd();
      }
      oprot.writeFieldBegin(DES_MESSAGE_ID_FIELD_DESC);
      oprot.writeI32(struct.desMessageId);
      oprot.writeFieldEnd();
      if (struct.desAudioId != null) {
        oprot.writeFieldBegin(DES_AUDIO_ID_FIELD_DESC);
        oprot.writeString(struct.desAudioId);
        oprot.writeFieldEnd();
      }
      oprot.writeFieldBegin(ACCEPT_LIMIT_ID_FIELD_DESC);
      oprot.writeI32(struct.acceptLimitId);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(COMPLETE_LIMIT_ID_FIELD_DESC);
      oprot.writeI32(struct.completeLimitId);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(COMPLETE_FUNC_ID_FIELD_DESC);
      oprot.writeI32(struct.completeFuncId);
      oprot.writeFieldEnd();
      oprot.writeFieldBegin(NEXT_MISSION_ID_FIELD_DESC);
      oprot.writeI32(struct.nextMissionId);
      oprot.writeFieldEnd();
      oprot.writeFieldStop();
      oprot.writeStructEnd();
    }

  }

  private static class MainMissionConfigTupleSchemeFactory implements SchemeFactory {
    public MainMissionConfigTupleScheme getScheme() {
      return new MainMissionConfigTupleScheme();
    }
  }

  private static class MainMissionConfigTupleScheme extends TupleScheme<MainMissionConfig> {

    @Override
    public void write(org.apache.thrift.protocol.TProtocol prot, MainMissionConfig struct) throws org.apache.thrift.TException {
      TTupleProtocol oprot = (TTupleProtocol) prot;
      BitSet optionals = new BitSet();
      if (struct.isSetId()) {
        optionals.set(0);
      }
      if (struct.isSetNameMessageId()) {
        optionals.set(1);
      }
      if (struct.isSetNameAudioId()) {
        optionals.set(2);
      }
      if (struct.isSetDesMessageId()) {
        optionals.set(3);
      }
      if (struct.isSetDesAudioId()) {
        optionals.set(4);
      }
      if (struct.isSetAcceptLimitId()) {
        optionals.set(5);
      }
      if (struct.isSetCompleteLimitId()) {
        optionals.set(6);
      }
      if (struct.isSetCompleteFuncId()) {
        optionals.set(7);
      }
      if (struct.isSetNextMissionId()) {
        optionals.set(8);
      }
      oprot.writeBitSet(optionals, 9);
      if (struct.isSetId()) {
        oprot.writeI32(struct.id);
      }
      if (struct.isSetNameMessageId()) {
        oprot.writeI32(struct.nameMessageId);
      }
      if (struct.isSetNameAudioId()) {
        oprot.writeString(struct.nameAudioId);
      }
      if (struct.isSetDesMessageId()) {
        oprot.writeI32(struct.desMessageId);
      }
      if (struct.isSetDesAudioId()) {
        oprot.writeString(struct.desAudioId);
      }
      if (struct.isSetAcceptLimitId()) {
        oprot.writeI32(struct.acceptLimitId);
      }
      if (struct.isSetCompleteLimitId()) {
        oprot.writeI32(struct.completeLimitId);
      }
      if (struct.isSetCompleteFuncId()) {
        oprot.writeI32(struct.completeFuncId);
      }
      if (struct.isSetNextMissionId()) {
        oprot.writeI32(struct.nextMissionId);
      }
    }

    @Override
    public void read(org.apache.thrift.protocol.TProtocol prot, MainMissionConfig struct) throws org.apache.thrift.TException {
      TTupleProtocol iprot = (TTupleProtocol) prot;
      BitSet incoming = iprot.readBitSet(9);
      if (incoming.get(0)) {
        struct.id = iprot.readI32();
        struct.setIdIsSet(true);
      }
      if (incoming.get(1)) {
        struct.nameMessageId = iprot.readI32();
        struct.setNameMessageIdIsSet(true);
      }
      if (incoming.get(2)) {
        struct.nameAudioId = iprot.readString();
        struct.setNameAudioIdIsSet(true);
      }
      if (incoming.get(3)) {
        struct.desMessageId = iprot.readI32();
        struct.setDesMessageIdIsSet(true);
      }
      if (incoming.get(4)) {
        struct.desAudioId = iprot.readString();
        struct.setDesAudioIdIsSet(true);
      }
      if (incoming.get(5)) {
        struct.acceptLimitId = iprot.readI32();
        struct.setAcceptLimitIdIsSet(true);
      }
      if (incoming.get(6)) {
        struct.completeLimitId = iprot.readI32();
        struct.setCompleteLimitIdIsSet(true);
      }
      if (incoming.get(7)) {
        struct.completeFuncId = iprot.readI32();
        struct.setCompleteFuncIdIsSet(true);
      }
      if (incoming.get(8)) {
        struct.nextMissionId = iprot.readI32();
        struct.setNextMissionIdIsSet(true);
      }
    }
  }

}

