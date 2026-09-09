namespace Brutario.Core.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;

using Maseya.Smas.Smb1;
using Maseya.Snes;

public class PaletteEditorModel
{
    private ForegroundPalette foregroundPalette_;
    private BackgroundPalette backgroundPalette_;
    private SpritePalette spritePalette_;

    private int paletteIndex_;
    private Player player_;
    private PlayerState playerState_;
    private bool isBonusArea_;

    private Point selectedPoint_;

    private int _saveHistoryIndex;
    private int _currentHistoryIndex;
    private bool _hasUnsavedChanges;

    public PaletteEditorModel()
    {
        UndoFactory = new UndoFactory();
        UndoFactory.Cleared += UndoFactory_Cleared; ;
        UndoFactory.UndoElementAdded += UndoFactory_UndoElementAdded; ;
        UndoFactory.UndoComplete += UndoFactory_UndoComplete;
        UndoFactory.RedoComplete += UndoFactory_RedoComplete;

        Rows = new byte[PaletteData.RowsPerPalette];
    }

    public ForegroundPalette ForegroundPalette
    {
        get
        {
            return foregroundPalette_;
        }
        set
        {
            if (ForegroundPalette == value)
            {
                return;
            }

            foregroundPalette_ = value;
            OnForegroundPaletteChanged(EventArgs.Empty);
        }
    }

    public BackgroundPalette BackgroundPalette
    {
        get
        {
            return backgroundPalette_;
        }
        set
        {
            if (BackgroundPalette == value)
            {
                return;
            }

            backgroundPalette_ = value;
            OnBackgroundPaletteChanged(EventArgs.Empty);
        }
    }

    public SpritePalette SpritePalette
    {
        get
        {
            return spritePalette_;
        }
        set
        {
            if (SpritePalette == value)
            {
                return;
            }

            spritePalette_ = value;
            OnSpritePaletteChanged(EventArgs.Empty);
        }
    }

    public Color32BppArgb[] GetCurrentPalette()
    {
        if (PaletteData is null)
        {
            throw new InvalidOperationException();
        }

        var result = new Color32BppArgb[PaletteData.TotalPaletteSize];
        PaletteData.ReadPalette(
            ForegroundPalette,
            BackgroundPalette,
            SpritePalette,
            IsBonusArea,
            Player,
            PlayerState,
            result);

        return result;
    }

    public Point SelectedPoint
    {
        get
        {
            return selectedPoint_;
        }

        set
        {
            if (value == SelectedPoint)
            {
                return;
            }

            selectedPoint_ = value;
            OnSelectedPointChanged(EventArgs.Empty);
        }
    }

    public int SelectedX
    {
        get
        {
            return selectedPoint_.X;
        }

        set
        {
            SelectedPoint = new Point(value, SelectedY);
        }
    }

    public int SelectedY
    {
        get
        {
            return SelectedPoint.Y;
        }

        set
        {
            SelectedPoint = new Point(SelectedX, value);
        }
    }

    public int SelectedIndex
    {
        get
        {
            return (SelectedY * PaletteData.ColorsPerRow) + SelectedX;
        }

        set
        {
            var x = value % PaletteData.ColorsPerRow;
            var y = value / PaletteData.ColorsPerRow;

            SelectedPoint = new Point(x, y);
        }
    }

    public bool HasUnsavedChanges
    {
        get
        {
            return _hasUnsavedChanges;
        }

        private set
        {
            if (HasUnsavedChanges == value)
            {
                return;
            }

            _hasUnsavedChanges = value;
            OnHasUnsavedChangesChanged(EventArgs.Empty);
        }
    }

    public bool CanUndo
    {
        get
        {
            return UndoFactory.CanUndo;
        }
    }

    public bool CanRedo
    {
        get
        {
            return UndoFactory.CanRedo;
        }
    }

    internal PaletteData? PaletteData
    {
        get; set;
    }

    internal string? Path { get; set; }

    internal Rom? Rom
    {
        get; set;
    }

    internal PaletteDataPointers? Pointers
    {
        get; set;
    }

    public int PaletteIndex
    {
        get
        {
            return paletteIndex_;
        }
        set
        {
            if (PaletteIndex == value)
            {
                return;
            }

            paletteIndex_ = value;
            OnPaletteIndexChanged(EventArgs.Empty);
        }
    }

    public Player Player
    {
        get
        {
            return player_;
        }
        set
        {
            if (Player == value)
            {
                return;
            }

            player_ = value;
            OnPlayerChanged(EventArgs.Empty);
        }
    }

