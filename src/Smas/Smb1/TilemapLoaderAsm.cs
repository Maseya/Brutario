// <copyright file="TilemapLoaderAsm.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;
using System;

using Maseya.Smas.Smb1.AreaData;
using Maseya.Snes;

public class TilemapLoaderAsm
{
    public TilemapLoaderAsm(Rom rom)
    {
        Rom = rom;
        RAM_7ED000 = new byte[0xD0000];
        RAM_7E2000 = new byte[0x100000];
        GFX = new byte[0x10000];
    }

    private Rom Rom
    {
        get;
    }

    private ushort RAM_7E0000;

    /// <summary>
    /// Relative index to tilemap command data.
    /// </summary>
    private ushort RAM_7E0002;

    private ushort RAM_7E0004;

    /// <summary>
    /// Area Type
    /// </summary>
    private ushort RAM_7E005C;

    /// <summary>
    /// Tileset index for tileset upload routine?
    /// </summary>
    private ushort RAM_7E0099;

    /// <summary>
    /// Area Index/Current background
    /// </summary>
    private ushort RAM_7E00DB;

    private ushort RAM_7E00E2;

    private ushort RAM_7E00E4;

    private ushort RAM_7E00E6;

    private ushort RAM_7E00E8;

    private ushort RAM_7E00EB;

    private ushort RAM_7E00ED;

    private ushort RAM_7E00EF;

    private ushort RAM_7E00F1;

    private byte RAM_7E02F8;

    private byte[] GFX;

    /// <summary>
    /// Mirror of DMA 4302 (Source Address)
    /// </summary>
    private ushort RAM_7E0285;

    /// <summary>
    /// Mirror of DMA 4304 (Source Address Bank)
    /// </summary>
    private byte RAM_7E0287;

    /// <summary>
    /// Mirror of DMA 4305 (Number of bytes to transfer)
    /// </summary>
    private ushort RAM_7E0288;

    /// <summary>
    /// Mirror of DMA 2116 (Address for VRAM Read/Write).
    /// </summary>
    private ushort RAM_7E028A;

    private byte RAM_7E028C;

    private byte RAM_7E028D;

    private byte RAM_7E0753;

    /// <summary>
    /// Mirror of $DB?
    /// </summary>
    private ushort RAM_7E0E65;

    private ushort RAM_7E0EC0;

    private byte RAM_7E0ED1;

    /// <summary>
    /// Flag for layer 3 processing??
    /// </summary>
    private byte RAM_7E0EDC;

    /// <summary>
    /// Layer 2 background
    /// </summary>
    private byte[] RAM_7E2000;

    private ushort Get7E2000Word(int index)
    {
        unsafe
        {
            fixed (byte* ptr = RAM_7E2000)
            {
                return *(ushort*)(ptr + index);
            }
        }
    }

    private void Set7E2000Word(int index, ushort value)
    {
        unsafe
        {
            fixed (byte* ptr = RAM_7E2000)
            {
                *(ushort*)(ptr + index) = value;
            }
        }
    }

    private byte[] RAM_7ED000
    {
        get;
    }

    private ushort Get7ED000Word(int index)
    {
        unsafe
        {
            fixed (byte* ptr = RAM_7ED000)
            {
                return *(ushort*)(ptr + index);
            }
        }
    }

    private void Set7ED000Word(int index, ushort value)
    {
        unsafe
        {
            fixed (byte* ptr = RAM_7ED000)
            {
                *(ushort*)(ptr + index) = value;
            }
        }
    }

    public event EventHandler<TilesetEventArgs>? LoadTileset;

    public void LoadTilemap(AreaType areaType, int areaIndex, Player player)
    {
        RAM_7E0753 = (byte)player;
        RAM_7E005C = (ushort)areaType;
        RAM_7E00DB = (ushort)areaIndex;
        unsafe
        {
            fixed (byte* ptr = RAM_7E2000)
            {
                var bg = (ObjTile*)ptr;
                for (var i = 0; i < RAM_7E2000.Length >> 1; i++)
                {
                    bg[i] = 0x1024;
                }
            }
        }

        FUNC_058000();
    }

    public void WriteTilemap(ObjTile[] dest)
    {
        const int width = 0x10;
        const int height = 0x10;
        const int screens = 0x20;
        var srcScreens = RAM_7E0EC0;
        for (var screen = 0; screen < screens; screen++)
        {
            var srcScreen = screen * 0x800;
            for (var y = 0; y < height; y++)
            {
                var srcRow = srcScreen + (y * 0x80);
                var destRow = (y * width * screens) << 2;
                for (var x = 0; x < width; x++)
                {
                    var srcX = srcRow + (x << 2);
                    var destX = ((screen * width) + x) << 1;

                    dest[destRow + destX] = Get7E2000Word(srcX + 0x00);
                    dest[destRow + destX + 1] = Get7E2000Word(srcX + 0x02);
                    dest[destRow + ((screens * width) << 1) + destX] = Get7E2000Word(srcX + 0x40);
                    dest[destRow + ((screens * width) << 1) + destX + 1] = Get7E2000Word(srcX + 0x42);
                }
            }
        }
    }

    private void FUNC_058000()
    {
        RAM_7E0EDC = 0;
        Array.Clear(RAM_7ED000);
        RAM_7E0EC0 = 0;
        if ((byte)RAM_7E0E65 != 0)
        {
            RAM_7E00DB = (byte)RAM_7E0E65;
        }

        RAM_7E0002 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x058057,
            RAM_7E00DB << 1);

