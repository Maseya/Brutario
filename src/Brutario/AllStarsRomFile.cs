// <copyright file="AllStarsRomFile.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario
{
    using System;
    using System.IO;
    using Pointers = Smb1.Pointers;

    public class AllStarsRomFile
    {
        public AllStarsRomFile(string path)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Rom = new RomIO(File.ReadAllBytes(path));
            Smb1RomData = new Smb1.GameData(
                Rom,
                GetPointers(Rom, System.IO.Path.GetFileName(path)));
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
            File.WriteAllBytes(Path, Rom.Data);
            HasUnsavedChanges = false;
        }

        private static Pointers GetPointers(RomIO rom, string name)
        {
            if (name.Contains("(U)"))
            {
                if (name.Contains("Super Mario Bros"))
                {
                    return Pointers.UsaSmb1;
                }
                if (name.Contains("World"))
                {
                    return Pointers.UsaPlusW;
                }
                return Pointers.Usa;
            }
            if (name.Contains("(E)"))
            {
                if (name.Contains("World"))
                {
                    return Pointers.EuPlusW;
                }
                return Pointers.Eu;
            }
            if (name.Contains("(J)"))
            {
                if (name.Contains("1.0"))
                {
                    return Pointers.Jp10;
                }
                if (name.Contains("1.1"))
                {
                    return Pointers.Jp11;
                }
            }

            return Pointers.Usa;
        }
    }
}
