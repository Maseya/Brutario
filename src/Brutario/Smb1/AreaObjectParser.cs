using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Brutario.Smb1
{
    public class AreaObjectParser
    {
        /// <summary>
        /// The default size of the object data buffer. This field is constant.
        /// </summary>
        public const int DefaultBufferSize = 5;

        public const int SingleTileObjectPointer = 0x03AC2B;

        public const int BrickRowTilePointer = 0x03AB6D;

        public const int StoneRowTilePointer = 0x03AB75;

        public const int CoinRowTilePointer = 0x03AB2D;

        public const int PipeCapTilePointer = 0x03AA3E;

        public const int PipeTilePointer = 0x03AA45;

        public const int BlockItemTilePointer = 0x03AC2B;

        public const int PulleyRopeTilePointer = 0x03A987;

        public const int WaterSurfaceTilePointer = 0x03AA8D;

        public const int JPipeTiles1Pointer = 0x03A9B4;

        public const int JPipeTiles2Pointer = 0x03A9C5;

        public const int JPipeTiles3Pointer = 0x03A9CB;

        public const int JPipeTiles4Pointer = 0x03A9DD;

        public const int CastleTilePointer = 0x048F61;

        public const int StoneStairHeightPointer = 0x049033;

        public const int StoneStairYPointer = 0x049030;

        public const int JPipeTiles5Pointer = 0x049194;

        public const int JPipeTiles6Pointer = 0x0491A6;

        public const int JPipeTiles7Pointer = 0x0491AC;

        public AreaObjectParser(
            AreaObjectRenderer areaObjectRenderer,
            IList<AreaObjectCommand> areaObjectData,
            int bufferSize = DefaultBufferSize)
        {
            AreaObjectRenderer = areaObjectRenderer
                ?? throw new ArgumentNullException(nameof(areaObjectRenderer));

            AreaData = new Collection<AreaObjectCommand>(areaObjectData);
            BufferSize = bufferSize >= 0
                ? bufferSize
                : throw new ArgumentOutOfRangeException(nameof(bufferSize));

            LengthBuffer = new int[BufferSize];
            IndexBuffer = new int[BufferSize];
            TreePlatformProperties = new bool[BufferSize];
            MushroomPlatformCenterCoordinate = new int[BufferSize];
            RenderCommands = new Dictionary<AreaObjectCode, Action>()
            {
                { AreaObjectCode.EnterablePipe, Pipe },
                { AreaObjectCode.AreaSpecificPlatform, AreaSpecificPlatform },
                { AreaObjectCode.HorizontalBricks, RowOfBricks },
                { AreaObjectCode.HorizontalStones, RowOfStones },
                { AreaObjectCode.HorizontalCoins, RowOfCoins },
                { AreaObjectCode.VerticalBricks, ColumnOfBricks },
                { AreaObjectCode.VerticalStones, ColumnOfStones },
                { AreaObjectCode.UnenterablePipe, Pipe },
                { AreaObjectCode.Hole, Hole },
                { AreaObjectCode.BalanceHorizontalRope, BalanceHorizontalRope },
                { AreaObjectCode.BridgeV7, HighBridge },
                { AreaObjectCode.BridgeV8, MidBridge },
                { AreaObjectCode.BridgeV10, LowBridge },
                { AreaObjectCode.HoleWithWaterOrLava, HoleWithWaterOrLava },
                { AreaObjectCode.HorizontalQuestionBlocksV3, HighRowOfCoinBlocks },
                { AreaObjectCode.HorizontalQuestionBlocksV7, LowRowOfCoinBlocks },
                { AreaObjectCode.QuestionBlockPowerup, ItemBlock },
                { AreaObjectCode.QuestionBlockCoin, ItemBlock },
                { AreaObjectCode.HiddenBlockCoin, ItemBlock },
                { AreaObjectCode.HiddenBlock1UP, AreaTypeBlock },
                { AreaObjectCode.BrickPowerup, AreaTypeBlock },
                { AreaObjectCode.BrickBeanstalk, AreaTypeBlock },
                { AreaObjectCode.BrickStar, AreaTypeBlock },
                { AreaObjectCode.Brick10Coins, AreaTypeBlock },
                { AreaObjectCode.Brick1UP, AreaTypeBlock },
                { AreaObjectCode.SidewaysPipe, SidewaysPipe },
                { AreaObjectCode.UsedBlock, FireBarBlock },
                { AreaObjectCode.SpringBoard, SpringBoard },
                { AreaObjectCode.JPipe, JPipe },
                { AreaObjectCode.AltJPipe, JPipe },
                { AreaObjectCode.FlagPole, FlagPole },
                { AreaObjectCode.AltFlagPole, FlagPole },
                { AreaObjectCode.BowserAxe, BowserAxe },
                { AreaObjectCode.RopeForAxe, RopeForAxe },
                { AreaObjectCode.BowserBridge, BowserBridge },
                { AreaObjectCode.ForegroundChange, ForegroundChange },
                { AreaObjectCode.BrickAndSceneryChange, TerrainModifier },
                { AreaObjectCode.RopeForLift, RopeForLift },
                { AreaObjectCode.PulleyRope, PulleyRope },
                { AreaObjectCode.Castle, Castle },
                { AreaObjectCode.CastleCeilingCap, CastleCeilineCap },
                { AreaObjectCode.Staircase, StoneStairs },
                { AreaObjectCode.CastleStairs, CastleDescendingSteps },
                { AreaObjectCode.CastleRectangularCeilingTiles, CastleRectangularCeilingTiles },
                { AreaObjectCode.CastleFloorRightEdge, CastleFloorRightEdge },
                { AreaObjectCode.CastleFloorLeftEdge, CastleFloorLeftEdge },
                { AreaObjectCode.CastleFloorLeftWall, CastleFloorLeftWall },
                { AreaObjectCode.CastleFloorRightWall, CastleFloorRightWall },
                { AreaObjectCode.VerticalSeaBlocks, VerticalSeaBlocks },
                { AreaObjectCode.ExtendableJPipe, ExtendableJPipe },
                { AreaObjectCode.VerticalBalls, VerticalClimbingObject },
            };
        }

        public AreaObjectRenderer AreaObjectRenderer
        {
            get;
        }

        public AreaObjectLoader AreaObjectLoader
        {
            get
            {
                return AreaObjectRenderer.AreaObjectLoader;
            }
        }

        public AreaLoader AreaLoader
        {
            get
            {
                return AreaObjectLoader.AreaLoader;
            }
        }

        public GameData RomData
        {
            get
            {
                return AreaLoader.RomData;
            }
        }

        public RomIO Rom
        {
            get
            {
                return RomData.Rom;
            }
        }

        public int SingleTileObjectAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(SingleTileObjectPointer);
            }
        }

        public int BrickLineTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(BrickRowTilePointer);
            }
        }

        public int StoneLineTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(StoneRowTilePointer);
            }
        }

        public int CoinLineTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(CoinRowTilePointer);
            }
        }

        public int PipeCapTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(PipeCapTilePointer);
            }
        }

        public int PipeTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(PipeTilePointer);
            }
        }

        public int BlockItemTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(BlockItemTilePointer);
            }
        }

        public int PulleyRopeTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(PulleyRopeTilePointer);
            }
        }

        public int WaterSurfaceTileAddress
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(WaterSurfaceTilePointer);
            }
        }

        public int JPipeTiles1Address
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(JPipeTiles1Pointer);
            }
        }

        public int JPipeTiles2Address
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(JPipeTiles2Pointer);
            }
        }

        public int JPipeTiles3Address
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(JPipeTiles3Pointer);
            }
        }

        public int JPipeTiles4Address
        {
            get
            {
                return 0x030000 | Rom.ReadInt16(JPipeTiles4Pointer);
            }
        }

        public int JPipeTiles5Address
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(JPipeTiles5Pointer);
            }
        }

        public int JPipeTiles6Address
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(JPipeTiles6Pointer);
            }
        }

        public int JPipeTiles7Address
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(JPipeTiles7Pointer);
            }
        }

        public int CastleTileAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(CastleTilePointer);
            }
        }

        public int StoneStairHeightAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(StoneStairHeightPointer);
            }
        }

        public int StoneStairYAddress
        {
            get
            {
                return 0x040000 | Rom.ReadInt16(StoneStairYPointer);
            }
        }

        public AreaType AreaType
        {
            get
            {
                return AreaObjectRenderer.AreaType;
            }
        }

        /// <summary>
        /// Gets the size of the object data buffers. The default value is set
        /// to <see cref="DefaultBufferSize"/>.
        /// </summary>
        public int BufferSize
        {
            get;
        }

        /// <summary>
        /// Gets the X register, which represents the current index of the
        /// object data buffer the <see cref="AreaObjectParser"/> is on.
        /// </summary>
        public int CurrentBufferIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the Y register, which represents the current index of the area
        /// object data collection the <see cref="AreaObjectParser"/> is on.
        /// </summary>
        public int AreaDataIndex
        {
            get;
            set;
        }

        /// <summary>
        /// Gets $FA, which represents the pointer to the area object data.
        /// </summary>
        public Collection<AreaObjectCommand> AreaData
        {
            get;
        }

        public AreaObjectCommand CurrentObjectCommand
        {
            get
            {
                return AreaData[AreaDataIndex];
            }

            set
            {
                AreaData[AreaDataIndex] = value;
            }
        }

        public AreaObjectCommand CurrentBufferObject
        {
            get
            {
                return AreaData[CurrentBufferObjectIndex];
            }

            set
            {
                AreaData[CurrentBufferObjectIndex] = value;
            }
        }

        public AreaObjectCode CurrentObjectCode
        {
            get
            {
                return CurrentObjectCommand.Code;
            }
        }

        public bool IsScreenFlag
        {
            get
            {
                return CurrentObjectCommand.ScreenFlag;
            }
        }

        public bool IsScreenJumpCommand
        {
            get
            {
                return CurrentObjectCode == AreaObjectCode.ScreenSkip;
            }
        }

        public AreaHeader CurrentHeader
        {
            get
            {
                return AreaObjectRenderer.CurrentHeader;
            }

            set
            {
                AreaObjectRenderer.CurrentHeader = value;
            }
        }

        /// <summary>
        /// Gets $06A1, which represents the tile data buffer.
        /// </summary>
        public int[] TileBuffer
        {
            get
            {
                return AreaObjectRenderer.TileBuffer;
            }
        }

        /// <summary>
        /// Gets $1300, which represents the current buffer object's width.
        /// </summary>
        public int[] LengthBuffer
        {
            get;
        }

        /// <summary>
        /// Gets $1300,x.
        /// </summary>
        public int CurrentBufferObjectWidth
        {
            get
            {
                return LengthBuffer[CurrentBufferIndex];
            }

            set
            {
                LengthBuffer[CurrentBufferIndex] = value;
            }
        }

        /// <summary>
        /// Gets a value that determines whether <see
        /// cref="CurrentBufferObjectWidth"/> has been set to a valid value.
        /// </summary>
        public bool CurrentBufferEnabled
        {
            get
            {
                return CurrentBufferObjectWidth >= 0;
            }
        }

        /// <summary>
        /// Gets $1305, which represents the area object data index
        /// </summary>
        public int[] IndexBuffer
        {
            get;
        }

        /// <summary>
        /// Gets $130F, which represents extra properties for the respective
        /// buffer object.
        /// </summary>
        public bool[] TreePlatformProperties
        {
            get;
        }

        public bool ObjectHasSpecialProperties
        {
            get
            {
                return TreePlatformProperties[CurrentBufferIndex];
            }

            set
            {
                TreePlatformProperties[CurrentBufferIndex] = value;
            }
        }

        public int[] MushroomPlatformCenterCoordinate
        {
            get;
        }

        public int ObjectSpecialCoordinate
        {
            get
            {
                return MushroomPlatformCenterCoordinate[CurrentBufferIndex];
            }

            set
            {
                MushroomPlatformCenterCoordinate[CurrentBufferIndex] = value;
            }
        }

        /// <summary>
        /// Gets $1305,x
        /// </summary>
        public int CurrentBufferObjectIndex
        {
            get
            {
                return IndexBuffer[CurrentBufferIndex];
            }

            set
            {
                IndexBuffer[CurrentBufferIndex] = value;
            }
        }

        /// <summary>
        /// Gets or sets $0725, which represents the screen the renderer is
        /// currently on.
        /// </summary>
        public int CurrentRenderingScreen
        {
            get
            {
                return AreaObjectRenderer.CurrentRenderingScreen;
            }

            set
            {
                AreaObjectRenderer.CurrentRenderingScreen = value;
            }
        }

        /// <summary>
        /// Gets or sets $0726, which represents the X-coordinate of the
        /// current screen the renderer is currently on.
        /// </summary>
        public int CurrentRenderingScreenX
        {
            get
            {
                return AreaObjectRenderer.CurrentRenderingScreenX;
            }

            set
            {
                AreaObjectRenderer.CurrentRenderingScreenX = value;
            }
        }

        /// <summary>
        /// Gets or sets the full X-coordinate the renderer is currently on.
        /// </summary>
        public int CurrentRenderingX
        {
            get
            {
                return AreaObjectRenderer.CurrentRenderingX;
            }

            set
            {
                AreaObjectRenderer.CurrentRenderingX = value;
            }
        }

        /// <summary>
        /// Gets or sets $072A, which represents the screen the area object
        /// data starts on.
        /// </summary>
        public int CurrentObjectScreen
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets $072B, which represents a flag that determines whether
        /// a page jump command has been activated for this current rendering
        /// pass.
        /// </summary>
        public bool IsScreenJumpSet
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets $072C, which represents the saved area data index.
        /// </summary>
        public int StoredAreaDataIndex
        {
            get;
            set;
        }

        public bool IsEndOfArea
        {
            get
            {
                return AreaDataIndex == AreaData.Count;
            }
        }

        /// <summary>
        /// Gets $0729, which determines whether the current object is behind
        /// the rendering screen.
        /// </summary>
        public bool IsObjectBehindRenderer
        {
            get;
            private set;
        }

        private int ObjectParameter
        {
            get;
            set;
        }

        private Dictionary<AreaObjectCode, Action> RenderCommands
        {
            get;
        }

        /// <summary>
        /// Clears all buffers in <see cref="LengthBuffer"/>. This should be
        /// called every time a new screen is going to be rendered.
        /// </summary>
        public void ResetBuffer()
        {
            for (var i = BufferSize; --i >= 0;)
            {
                LengthBuffer[i] = -1;
            }
        }

        /// <summary>
        /// Parses the area data for object at <see cref="CurrentRenderingX"/>.
        /// The result is stored to <see cref="TileBuffer"/>.
        /// </summary>
        public void ParseAreaData()
        {
            do
            {
                LoadBufferData();
            }
            while (IsObjectBehindRenderer);
        }

        private void LoadBufferData()
        {
            CurrentBufferIndex = BufferSize - 1;

            do
            {
                DecodeBufferData();
            }
            while (MoveToNextBuffer());
        }

        private void DecodeBufferData()
        {
            // Return to the current area index if a buffer changed it at any
            // time.
            AreaDataIndex = StoredAreaDataIndex;

            IsObjectBehindRenderer = false;

            if (IsRenderableObject())
            {
                // If we have an object to render, then let's render it.
                DecodeAreaData();
            }
            else
            {
                // Go to next object if current one cannot be rendered.
                IncrementAreaDataIndex();
            }
        }

        /// <summary>
        /// Gets a value that determines whether we can decode the current
        /// object or if we should instead read the next one.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if there is an object in the buffer that can
        /// be rendered or if there is no more area data to parse; otherwise,
        /// <see langword="false"/>.
        /// </returns>
        private bool IsRenderableObject()
        {
            // If we're at the end of the array, attempt to decode any buffer
            // objects. Or, if there is a currently set buffer object, decode
            // it right away.
            if (IsEndOfArea || CurrentBufferEnabled)
            {
                return true;
            }

            // Increment screen if we encounter a screen flag.
            if (CurrentObjectCommand.ScreenFlag && !IsScreenJumpSet)
            {
                CurrentObjectScreen++;
                IsScreenJumpSet = true;
            }

            // Update screen if we encounter a screen skip object.
            if (IsScreenJumpCommand && !IsScreenJumpSet)
            {
                CurrentObjectScreen = CurrentObjectCommand.BaseCommand;
                IsScreenJumpSet = true;
                return false;
            }

            // Object is behind render when its page before rendering page.
            IsObjectBehindRenderer = CurrentObjectScreen < CurrentRenderingScreen;

            // Decode area data of object is on or in front of renderer.
            return !IsObjectBehindRenderer;
        }

        /// <summary>
        /// Update the area data index and resets <see cref="IsScreenJumpSet"/>
        /// for the next object.
        /// </summary>
        private void IncrementAreaDataIndex()
        {
            StoredAreaDataIndex++;
            IsScreenJumpSet = false;
        }

        /// <summary>
        /// Moves to next buffer index and updates the length of the current
        /// buffer it is enabled.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if there is another buffer to read;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        private bool MoveToNextBuffer()
        {
            if (CurrentBufferEnabled)
            {
                CurrentBufferObjectWidth--;
            }

            return --CurrentBufferIndex >= 0;
        }

        private void DecodeAreaData()
        {
            // Render object in buffer if we have one.
            if (CurrentBufferEnabled)
            {
                AreaDataIndex = CurrentBufferObjectIndex;
            }

            // Do not render if end of area or if we had a screen jump command.
            if (IsEndOfArea || IsScreenJumpCommand)
            {
                return;
            }

            // Do not render object at wrong coordinate.
            if (!IsObjectAtRenderCoordinate())
            {
                return;
            }

            // Get the render command for the specific object.
            if (RenderCommands.TryGetValue(CurrentObjectCode, out var command))
            {
                command();
            }
        }

        private bool IsObjectAtRenderCoordinate()
        {
            // If we're rendering a buffer object, then it is guaranteed that
            // it is at the rendering coordinate.
            if (CurrentBufferEnabled)
            {
                return true;
            }

            // Do not render objects on wrong screen.
            if (CurrentObjectScreen != CurrentRenderingScreen)
            {
                return false;
            }

            // Render object if it is on the rendering X-coordinate.
            if (CurrentObjectCommand.X == CurrentRenderingScreenX)
            {
                // Save this object to the current buffer.
                CurrentBufferObjectIndex = AreaDataIndex;

                // We can now start reading at the next object.
                IncrementAreaDataIndex();
                return true;
            }

            return false;
        }

        private void InitSingleTileRow(int tile)
        {
            TrySetCurrentBufferObjectWidth();
            RenderSingleTile(tile);
        }

        private void RenderSingleTile(int tile)
        {
            RenderTileColumn(tile, CurrentObjectCommand.Y, 0);
        }

        private void RenderTileColumn(int tile)
        {
            (var y, var height) = GetObjectColumnProperties();
            RenderTileColumn(tile, y, height);
        }

        private void RenderTileColumn(int tile, int y, int extraHeight)
        {
            for (; extraHeight-- >= 0 && y < 0x0D; y++)
            {
                if (CanWriteTile(tile, TileBuffer[y]))
                {
                    TileBuffer[y] = tile;
                }
            }
        }

        private bool CanWriteTile(int tile, int currentTile)
        {
            switch (currentTile)
            {
                case 0:
                    return true;

                case 0x1B:
                case 0x1E:
                case 0x46:
                case 0x4A:
                    return false;

                case 0x56:
                case 0x57:
                    return tile != 0x50;

                default:
                    return currentTile < 0xE7;
            }
        }

        private bool TrySetCurrentBufferObjectWidth()
        {
            return TrySetCurrentBufferObjectWidth(
                CurrentBufferObject.Parameter);
        }

        private bool TrySetCurrentBufferObjectWidth(int width)
        {
            if (CurrentBufferEnabled)
            {
                return false;
            }

            CurrentBufferObjectWidth = width;
            return true;
        }

        private (int y, int parameter) GetObjectColumnProperties()
        {
            return (
                CurrentBufferObject.Y,
                CurrentBufferObject.Parameter);
        }

        private void Pipe()
        {
            TrySetCurrentBufferObjectWidth(1);
            (var y, var height) = GetObjectColumnProperties();
            height &= 7;

            var index = CurrentBufferObjectWidth;
            if (CurrentObjectCode == AreaObjectCode.UnenterablePipe)
            {
                index += 4;
            }

            TileBuffer[y] = Rom.ReadByte(PipeCapTileAddress + index);
            RenderTileColumn(
                Rom.ReadByte(PipeTileAddress + index),
                y + 1,
                height - 1);
        }

        private void AreaSpecificPlatform()
        {
            switch (CurrentHeader.MiscPlatformType)
            {
                case AreaPlatformType.Trees:
                    RenderTreePlatform();
                    break;

                case AreaPlatformType.Mushrooms:
                    RenderMushroomPlatform();
                    break;

                case AreaPlatformType.CloudGround:
                    RenderCloudPlatform();
                    break;

                case AreaPlatformType.BulletBillTurrets:
                    RenderBulletBillTurrets();
                    break;
            }
        }

        private void RenderTreePlatform()
        {
            var (y, width) = GetObjectColumnProperties();
            if (CurrentBufferObjectWidth == 0)
            {
                RenderSingleTile(0x1C);
            }
            else if (CurrentBufferObjectWidth > 0)
            {
                RenderTreePlatformColumn(y, CurrentBufferObjectWidth);
            }
            else
            {
                CurrentBufferObjectWidth = width;
                if (CurrentRenderingX == 0)
                {
                    RenderTreePlatformColumn(y, width);
                }
                else
                {
                    RenderTileColumn(0x1A, y, 0);
                }
            }
        }

        private void RenderTreePlatformColumn(int y, int width)
        {
            TileBuffer[y++] = 0x1B;
            if (--width == 0)
            {
                if (ObjectHasSpecialProperties)
                {
                    ObjectHasSpecialProperties = false;
                    TileBuffer[y] = 0x47;
                    RenderStemColumn(0x4B, y);
                }
                else
                {
                    ObjectHasSpecialProperties = false;
                    TileBuffer[y] = 0x48;
                    RenderStemColumn(0x4C, y);
                }
            }
            else
            {
                if (ObjectHasSpecialProperties)
                {
                    TileBuffer[y] = 0x46;
                    RenderStemColumn(0x4A, y);
                }
                else
                {
                    ObjectHasSpecialProperties = true;
                    TileBuffer[y] = 0x45;
                    RenderStemColumn(0x49, y);
                }
            }
        }

        private void RenderStemColumn(int tile, int y)
        {
            RenderTileColumn(tile, y + 1, 0x0F);
        }

        private void RenderMushroomPlatform()
        {
            (var y, var width) = GetObjectColumnProperties();
            if (TrySetCurrentBufferObjectWidth(width))
            {
                ObjectSpecialCoordinate =
                    CurrentBufferObjectWidth >> 1;

                RenderSingleTile(0x1D);
            }
            else
            {
                if (CurrentBufferObjectWidth != 0)
                {
                    RenderSingleTile(0x1E);
                    if (CurrentBufferObjectWidth == ObjectSpecialCoordinate)
                    {
                        TileBuffer[++y] = 0x4F;
                        RenderStemColumn(0x50, y);
                    }
                }
                else
                {
                    RenderSingleTile(0x1F);
                }
            }
        }

        private void RenderCloudPlatform()
        {
        }

        private void RenderBulletBillTurrets()
        {
            (var y, var height) = GetObjectColumnProperties();
            TileBuffer[y++] = 0x6C;
            if (--height >= 0)
            {
                TileBuffer[y++] = 0x6D;
                RenderTileColumn(0x6E, y, --height);
            }
        }

        private void RowOfBricks()
        {
            var index = AreaObjectRenderer.IsCloudPlatform
                ? 4
                : (int)AreaType;

            var tile = Rom.ReadByte(BrickLineTileAddress + index);
            InitSingleTileRow(tile);
        }

        private void RowOfStones()
        {
            var tile = Rom.ReadByte(StoneLineTileAddress + (int)AreaType);

            InitSingleTileRow(tile);
        }

        private void RowOfCoins()
        {
            var tile = Rom.ReadByte(CoinLineTileAddress + (int)AreaType);

            InitSingleTileRow(tile);
        }

        private void ColumnOfBricks()
        {
            var tile = Rom.ReadByte(BrickLineTileAddress + (int)AreaType);

            RenderTileColumn(tile);
        }

        private void ColumnOfStones()
        {
            var tile = Rom.ReadByte(StoneLineTileAddress + (int)AreaType);

            RenderTileColumn(tile);
        }

        private void Hole()
        {
            TrySetCurrentBufferObjectWidth();
            RenderTileColumn(0, 0x08, 0x0F);
        }

        private void BalanceHorizontalRope()
        {
            var index = TrySetCurrentBufferObjectWidth()
                ? 0
                : CurrentBufferObjectWidth != 0
                ? 1
                : 2;

            var tile = Rom.ReadByte(PulleyRopeTileAddress + index);
            TileBuffer[0] = tile;
        }

        private void HighBridge()
        {
            Bridge(6);
        }

        private void MidBridge()
        {
            Bridge(7);
        }

        private void LowBridge()
        {
            Bridge(9);
        }

        private void Bridge(int y)
        {
            TrySetCurrentBufferObjectWidth();
            if (CurrentBufferObjectWidth != 0)
            {
                if (!ObjectHasSpecialProperties)
                {
                    ObjectHasSpecialProperties = true;
                    RenderBridge(0x0E);
                }
                else
                {
                    RenderBridge(0x0D);
                }
            }
            else
            {
                ObjectHasSpecialProperties = false;
                RenderBridge(0x0F);
            }

            void RenderBridge(int tile)
            {
                TileBuffer[y++] = tile;
                RenderTileColumn(0x6B, y, 0);
            }
        }

        private void HoleWithWaterOrLava()
        {
            TrySetCurrentBufferObjectWidth();
            var y = AreaType == AreaType.Castle ? 0x0B : 0x0A;
            var tile = Rom.ReadByte(WaterSurfaceTileAddress + (int)AreaType);
            TileBuffer[y++] = tile;

            tile = Rom.ReadByte(
                WaterSurfaceTileAddress + 4 + ((int)AreaType >> 1));

            RenderTileColumn(tile, y, 1);
        }

        private void HighRowOfCoinBlocks()
        {
            RowOfCoinBlocks(3);
        }

        private void LowRowOfCoinBlocks()
        {
            RowOfCoinBlocks(7);
        }

        private void RowOfCoinBlocks(int y)
        {
            TrySetCurrentBufferObjectWidth();
            TileBuffer[y] = 0xE7;
        }

        private void ItemBlock()
        {
            var tile = Rom.ReadByte(SingleTileObjectAddress
                + CurrentObjectCommand.Parameter);

            TileBuffer[CurrentBufferObject.Y] = tile;
        }

        private void AreaTypeBlock()
        {
            var index = CurrentBufferObject.Parameter;
            if (AreaType == AreaType.Water)
            {
                index += 5;
            }

            var tile = Rom.ReadByte(BlockItemTileAddress + index);
            TileBuffer[CurrentBufferObject.Y] = tile;
        }

        private void SidewaysPipe()
        {
            TrySetCurrentBufferObjectWidth();
            var y = CurrentBufferObject.Y;
            TileBuffer[y++] = 0x75;
            TileBuffer[y] = 0x76;
        }

        private void FireBarBlock()
        {
            RenderSingleTile(0xFC);
        }

        private void SpringBoard()
        {
            var y = CurrentBufferObject.Y;
            TileBuffer[y++] = 0x6F;
            TileBuffer[y] = 0x70;
        }

        private void JPipe()
        {
            var firstTile = TrySetCurrentBufferObjectWidth(3);
            var y = 0x0A - 1;
            var index = CurrentBufferObjectWidth;
            var tile = Rom.ReadByte(JPipeTiles1Address + index);
            if (tile != 0)
            {
                RenderTileColumn(tile, y + 1, 0);
                firstTile = false;
            }

            tile = Rom.ReadByte(JPipeTiles2Address + index);
            TileBuffer[y++] = tile;

            tile = Rom.ReadByte(JPipeTiles3Address + index);
            TileBuffer[y++] = tile;

            if (firstTile)
            {
                return;
            }

            do
            {
                TileBuffer[y] = 0;
            }
            while (--y >= 0);

            tile = Rom.ReadByte(JPipeTiles4Address + index);
            TileBuffer[7] = tile;
        }

        private void FlagPole()
        {
            TileBuffer[0] = 0x28;
            RenderTileColumn(0x29, 1, 8);
            TileBuffer[0x0A] = 0x64;
        }

        private void BowserAxe()
        {
            BowserBridge(0xFD, 6);
        }

        private void RopeForAxe()
        {
            BowserBridge(0x10, 7);
        }

        private void BowserBridge()
        {
            TrySetCurrentBufferObjectWidth(0x0C);
            BowserBridge(0x8D, 8);
        }

        private void BowserBridge(int tile, int y)
        {
            RenderTileColumn(tile, y, 0);
        }

        private void ForegroundChange()
        {
            var header = CurrentHeader;
            header.ForegroundScenery = (ForegroundScenery)
                (CurrentBufferObject.BaseCommand & 7);

            CurrentHeader = header;
        }

        private void TerrainModifier()
        {
            var header = CurrentHeader;
            header.TerrainMode = (TerrainMode)
                (CurrentBufferObject.BaseCommand & 0x0F);

            header.BackgroundScenery = (BackgroundScenery)
                (CurrentBufferObject.BaseCommand >> 4);

            CurrentHeader = header;
        }

        private void RopeForLift()
        {
            RenderTileColumn(0x40, 0, 0x0F);
        }

        private void PulleyRope()
        {
            RenderTileColumn(0x44, 1, 0x0F);
            (var y, var height) = GetObjectColumnProperties();

            RenderTileColumn(0x40, 1, height);
        }

        private void EmptyTile()
        {
        }

        private void Castle()
        {
            var parameter = CurrentBufferObject.Parameter;
            var y = parameter;
            TrySetCurrentBufferObjectWidth(y == 0 ? 9 : y + 1);
            var index = CurrentBufferObjectWidth;
            for (var i = 0x16; y != 0x0B;)
            {
                var tile = Rom.ReadByte(CastleTileAddress + index);
                TileBuffer[y++] = tile;
                if (i != 0)
                {
                    index += 0x0A;
                    i--;
                }
            }

            if (CurrentBufferObject.Parameter != 0
                && CurrentBufferObjectWidth == 0)
            {
                TileBuffer[0x0A] = 0;
            }

            if (CurrentRenderingScreen != 0 && TileBuffer[0x0B] == 0x56)
            {
                TileBuffer[0x0B] = 0xFB;
            }
        }

        private void CastleCeilineCap()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            var firstTile = TileBuffer[0];
            var secondTile = firstTile + (firstTile == 0x65 ? +1 : -1);
            do
            {
                TileBuffer[y] = (y & 1) == 0 ? firstTile : secondTile;
            }
            while (--CurrentBufferObjectWidth >= 0);
        }

        private void StoneStairs()
        {
            (var y, var height) = GetObjectColumnProperties();
            if (TrySetCurrentBufferObjectWidth(height))
            {
                ObjectParameter = 9;
            }

            y = Rom.ReadByte(StoneStairYAddress + --ObjectParameter);
            height = Rom.ReadByte(StoneStairHeightAddress + ObjectParameter);
            RenderTileColumn(0x64, y, height);
        }

        private void CastleDescendingSteps()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            if (CurrentBufferObjectWidth == 0)
            {
                TileBuffer[y++] = 0xF3;
                if (TileBuffer[y] == 0)
                {
                    TileBuffer[y++] = 0xF4;
                }
            }
            else if (TileBuffer[y] == 0)
            {
                TileBuffer[y] = 0xF5;
            }

            do
            {
                if (TileBuffer[y] == 0)
                {
                    TileBuffer[y] = 0xF6;
                }
            }
            while (++y != 0x0D);
        }

        private void CastleRectangularCeilingTiles()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            TileBuffer[y++] = 0x67;
            while (y < 0x0C && TileBuffer[++y] == 0x65 && TileBuffer[y] == 0x66)
            {
                TileBuffer[y++] = 0x67;
            }
        }

        private void CastleFloorRightEdge()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            TileBuffer[y++] = 0xF7;
            while (y != 0x0D && TileBuffer[y] != 0xEB)
            {
                TileBuffer[y++] = 0xF8;
            }
        }

        private void CastleFloorLeftEdge()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            if (TileBuffer[y] != 0xFC)
            {
                TileBuffer[y] = 0xF9;
            }

            while (++y != 0x0D && TileBuffer[y] != 0xF0)
            {
                TileBuffer[y] = 0xFA;
            }
        }

        private void CastleFloorLeftWall()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            if (CurrentBufferObjectWidth == 0)
            {
                TileBuffer[y++] = 2;
                TileBuffer[y] = 0xED;
            }
            else
            {
                TileBuffer[y] = TileBuffer[y] == 0x68 ? 0xEE : 0xEB;
                TileBuffer[++y] = 0xEC;
                TileBuffer[++y] = 0x69;
            }
        }

        private void CastleFloorRightWall()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            if (CurrentBufferObjectWidth == 0)
            {
                TileBuffer[y] = TileBuffer[y] == 0x68 ? 0xF2 : 0xF0;
                TileBuffer[++y] = 0xF1;
                TileBuffer[++y] = 0x69;
                while (++y != 0x0D)
                {
                    TileBuffer[y] = 0xEF;
                }
            }
            else
            {
                TileBuffer[y++] = 3;
                TileBuffer[y] = 0xEF;
            }
        }

        private void VerticalSeaBlocks()
        {
            TrySetCurrentBufferObjectWidth();
            (var y, var height) = GetObjectColumnProperties();
            do
            {
                TileBuffer[y++] = 0x71;
            }
            while (--CurrentBufferObjectWidth >= 0);
        }

        private void ExtendableJPipe()
        {
            TrySetCurrentBufferObjectWidth(3);
            (var y, var height) = GetObjectColumnProperties();
            height -= 2;
            y = height + 1;
            var index = CurrentBufferObjectWidth;
            var tile = Rom.ReadByte(JPipeTiles5Address + index);
            if (tile != 0)
            {
                RenderTileColumn(tile, 0, height);
            }

            tile = Rom.ReadByte(JPipeTiles6Address + index);
            TileBuffer[y++] = tile;

            tile = Rom.ReadByte(JPipeTiles7Address + index);
            TileBuffer[y] = tile;
        }

        private void VerticalClimbingObject()
        {
            (var y, var height) = GetObjectColumnProperties();
            RenderTileColumn(0x77, 2, height);
        }
    }
}
