
using global::System.Reflection;
using global::System.Runtime.CompilerServices;
using global::System.Runtime.InteropServices;
using global::Karmem;

namespace SerializationBenchmarks.Models.KM;

internal static unsafe class _Globals
{
    private static long _largest = 109;
    private static void* _null = null;
    private static Karmem.Reader? _nullReader = null;

    public static void* Null()
    {
        if (_null == null)
        {
            var n = Marshal.AllocHGlobal((int)_largest);
            Unsafe.InitBlockUnaligned(n.ToPointer(), 0, (uint)_largest);
            _null = n.ToPointer();
        }
        return _null;
    }
    public static Karmem.Reader NullReader()
    {
        _nullReader ??= Karmem.Reader.NewReader(new IntPtr(Null()), _largest, _largest);
        return _nullReader.Value;
    }
}

public enum Gender : byte {
    Male = 0,
    Female = 1,
}
    
public enum PacketIdentifier : ulong {
    Order = 13636938447310947947,
    User = 11139411273728609426,
    UserWrapper = 3642776698039132450,
}
    
public unsafe struct Order {
    public int _OrderId = 0;
    public string _Item = "";
    public int _Quantity = 0;
    public int _LotNumber = 0;

    public Order() {}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Order NewOrder() {
        return new Order();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketIdentifier GetPacketIdentifier() {
        return PacketIdentifier.Order;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() {
        this.ReadAsRoot(_Globals.NullReader());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool WriteAsRoot(Karmem.Writer writer) {
        return this.Write(writer, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Write(Karmem.Writer writer, uint start) {
        var offset = start;
        var size = (uint)20;
        if (offset == 0) {
            offset = writer.Alloc(size);
            if (offset == uint.MaxValue) {
                return false;
            }
        }
        var __OrderIdOffset = offset+0;
        writer.WriteAt(__OrderIdOffset, this._OrderId);
        var __ItemSize = (uint)(4 * this._Item.Length);
        var __ItemOffset = writer.Alloc(__ItemSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+4, (uint)__ItemOffset);
        var __ItemStringSize = writer.WriteAt(__ItemOffset, this._Item);
        writer.WriteAt(offset+4 + 4, (uint)__ItemStringSize);
        var __QuantityOffset = offset+12;
        writer.WriteAt(__QuantityOffset, this._Quantity);
        var __LotNumberOffset = offset+16;
        writer.WriteAt(__LotNumberOffset, this._LotNumber);

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ReadAsRoot(Karmem.Reader reader) {
        this.Read(OrderViewer.NewOrderViewer(reader, 0), reader);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Read(OrderViewer viewer, Karmem.Reader reader) {
        this._OrderId = viewer.OrderId();
        this._Item = viewer.Item(reader);
        this._Quantity = viewer.Quantity();
        this._LotNumber = viewer.LotNumber();
    }
}
public unsafe struct User {
    public int _Id = 0;
    public string _FirstName = "";
    public string _LastName = "";
    public string _FullName = "";
    public string _UserName = "";
    public string _Email = "";
    public string _SomethingUnique = "";
    public List<byte> _SomeGuid = new List<byte>();
    public string _Avatar = "";
    public List<byte> _CartId = new List<byte>();
    public string _SSN = "";
    public Gender _Gender = 0;
    public List<Order> _Orders = new List<Order>();

    public User() {}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static User NewUser() {
        return new User();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketIdentifier GetPacketIdentifier() {
        return PacketIdentifier.User;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() {
        this.ReadAsRoot(_Globals.NullReader());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool WriteAsRoot(Karmem.Writer writer) {
        return this.Write(writer, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Write(Karmem.Writer writer, uint start) {
        var offset = start;
        var size = (uint)109;
        if (offset == 0) {
            offset = writer.Alloc(size);
            if (offset == uint.MaxValue) {
                return false;
            }
        }
        var __IdOffset = offset+0;
        writer.WriteAt(__IdOffset, this._Id);
        var __FirstNameSize = (uint)(4 * this._FirstName.Length);
        var __FirstNameOffset = writer.Alloc(__FirstNameSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+4, (uint)__FirstNameOffset);
        var __FirstNameStringSize = writer.WriteAt(__FirstNameOffset, this._FirstName);
        writer.WriteAt(offset+4 + 4, (uint)__FirstNameStringSize);
        var __LastNameSize = (uint)(4 * this._LastName.Length);
        var __LastNameOffset = writer.Alloc(__LastNameSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+12, (uint)__LastNameOffset);
        var __LastNameStringSize = writer.WriteAt(__LastNameOffset, this._LastName);
        writer.WriteAt(offset+12 + 4, (uint)__LastNameStringSize);
        var __FullNameSize = (uint)(4 * this._FullName.Length);
        var __FullNameOffset = writer.Alloc(__FullNameSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+20, (uint)__FullNameOffset);
        var __FullNameStringSize = writer.WriteAt(__FullNameOffset, this._FullName);
        writer.WriteAt(offset+20 + 4, (uint)__FullNameStringSize);
        var __UserNameSize = (uint)(4 * this._UserName.Length);
        var __UserNameOffset = writer.Alloc(__UserNameSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+28, (uint)__UserNameOffset);
        var __UserNameStringSize = writer.WriteAt(__UserNameOffset, this._UserName);
        writer.WriteAt(offset+28 + 4, (uint)__UserNameStringSize);
        var __EmailSize = (uint)(4 * this._Email.Length);
        var __EmailOffset = writer.Alloc(__EmailSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+36, (uint)__EmailOffset);
        var __EmailStringSize = writer.WriteAt(__EmailOffset, this._Email);
        writer.WriteAt(offset+36 + 4, (uint)__EmailStringSize);
        var __SomethingUniqueSize = (uint)(4 * this._SomethingUnique.Length);
        var __SomethingUniqueOffset = writer.Alloc(__SomethingUniqueSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+44, (uint)__SomethingUniqueOffset);
        var __SomethingUniqueStringSize = writer.WriteAt(__SomethingUniqueOffset, this._SomethingUnique);
        writer.WriteAt(offset+44 + 4, (uint)__SomethingUniqueStringSize);
        var __SomeGuidOffset = offset+52;
        for (var i = 0; i < 16; i++) {
            if (i < this._SomeGuid.Count) {
                writer.WriteAt(__SomeGuidOffset, this._SomeGuid[i]);
            } else {
                writer.WriteAt(__SomeGuidOffset, 0);
            }
            __SomeGuidOffset += 1;
        }
        var __AvatarSize = (uint)(4 * this._Avatar.Length);
        var __AvatarOffset = writer.Alloc(__AvatarSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+68, (uint)__AvatarOffset);
        var __AvatarStringSize = writer.WriteAt(__AvatarOffset, this._Avatar);
        writer.WriteAt(offset+68 + 4, (uint)__AvatarStringSize);
        var __CartIdOffset = offset+76;
        for (var i = 0; i < 16; i++) {
            if (i < this._CartId.Count) {
                writer.WriteAt(__CartIdOffset, this._CartId[i]);
            } else {
                writer.WriteAt(__CartIdOffset, 0);
            }
            __CartIdOffset += 1;
        }
        var __SSNSize = (uint)(4 * this._SSN.Length);
        var __SSNOffset = writer.Alloc(__SSNSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+92, (uint)__SSNOffset);
        var __SSNStringSize = writer.WriteAt(__SSNOffset, this._SSN);
        writer.WriteAt(offset+92 + 4, (uint)__SSNStringSize);
        var __GenderOffset = offset+100;
        writer.WriteAt(__GenderOffset, (long)this._Gender);
        var __OrdersSize = (uint)(20 * this._Orders.Count);
        var __OrdersOffset = writer.Alloc(__OrdersSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+101, (uint)__OrdersOffset);
        writer.WriteAt(offset+101 + 4, (uint)__OrdersSize);
            for (var i = 0; i < this._Orders.Count; i++) {
                if (!this._Orders[i].Write(writer, __OrdersOffset)) {
                    return false;
                }
                __OrdersOffset += 20;
            }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ReadAsRoot(Karmem.Reader reader) {
        this.Read(UserViewer.NewUserViewer(reader, 0), reader);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Read(UserViewer viewer, Karmem.Reader reader) {
        this._Id = viewer.Id();
        this._FirstName = viewer.FirstName(reader);
        this._LastName = viewer.LastName(reader);
        this._FullName = viewer.FullName(reader);
        this._UserName = viewer.UserName(reader);
        this._Email = viewer.Email(reader);
        this._SomethingUnique = viewer.SomethingUnique(reader);
        var __SomeGuidSlice = viewer.SomeGuid();
        var __SomeGuidLen = __SomeGuidSlice.Length;
        if (__SomeGuidLen > 16) {
            __SomeGuidLen = 16;
        }
        if (this._SomeGuid.Count != __SomeGuidLen) {
            if (__SomeGuidLen > this._SomeGuid.Capacity) {
                this._SomeGuid.EnsureCapacity(__SomeGuidLen);
                for (var i = this._SomeGuid.Count; i < __SomeGuidLen; i++) {
                    this._SomeGuid.Add(0);
                }
            }
            this._SomeGuid.GetType().GetField("_size", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).SetValue(this._SomeGuid, __SomeGuidLen);
        }
        for (var i = 0; i < __SomeGuidLen; i++) {
            this._SomeGuid[i] = __SomeGuidSlice[i];
        }
        for (var i = __SomeGuidLen; i < this._SomeGuid.Count; i++) {
            this._SomeGuid[i] = 0;
        }
        this._Avatar = viewer.Avatar(reader);
        var __CartIdSlice = viewer.CartId();
        var __CartIdLen = __CartIdSlice.Length;
        if (__CartIdLen > 16) {
            __CartIdLen = 16;
        }
        if (this._CartId.Count != __CartIdLen) {
            if (__CartIdLen > this._CartId.Capacity) {
                this._CartId.EnsureCapacity(__CartIdLen);
                for (var i = this._CartId.Count; i < __CartIdLen; i++) {
                    this._CartId.Add(0);
                }
            }
            this._CartId.GetType().GetField("_size", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).SetValue(this._CartId, __CartIdLen);
        }
        for (var i = 0; i < __CartIdLen; i++) {
            this._CartId[i] = __CartIdSlice[i];
        }
        for (var i = __CartIdLen; i < this._CartId.Count; i++) {
            this._CartId[i] = 0;
        }
        this._SSN = viewer.SSN(reader);
        this._Gender = (Gender)(viewer.Gender());
        var __OrdersSlice = viewer.Orders(reader);
        var __OrdersLen = __OrdersSlice.Length;
        if (this._Orders.Count != __OrdersLen) {
            if (__OrdersLen > this._Orders.Capacity) {
                this._Orders.EnsureCapacity(__OrdersLen);
                for (var i = this._Orders.Count; i < __OrdersLen; i++) {
                    this._Orders.Add(new Order());
                }
            }
            this._Orders.GetType().GetField("_size", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).SetValue(this._Orders, __OrdersLen);
        }
        var __OrdersSpan = CollectionsMarshal.AsSpan(this._Orders);
        for (var i = 0; i < __OrdersLen; i++) {
            ref var __OrdersItem = ref __OrdersSpan[i];
            __OrdersItem.Read(__OrdersSlice[i], reader);
        }
    }
}
public unsafe struct UserWrapper {
    public List<User> _Users = new List<User>();

    public UserWrapper() {}

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UserWrapper NewUserWrapper() {
        return new UserWrapper();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketIdentifier GetPacketIdentifier() {
        return PacketIdentifier.UserWrapper;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() {
        this.ReadAsRoot(_Globals.NullReader());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool WriteAsRoot(Karmem.Writer writer) {
        return this.Write(writer, 0);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Write(Karmem.Writer writer, uint start) {
        var offset = start;
        var size = (uint)8;
        if (offset == 0) {
            offset = writer.Alloc(size);
            if (offset == uint.MaxValue) {
                return false;
            }
        }
        var __UsersSize = (uint)(109 * this._Users.Count);
        var __UsersOffset = writer.Alloc(__UsersSize);
        if (offset == uint.MaxValue) {
            return false;
        }
        writer.WriteAt(offset+0, (uint)__UsersOffset);
        writer.WriteAt(offset+0 + 4, (uint)__UsersSize);
            for (var i = 0; i < this._Users.Count; i++) {
                if (!this._Users[i].Write(writer, __UsersOffset)) {
                    return false;
                }
                __UsersOffset += 109;
            }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void ReadAsRoot(Karmem.Reader reader) {
        this.Read(UserWrapperViewer.NewUserWrapperViewer(reader, 0), reader);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Read(UserWrapperViewer viewer, Karmem.Reader reader) {
        var __UsersSlice = viewer.Users(reader);
        var __UsersLen = __UsersSlice.Length;
        if (this._Users.Count != __UsersLen) {
            if (__UsersLen > this._Users.Capacity) {
                this._Users.EnsureCapacity(__UsersLen);
                for (var i = this._Users.Count; i < __UsersLen; i++) {
                    this._Users.Add(new User());
                }
            }
            this._Users.GetType().GetField("_size", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField).SetValue(this._Users, __UsersLen);
        }
        var __UsersSpan = CollectionsMarshal.AsSpan(this._Users);
        for (var i = 0; i < __UsersLen; i++) {
            ref var __UsersItem = ref __UsersSpan[i];
            __UsersItem.Read(__UsersSlice[i], reader);
        }
    }
}

[StructLayout(LayoutKind.Sequential, Pack=1, Size=20)]
public unsafe struct OrderViewer {
    private readonly ulong _0;
    private readonly ulong _1;
    private readonly uint _2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref OrderViewer NewOrderViewer(Karmem.Reader reader, uint offset) {
        if (!reader.IsValidOffset(offset, 20)) {
            return ref *(OrderViewer*)(nuint)_Globals.Null();
        }
        ref OrderViewer v = ref *(OrderViewer*)(reader.MemoryPointer + offset);
        return ref v;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private uint KarmemSizeOf() {
        return 20;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int OrderId() {
        return *(int*)((nuint)Unsafe.AsPointer(ref this) + 0);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Item(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 4);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 4 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Quantity() {
        return *(int*)((nuint)Unsafe.AsPointer(ref this) + 12);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int LotNumber() {
        return *(int*)((nuint)Unsafe.AsPointer(ref this) + 16);
    }
}
    
[StructLayout(LayoutKind.Sequential, Pack=1, Size=109)]
public unsafe struct UserViewer {
    private readonly ulong _0;
    private readonly ulong _1;
    private readonly ulong _2;
    private readonly ulong _3;
    private readonly ulong _4;
    private readonly ulong _5;
    private readonly ulong _6;
    private readonly ulong _7;
    private readonly ulong _8;
    private readonly ulong _9;
    private readonly ulong _10;
    private readonly ulong _11;
    private readonly ulong _12;
    private readonly uint _13;
    private readonly byte _14;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref UserViewer NewUserViewer(Karmem.Reader reader, uint offset) {
        if (!reader.IsValidOffset(offset, 109)) {
            return ref *(UserViewer*)(nuint)_Globals.Null();
        }
        ref UserViewer v = ref *(UserViewer*)(reader.MemoryPointer + offset);
        return ref v;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private uint KarmemSizeOf() {
        return 109;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int Id() {
        return *(int*)((nuint)Unsafe.AsPointer(ref this) + 0);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string FirstName(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 4);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 4 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string LastName(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 12);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 12 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string FullName(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 20);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 20 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string UserName(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 28);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 28 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Email(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 36);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 36 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string SomethingUnique(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 44);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 44 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> SomeGuid() {
        return new ReadOnlySpan<byte>((void*)((nuint)Unsafe.AsPointer(ref this) + 52), 16);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Avatar(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 68);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 68 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<byte> CartId() {
        return new ReadOnlySpan<byte>((void*)((nuint)Unsafe.AsPointer(ref this) + 76), 16);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string SSN(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 92);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 92 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return "";
        }
        var length = size / 1;
        return Marshal.PtrToStringUTF8((IntPtr)(reader.Memory.ToInt64() + offset), (int)length);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Gender Gender() {
        return *(Gender*)((nuint)Unsafe.AsPointer(ref this) + 100);
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<OrderViewer> Orders(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 101);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 101 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return new ReadOnlySpan<OrderViewer>();
        }
        var length = size / 20;
        return new ReadOnlySpan<OrderViewer>((void*)(reader.MemoryPointer + offset), (int)length);
    }
}
    
[StructLayout(LayoutKind.Sequential, Pack=1, Size=8)]
public unsafe struct UserWrapperViewer {
    private readonly ulong _0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref UserWrapperViewer NewUserWrapperViewer(Karmem.Reader reader, uint offset) {
        if (!reader.IsValidOffset(offset, 8)) {
            return ref *(UserWrapperViewer*)(nuint)_Globals.Null();
        }
        ref UserWrapperViewer v = ref *(UserWrapperViewer*)(reader.MemoryPointer + offset);
        return ref v;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private uint KarmemSizeOf() {
        return 8;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ReadOnlySpan<UserViewer> Users(Karmem.Reader reader) {
        var offset = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 0);
        var size = *(uint*)((nuint)Unsafe.AsPointer(ref this) + 0 + 4);
        if (!reader.IsValidOffset(offset, size)) {
            return new ReadOnlySpan<UserViewer>();
        }
        var length = size / 109;
        return new ReadOnlySpan<UserViewer>((void*)(reader.MemoryPointer + offset), (int)length);
    }
}
    
