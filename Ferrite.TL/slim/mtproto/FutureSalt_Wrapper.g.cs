//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.CompilerServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly unsafe struct FutureSalt : ITLObjectReader, ITLSerializable
{
    private readonly byte* _buff;
    private readonly IMemoryOwner<byte>? _memoryOwner;
    private FutureSalt(Span<byte> buffer, IMemoryOwner<byte> memoryOwner)
    {
        _buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
        Length = buffer.Length;
        _memoryOwner = memoryOwner;
    }
    internal FutureSalt(byte* buffer, in int length, IMemoryOwner<byte> memoryOwner)
    {
        _buff = buffer;
        Length = length;
        _memoryOwner = memoryOwner;
    }
    public int Length { get; }
    public ReadOnlySpan<byte> ToReadOnlySpan() => new (_buff, Length);
    public ref readonly int Constructor => ref *(int*)_buff;
    public static ITLSerializable? Read(Span<byte> data, in int offset, out int bytesRead)
    {
        var ptr = (byte*)Unsafe.AsPointer(ref data[offset..][0]);

        if(*(int*)ptr == unchecked((int)0x0949d9dc))
        {
            return future_salt.Read(data, offset, out bytesRead);
        }
        bytesRead = 0;
        return null;
    }

    public static unsafe ITLSerializable? Read(byte* buffer, in int length, in int offset, out int bytesRead)
    {

        if(*(int*)buffer == unchecked((int)0x0949d9dc))
        {
            return future_salt.Read(buffer, length, offset, out bytesRead);
        }
        bytesRead = 0;
        return null;
    }

    public static int ReadSize(Span<byte> data, in int offset)
    {
        var ptr = (byte*)Unsafe.AsPointer(ref data[offset..][0]);

        if(*(int*)ptr == unchecked((int)0x0949d9dc))
        {
            return future_salt.ReadSize(data, offset);
        }
        return 0;
    }

    public static unsafe int ReadSize(byte* buffer, in int length, in int offset)
    {

        if(*(int*)buffer == unchecked((int)0x0949d9dc))
        {
            return future_salt.ReadSize(buffer, length, offset);
        }
        return 0;
    }
    public void Dispose()
    {
        _memoryOwner?.Dispose();
    }
}