    public PlayerState PlayerState
    {
        get
        {
            return playerState_;
        }
        set
        {
            if (PlayerState == value)
            {
                return;
            }

            playerState_ = value;
            OnPlayerStateChanged(EventArgs.Empty);
        }
    }

    public bool IsBonusArea
    {
        get
        {
            return isBonusArea_;
        }
        set
        {
            if (IsBonusArea == value)
            {
                return;
            }

            isBonusArea_ = value;
            OnIsBonusAreaChanged(EventArgs.Empty);
        }
    }

    private UndoFactory UndoFactory { get; }

    private int SaveHistoryIndex
    {
        get
        {
            return _saveHistoryIndex;
        }
        set
        {
            _saveHistoryIndex = value;
            HasUnsavedChanges = SaveHistoryIndex != CurrentHistoryIndex;
        }
    }

    private int CurrentHistoryIndex
    {
        get
        {
            return _currentHistoryIndex;
        }
        set
        {
            _currentHistoryIndex = value;
            HasUnsavedChanges = SaveHistoryIndex != CurrentHistoryIndex;
        }
    }

    private byte[] Rows { get; }

    public event EventHandler? Saved;

    public event EventHandler? ForegroundPaletteChanged;
    public event EventHandler? BackgroundPaletteChanged;
    public event EventHandler? SpritePaletteChanged;

    public event EventHandler? PaletteIndexChanged;
    public event EventHandler? PlayerChanged;
    public event EventHandler? PlayerStateChanged;
    public event EventHandler? IsBonusAreaChanged;

    public event EventHandler? SelectedIndexChanged;
    public event EventHandler? PaletteChanged;

    public event EventHandler? HasUnsavedChangesChanged;
    public event EventHandler? HistoryCleared;
    public event EventHandler? UndoElementAdded;
    public event EventHandler? UndoComplete;
    public event EventHandler? RedoComplete;

    public void Reset()
    {
        if (PaletteData is null)
        {
            throw new InvalidOperationException();
        }

        ClearHistory();
        PaletteData.Reset(Rom!, Pointers!);
        SetSceneryPalettes();
    }

    public void Save()
    {
        if (PaletteData is null)
        {
            throw new InvalidOperationException();
        }

        PaletteData.WriteToGameData(Rom!, Pointers!);
        File.WriteAllBytes(Path!, Rom!.GetData());

        OnSaved(EventArgs.Empty);
    }

    public void EditColor(int index, Color32BppArgb color)
    {
        if (PaletteData is null)
        {
            throw new InvalidOperationException();
        }

        var row = index / PaletteData.ColorsPerRow;
        var column = index % PaletteData.ColorsPerRow;
        var rowIndex = PaletteData.GetRowIndex(
            ForegroundPalette,
            BackgroundPalette,
            SpritePalette,
            row);
        var originalColor = PaletteData.GetColor(rowIndex, column, Player, PlayerState, IsBonusArea);
        PushUndoAction(
            undo: () => SetColorInternal(rowIndex, column, originalColor),
            redo: () => SetColorInternal(rowIndex, column, color),
            act: true);
        OnPaletteChanged(EventArgs.Empty);
    }

    private void SetColorInternal(int rowIndex, int column, Color32BppArgb color)
    {
        PaletteData!.SetColor(rowIndex, column, Player, PlayerState, IsBonusArea, color);
        OnPaletteChanged(EventArgs.Empty);
    }

    public PaletteFileType PaletteFileTypeFromExtension(string path)
    {
        var ext = System.IO.Path.GetExtension(path);
        return StringComparer.OrdinalIgnoreCase.Compare(ext, ".rpf") == 0
            ? PaletteFileType.Rpf
            : StringComparer.OrdinalIgnoreCase.Compare(ext, ".tpl") == 0
            ? PaletteFileType.Tpl
            : StringComparer.OrdinalIgnoreCase.Compare(ext, ".pal") == 0
            ? PaletteFileType.Pal
            : PaletteFileType.Rpf;
    }

    public void ExportPalette(string path)
    {
        ExportPalette(path, PaletteFileTypeFromExtension(path));
    }

    public void ExportPalette(string path, PaletteFileType paletteFileType)
    {
        var result = paletteFileType switch
        {
            PaletteFileType.Rpf => ToRpf(),
            PaletteFileType.Tpl => ToTpl(),
            PaletteFileType.Pal => ToPal(),
            _ => throw new InvalidEnumArgumentException(),
        };

        File.WriteAllBytes(path, result);
    }


