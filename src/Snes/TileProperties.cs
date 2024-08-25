// <copyright file="TileProperties.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

using System;

[Flags]
public enum TileProperties
{
    None = 0,
    Invert = 1 << 8,
    Red = 1 << 9,
    Green = 1 << 10,
    Blue = 1 << 11,
    Yellow = Red | Green,
    Magenta = Red | Blue,
    Cyan = Green | Blue,
    White = Red | Green | Blue,
    Transparent = 1 << 12,
}