        CODE_05805B();
    }

    private void CODE_05805B()
    {
    // This label seems to be the master return point. So, it should be its
    // function. Any jump or branch to this label should be considered a
    // return to the loop.
    CODE_05805B:
        RAM_7E0004 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x58060,
            RAM_7E0002);

        RAM_7E0002 += 2;

        RAM_7E00EF = (ushort)((RAM_7E0004 & 0x03F0) >> 4);
        RAM_7E00F1 = (ushort)(RAM_7E0004 & 0x000F);
        RAM_7E00ED = (ushort)(RAM_7E0004 & 0xE000);
        RAM_7E00ED |= (ushort)((RAM_7E0004 >> 1) & 0x0E00);
        RAM_7E00ED = (ushort)Rom.XBA(RAM_7E00ED);

        // Not a termination check, as CODE_0580B3 returns to CODE_05805B.
        if ((RAM_7E00ED & 0xF0) != 0xE0)
        {
            goto CODE_0580B3;
        }

        // Also not a termination check.
        if (RAM_7E00EF != 0x3F)
        {
            goto CODE_0580AE;
        }

        // This must be what causes the termination to happen. Once the layer2
        // tilemap buffer is done being written to, we add two more pages and
        // send the end command 0xFFFF.
        RAM_7E0EC0 += 2;
        Set7ED000Word(Rom.XBA(RAM_7E0EC0), 0xFFFF);

        // Therefore, the expectation is that this should execute outside of the
        // main loop. Everything from CODE_05805B to here can be considered a
        // check to terminate the loop.
        CODE_059166();
        return;

    CODE_0580AE:
        CODE_058F19();
        goto CODE_05805B;

    CODE_0580B3:
        RAM_7E00EB = (ushort)(Rom.XBA(RAM_7E0EC0) + RAM_7E00ED);
        if (RAM_7E00EF < 0x10)
        {
            goto CODE_0580C9;
        }

        CODE_058E85();
        goto CODE_05805B;

    CODE_0580C9:
        CODE_0580C9();

        // I could be wrong, and will need to check, but at a glance, every jump
        // will then jump back to CODE_05805B.
        goto CODE_05805B;
    }

    private void CODE_0580C9()
    {
        RAM_7E0004 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x580D0,
            RAM_7E00DB << 1);

        RAM_7E0000 = (ushort)Rom.ReadInt16Indexed(
            0x050000 | RAM_7E0004,
            RAM_7E00EF << 1);

        // We need to now do an indirect jump based on $0000. The commands here
        // all seem to be filling in the 7ED000 buffer.
        switch (RAM_7E0000)
        {
        case 0x823F:
            CODE_05823F();
            break;
        case 0x8244:
            CODE_058244();
            break;
        case 0x81F5:
            CODE_0581F5();
            break;
        case 0x82E2:
            CODE_0582E2();
            break;
        case 0x9004:
            CODE_059004();
            break;
        case 0x864E:
            CODE_05864E();
            break;
        case 0x864C:
            CODE_05864C();
            break;
        case 0x8643:
            CODE_058643();
            break;
        case 0x8639:
            CODE_058639();
            break;
        case 0x8724:
            CODE_058724();
            break;
        case 0x8726:
            CODE_058726();
            break;
        case 0x875E:
            CODE_05875E();
            break;
        case 0x8760:
            CODE_058760();
            break;
        case 0x8995:
            CODE_058995();
            break;
        case 0x86E9:
            CODE_0586E9();
            break;
        case 0x8845:
            CODE_058845();
            break;
        case 0x8843:
            CODE_058843();
            break;
        case 0x883A:
            CODE_05883A();
            break;
        case 0x8838:
            CODE_058838();
            break;
        case 0x87EA:
            CODE_0587EA();
            break;
        case 0x87F4:
            CODE_0587F4();
            break;
        case 0x8800:
            CODE_058800();
            break;
        case 0x880C:
            CODE_05880C();
            break;
        case 0x8818:
            CODE_058818();
            break;
        case 0x8824:
            CODE_058824();
            break;
        case 0x882C:
            CODE_05882C();
            break;
        case 0x848D:
            CODE_05848D();
            break;
        case 0x89E0:
            CODE_0589E0();
            break;
        case 0x89FD:
            CODE_0589FD();
            break;
        case 0x8A2E:
            CODE_058A2E();
            break;
        case 0x85EE:
            CODE_0585EE();
            break;
        case 0x85EC:
            CODE_0585EC();
            break;
        case 0x8B51:
            CODE_058B51();
            break;
        case 0x8B8F:
            CODE_058B8F();
            break;
        case 0x8C66:
            CODE_058C66();
            break;
        case 0x8CE0:
            CODE_058CE0();
            break;
        case 0x8D8A:
            CODE_058D8A();
            break;
        case 0x8B22:
            CODE_058B22();
            break;
        case 0x8A68:
            CODE_058A68();
            break;
        case 0x8AD5:
            CODE_058AD5();
            break;
        case 0x8DD3:
            CODE_058DD3();
            break;
        case 0x8DD1:
            CODE_058DD1();
            break;
        case 0x8DC5:
            CODE_058DC5();
            break;
        case 0x8699:
            CODE_058699();
            break;
        case 0x8445:
            CODE_058445();
            break;
        case 0x8443:
            CODE_058443();
            break;
        case 0x8439:
            CODE_058439();
            break;
        case 0x8437:
            CODE_058437();
            break;
        case 0x8432:
            CODE_058432();
            break;
        case 0x8430:
            CODE_058430();
            break;
        case 0x8426:
            CODE_058426();
            break;
        case 0x8424:
            CODE_058424();
            break;
        case 0x83E9:
            CODE_0583E9();
            break;
        case 0x83E7:
            CODE_0583E7();
            break;
        case 0x83DD:
            CODE_0583DD();
            break;
        case 0x83DB:
            CODE_0583DB();
            break;
        case 0x8307:
            CODE_058307();
            break;
        default:
            throw new NotImplementedException(
                $"Could not find function at $05:{RAM_7E0000:X4}.");
        }
    }

    private void CODE_0581F5()
    {
        var y = RAM_7E00F1;
        var x = RAM_7E00EB;
        var a = Rom.ReadByteIndirectIndexed(0x0581FC, y);
        RAM_7ED000[x + 0x00] = a;
        a = Rom.ReadByteIndirectIndexed(0x058203, y);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirectIndexed(0x05820A, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x058211, y);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirectIndexed(0x058218, y);
        RAM_7ED000[x + 0x40] = a;
    }

    private void CODE_05823F()
    {
        ushort y = 0x000E;
        CODE_058247(y);
    }

    private void CODE_058244()
    {
        ushort y = 0x0000;
        CODE_058247(y);
    }

    private void CODE_058247(ushort y)
    {
        var x = RAM_7E00EB;
        x++;
        var a = Rom.ReadByteIndirectIndexed(0x05824D, y);
        RAM_7ED000[x + 0x00] = a;
        a = Rom.ReadByteIndirectIndexed(0x058254, y);
        RAM_7ED000[x + 0x01] = a;
        a = Rom.ReadByteIndirectIndexed(0x05825B, y);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirectIndexed(0x058262, y);
        RAM_7ED000[x + 0x11] = a;
        a = Rom.ReadByteIndirectIndexed(0x058269, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x058270, y);
        RAM_7ED000[x + 0x21] = a;
        RAM_7E00E4 = 0x0030;

    CODE_05827C:
        x += RAM_7E00E4;
        if ((x & 0xFF) >= 0xD0)
        {
            goto CODE_0582AD;
        }

        a = Rom.ReadByteIndirectIndexed(0x05828A, y);
        RAM_7ED000[x + 0x00] = a;
        a = Rom.ReadByteIndirectIndexed(0x058291, y);
        RAM_7ED000[x + 0x01] = a;
        a = Rom.ReadByteIndirectIndexed(0x058298, y);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirectIndexed(0x05829F, y);
        RAM_7ED000[x + 0x11] = a;
        RAM_7E00E4 = 0x0020;
        goto CODE_05827C;

    CODE_0582AD:
        a = Rom.ReadByteIndirectIndexed(0x0582AE, y);
        RAM_7ED000[x + 0x00] = a;
        a = Rom.ReadByteIndirectIndexed(0x0582B5, y);
        RAM_7ED000[x + 0x01] = a;
        a = Rom.ReadByteIndirectIndexed(0x0582BC, y);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirectIndexed(0x0582C3, y);
        RAM_7ED000[x + 0x11] = a;
        a = Rom.ReadByteIndirectIndexed(0x0582CA, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x0582D1, y);
        RAM_7ED000[x + 0x21] = a;
    }

    private void CODE_0582E2()
    {
        var y = (ushort)(RAM_7E00F1 << 1);
        y = (ushort)Rom.ReadInt16IndirectIndexed(
            0x0582E7,
            y);
        var x = (ushort)(RAM_7E00EB + 0x10);

    CODE_0582F3:
        var a = Rom.ReadByteIndirectIndexed(
            0x0582F4,
            y);
        if (a == 0xFF)
        {
            goto CODE_058302;
        }

        RAM_7ED000[x] = a;
        y++;
        x++;
        goto CODE_0582F3;

    CODE_058302:
        return;
    }

    private void CODE_058307()
    {
        var x = RAM_7E00EB;
        RAM_7E00E6 = RAM_7E00F1;
        RAM_7E00E8 = 0;

    CODE_058311:
        RAM_7ED000[x + 0x00] = 0x09;
        RAM_7ED000[x + 0x10] = 0x0E;
        RAM_7ED000[x + 0x30] = 0x0E;
        RAM_7ED000[x + 0x50] = 0x0E;
        RAM_7ED000[x + 0x20] = 0x12;
        RAM_7ED000[x + 0x40] = 0x12;
        x++;
        if ((x & 0x000F) != 0)
        {
            goto CODE_058340;
        }

        x += 0x00F0;
        RAM_7E00E8++;

    CODE_058340:
        RAM_7E00E6--;
        if (RAM_7E00E6 >= 0x8000)
        {
            goto CODE_05837F;
        }

        RAM_7ED000[x + 0x00] = 0x09;
        RAM_7ED000[x + 0x10] = 0x0F;
        RAM_7ED000[x + 0x30] = 0x0F;
        RAM_7ED000[x + 0x50] = 0x0F;
        RAM_7ED000[x + 0x20] = 0x13;
        RAM_7ED000[x + 0x40] = 0x13;
        x++;
        if ((x & 0x000F) != 0)
        {
            goto CODE_058377;
        }

        x += 0x00F0;
        RAM_7E00E8++;

    CODE_058377:
        RAM_7E00E6--;
        if (RAM_7E00E6 < 0x8000)
        {
            goto CODE_058311;
        }

    CODE_05837F:
        x = RAM_7E00EB;
        var a = (byte)RAM_7E00F1;
        if (a < 4)
        {
            goto CODE_058389;
        }

        a = 4;

    CODE_058389:
        ushort y = a;
        a = Rom.ReadByteIndirectIndexed(
            0x05838B,
            y);
        RAM_7ED000[x - 1] = a;
        if (RAM_7E00E8 == 0)
        {
            goto CODE_05839D;
        }

        x += 0xF0;
    CODE_05839D:
        x += RAM_7E00F1;
        a = Rom.ReadByteIndirectIndexed(
            0x0583A5,
            y);
        RAM_7ED000[x + 1] = a;
    }

    private void CODE_0583DB()
    {
        RAM_7E00EB++;
        CODE_0583DD();
    }

    private void CODE_0583DD()
    {
        RAM_7E00EB += 0x10;
        CODE_0583E9();
    }

    private void CODE_0583E7()
    {
        RAM_7E00EB++;
        CODE_0583E9();
    }

    private void CODE_0583E9()
    {
        var x = RAM_7E00EB;
        var y = RAM_7E00F1;
        RAM_7E00F1 = (ushort)(Rom.ReadInt16IndirectIndexed(0x0583EF, y) & 0x00FF);
        y = (ushort)(Rom.ReadInt16IndirectIndexed(0x0583F7, y) & 0x00FF);

    CODE_0583FF:
        var a = Rom.ReadByteIndirectIndexed(0x058400, y);
        RAM_7ED000[x] = a;
        x++;
        y++;
        if ((x & 0x0F) != 0)
        {
            goto CODE_058417;
        }

        x += 0x0F0;
    CODE_058417:
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_0583FF;
        }

        return;
    }

    private void CODE_058424()
    {
        RAM_7E00EB++;
        CODE_058426();
    }

    private void CODE_058426()
    {
        RAM_7E00EB += 0x10;
        CODE_058432();
    }

    private void CODE_058430()
    {
        RAM_7E00EB++;
        CODE_058432();
    }

    private void CODE_058432()
    {
        ushort y = 0x0001;
        CODE_058448(y);
    }

    private void CODE_058437()
    {
        RAM_7E00EB++;
        CODE_058439();
    }

    private void CODE_058439()
    {
        RAM_7E00EB += 0x10;
        CODE_058445();
    }

    private void CODE_058443()
    {
        RAM_7E00EB++;
        CODE_058445();
    }

    private void CODE_058445()
    {
        ushort y = 0x0000;
        CODE_058448(y);
    }

    private void CODE_058448(ushort y)
    {
        var x = RAM_7E00EB;
        var a = Rom.ReadByteIndirectIndexed(0x05844D, y);
        RAM_7ED000[x] = a;
        x += 0x0010;
        RAM_7E00F1--;

    CODE_05845F:
        var a8 = RAM_7ED000[x];
        if (a8 != 0x0E)
        {
            goto CODE_05846B;
        }

        a8 = 0x0D;
        goto CODE_058476;

    CODE_05846B:
        if (a8 != 0x02)
        {
            goto CODE_058473;
        }

        a8 = 0x0B;
        goto CODE_058476;

    CODE_058473:
        a8 = Rom.ReadByteIndirectIndexed(0x058474, y);

    CODE_058476:
        RAM_7ED000[x] = a8;
        x += 0x0010;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_05845F;
        }

        return;
    }

    private void CODE_05848D()
    {
        var x = (ushort)(RAM_7E00EB + 0x0010);
        RAM_7ED000[x + 0x20] = 0x4E;
        var a = RAM_7ED000[x];
        if (a == 0)
        {
            goto CODE_0584A6;
        }

        a = 0x38;
        goto CODE_0584A8;

    CODE_0584A6:
        a = 0x34;

    CODE_0584A8:
        RAM_7ED000[x] = a;
        a = RAM_7ED000[x + 0x10];
        if (a == 0)
        {
            goto CODE_0584BA;
        }

        if (a == 0x12)
        {
            goto CODE_0584BA;
        }

        a = 0x44;
        goto CODE_0584BC;

    CODE_0584BA:
        a = 0x40;

    CODE_0584BC:
        RAM_7ED000[x + 0x10] = a;
        a = RAM_7ED000[x + 0x20];
        if (a == 0)
        {
            goto CODE_0584CE;
        }

        if (a == 0x12)
        {
            goto CODE_0584CE;
        }

        a = 0x4D;
        goto CODE_0584D0;

    CODE_0584CE:
        a = 0x40;

    CODE_0584D0:
        RAM_7ED000[x + 0x20] = a;
        x++;
        a = RAM_7ED000[x];
        if (a == 0)
        {
            goto CODE_0584F7;
        }

        if (a == 0x12)
        {
            goto CODE_0584F7;
        }

        if (a == 0x18)
        {
            goto CODE_0584F3;
        }

        if (a == 0x07)
        {
            goto CODE_0584F3;
        }

        if (a == 0x17)
        {
            goto CODE_0584EF;
        }

        a = 0x39;
        goto CODE_0584F9;

    CODE_0584EF:
        a = 0x3D;
        goto CODE_0584F9;

    CODE_0584F3:
        a = 0x36;
        goto CODE_0584F9;

    CODE_0584F7:
        a = 0x35;

    CODE_0584F9:
        RAM_7ED000[x + 0x00] = a;
        RAM_7ED000[x + 0x10] = 0x41;
        RAM_7ED000[x + 0x20] = 0x48;
        RAM_7ED000[x + 0x30] = 0x4E;
        x++;

    CODE_058510:
        RAM_7E00F1--;
        if (RAM_7E00F1 != 0)
        {
            goto CODE_058519;
        }

        goto CODE_058599;

    CODE_058519:
        a = RAM_7ED000[x];
        if (a != 0)
        {
            goto CODE_058539;
        }

        RAM_7ED000[x + 0x00] = 0x36;
        RAM_7ED000[x + 0x01] = 0x35;
        RAM_7ED000[x + 0x10] = 0x42;
        RAM_7ED000[x + 0x11] = 0x41;
        goto CODE_05856F;

    CODE_058539:
        if (a == 0x18)
        {
            goto CODE_058557;
        }

        RAM_7ED000[x + 0x00] = 0x3A;
        RAM_7ED000[x + 0x01] = 0x39;
        RAM_7ED000[x + 0x10] = 0x42;
        RAM_7ED000[x + 0x11] = 0x41;
        goto CODE_05856F;

    CODE_058557:
        RAM_7ED000[x + 0x00] = 0x36;
        RAM_7ED000[x + 0x01] = 0x35;
        RAM_7ED000[x + 0x10] = 0x42;
        RAM_7ED000[x + 0x11] = 0x41;

    CODE_05856F:
        RAM_7ED000[x + 0x20] = 0x47;
        RAM_7ED000[x + 0x21] = 0x48;
        RAM_7ED000[x + 0x30] = 0x4E;
        RAM_7ED000[x + 0x31] = 0x4E;
        x += 2;

        if ((x & 0x0F) != 0)
        {
            goto CODE_058510;
        }

        x += 0xF0;
        goto CODE_058510;

    CODE_058599:
        RAM_7ED000[x + 0x10] = 0x42;
        RAM_7ED000[x + 0x20] = 0x47;
        RAM_7ED000[x + 0x30] = 0x4E;
        RAM_7ED000[x + 0x31] = 0x4E;
        a = RAM_7ED000[x];
        if (a != 0)
        {
            goto CODE_0585CF;
        }

        RAM_7ED000[x + 0x00] = 0x36;
        RAM_7ED000[x + 0x01] = 0x37;
        RAM_7ED000[x + 0x11] = 0x43;
        RAM_7ED000[x + 0x21] = 0x49;
        goto CODE_0585E7;

    CODE_0585CF:
        RAM_7ED000[x + 0x00] = 0x3A;
        RAM_7ED000[x + 0x01] = 0x3B;
        RAM_7ED000[x + 0x11] = 0x45;
        RAM_7ED000[x + 0x21] = 0x4A;

    CODE_0585E7:
        return;
    }

    private void CODE_0585EC()
    {
        RAM_7E00EB++;
        CODE_0585EE();
    }

    private void CODE_0585EE()
    {
        var x = RAM_7E00EB;
        RAM_7E00E2 = x;
        var y = (ushort)Rom.ReadInt16IndirectIndexed(0x0585F7, RAM_7E00EF << 1);

    CODE_0585FC:
        var a = Rom.ReadByteIndirectIndexed(0x0585FD, y);
        if (a == 0x00)
        {
            goto CODE_058610;
        }

        if (a == 0xFF)
        {
            goto CODE_058634;
        }

        if (a == 0xFE)
        {
            goto CODE_058624;
        }

        a = Rom.ReadByteIndirectIndexed(0x05860A, y);
        RAM_7ED000[x] = a;

    CODE_058610:
        y++;
        x++;
        if ((x & 0x000F) != 0)
        {
            goto CODE_058620;
        }

        x += 0x00F0;

    CODE_058620:
        goto CODE_0585FC;

    CODE_058624:
        RAM_7E00E2 += 0x0010;
        x = RAM_7E00E2;
        y++;
        goto CODE_0585FC;

    CODE_058634:
        return;
    }

    private void CODE_058639()
    {
        var x = (ushort)(RAM_7E00EB + 0x10);
        x++;
        CODE_058650(x);
    }

    private void CODE_058643()
    {
        var x = (ushort)(RAM_7E00EB + 0x10);
        CODE_058650(x);
    }

    private void CODE_05864C()
    {
        RAM_7E00EB++;
        CODE_05864E();
    }

    private void CODE_05864E()
    {
        var x = RAM_7E00EB;
        CODE_058650(x);
    }

    private void CODE_058650(ushort x)
    {
        RAM_7E00E2 = x;
        var y = Rom.ReadInt16IndirectIndexed(
            0x058657,
            RAM_7E00EF << 1);

    CODE_05865C:
        var a = Rom.ReadByteIndirectIndexed(
            0x05865D,
            y);
        if (a == 0)
        {
            goto CODE_058670;
        }

        if (a == 0xFF)
        {
            goto CODE_058694;
        }

        if (a == 0xFE)
        {
            goto CODE_058684;
        }

        RAM_7ED000[x] = Rom.ReadByteIndirectIndexed(
            0x05866A,
            y);

    CODE_058670:
        y++;
        x++;
        if ((x & 0x000F) != 0)
        {
            goto CODE_058680;
        }

        x += 0x00F0;

    CODE_058680:
        goto CODE_05865C;

    CODE_058684:
        RAM_7E00E2 += 0x0010;
        x = RAM_7E00E2;
        y++;
        goto CODE_05865C;

    CODE_058694:
        return;
    }

    private void CODE_058699()
    {
        var x = RAM_7E00EB;
        RAM_7E00E2 = x;
        var y = RAM_7E00EF << 1;
        y = (ushort)Rom.ReadInt16IndirectIndexed(
            0x0586A2,
            y);

    CODE_0586A7:
        var a = Rom.ReadByteIndirectIndexed(
            0x0586A8,
            y);
        if (a == 0)
        {
            goto CODE_0586B8;
        }

        if (a == 0xFF)
        {
            goto CODE_0586E4;
        }

        if (a == 0xFE)
        {
            goto CODE_0586CC;
        }

        RAM_7ED000[x] = a;

    CODE_0586B8:
        y++;
        x++;
        if ((x & 0x000F) != 0)
        {
            goto CODE_0586C8;
        }

        x += 0x00F0;

    CODE_0586C8:
        goto CODE_0586A7;

    CODE_0586CC:
        RAM_7E00E2 += 0x0010;
        x = RAM_7E00E2;
        if ((x & 0x00F0) == 0x00F0)
        {
            goto CODE_0586E4;
        }

        y++;
        goto CODE_0586A7;

    CODE_0586E4:
        return;
    }

    private void CODE_0586E9()
    {
        RAM_7E00EF--;
        var y = (ushort)((((RAM_7E00EF & 0x0004) << 4) | RAM_7E00F1) << 2);
        var x = RAM_7E00EB;

        RAM_7ED000[x + 0x00] = Rom.ReadByteIndirectIndexed(
            0x0586FE,
            y);
        RAM_7ED000[x + 0x01] = Rom.ReadByteIndirectIndexed(
            0x058705,
            y);
        RAM_7ED000[x + 0x10] = Rom.ReadByteIndirectIndexed(
            0x05870C,
            y);
        RAM_7ED000[x + 0x11] = Rom.ReadByteIndirectIndexed(
            0x058713,
            y);
    }

    /// <summary>
    /// Never used?
    /// </summary>
    private void CODE_058724()
    {
        RAM_7E00EB++;
        CODE_058726();
    }

    private void CODE_058726()
    {
        var x = (ushort)(RAM_7E00EB + 0x0020);
        RAM_7ED000[x + 0x00] = Rom.ReadByteIndirect(0x058730);
        RAM_7ED000[x + 0x01] = Rom.ReadByteIndirect(0x058737);
        RAM_7ED000[x + 0x10] = Rom.ReadByteIndirect(0x05873E);
        RAM_7ED000[x + 0x11] = Rom.ReadByteIndirect(0x058745);
        RAM_7ED000[x + 0x20] = Rom.ReadByteIndirect(0x05874C);
        RAM_7ED000[x + 0x21] = Rom.ReadByteIndirect(0x058753);
    }

    /// <summary>
    /// Never used?
    /// </summary>
    private void CODE_05875E()
    {
        RAM_7E00EB++;
        CODE_058760();
    }

    private void CODE_058760()
    {
        var x = (ushort)(RAM_7E00EB + 0x0020);
        RAM_7ED000[x + 0x10] = Rom.ReadByteIndirect(0x05876A);
        RAM_7ED000[x + 0x11] = Rom.ReadByteIndirect(0x058771);
        RAM_7ED000[x + 0x20] = Rom.ReadByteIndirect(0x058778);
        RAM_7ED000[x + 0x21] = Rom.ReadByteIndirect(0x05877F);
    }

    private void CODE_0587EA()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x0587EB);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0002);
        CODE_05884A(x, y);
    }

    private void CODE_0587F4()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x0587F5);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0012);
        CODE_05884A(x, y);
    }

    private void CODE_058800()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x058801);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0001);
        CODE_05884A(x, y);
    }

    private void CODE_05880C()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x05880D);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0011);
        CODE_05884A(x, y);
    }

    private void CODE_058818()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x058819);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0020);
        CODE_05884A(x, y);
    }

    private void CODE_058824()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x05882D);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0001);
        CODE_05884A(x, y);
    }

    private void CODE_05882C()
    {
        var y = (ushort)Rom.ReadInt16Indirect(0x05882D);
        var a = RAM_7E00EB;
        var x = (ushort)(a + 0x0012);
        CODE_05884A(x, y);
    }

    private void CODE_058838()
    {
        RAM_7E00EB++;
        CODE_05883A();
    }

    private void CODE_05883A()
    {
        var x = (ushort)(RAM_7E00EB + 0x0010);
        CODE_058847(x);
    }

    private void CODE_058843()
    {
        RAM_7E00EB++;
        CODE_058845();
    }

    private void CODE_058845()
    {
        var x = RAM_7E00EB;
        CODE_058847(x);
    }

    private void CODE_058847(ushort x)
    {
        ushort y = 0;
        CODE_05884A(x, y);
    }

    private void CODE_05884A(ushort x, ushort y)
    {
        RAM_7E00E2 = x;

    CODE_05884E:
        var a = Rom.ReadByteIndirectIndexed(
            0x05884F,
            y);

        if (a == 0xFF)
        {
            goto CODE_0588C6;
        }

        if (a == 0xFE)
        {
            goto CODE_0588A1;
        }

        RAM_7ED000[x] = CODE_058874(a);
        y++;
        x++;
        if ((x & 0x000F) != 0)
        {
            goto CODE_058870;
        }

        x += 0xF0;

    CODE_058870:
        goto CODE_05884E;

        byte CODE_058874(byte a)
        {
            RAM_7E00E4 = a;
            if (a != 0x12)
            {
                goto CODE_05887D;
            }

            return CODE_0588CB();

        CODE_05887D:
            if (a != 0x03)
            {
                goto CODE_058884;
            }

            return CODE_0588F8();

        CODE_058884:
            if (a != 0x04)
            {
                goto CODE_05888B;
            }

            return CODE_058905();

        CODE_05888B:
            if (a != 0x05)
            {
                goto CODE_058892;
            }

            return CODE_05891A();

        CODE_058892:
            if (a != 0x18)
            {
                goto CODE_058899;
            }

            return CODE_05892F();

        CODE_058899:
            if (a != 0x07)
            {
                goto CODE_0588A0;
            }

            return CODE_05894C();

        CODE_0588A0:
            return a;
        }

    CODE_0588A1:
        y++;
        x = (ushort)(RAM_7E00E2 + 0x10);
        if ((x & 0x00F0) == 0)
        {
            goto CODE_0588C6;
        }

        x--;
        if ((x & 0x000F) != 0x000F)
        {
            goto CODE_0588C0;
        }

        x -= 0x00F0;

    CODE_0588C0:
        RAM_7E00E2 = x;
        goto CODE_05884E;

    CODE_0588C6:
        return;

        byte CODE_0588CB()
        {
            a = RAM_7ED000[x];
            if (a == 0)
            {
                goto CODE_0588F5;
            }

            if (a != 0x18)
            {
                goto CODE_0588D9;
            }

            a = 0x02;
            goto CODE_0588F7;

        CODE_0588D9:
            if (a != 0x06)
            {
                goto CODE_0588E1;
            }

            a = 0x11;
            goto CODE_0588F7;

        CODE_0588E1:
            if (a != 0x16)
            {
                goto CODE_0588E9;
            }

            a = 0x01;
            goto CODE_0588F7;

        CODE_0588E9:
            if (a != 0x05)
            {
                goto CODE_0588F1;
            }

            a = 0x62;
            goto CODE_0588F7;

        CODE_0588F1:
            a = 0x10;
            goto CODE_0588F7;

        CODE_0588F5:
            a = (byte)RAM_7E00E4;

        CODE_0588F7:
            return a;
        }

        byte CODE_0588F8()
        {
            a = RAM_7ED000[x];
            if (a == 0)
            {
                goto CODE_058902;
            }

            a = 0x36;
            goto CODE_058904;

        CODE_058902:
            a = (byte)RAM_7E00E4;

        CODE_058904:
            return a;
        }

        byte CODE_058905()
        {
            a = RAM_7ED000[x];
            if (a == 0)
            {
                goto CODE_058917;
            }

            if (a != 0x12)
            {
                goto CODE_058913;
            }

            a = 0x14;
            goto CODE_058919;

        CODE_058913:
            a = 0x37;
            goto CODE_058919;

        CODE_058917:
            a = (byte)RAM_7E00E4;

        CODE_058919:
            return a;
        }

        byte CODE_05891A()
        {
            a = RAM_7ED000[x];
            if (a == 0)
            {
                goto CODE_05892C;
            }

            if (a != 0x13)
            {
                goto CODE_058928;
            }

            a = 0x15;
            goto CODE_05892E;

        CODE_058928:
            a = 0x38;
            goto CODE_05892E;

        CODE_05892C:
            a = (byte)RAM_7E00E4;

        CODE_05892E:
            return a;
        }

        byte CODE_05892F()
        {
            a = RAM_7ED000[x];
            if (a == 0)
            {
                goto CODE_058949;
            }

            if (a != 0x16)
            {
                goto CODE_05893D;
            }

            a = 0x1E;
            goto CODE_05894B;

        CODE_05893D:
            if (a != 0x13)
            {
                goto CODE_058945;
            }

            a = 0x1E;
            goto CODE_05894B;

        CODE_058945:
            a = 0x1D;
            goto CODE_05894B;

        CODE_058949:
            a = (byte)RAM_7E00E4;

        CODE_05894B:
            return a;
        }

        byte CODE_05894C()
        {
            a = RAM_7ED000[x];
            if (a == 0)
            {
                goto CODE_05896E;
            }

            if (a != 0x16)
            {
                goto CODE_05895A;
            }

            a = 0x0E;
            goto CODE_058970;

        CODE_05895A:
            if (a != 0x04)
            {
                goto CODE_058962;
            }

            a = 0x70;
            goto CODE_058970;

        CODE_058962:
            if (a != 0x0B)
            {
                goto CODE_05896A;
            }

            a = 0x90;
            goto CODE_058970;

        CODE_05896A:
            a = 0x49;
            goto CODE_058970;

        CODE_05896E:
            a = (byte)RAM_7E00E4;

        CODE_058970:
            return a;
        }
    }

    private void CODE_058995()
    {
        var x = RAM_7E00EB;
        ushort y = 0x0000;

    CODE_05899C:
        var a = Rom.ReadByteIndirectIndexed(0x05899D, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x0589A4, y);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirectIndexed(0x0589AB, y);
        RAM_7ED000[x + 0x22] = a;
        a = Rom.ReadByteIndirectIndexed(0x0589B2, y);
        RAM_7ED000[x + 0x23] = a;
        a = Rom.ReadByteIndirectIndexed(0x0589B9, y);
        RAM_7ED000[x + 0x24] = a;
        a = Rom.ReadByteIndirectIndexed(0x0589C0, y);
        RAM_7ED000[x + 0x25] = a;
        x += 0x10;
        y += 6;
        if (y != 0x0024)
        {
            goto CODE_05899C;
        }
    }

    private void CODE_0589E0()
    {
        var x = RAM_7E00EB;

    CODE_0589E4:
        RAM_7ED000[x + 0x20] = 0x09;
        RAM_7ED000[x + 0x21] = 0x0B;
        x += 2;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_0589E4;
        }

        return;
    }

    private void CODE_0589FD()
    {
        var x = RAM_7E00EB;

    CODE_058A01:
        var a = RAM_7ED000[x + 0x20];
        if (a != 0x09)
        {
            goto CODE_058A0D;
        }

        a = 0x0A;
        goto CODE_058A0F;

    CODE_058A0D:
        a = 0x04;

    CODE_058A0F:
        RAM_7ED000[x + 0x20] = a;
        RAM_7ED000[x + 0x30] = 0x01;

        x += 0x0020;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_058A01;
        }

        return;
    }

    private void CODE_058A2E()
    {
        var x = RAM_7E00EB;

    CODE_058A32:
        var a = RAM_7ED000[x + 0x21];
        if (a != 0x0B)
        {
            goto CODE_058A3E;
        }

        a = 0x0A;
        goto CODE_058A40;

    CODE_058A3E:
        a = 0x04;

    CODE_058A40:
        RAM_7ED000[x + 0x21] = a;
        RAM_7ED000[x + 0x31] = 0x01;

        x += 0x0020;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_058A32;
        }

        return;
    }

    private void CODE_058A68()
    {
        var x = RAM_7E00EB;
        RAM_7E00F1 = 0x0006;
        var a = Rom.ReadByteIndirect(0x058A72);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058A79);
        RAM_7ED000[x + 0x21] = a;

    CODE_058A7F:
        a = Rom.ReadByteIndirect(0x058A80);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058A87);
        RAM_7ED000[x + 0x31] = a;
        x += 0x0010;
        RAM_7E00F1--;
        if (RAM_7E00F1 != 0)
        {
            goto CODE_058A7F;
        }

        a = Rom.ReadByteIndirect(0x058A9E);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058AA5);
        RAM_7ED000[x + 0x31] = a;
        a = Rom.ReadByteIndirect(0x058AAC);
        RAM_7ED000[x + 0x40] = a;
        a = Rom.ReadByteIndirect(0x058AB3);
        RAM_7ED000[x + 0x41] = a;
        a = Rom.ReadByteIndirect(0x058ABA);
        RAM_7ED000[x + 0x42] = a;
        return;
    }

    private void CODE_058AD5()
    {
        var x = RAM_7E00EB;
        x++;
        RAM_7E00E4 &= 0x00FF;
        ushort y = 0x0000;

    CODE_058ADF:
        var a = Rom.ReadByteIndirectIndexed(0x058AE0, y);
        RAM_7ED000[x + 0x00] = a;
        a = Rom.ReadByteIndirectIndexed(0x058AE7, y);
        RAM_7ED000[x + 0x01] = a;
        a = Rom.ReadByteIndirectIndexed(0x058AEE, y);
        RAM_7ED000[x + 0x02] = a;
        a = Rom.ReadByteIndirectIndexed(0x058AF5, y);
        RAM_7ED000[x + 0x03] = a;
        y += 4;
        x += 4;
        x += 0x000C;
        RAM_7E00E4 += 0x100;
        if (RAM_7E00E4 >> 8 != 0x04)
        {
            goto CODE_058ADF;
        }

        return;
    }

    private void CODE_058B22()
    {
        ushort y = 0x0000;
        var x = RAM_7E00EB;

    CODE_058B29:
        var a = Rom.ReadByteIndirectIndexed(0x058B2A, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x058B31, y);
        RAM_7ED000[x + 0x21] = a;
        x += 0x0010;
        y += 2;
        if (y != 0x0008)
        {
            goto CODE_058B29;
        }

        return;
    }

    private void CODE_058B51()
    {
        var x = RAM_7E00EB;
        var y = RAM_7E00EF;

    CODE_058B57:
        var a = Rom.ReadByteIndirectIndexed(0x058B58, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x058B5F, y);
        RAM_7ED000[x + 0x21] = a;
        x += 0x0010;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_058B57;
        }

        return;
    }

    private void CODE_058B8F()
    {
        var x = RAM_7E00EB;

    CODE_058B93:
        if ((x & 0xF0) == 0)
        {
            goto CODE_058BB7;
        }

        var a = RAM_7ED000[x + 0x20];
        if (a == 0x0C)
        {
            goto CODE_058BE3;
        }

        a = Rom.ReadByteIndirect(0x058BA1);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058BA8);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirect(0x058BAF);
        RAM_7ED000[x + 0x22] = a;
        goto CODE_058BCC;

    CODE_058BB7:
        a = Rom.ReadByteIndirect(0x058BB8);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058BBF);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirect(0x058BC6);
        RAM_7ED000[x + 0x22] = a;

    CODE_058BCC:
        a = Rom.ReadByteIndirect(0x058BCD);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058BD4);
        RAM_7ED000[x + 0x31] = a;
        a = Rom.ReadByteIndirect(0x058BDB);
        RAM_7ED000[x + 0x32] = a;
        goto CODE_058C43;

    CODE_058BE3:
        a = Rom.ReadByteIndirect(0x058BE4);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058BEB);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirect(0x058BF2);
        RAM_7ED000[x + 0x22] = a;
        a = Rom.ReadByteIndirect(0x058BF9);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058C00);
        RAM_7ED000[x + 0x31] = a;
        a = Rom.ReadByteIndirect(0x058C07);
        RAM_7ED000[x + 0x32] = a;
        a = Rom.ReadByteIndirect(0x058C0E);
        RAM_7ED000[x + 0x40] = a;
        a = Rom.ReadByteIndirect(0x058C15);
        RAM_7ED000[x + 0x41] = a;
        a = Rom.ReadByteIndirect(0x058C1C);
        RAM_7ED000[x + 0x42] = a;
        a = Rom.ReadByteIndirect(0x058C23);
        RAM_7ED000[x + 0x50] = a;
        a = Rom.ReadByteIndirect(0x058C2A);
        RAM_7ED000[x + 0x51] = a;
        a = Rom.ReadByteIndirect(0x058C31);
        RAM_7ED000[x + 0x52] = a;
        x += 0x0020;
        RAM_7E00F1--;

    CODE_058C43:
        x += 0x20;
        RAM_7E00F1--;
        if (RAM_7E00F1 >= 0x8000)
        {
            goto CODE_058C56;
        }

        goto CODE_058B93;

    CODE_058C56:
        return;
    }

    private void CODE_058C66()
    {
        var x = RAM_7E00EB;

    CODE_058C6A:
        var a = RAM_7ED000[x + 0x20];
        if (a != 0x1F)
        {
            goto CODE_058CAF;
        }

        a = Rom.ReadByteIndirect(0x058C73);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirect(0x058C7A);
        RAM_7ED000[x + 0x11] = a;
        a = Rom.ReadByteIndirect(0x058C81);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058C88);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirect(0x058C8F);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058C96);
        RAM_7ED000[x + 0x31] = a;
        a = Rom.ReadByteIndirect(0x058C9D);
        RAM_7ED000[x + 0x40] = a;
        a = Rom.ReadByteIndirect(0x058CA4);
        RAM_7ED000[x + 0x41] = a;
        x++;
        RAM_7E00F1--;
        goto CODE_058CC4;

    CODE_058CAF:
        a = Rom.ReadByteIndirect(0x058CB0);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058CB7);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058CBE);
        RAM_7ED000[x + 0x40] = a;

    CODE_058CC4:
        x++;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_058C6A;
        }

        return;
    }

    private void CODE_058CE0()
    {
        var x = RAM_7E00EB;
        var a = RAM_7ED000[x + 0x20];
        if (a == 0x0C)
        {
            goto CODE_058D26;
        }

        a = Rom.ReadByteIndirect(0x058CED);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirect(0x058CF4);
        RAM_7ED000[x + 0x11] = a;
        a = Rom.ReadByteIndirect(0x058CFB);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058D02);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirect(0x058D09);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058D10);
        RAM_7ED000[x + 0x31] = a;
        a = Rom.ReadByteIndirect(0x058D17);
        RAM_7ED000[x + 0x40] = a;
        a = Rom.ReadByteIndirect(0x058D1E);
        RAM_7ED000[x + 0x41] = a;
        goto CODE_058D5E;

    CODE_058D26:
        a = Rom.ReadByteIndirect(0x058D27);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirect(0x058D2E);
        RAM_7ED000[x + 0x11] = a;
        a = Rom.ReadByteIndirect(0x058D35);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirect(0x058D3C);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirect(0x058D43);
        RAM_7ED000[x + 0x30] = a;
        a = Rom.ReadByteIndirect(0x058D4A);
        RAM_7ED000[x + 0x31] = a;
        a = Rom.ReadByteIndirect(0x058D51);
        RAM_7ED000[x + 0x40] = a;
        a = Rom.ReadByteIndirect(0x058D58);
        RAM_7ED000[x + 0x41] = a;

    CODE_058D5E:
        RAM_7E00F1 -= 3;
        RAM_7E00EB += 0x0030;
        RAM_7E00EF = 1;
        CODE_058B51();
    }

    private void CODE_058D8A()
    {
        var x = RAM_7E00EB;
        ushort y = 0x0000;

    CODE_058D91:
        var a = Rom.ReadByteIndirectIndexed(0x058D92, y);
        RAM_7ED000[x + 0x20] = a;
        a = Rom.ReadByteIndirectIndexed(0x058D99, y);
        RAM_7ED000[x + 0x21] = a;
        a = Rom.ReadByteIndirectIndexed(0x058DA0, y);
        RAM_7ED000[x + 0x22] = a;
        a = Rom.ReadByteIndirectIndexed(0x058DA7, y);
        RAM_7ED000[x + 0x23] = a;
        x += 0x0010;
        y += 4;
        if (y != 0x0014)
        {
            goto CODE_058D91;
        }

        return;
    }

    private void CODE_058DC5()
    {
        var x = RAM_7E00EB;
        Set7ED000Word(x, 0x1B1A);
        return;
    }

    private void CODE_058DD1()
    {
        RAM_7E00EB++;
        CODE_058DD3();
    }

    private void CODE_058DD3()
    {
        var x = (ushort)(RAM_7E00EB + 0x0010);
        RAM_7ED000[x + 0x00] = 0x67;
        RAM_7ED000[x + 0x01] = 0x69;
        RAM_7ED000[x + 0x10] = 0x68;
        RAM_7ED000[x + 0x11] = 0x6A;
        return;
    }

    /// <summary>
    /// Generic function to set a grid of tiles?
    /// </summary>
    private void CODE_058E85()
    {
        var x = (ushort)((RAM_7E005C & 0xFF) << 1);
        var address = 0x050000 | Rom.ReadInt16IndirectIndexed(
            0x058E92,
            x);
        if ((RAM_7E005C & 0xFF) >= 2)
        {
            goto CODE_058EA9;
        }

        x = (ushort)(RAM_7E00EB + 0x10);
        goto CODE_058EAB;

    CODE_058EA9:
        x = RAM_7E00EB;

    CODE_058EAB:
        var y = (ushort)((RAM_7E00EF - 0x10) << 2);

        var tile = Rom.ReadByteIndexed(address, y);
        RAM_7ED000[x + 0x20] = tile;
        y++;
        tile = Rom.ReadByteIndexed(address, y);
        RAM_7ED000[x + 0x21] = tile;
        y++;
        tile = Rom.ReadByteIndexed(address, y);
        RAM_7ED000[x + 0x30] = tile;
        y++;
        tile = Rom.ReadByteIndexed(address, y);
        RAM_7ED000[x + 0x31] = tile;
        x += 2;
        RAM_7E00F1--;
        if (RAM_7E00F1 < 0x8000)
        {
            goto CODE_058EAB;
        }

        return;
    }

    private void CODE_058EDC_16bit(ushort a, ushort x)
    {
        Set7ED000Word(0x0000 + x, a);
        Set7ED000Word(0x0100 + x, a);
        Set7ED000Word(0x0200 + x, a);
        Set7ED000Word(0x0300 + x, a);
        Set7ED000Word(0x0400 + x, a);
        Set7ED000Word(0x0500 + x, a);
        Set7ED000Word(0x0600 + x, a);
        Set7ED000Word(0x0700 + x, a);
        Set7ED000Word(0x0800 + x, a);
        Set7ED000Word(0x0900 + x, a);
        Set7ED000Word(0x0A00 + x, a);
        Set7ED000Word(0x0B00 + x, a);
        Set7ED000Word(0x0C00 + x, a);
        Set7ED000Word(0x0D00 + x, a);
        Set7ED000Word(0x0E00 + x, a);
    }

    private void CODE_058EDC_8bit(byte a, ushort x)
    {
        RAM_7ED000[x + 0x0000] = a;
        RAM_7ED000[x + 0x0100] = a;
        RAM_7ED000[x + 0x0200] = a;
        RAM_7ED000[x + 0x0300] = a;
        RAM_7ED000[x + 0x0400] = a;
        RAM_7ED000[x + 0x0500] = a;
        RAM_7ED000[x + 0x0600] = a;
        RAM_7ED000[x + 0x0700] = a;
        RAM_7ED000[x + 0x0800] = a;
        RAM_7ED000[x + 0x0900] = a;
        RAM_7ED000[x + 0x0A00] = a;
        RAM_7ED000[x + 0x0B00] = a;
        RAM_7ED000[x + 0x0C00] = a;
        RAM_7ED000[x + 0x0D00] = a;
        RAM_7ED000[x + 0x0E00] = a;
    }

    /// <summary>
    /// Seems to be an indirect jump based on $EF
    /// </summary>
    private void CODE_058F19()
    {
        // Just remember that every RTS from the indirect jumps will be a return
        // for this function, since the JMP command does not push the PC value
        // to the stack, but we got to this function by a JSR at CODE_0580AE.
        RAM_7E0000 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x058F1E,
            RAM_7E00EF << 1);
        switch (RAM_7E0000)
        {
        case 0x90B6:
            CODE_0590B6();
            break;

        case 0x90BA:
            CODE_0590BA();
            break;

        case 0x90D2:
            CODE_0590D2();
            break;

        case 0x90E9:
            CODE_0590E9();
            break;

        case 0x910D:
            CODE_05910D();
            break;

        case 0x9099:
            CODE_059099();
            break;

        case 0x905F:
            CODE_05905F();
            break;

        case 0x903D:
            CODE_05903D();
            break;

        case 0x9004:
            CODE_059004();
            break;

        case 0x8FFA:
            CODE_058FFA();
            break;

        case 0x8F97:
            CODE_058F97();
            break;

        case 0x9116:
            CODE_059116();
            break;

        case 0x8F6F:
            CODE_058F6F();
            break;

        default:
            throw new NotImplementedException(
                $"Could not find function at $05:{RAM_7E0000:X4}.");
        }
    }

    private void CODE_058F6F()
    {
        ushort x = 0x00D0;
        ushort y = 0x0000;

    CODE_058F77:
        var a = Rom.ReadByteIndirectIndexed(
            0x058F78,
            y);
        CODE_058EDC_8bit(a, x);
        x++;
        y++;
        if (y != 0x0030)
        {
            goto CODE_058F77;
        }

        return;
    }

    private void CODE_058F97()
    {
        RAM_7E00E4 = 0;

    CODE_058F99:
        var a = RAM_7E00E4;
        var x = (ushort)((RAM_7E00E4 << 8) + 0xA0);
        ushort y = 0x0000;

    CODE_058FA9:
        var a8 = Rom.ReadByteIndirectIndexed(
            0x058FAA,
            y);
        RAM_7ED000[x] = a8;
        if ((x & 1) != 0)
        {
            goto CODE_058FCF;
        }

        a8 = 0x0C;
        RAM_7ED000[x + 0x10] = a8;
        RAM_7ED000[x + 0x30] = a8;
        RAM_7ED000[x + 0x50] = a8;

        a8 = 0x10;
        RAM_7ED000[x + 0x20] = a8;
        RAM_7ED000[x + 0x40] = a8;

        goto CODE_058FE7;

    CODE_058FCF:
        a8 = 0x0D;
        RAM_7ED000[x + 0x10] = a8;
        RAM_7ED000[x + 0x30] = a8;
        RAM_7ED000[x + 0x50] = a8;

        a8 = 0x11;
        RAM_7ED000[x + 0x20] = a8;
        RAM_7ED000[x + 0x40] = a8;

    CODE_058FE7:
        x++;
        y++;
        if ((y & 0x0F) != 0)
        {
            goto CODE_058FA9;
        }

        RAM_7E00E4++;
        if (RAM_7E00E4 != 0x0006)
        {
            goto CODE_058F99;
        }

        return;
    }

    /// <summary>
    /// Enable layer 3 image processing
    /// </summary>
    private void CODE_058FFA()
    {
        RAM_7E0EDC = (byte)RAM_7E00F1;
    }

    private void CODE_059004()
    {
        ushort x = 0x0080;

    CODE_059007:
        Set7ED000Word(x + 0x00, 0x0403);
        Set7ED000Word(x + 0x10, 0x0909);
        Set7ED000Word(x + 0x20, 0x0909);
        Set7ED000Word(x + 0x30, 0x0909);
        Set7ED000Word(x + 0x40, 0x0909);
        Set7ED000Word(x + 0x50, 0x0909);
        Set7ED000Word(x + 0x60, 0x0909);
        x += 2;
        if ((x & 0x000F) != 0)
        {
            goto CODE_059007;
        }

        x += 0x00F0;
        if (x < 0x1000)
        {
            goto CODE_059007;
        }
    }

    /// <summary>
    /// Fill top 3 rows with blank tiles for underwater levels
    /// </summary>
    private void CODE_05903D()
    {
        ushort x = 0x0000;
        ushort a = 0x0202;

    CODE_059043:
        CODE_058EDC_16bit(a, x);
        x += 2;
        if (x != 0x0020)
        {
            goto CODE_059043;
        }

        a = 0x0101;

    CODE_059050:
        CODE_058EDC_16bit(a, x);
        x += 2;
        if (x != 0x0030)
        {
            goto CODE_059050;
        }

        return;
    }

    private void CODE_05905F()
    {
        ushort x = 0;
    CODE_059064:
        var a = Rom.ReadByteIndirect(0x059065);
        RAM_7ED000[x + 0x00] = a;
        a = Rom.ReadByteIndirect(0x05906C);
        RAM_7ED000[x + 0x01] = a;
        a = Rom.ReadByteIndirect(0x059073);
        RAM_7ED000[x + 0x10] = a;
        a = Rom.ReadByteIndirect(0x05907A);
        RAM_7ED000[x + 0x11] = a;
        x += 2;
        if ((x & 0x0F) != 0)
        {
            goto CODE_059064;
        }

        x += 0x10;
        if (x != 0x0800)
        {
            goto CODE_059064;
        }

        return;
    }

    private void CODE_059099()
    {
        var a = (byte)RAM_7E005C;
        if (a == 0x02)
        {
            goto CODE_0590A5;
        }

        a = 0x5F;
        goto CODE_0590A7;

    CODE_0590A5:
        a = 0x00;

    CODE_0590A7:
        ushort x = 0;

    CODE_0590AA:
        CODE_058EDC_8bit(a, x);
        x++;
        if (x != 0x0020)
        {
            goto CODE_0590AA;
        }

        return;
    }

    /// <summary>
    /// Increase index to the latest background map16 page written to.
    /// </summary>
    private void CODE_0590B6()
    {
        RAM_7E0EC0++;
    }

    /// <summary>
    /// Enable Generic HDMA
    /// </summary>
    private void CODE_0590BA()
    {
        var a = (byte)RAM_7E00F1;
        if (a != 0x02)
        {
            goto CODE_0590C4;
        }

        a = 0xFF;

    CODE_0590C4:
        if (a != 0x01)
        {
            goto CODE_0590CB;
        }

        a = RAM_7E0ED1;

    CODE_0590CB:
        CODE_04825E(a);
    }

    /// <summary>
    /// Used for HDMA (gradients only, or wavy effect too?)
    /// </summary>
    /// <param name="a">
    /// Gradient type
    /// </param>
    private void CODE_04825E(byte a)
    {
        // Do nothing for now. I don't feel like supporting HDMA yet.
    }

    /// <summary>
    /// Enable Underwater HDMA gradient.
    /// </summary>
    private void CODE_0590D2()
    {
        CODE_04825E(0x02);
    }

    private void CODE_0590E9()
    {
        ushort x = 0x00D0;
        var y = RAM_7E00F1;
        var a = (ushort)Rom.ReadInt16IndirectIndexed(
            0X0590EF,
            y);

    CODE_0590F1:
        CODE_058EDC_16bit(a, x);
        x++;
        if (x != 0x00E0)
        {
            goto CODE_0590F1;
        }

        a = (ushort)Rom.ReadInt16IndirectIndexed(
            0x0590FB,
            y);

    CODE_0590FD:
        CODE_058EDC_16bit(a, x);
        x++;
        if (x != 0x00F0)
        {
            goto CODE_0590FD;
        }

        a = 0x0050;
        CODE_058EDC_16bit(a, x);
        return;
    }

    /// <summary>
    /// Upload a specific tileset based on bg command data.
    /// </summary>
    private void CODE_05910D()
    {
        RAM_7E0099 = RAM_7E00F1;
        CODE_05E6B1();
    }

    private void CODE_059116()
    {
        RAM_7E0099 = (ushort)(RAM_7E00F1 | 0x0010);
        //RAM_7E0099 = 2;
        CODE_05E6B1();
    }

    /// <summary>
    /// Fill the layer2 OBJ map using the tilemap buffer $7E:D000
    /// </summary>
    private void CODE_059166()
    {
        // This gets called at the very end of the generation command, and is
        // therefore the final step of creating backgrounds.
        var i = (ushort)(Rom.ReadInt16IndirectIndexed(
            0x05916B,
            RAM_7E00DB << 1) << 1);
        RAM_7E0000 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x59170,
            i);
        var address = 0x050000 | RAM_7E0000;

        ushort x = 0;
        ushort y = 0;

    CODE_05917F:
        RAM_7E00E4 = x;
        x = y;
        var tile = Get7ED000Word(x);

        // Stop writing to the background as soon as we hit the EOF value.
        if (tile == 0xFFFF)
        {
            goto CODE_0591CA;
        }

        var phy = y;
        x = RAM_7E00E4;
        y = (ushort)((tile & 0xFF) << 3);

        var obj = (ushort)Rom.ReadInt16Indexed(
            address,
            y);
        Set7E2000Word(x, obj);
        y += 2;
        obj = (ushort)Rom.ReadInt16Indexed(
            address,
            y);
        Set7E2000Word(x + 2, obj);
        y += 2;
        obj = (ushort)Rom.ReadInt16Indexed(
            address,
            y);
        Set7E2000Word(x + 0x40, obj);
        y += 2;
        obj = (ushort)Rom.ReadInt16Indexed(
            address,
            y);
        Set7E2000Word(x + 0x42, obj);

        y = phy;
        y++;
        if ((y & 0x0F) != 0)
        {
            goto CODE_0591C1;
        }

        x += 0x40;

    CODE_0591C1:
        x += 4;
        if (y != 0x1000)
        {
            goto CODE_05917F;
        }

    CODE_0591CA:
        return;
    }

    // Upload tileset graphics
    private void CODE_05E6B1()
    {
        // Check if this is a bonus room.
        if ((byte)RAM_7E0099 != 0x01)
        {
            goto CODE_05E6C7;
        }

        // Update tileset based on the player.
        RAM_7E02F8 = (byte)RAM_7E0099;
        RAM_7E0099 = Rom.ReadByteIndirectIndexed(
            0x05E6C3,
            RAM_7E0753);

    CODE_05E6C7:
        CODE_05E82A(RAM_7E0099);
        RAM_7E0000 = Rom.ReadByteIndirectIndexed(
            0x05E6CF,
            RAM_7E0099 << 1);

        RAM_7E0000 |= (ushort)(Rom.ReadByteIndirectIndexed(
            0x05E6D4,
            RAM_7E0099 << 1) << 8);

        switch (RAM_7E0000)
        {
        case 0xE71B:
            CODE_05E71B();
            return;

        case 0xE727:
            CODE_05E727();
            return;

        case 0xE73C:
            CODE_05E73C();
            return;

        case 0xE743:
            CODE_05E743();
            return;

        case 0xE74F:
            CODE_05E74F();
            return;

        case 0xE75B:
            CODE_05E75B();
            return;

        default:
            throw new NotImplementedException();
        }
    }

    private void CODE_05E71B()
    {
        RAM_7E0099 = 0;
        RAM_7E028D = 0;
        RAM_7E028C = 1;
    }

    private void CODE_05E727()
    {
        switch ((byte)RAM_7E00DB)
        {
        case 0x16:
        case 0x14:
        case 0x0D:
            CODE_05E748();
            return;

        default:
            CODE_05E82A(0x17);
            CODE_05E71B();
            return;

        }
    }

    private void CODE_05E73C()
    {
        CODE_05E82A(0x11);
        CODE_05E71B();
    }

    private void CODE_05E743()
    {
        CODE_05E82A(0x16);
        CODE_05E748();
    }

    private void CODE_05E748()
    {
        CODE_05E82A(0x12);
        CODE_05E71B();
    }

    private void CODE_05E74F()
    {
        CODE_05E82A(0x13);
        CODE_05E82A(0x14);
        CODE_05E71B();
    }

    private void CODE_05E75B()
    {
        CODE_05E82A(0x15);
        CODE_05E71B();
    }

    private void CODE_05E82A(int tileset)
    {
        OnLoadTileset(new TilesetEventArgs((Tileset)tileset));
        var x = tileset << 1;

        // Note that this table has word values, but we only read the high word.
        RAM_7E0287 = Rom.ReadByteIndirectIndexed(
            0x05E82D,
            x);

        RAM_7E0285 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x05E835,
            x);

        RAM_7E028A = (ushort)Rom.ReadInt16IndirectIndexed(
            0x05E83B,
            x);

        RAM_7E0288 = (ushort)Rom.ReadInt16IndirectIndexed(
            0x05E841,
            x);

        CODE_05E84C();
    }

    protected virtual void OnLoadTileset(TilesetEventArgs e)
    {
        LoadTileset?.Invoke(this, e);
    }

    /// <summary>
    /// DMA transfer?
    /// </summary>
    private void CODE_05E84C()
    {
        // TODO(swr): The transfer will actually take place in different parts
        // of the GFX DMA. I need to get the destination value, but I'm too
        // lazy to read the guide:
        // https://wiki.superfamicom.org/grog's-guide-to-dma-and-hdma-on-the-snes
        var address = (RAM_7E0287 << 0x10) | RAM_7E0285;
        var span = new Span<byte>(GFX, RAM_7E028A, RAM_7E0288);
        Rom.ReadBytes(address, span);
    }
}
