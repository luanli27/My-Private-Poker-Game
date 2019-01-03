// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: appprotobuf.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from appprotobuf.proto</summary>
public static partial class AppprotobufReflection {

  #region Descriptor
  /// <summary>File descriptor for appprotobuf.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static AppprotobufReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "ChFhcHBwcm90b2J1Zi5wcm90byIcCghSZXFMb2dpbhIQCghVc2VyTmFtZRgB",
          "IAEoCSIvCgxBY2tFbnRlclJvb20SHwoKUGxheWVySW5mbxgBIAMoCzILLlBs",
          "YXllckluZm8iOwoVQWNrTmV3UGxheWVyRW50ZXJSb29tEiIKDW5ld1BsYXll",
          "ckluZm8YASABKAsyCy5QbGF5ZXJJbmZvIi8KClBsYXllckluZm8SEAoIVXNl",
          "ck5hbWUYASABKAkSDwoHZ29sZE51bRgCIAEoBWIGcHJvdG8z"));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
          new pbr::GeneratedClrTypeInfo(typeof(global::ReqLogin), global::ReqLogin.Parser, new[]{ "UserName" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::AckEnterRoom), global::AckEnterRoom.Parser, new[]{ "PlayerInfo" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::AckNewPlayerEnterRoom), global::AckNewPlayerEnterRoom.Parser, new[]{ "NewPlayerInfo" }, null, null, null),
          new pbr::GeneratedClrTypeInfo(typeof(global::PlayerInfo), global::PlayerInfo.Parser, new[]{ "UserName", "GoldNum" }, null, null, null)
        }));
  }
  #endregion

}
#region Messages
public sealed partial class ReqLogin : pb::IMessage<ReqLogin> {
  private static readonly pb::MessageParser<ReqLogin> _parser = new pb::MessageParser<ReqLogin>(() => new ReqLogin());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<ReqLogin> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::AppprotobufReflection.Descriptor.MessageTypes[0]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ReqLogin() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ReqLogin(ReqLogin other) : this() {
    userName_ = other.userName_;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public ReqLogin Clone() {
    return new ReqLogin(this);
  }

  /// <summary>Field number for the "UserName" field.</summary>
  public const int UserNameFieldNumber = 1;
  private string userName_ = "";
  /// <summary>
  /// 玩家姓名
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string UserName {
    get { return userName_; }
    set {
      userName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as ReqLogin);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(ReqLogin other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (UserName != other.UserName) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (UserName.Length != 0) hash ^= UserName.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (UserName.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(UserName);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (UserName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(UserName);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(ReqLogin other) {
    if (other == null) {
      return;
    }
    if (other.UserName.Length != 0) {
      UserName = other.UserName;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 10: {
          UserName = input.ReadString();
          break;
        }
      }
    }
  }

}

public sealed partial class AckEnterRoom : pb::IMessage<AckEnterRoom> {
  private static readonly pb::MessageParser<AckEnterRoom> _parser = new pb::MessageParser<AckEnterRoom>(() => new AckEnterRoom());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<AckEnterRoom> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::AppprotobufReflection.Descriptor.MessageTypes[1]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public AckEnterRoom() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public AckEnterRoom(AckEnterRoom other) : this() {
    playerInfo_ = other.playerInfo_.Clone();
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public AckEnterRoom Clone() {
    return new AckEnterRoom(this);
  }

  /// <summary>Field number for the "PlayerInfo" field.</summary>
  public const int PlayerInfoFieldNumber = 1;
  private static readonly pb::FieldCodec<global::PlayerInfo> _repeated_playerInfo_codec
      = pb::FieldCodec.ForMessage(10, global::PlayerInfo.Parser);
  private readonly pbc::RepeatedField<global::PlayerInfo> playerInfo_ = new pbc::RepeatedField<global::PlayerInfo>();
  /// <summary>
  /// 玩家列表
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public pbc::RepeatedField<global::PlayerInfo> PlayerInfo {
    get { return playerInfo_; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as AckEnterRoom);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(AckEnterRoom other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if(!playerInfo_.Equals(other.playerInfo_)) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    hash ^= playerInfo_.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    playerInfo_.WriteTo(output, _repeated_playerInfo_codec);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    size += playerInfo_.CalculateSize(_repeated_playerInfo_codec);
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(AckEnterRoom other) {
    if (other == null) {
      return;
    }
    playerInfo_.Add(other.playerInfo_);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 10: {
          playerInfo_.AddEntriesFrom(input, _repeated_playerInfo_codec);
          break;
        }
      }
    }
  }

}

public sealed partial class AckNewPlayerEnterRoom : pb::IMessage<AckNewPlayerEnterRoom> {
  private static readonly pb::MessageParser<AckNewPlayerEnterRoom> _parser = new pb::MessageParser<AckNewPlayerEnterRoom>(() => new AckNewPlayerEnterRoom());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<AckNewPlayerEnterRoom> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::AppprotobufReflection.Descriptor.MessageTypes[2]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public AckNewPlayerEnterRoom() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public AckNewPlayerEnterRoom(AckNewPlayerEnterRoom other) : this() {
    NewPlayerInfo = other.newPlayerInfo_ != null ? other.NewPlayerInfo.Clone() : null;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public AckNewPlayerEnterRoom Clone() {
    return new AckNewPlayerEnterRoom(this);
  }

  /// <summary>Field number for the "newPlayerInfo" field.</summary>
  public const int NewPlayerInfoFieldNumber = 1;
  private global::PlayerInfo newPlayerInfo_;
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public global::PlayerInfo NewPlayerInfo {
    get { return newPlayerInfo_; }
    set {
      newPlayerInfo_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as AckNewPlayerEnterRoom);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(AckNewPlayerEnterRoom other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (!object.Equals(NewPlayerInfo, other.NewPlayerInfo)) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (newPlayerInfo_ != null) hash ^= NewPlayerInfo.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (newPlayerInfo_ != null) {
      output.WriteRawTag(10);
      output.WriteMessage(NewPlayerInfo);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (newPlayerInfo_ != null) {
      size += 1 + pb::CodedOutputStream.ComputeMessageSize(NewPlayerInfo);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(AckNewPlayerEnterRoom other) {
    if (other == null) {
      return;
    }
    if (other.newPlayerInfo_ != null) {
      if (newPlayerInfo_ == null) {
        newPlayerInfo_ = new global::PlayerInfo();
      }
      NewPlayerInfo.MergeFrom(other.NewPlayerInfo);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 10: {
          if (newPlayerInfo_ == null) {
            newPlayerInfo_ = new global::PlayerInfo();
          }
          input.ReadMessage(newPlayerInfo_);
          break;
        }
      }
    }
  }

}

public sealed partial class PlayerInfo : pb::IMessage<PlayerInfo> {
  private static readonly pb::MessageParser<PlayerInfo> _parser = new pb::MessageParser<PlayerInfo>(() => new PlayerInfo());
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pb::MessageParser<PlayerInfo> Parser { get { return _parser; } }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public static pbr::MessageDescriptor Descriptor {
    get { return global::AppprotobufReflection.Descriptor.MessageTypes[3]; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  pbr::MessageDescriptor pb::IMessage.Descriptor {
    get { return Descriptor; }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PlayerInfo() {
    OnConstruction();
  }

  partial void OnConstruction();

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PlayerInfo(PlayerInfo other) : this() {
    userName_ = other.userName_;
    goldNum_ = other.goldNum_;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public PlayerInfo Clone() {
    return new PlayerInfo(this);
  }

  /// <summary>Field number for the "UserName" field.</summary>
  public const int UserNameFieldNumber = 1;
  private string userName_ = "";
  /// <summary>
  /// 玩家姓名
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public string UserName {
    get { return userName_; }
    set {
      userName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
    }
  }

  /// <summary>Field number for the "goldNum" field.</summary>
  public const int GoldNumFieldNumber = 2;
  private int goldNum_;
  /// <summary>
  /// 玩家金币数量
  /// </summary>
  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int GoldNum {
    get { return goldNum_; }
    set {
      goldNum_ = value;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override bool Equals(object other) {
    return Equals(other as PlayerInfo);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public bool Equals(PlayerInfo other) {
    if (ReferenceEquals(other, null)) {
      return false;
    }
    if (ReferenceEquals(other, this)) {
      return true;
    }
    if (UserName != other.UserName) return false;
    if (GoldNum != other.GoldNum) return false;
    return true;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override int GetHashCode() {
    int hash = 1;
    if (UserName.Length != 0) hash ^= UserName.GetHashCode();
    if (GoldNum != 0) hash ^= GoldNum.GetHashCode();
    return hash;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public override string ToString() {
    return pb::JsonFormatter.ToDiagnosticString(this);
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void WriteTo(pb::CodedOutputStream output) {
    if (UserName.Length != 0) {
      output.WriteRawTag(10);
      output.WriteString(UserName);
    }
    if (GoldNum != 0) {
      output.WriteRawTag(16);
      output.WriteInt32(GoldNum);
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public int CalculateSize() {
    int size = 0;
    if (UserName.Length != 0) {
      size += 1 + pb::CodedOutputStream.ComputeStringSize(UserName);
    }
    if (GoldNum != 0) {
      size += 1 + pb::CodedOutputStream.ComputeInt32Size(GoldNum);
    }
    return size;
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(PlayerInfo other) {
    if (other == null) {
      return;
    }
    if (other.UserName.Length != 0) {
      UserName = other.UserName;
    }
    if (other.GoldNum != 0) {
      GoldNum = other.GoldNum;
    }
  }

  [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
  public void MergeFrom(pb::CodedInputStream input) {
    uint tag;
    while ((tag = input.ReadTag()) != 0) {
      switch(tag) {
        default:
          input.SkipLastField();
          break;
        case 10: {
          UserName = input.ReadString();
          break;
        }
        case 16: {
          GoldNum = input.ReadInt32();
          break;
        }
      }
    }
  }

}

#endregion


#endregion Designer generated code
