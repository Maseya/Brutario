namespace Brutario.Tests
{
    using System;
    using Xunit;

    public class PaletteEditorPipelineTests
    {
        [Fact]
        public void Test1()
        {
        }

        private class MockPaletteEditor : Brutario.Smb1.ISmb1PaletteEditor
        {
            public void LoadPalette(int areaNumber)
            {
            }
        }
    }
}
