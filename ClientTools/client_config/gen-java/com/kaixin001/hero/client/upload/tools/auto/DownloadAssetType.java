/**
 * Autogenerated by Thrift Compiler (0.9.1)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
package com.kaixin001.hero.client.upload.tools.auto;


import java.util.Map;
import java.util.HashMap;
import org.apache.thrift.TEnum;

public enum DownloadAssetType implements org.apache.thrift.TEnum {
  ConfigByte(0),
  ConfigText(1),
  Image(2),
  Audio(3),
  AssetBundle(4);

  private final int value;

  private DownloadAssetType(int value) {
    this.value = value;
  }

  /**
   * Get the integer value of this enum value, as defined in the Thrift IDL.
   */
  public int getValue() {
    return value;
  }

  /**
   * Find a the enum type by its integer value, as defined in the Thrift IDL.
   * @return null if the value is not found.
   */
  public static DownloadAssetType findByValue(int value) { 
    switch (value) {
      case 0:
        return ConfigByte;
      case 1:
        return ConfigText;
      case 2:
        return Image;
      case 3:
        return Audio;
      case 4:
        return AssetBundle;
      default:
        return null;
    }
  }
}
