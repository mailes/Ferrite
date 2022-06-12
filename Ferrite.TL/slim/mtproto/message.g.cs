//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.CompilerServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly unsafe struct Message : ITLObjectReader, ITLSerializable
{
    private readonly byte* _buff;
    private readonly IMemoryOwner<byte>? _memoryOwner;
    private Message(Span<byte> buffer, IMemoryOwner<byte> memoryOwner)
    {
        _buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
        Length = buffer.Length;
        _memoryOwner = memoryOwner;
    }
    private Message(byte* buffer, in int length, IMemoryOwner<byte> memoryOwner)
    {
        _buff = buffer;
        Length = length;
        _memoryOwner = memoryOwner;
    }
    
    public int Length { get; }
    public ReadOnlySpan<byte> ToReadOnlySpan() => new (_buff, Length);
    public static ITLSerializable? Read(Span<byte> data, in int offset, out int bytesRead)
    {
        bytesRead = GetOffset(5, (byte*)Unsafe.AsPointer(ref data[offset..][0]), data.Length);
        var obj = new Message(data.Slice(offset, bytesRead), null);
        return obj;
    }
    public static ITLSerializable? Read(byte* buffer, in int length, in int offset, out int bytesRead)
    {
        bytesRead = GetOffset(5, buffer + offset, length);
        var obj = new Message(buffer + offset, bytesRead, null);
        return obj;
    }

    public static int GetRequiredBufferSize(int len_body)
    {
        return 8 + 4 + 4 + len_body;
    }
    public static Message Create(long msg_id, int seqno, int bytes, ITLSerializable body, MemoryPool<byte>? pool = null)
    {
        var length = GetRequiredBufferSize(body.Length);
        var memory = pool != null ? pool.Rent(length) : MemoryPool<byte>.Shared.Rent(length);
        var obj = new Message(memory.Memory.Span[..length], memory);
        obj.Set_msg_id(msg_id);
        obj.Set_seqno(seqno);
        obj.Set_bytes(bytes);
        obj.Set_body(body.ToReadOnlySpan());
        return obj;
    }
    public static int ReadSize(Span<byte> data, in int offset)
    {
        return GetOffset(5, (byte*)Unsafe.AsPointer(ref data[offset..][0]), data.Length);
    }

    public static int ReadSize(byte* buffer, in int length, in int offset)
    {
        return GetOffset(5, buffer + offset, length);
    }
    public ref readonly long msg_id => ref *(long*)(_buff + GetOffset(1, _buff, Length));
    private void Set_msg_id(in long value)
    {
        var p = (long*)(_buff + GetOffset(1, _buff, Length));
        *p = value;
    }
    public ref readonly int seqno => ref *(int*)(_buff + GetOffset(2, _buff, Length));
    private void Set_seqno(in int value)
    {
        var p = (int*)(_buff + GetOffset(2, _buff, Length));
        *p = value;
    }
    public ref readonly int bytes => ref *(int*)(_buff + GetOffset(3, _buff, Length));
    private void Set_bytes(in int value)
    {
        var p = (int*)(_buff + GetOffset(3, _buff, Length));
        *p = value;
    }
    public ITLSerializable body => BoxedObject.Read(_buff, Length, GetOffset(4, _buff, Length), out var bytesRead);
    private void Set_body(ReadOnlySpan<byte> value)
    {
        fixed (byte* p = value)
        {
            int offset = GetOffset(4, _buff, Length);
            Buffer.MemoryCopy(p, _buff + offset,
                Length - offset, value.Length);
        }
    }
    private static int GetOffset(int index, byte* buffer, int length)
    {
        int offset = 0;
        if(index >= 2) offset += 8;
        if(index >= 3) offset += 4;
        if(index >= 4) offset += 4;
        if(index >= 5) offset += BoxedObject.ReadSize(buffer, length, offset);
        return offset;
    }
    public void Dispose()
    {
        _memoryOwner?.Dispose();
    }
}
