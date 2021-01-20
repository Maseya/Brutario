using System;
using System.Collections.Generic;
using System.IO;

namespace Brutario
{
    public class AllStarsRomFile
    {
        public AllStarsRomFile(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Rom = new RomIO(File.ReadAllBytes(path));
            Smb1RomData = new Smb1.GameData(Rom);
        }

        public string Path
        {
            get;
        }

        public bool HasUnsavedChanges
        {
            get;
            set;
        }

        public RomIO Rom
        {
            get;
        }

        public Smb1.GameData Smb1RomData
        {
            get;
        }

        public void Save()
        {
        }
    }
}