    public void ImportPalette(string path)
    {
        ImportPalette(path, PaletteFileTypeFromExtension(path));
    }

    public void ImportPalette(string path, PaletteFileType paletteFileType)
    {
        if (PaletteData is null)
        {
            throw new InvalidOperationException();
        }

        var data = File.ReadAllBytes(path);
        var result = paletteFileType switch
        {
            PaletteFileType.Rpf => FromRpf(data),
            PaletteFileType.Tpl => FromTpl(data),
            PaletteFileType.Pal => FromPal(data),
            _ => throw new InvalidEnumArgumentException(),
        };

        var originalForegroundPalette = ForegroundPalette;
        var originalBackgroundPalette = BackgroundPalette;
        var originalSpritePalette = SpritePalette;
        var originalIsBonusArea = IsBonusArea;
        var originalPlayer = Player;
        var originalPlayerState = PlayerState;
        var originalPalette = GetCurrentPalette();
        void action()
        {
            ImportPaletteInternal(
                originalForegroundPalette,
                originalBackgroundPalette,
                originalSpritePalette,
                originalIsBonusArea,
                originalPlayer,
                originalPlayerState,
                result);
        }

        void redo()
        {
            ImportPaletteInternal(
                originalForegroundPalette,
                originalBackgroundPalette,
                originalSpritePalette,
                originalIsBonusArea,
                originalPlayer,
                originalPlayerState,
                originalPalette);
        }

        PushUndoAction(redo, action, act: true);
    }

    private void ImportPaletteInternal(
        ForegroundPalette foregroundPalette,
        BackgroundPalette backgroundPalette,
        SpritePalette spritePalette,
        bool isBonusArea,
        Player player,
        PlayerState playerState,
        ReadOnlySpan<Color32BppArgb> palette)
    {
        PaletteData!.WritePalette(
            foregroundPalette,
            backgroundPalette,
            spritePalette,
            isBonusArea,
            player,
            playerState,
            palette);
        OnPaletteChanged(EventArgs.Empty);
    }

    public byte[] ToRpf()
    {
        var palette = GetCurrentPalette();
        var result = new byte[PaletteData.TotalPaletteSize * sizeof(short)];
        using var stream = new MemoryStream(result);
        using var writer = new BinaryWriter(stream);
        for (var i = 0; i < PaletteData.TotalPaletteSize; i++)
        {
            writer.Write((ushort)Color32BppArgb.ToSnesColor(palette[i]));
        }

        return result;
    }

    public Color32BppArgb[] FromRpf(ReadOnlySpan<byte> data)
    {
        var result = new Color32BppArgb[PaletteData.TotalPaletteSize];
        if (data.Length != result.Length * sizeof(ushort))
        {
            throw new ArgumentException();
        }

        for (var i = 0; i < result.Length; i++)
        {
            var word = BitConverter.ToUInt16(data.Slice(i * sizeof(ushort), sizeof(ushort)));
            result[i] = Color32BppArgb.FromSnesColor(word);
        }

        return result;
    }

    public byte[] ToTpl()
    {
        var rpf = ToRpf();
        var result = new byte[rpf.Length + 4];
        using var stream = new MemoryStream(result);
        using var writer = new BinaryWriter(stream);
        writer.Write(Encoding.ASCII.GetBytes("TPL"));
        writer.Write((byte)2);
        writer.Write(rpf);
        return result;
    }

    public Color32BppArgb[] FromTpl(ReadOnlySpan<byte> data)
    {
        if (data.Length != (PaletteData.TotalPaletteSize * sizeof(short)) + 4)
        {
            throw new ArgumentException();
        }

        return FromRpf(data[4..]);
    }

    public byte[] ToPal()
    {
        var palette = GetCurrentPalette();
        var result = new byte[PaletteData.TotalPaletteSize * 3];
        using var stream = new MemoryStream(result);
        using var writer = new BinaryWriter(stream);
        for (var i = 0; i < PaletteData.TotalPaletteSize; i++)
        {
            writer.Write(palette[i].R);
            writer.Write(palette[i].G);
            writer.Write(palette[i].B);
        }

        return result;
    }

