// <copyright file="Pointers.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;

using System;

using AreaData;
using AreaData.ObjectData;

using Snes;

public class Pointers
{
    public static readonly Pointers Jp10 = new(
        PaletteDataPointers.Jp10,
        GfxDataPointers.Jp10,
        Map16DataPointers.Jp10,
        TilemapLoaderPointers.Jp10,
        AreaLoaderPointers.Jp10,
        AreaObjectRendererPointers.Jp10);

    public static readonly Pointers Jp11 = new(
        PaletteDataPointers.Jp11,
        GfxDataPointers.Jp11,
        Map16DataPointers.Jp11,
        TilemapLoaderPointers.Jp11,
        AreaLoaderPointers.Jp11,
        AreaObjectRendererPointers.Jp11);

    public static readonly Pointers Usa = new(
        PaletteDataPointers.Usa,
        GfxDataPointers.Usa,
        Map16DataPointers.Usa,
        TilemapLoaderPointers.Usa,
        AreaLoaderPointers.Usa,
        AreaObjectRendererPointers.Usa);

    public static readonly Pointers UsaPlusW = new(
        PaletteDataPointers.UsaPlusW,
        GfxDataPointers.UsaPlusW,
        Map16DataPointers.UsaPlusW,
        TilemapLoaderPointers.UsaPlusW,
        AreaLoaderPointers.UsaPlusW,
        AreaObjectRendererPointers.UsaPlusW);

    public static readonly Pointers Eu = new(
        PaletteDataPointers.Eu,
        GfxDataPointers.Eu,
        Map16DataPointers.Eu,
        TilemapLoaderPointers.Eu,
        AreaLoaderPointers.Eu,
        AreaObjectRendererPointers.Eu);

    public static readonly Pointers EuPlusW = new(
        PaletteDataPointers.EuPlusW,
        GfxDataPointers.EuPlusW,
        Map16DataPointers.EuPlusW,
        TilemapLoaderPointers.EuPlusW,
        AreaLoaderPointers.EuPlusW,
        AreaObjectRendererPointers.EuPlusW);

    public static readonly Pointers UsaSmb1 = new(
        PaletteDataPointers.UsaSmb1,
        GfxDataPointers.UsaSmb1,
        Map16DataPointers.UsaSmb1,
        TilemapLoaderPointers.UsaSmb1,
        AreaLoaderPointers.UsaSmb1,
        AreaObjectRendererPointers.UsaSmb1);

    private Pointers(
        PaletteDataPointers paletteDataPointers,
        GfxDataPointers gfxDataPointers,
        Map16DataPointers map16DataPointers,
        TilemapLoaderPointers tilemapLoaderPointers,
        AreaLoaderPointers areaLoaderPointers,
        AreaObjectRendererPointers areaObjectRendererPointers)
    {
        PaletteDataPointers = paletteDataPointers;
        GfxDataPointers = gfxDataPointers;
        Map16DataPointers = map16DataPointers;
        TilemapLoaderPointers = tilemapLoaderPointers;
        AreaLoaderPointers = areaLoaderPointers;
        AreaObjectRendererPointers = areaObjectRendererPointers;
    }

    public PaletteDataPointers PaletteDataPointers
    {
        get;
    }

    public GfxDataPointers GfxDataPointers
    {
        get;
    }

    public Map16DataPointers Map16DataPointers
    {
        get;
    }

    public TilemapLoaderPointers TilemapLoaderPointers
    {
        get;
    }

    public AreaLoaderPointers AreaLoaderPointers
    {
        get;
    }

    public AreaObjectRendererPointers AreaObjectRendererPointers
    {
        get;
    }

    public static Pointers GetPointers(Rom rom)
    {
        // TODO(nrg): This will need to be customized more later.
        switch (rom.DestinationCode)
        {
            case DestinationCode.NorthAmerica:
                if (rom.GameTitle.Contains("WORLD"))
                {
                    return UsaPlusW;
                }

                if (rom.GameTitle == "Super Mario Bros. 1")
                {
                    return UsaSmb1;
                }

                return Usa;

            case DestinationCode.Japan:
                if (rom.MaskRomVersion == 1)
                {
                    return Jp11;
                }

                return Jp10;

            case DestinationCode.Europe:
                if (rom.GameTitle.Contains("WORLD"))
                {
                    return EuPlusW;
                }

                return Eu;

            default:
                throw new ArgumentException(
                    "Could not determine which type of All-Stars ROM was loaded.",
                    nameof(rom));
        }
    }
}
