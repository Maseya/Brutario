namespace Maseya.Smas.Smb1;

using AreaData;
using AreaData.ObjectData;
using AreaData.SpriteData;

using Snes;

public class GameData
{
    public GameData(Rom rom, Pointers? pointers = null)
    {
        pointers ??= Pointers.GetPointers(rom);

        PaletteData = new PaletteData(rom, pointers.PaletteDataPointers);
        GfxData = new GfxData(rom, pointers.GfxDataPointers);
        Map16Data = new Map16Data(rom, pointers.Map16DataPointers);
        AreaLoader = new AreaLoader(rom, pointers.AreaLoaderPointers);
        TilemapLoader = new TilemapLoader(
            rom,
            pointers.TilemapLoaderPointers,
            AreaLoader.NumberOfAreas);
        AreaObjectRenderer = new AreaObjectRenderer(
            rom,
            pointers.AreaObjectRendererPointers);
        AreaSpriteRenderer = new AreaSpriteRenderer();
    }

    public PaletteData PaletteData { get; }

    public GfxData GfxData { get; }

    public Map16Data Map16Data { get; }

    public AreaLoader AreaLoader { get; }

    public TilemapLoader TilemapLoader { get; }

    public AreaObjectRenderer AreaObjectRenderer { get; }

    public AreaSpriteRenderer AreaSpriteRenderer { get; }

    public void WriteToGameData(Rom rom, Pointers? pointers = null)
    {
        pointers ??= Pointers.GetPointers(rom);

        AreaObjectRenderer.WriteToGameData(rom, pointers.AreaObjectRendererPointers);
        AreaLoader.WriteToGameData(rom, pointers.AreaLoaderPointers);
        Map16Data.WriteToGameData(rom, pointers.Map16DataPointers);
        GfxData.WriteToGameData(rom, pointers.GfxDataPointers);
        PaletteData.WriteToGameData(rom, pointers.PaletteDataPointers);
    }
}
