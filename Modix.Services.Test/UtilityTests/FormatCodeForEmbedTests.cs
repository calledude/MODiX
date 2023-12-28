using Modix.Services.Utilities;
using NUnit.Framework;
using Shouldly;

namespace Modix.Services.Test.UtilityTests
{
    [TestFixture]
    public class FormatCodeForEmbedTests
    {
        [Test]
        public void TestCSharp()
        {
            const string Source =
@"#nullable enable

var c = new C();
c.M(c = null, $"");
c.ToString();

class C
{
    public void M(object? o1, [InterpolatedStringHandlerArgument("")] CustomHandler c) => throw null!;
}

[InterpolatedStringHandler]
struct CustomHandler
{
    public CustomHandler(int literalLength, int formattedCount, [NotNull] C? o){}
}
";

            const string Expected =
@"```cs
#nullable enable
var c = new C();
c.M(c = null, $"");
c.ToString();
class C {
    public void M(object? o1, [InterpolatedStringHandlerAr...
}
[InterpolatedStringHandler]
struct CustomHandler {
    public CustomHandler(int literalLength, int formattedC...
}
```";

            Verify("cs", Source, Expected);
        }

        [Test]
        public void TestVisualBasic()
        {
            const string Source =
@"Imports System
Imports System.Threading.Tasks
Public Class C
    Public Sub M()
        Console.WriteLine(""something"")
        Console.WriteLine(""Hello this is a long line of test to test the truncation feature"")
        Console.WriteLine(""something"")
    End Sub


    Public Async Function SomeFunction(arg As Double) As Task
        Console.WriteLine(""something"")
        Await Task.Delay(arg)
        Console.WriteLine(""something"")
    End Function
End Class
";

            const string Expected =
@"```vb
Imports System
Imports System.Threading.Tasks
Public Class C
    Public Sub M()
        Console.WriteLine(""something"")
        Console.WriteLine(""Hello this is a long line of te...
        Console.WriteLine(""something"")
    End Sub
    Public Async Function SomeFunction(arg As Double) As T...
        Console.WriteLine(""something"")
' 4 more lines. Follow the link to view.
```";

            Verify("vb", Source, Expected);
        }

        [Test]
        public void TestFSharp()
        {
            const string Source =
@"open System

let printMessage name =
    printfn $""Hello there, {name}!""

let printNames names =
    for name in names do
        printMessage name

let names = [ ""Ana""; ""Felipe""; ""Emillia"" ]
printNames names
";

            const string Expected =
@"```fs
open System
let printMessage name =
    printfn $""Hello there, {name}!""
let printNames names =
    for name in names do
        printMessage name
let names = [ ""Ana""; ""Felipe""; ""Emillia"" ]
printNames names
```";

            Verify("fs", Source, Expected);
        }

        [Test]
        public void TestIL()
        {
            const string Source =
@".assembly A
{
}

.class public auto ansi abstract sealed beforefieldinit C
    extends System.Object
{
    .method public hidebysig static
        void M () cil managed
    {
        .maxstack 8

        ret
    }
}
";

            const string Expected =
@"```il
.assembly A {
}
.class public auto ansi abstract sealed beforefieldinit C
    extends System.Object {
    .method public hidebysig static
        void M () cil managed {
        .maxstack 8
        ret
    }
}
```";

            Verify("il", Source, Expected);
        }

        [Test]
        public void TestCSharpWithMaxLength()
        {
            const string Source =
@"
public class C {
    uint[] s_crcTable = new uint[256];
        
    public uint Crc32C(uint crc, uint data)
    {
        Span<byte> bytes = stackalloc byte[sizeof(uint)];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(bytes), data);

        foreach (byte b in bytes)
        {
            int tableIndex = (int)((crc ^ b) & 0xFF);
            crc = s_crcTable[tableIndex] ^ (crc >> 8);
        }

        return crc;
    }
    
    public uint Crc32C2(uint crc, uint data)
    {
        if (!BitConverter.IsLittleEndian)
            data = BinaryPrimitives.ReverseEndianness(data);

        ref uint lut = ref MemoryMarshal.GetArrayDataReference(s_crcTable);
        
        for (int i = 0; i < sizeof(uint); i++)
        {
            crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
            data >>= 8;
        }

        return crc;
    }

    public uint Crc32C3(uint crc, uint data)
    {
        if (!BitConverter.IsLittleEndian)
            data = BinaryPrimitives.ReverseEndianness(data);

        ref uint lut = ref MemoryMarshal.GetArrayDataReference(s_crcTable);
        
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
        data >>= 8;
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
        data >>= 8;
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
        data >>= 8;
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ data)) ^ (crc >> 8);

        return crc;
    }

    public uint Crc32C4(uint crc, uint data)
    {
        if (!BitConverter.IsLittleEndian)
            data = BinaryPrimitives.ReverseEndianness(data);

        ref uint lut = ref MemoryMarshal.GetArrayDataReference(s_crcTable);
        
        return Crc32CImpl(ref lut, crc, data);
    }

    public uint Crc32C4(uint crc, ulong data)
    {
        if (!BitConverter.IsLittleEndian)
            data = BinaryPrimitives.ReverseEndianness(data);

        ref uint lut = ref MemoryMarshal.GetArrayDataReference(s_crcTable);
        
        crc = Crc32CImpl(ref lut, crc, (uint)data);
        data >>= 32;
        crc = Crc32CImpl(ref lut, crc, (uint)data);
        
        return crc;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static uint Crc32CImpl(ref uint lut, uint crc, uint data)
    {
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
        data >>= 8;
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
        data >>= 8;
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ (byte)data)) ^ (crc >> 8);
        data >>= 8;
        crc = Unsafe.Add(ref lut, (nuint)(byte)(crc ^ data)) ^ (crc >> 8);
        
        return crc;
    }
}
";

            const string Expected =
@"```cs
public class C {
    uint[] s_crcTable = new uint[256];
    public uint Crc32C(uint crc, uint data) {
        Span<byte> bytes = stackalloc byte[sizeof(uint)];
        Unsafe.WriteUnaligned(ref MemoryMarshal.GetReferen...
// 63 more lines. Follow the link to view.
```";

            VerifyWithLength("cs", 293, Source, Expected);
        }

        private static void Verify(string language, string source, string expected)
        {
            var actual = FormatUtilities.FormatCodeForEmbed(language, source, 2048);
            actual.ShouldBe(expected.Replace("\r", string.Empty));
        }

        private static void VerifyWithLength(string language, int length, string source, string expected)
        {
            var actual = FormatUtilities.FormatCodeForEmbed(language, source, length);
            actual.ShouldBe(expected.Replace("\r", string.Empty));
        }
    }
}
