//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.CompilerServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly unsafe struct destroy_session : ITLObjectReader, ITLSerializable
{
    private readonly byte* _buff;
    private readonly IMemoryOwner<byte>? _memoryOwner;
    private destroy_session(Span<byte> buffer, IMemoryOwner<byte> memoryOwner)
    {
        _buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
        Length = buffer.Length;
        _memoryOwner = memoryOwner;
    }
    private destroy_session(byte* buffer, in int length, IMemoryOwner<byte> memoryOwner)
    {
        _buff = buffer;
        Length = length;
        _memoryOwner = memoryOwner;
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
        bytesRead = GetOffset(2, (byte*)Unsafe.AsPointer(ref data[offset..][0]), data.Length);
        var obj = new destroy_session(data.Slice(offset, bytesRead), null);
        return obj;
    }
    public static ITLSerializable? Read(byte* buffer, in int length, in int offset, out int bytesRead)
    {
        bytesRead = GetOffset(2, buffer + offset, length);
        var obj = new destroy_session(buffer + offset, bytesRead, null);
        return obj;
    }

    public static int GetRequiredBufferSize()
    {
        return 4 + 8;
    }
    public static destroy_session Create(long session_id, MemoryPool<byte>? pool = null)
    {
        var length = GetRequiredBufferSize();
        var memory = pool != null ? pool.Rent(length) : MemoryPool<byte>.Shared.Rent(length);
        var obj = new destroy_session(memory.Memory.Span[..length], memory);
        obj.SetConstructor(unchecked((int)0xe7512126));
        obj.Set_session_id(session_id);
        return obj;
    }
    public static int ReadSize(Span<byte> data, in int offset)
    {
        return GetOffset(2, (byte*)Unsafe.AsPointer(ref data[offset..][0]), data.Length);
    }

    public static int ReadSize(byte* buffer, in int length, in int offset)
    {
        return GetOffset(2, buffer + offset, length);
    }
    public ref readonly long session_id => ref *(long*)(_buff + GetOffset(1, _buff, Length));
    private void Set_session_id(in long value)
    {
        var p = (long*)(_buff + GetOffset(1, _buff, Length));
        *p = value;
    }
    private static int GetOffset(int index, byte* buffer, int length)
    {
        int offset = 4;
        if(index >= 2) offset += 8;
        return offset;
    }
    public void Dispose()
    {
        _memoryOwner?.Dispose();
    }
}
