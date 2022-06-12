//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.CompilerServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly unsafe struct msgs_all_info : ITLObjectReader, ITLSerializable
{
    private readonly byte* _buff;
    private readonly IMemoryOwner<byte>? _memoryOwner;
    private msgs_all_info(Span<byte> buffer, IMemoryOwner<byte> memoryOwner)
    {
        _buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
        Length = buffer.Length;
        _memoryOwner = memoryOwner;
    }
    private msgs_all_info(byte* buffer, in int length, IMemoryOwner<byte> memoryOwner)
    {
        _buff = buffer;
        Length = length;
        _memoryOwner = memoryOwner;
    }
    
    public MsgsAllInfo GetAsMsgsAllInfo()
    {
        return new MsgsAllInfo(_buff, Length, _memoryOwner);
    }
    public ref readonly int Constructor => ref *(int*)_buff;

    private void SetConstructor(int constructor)
    {
        var p = (int*)_buff;
        *p = constructor;
    }
    public int Length { get; }
    public ReadOnlySpan<byte> ToReadOnlySpan() => new (_buff, Length);
    public static ITLSerializable? Read(Span<byte> data, in int offset, out int bytesRead)
    {
        bytesRead = GetOffset(3, (byte*)Unsafe.AsPointer(ref data[offset..][0]), data.Length);
        var obj = new msgs_all_info(data.Slice(offset, bytesRead), null);
        return obj;
    }
    public static ITLSerializable? Read(byte* buffer, in int length, in int offset, out int bytesRead)
    {
        bytesRead = GetOffset(3, buffer + offset, length);
        var obj = new msgs_all_info(buffer + offset, bytesRead, null);
        return obj;
    }

    public static int GetRequiredBufferSize(int len_msg_ids, int len_info)
    {
        return 4 + len_msg_ids + BufferUtils.CalculateTLBytesLength(len_info);
    }
    public static msgs_all_info Create(VectorOfLong msg_ids, ReadOnlySpan<byte> info, MemoryPool<byte>? pool = null)
    {
        var length = GetRequiredBufferSize(msg_ids.Length, info.Length);
        var memory = pool != null ? pool.Rent(length) : MemoryPool<byte>.Shared.Rent(length);
        var obj = new msgs_all_info(memory.Memory.Span[..length], memory);
        obj.SetConstructor(unchecked((int)0x8cc0d131));
        obj.Set_msg_ids(msg_ids.ToReadOnlySpan());
        obj.Set_info(info);
        return obj;
    }
    public static int ReadSize(Span<byte> data, in int offset)
    {
        return GetOffset(3, (byte*)Unsafe.AsPointer(ref data[offset..][0]), data.Length);
    }

    public static int ReadSize(byte* buffer, in int length, in int offset)
    {
        return GetOffset(3, buffer + offset, length);
    }
    public VectorOfLong msg_ids => (VectorOfLong)VectorOfLong.Read(_buff, Length, GetOffset(1, _buff, Length), out var bytesRead);
    private void Set_msg_ids(ReadOnlySpan<byte> value)
    {
        fixed (byte* p = value)
        {
            int offset = GetOffset(1, _buff, Length);
            Buffer.MemoryCopy(p, _buff + offset,
                Length - offset, value.Length);
        }
    }
    public ReadOnlySpan<byte> info => BufferUtils.GetTLBytes(_buff, GetOffset(2, _buff, Length), Length);
    private void Set_info(ReadOnlySpan<byte> value)
    {
        if(value.Length == 0)
        {
            return;
        }
        var offset = GetOffset(2, _buff, Length);
        var lenBytes = BufferUtils.WriteLenBytes(_buff, value, offset, Length);
        fixed (byte* p = value)
        {
            Buffer.MemoryCopy(p, _buff + offset + lenBytes,
                Length - offset, value.Length);
        }
    }
    private static int GetOffset(int index, byte* buffer, int length)
    {
        int offset = 4;
        if(index >= 2) offset += VectorOfLong.ReadSize(buffer, length, offset);
        if(index >= 3) offset += BufferUtils.GetTLBytesLength(buffer, offset, length);
        return offset;
    }
    public void Dispose()
    {
        _memoryOwner?.Dispose();
    }
}
