//  <auto-generated>
//  This file was auto-generated by the Ferrite TL Generator.
//  Please do not modify as all changes will be lost.
//  <auto-generated/>

#nullable enable

using System.Buffers;
using System.Runtime.CompilerServices;
using Ferrite.Utils;

namespace Ferrite.TL.slim.mtproto;

public readonly unsafe struct P_Q_inner_data : ITLObjectReader, ITLSerializable
{
    private readonly byte* _buff;
    private readonly IMemoryOwner<byte>? _memoryOwner;
    private P_Q_inner_data(Span<byte> buffer, IMemoryOwner<byte> memoryOwner)
    {
        _buff = (byte*)Unsafe.AsPointer(ref buffer[0]);
        Length = buffer.Length;
        _memoryOwner = memoryOwner;
    }
    internal P_Q_inner_data(byte* buffer, in int length, IMemoryOwner<byte> memoryOwner)
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

        if(*(int*)ptr == unchecked((int)0x83c95aec))
        {
            return p_q_inner_data.Read(data, offset, out bytesRead);
        }
        else if(*(int*)ptr == unchecked((int)0x3c6a84d4))
        {
            return p_q_inner_data_temp.Read(data, offset, out bytesRead);
        }
        else if(*(int*)ptr == unchecked((int)0xa9f55f95))
        {
            return p_q_inner_data_dc.Read(data, offset, out bytesRead);
        }
        else if(*(int*)ptr == unchecked((int)0x56fddf88))
        {
            return p_q_inner_data_temp_dc.Read(data, offset, out bytesRead);
        }
        bytesRead = 0;
        return null;
    }

    public static unsafe ITLSerializable? Read(byte* buffer, in int length, in int offset, out int bytesRead)
    {

        if(*(int*)buffer == unchecked((int)0x83c95aec))
        {
            return p_q_inner_data.Read(buffer, length, offset, out bytesRead);
        }
        else if(*(int*)buffer == unchecked((int)0x3c6a84d4))
        {
            return p_q_inner_data_temp.Read(buffer, length, offset, out bytesRead);
        }
        else if(*(int*)buffer == unchecked((int)0xa9f55f95))
        {
            return p_q_inner_data_dc.Read(buffer, length, offset, out bytesRead);
        }
        else if(*(int*)buffer == unchecked((int)0x56fddf88))
        {
            return p_q_inner_data_temp_dc.Read(buffer, length, offset, out bytesRead);
        }
        bytesRead = 0;
        return null;
    }

    public static int ReadSize(Span<byte> data, in int offset)
    {
        var ptr = (byte*)Unsafe.AsPointer(ref data[offset..][0]);

        if(*(int*)ptr == unchecked((int)0x83c95aec))
        {
            return p_q_inner_data.ReadSize(data, offset);
        }
        else if(*(int*)ptr == unchecked((int)0x3c6a84d4))
        {
            return p_q_inner_data_temp.ReadSize(data, offset);
        }
        else if(*(int*)ptr == unchecked((int)0xa9f55f95))
        {
            return p_q_inner_data_dc.ReadSize(data, offset);
        }
        else if(*(int*)ptr == unchecked((int)0x56fddf88))
        {
            return p_q_inner_data_temp_dc.ReadSize(data, offset);
        }
        return 0;
    }

    public static unsafe int ReadSize(byte* buffer, in int length, in int offset)
    {

        if(*(int*)buffer == unchecked((int)0x83c95aec))
        {
            return p_q_inner_data.ReadSize(buffer, length, offset);
        }
        else if(*(int*)buffer == unchecked((int)0x3c6a84d4))
        {
            return p_q_inner_data_temp.ReadSize(buffer, length, offset);
        }
        else if(*(int*)buffer == unchecked((int)0xa9f55f95))
        {
            return p_q_inner_data_dc.ReadSize(buffer, length, offset);
        }
        else if(*(int*)buffer == unchecked((int)0x56fddf88))
        {
            return p_q_inner_data_temp_dc.ReadSize(buffer, length, offset);
        }
        return 0;
    }
    public void Dispose()
    {
        _memoryOwner?.Dispose();
    }
}