    public Color32BppArgb[] FromPal(ReadOnlySpan<byte> data)
    {
        var result = new Color32BppArgb[PaletteData.TotalPaletteSize];
        if (data.Length != result.Length * 3)
        {
            throw new ArgumentException();
        }

        for (var i = 0; i < result.Length; i++)
        {
            result[i].A = Byte.MaxValue;
            result[i].R = data[(i * 3) + 0];
            result[i].G = data[(i * 3) + 1];
            result[i].B = data[(i * 3) + 2];
        }

        return result;
    }

    public void Undo()
    {
        UndoFactory.Undo();
    }

    public void Redo()
    {
        UndoFactory.Redo();
    }

    private void ClearHistory()
    {
        UndoFactory.Clear();
        _saveHistoryIndex = 0;
        _currentHistoryIndex = 0;
        HasUnsavedChanges = false;
    }

    protected void PushUndoAction(Action undo, Action redo, bool act = false)
    {
        if (act) { redo(); }
        CurrentHistoryIndex++;
        UndoFactory.Add(undo, redo);
    }

    protected virtual void OnSaved(EventArgs e)
    {
        Saved?.Invoke(this, e);
    }

    protected virtual void OnForegroundPaletteChanged(EventArgs e)
    {
        if (PaletteData is not null)
        {
            OnPaletteChanged(EventArgs.Empty);
            PaletteData.UpdateForegroundPalette(PaletteIndex, ForegroundPalette);
        }

        ForegroundPaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnBackgroundPaletteChanged(EventArgs e)
    {
        if (PaletteData is not null)
        {
            OnPaletteChanged(EventArgs.Empty);
            PaletteData.UpdateBackgroundPalette(PaletteIndex, BackgroundPalette);
        }

        BackgroundPaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnSpritePaletteChanged(EventArgs e)
    {
        if (PaletteData is not null)
        {
            OnPaletteChanged(EventArgs.Empty);
            PaletteData.UpdateSpritePalette(PaletteIndex, SpritePalette);
        }

        SpritePaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnPaletteIndexChanged(EventArgs e)
    {
        SetSceneryPalettes();
        PaletteIndexChanged?.Invoke(this, e);
    }

    protected virtual void OnPlayerChanged(EventArgs e)
    {
        if (PaletteData is not null)
        {
            OnPaletteChanged(EventArgs.Empty);
        }

        PlayerChanged?.Invoke(this, e);
    }

    protected virtual void OnPlayerStateChanged(EventArgs e)
    {
        if (PaletteData is not null)
        {
            OnPaletteChanged(EventArgs.Empty);
        }

        PlayerStateChanged?.Invoke(this, e);
    }

    protected virtual void OnIsBonusAreaChanged(EventArgs e)
    {
        if (PaletteData is not null)
        {
            OnPaletteChanged(EventArgs.Empty);
        }

        IsBonusAreaChanged?.Invoke(this, e);
    }

    protected virtual void OnSelectedPointChanged(EventArgs e)
    {
        SelectedIndexChanged?.Invoke(this, e);
    }

    protected virtual void OnPaletteChanged(EventArgs e)
    {
        PaletteChanged?.Invoke(this, e);
    }

    protected virtual void OnHasUnsavedChangesChanged(EventArgs e)
    {
        HasUnsavedChangesChanged?.Invoke(this, e);
    }

    private void SetSceneryPalettes()
    {
        if (PaletteData is not null)
        {
            if (PaletteData.TryGetForegroundPalette(PaletteIndex, out var foregroundPalette))
            {
                ForegroundPalette = foregroundPalette;
            }

            if (PaletteData.TryGetBackgroundPalette(PaletteIndex, out var backgroundPalette))
            {
                BackgroundPalette = backgroundPalette;
            }

            if (PaletteData.TryGetSpritePalette(PaletteIndex, out var spritePalette))
            {
                SpritePalette = spritePalette;
            }

            OnPaletteChanged(EventArgs.Empty);
        }
    }

    private void UndoFactory_Cleared(object? sender, EventArgs e)
    {
        HistoryCleared?.Invoke(this, EventArgs.Empty);
    }

    private void UndoFactory_UndoElementAdded(object? sender, UndoEventArgs e)
    {
        UndoElementAdded?.Invoke(this, EventArgs.Empty);
    }

    private void UndoFactory_UndoComplete(object? sender, UndoEventArgs e)
    {
        CurrentHistoryIndex--;
        UndoComplete?.Invoke(this, e);
    }

    private void UndoFactory_RedoComplete(object? sender, UndoEventArgs e)
    {
        CurrentHistoryIndex++;
        RedoComplete?.Invoke(this, e);
    }
}
