// <copyright file="TilesetEventArgs.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1;
using System;
using System.ComponentModel;

public class TilesetEventArgs : EventArgs
{
    private Tileset _tileset;

    public TilesetEventArgs(Tileset tileset)
    {

        Tileset = tileset;
    }

    public Tileset Tileset
    {
        get
        {
            return _tileset;
        }

        set
        {
            if (!Enum.IsDefined(value))
            {
                throw new InvalidEnumArgumentException(
                    nameof(Tileset),
                    (int)value,
                    typeof(Tileset));
            }

            _tileset = value;
        }
    }
}
