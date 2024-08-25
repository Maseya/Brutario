﻿// <copyright file="StartYPosition.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.HeaderData;

/// <summary>
/// The player's starting Y position when entering the area.
/// </summary>
/// <remarks>
/// The names specify the absolute screen coordinates, but the descriptions specify the
/// relative tile coordinates.
/// </remarks>
public enum StartYPosition
{
    /// <summary>
    /// Y = -1
    /// </summary>
    Y00,

    /// <summary>
    /// Y = -1 entering from another area.
    /// </summary>
    Y20,

    /// <summary>
    /// Y = 10
    /// </summary>
    YB0,

    /// <summary>
    /// Y = 4
    /// </summary>
    Y50,

    /// <summary>
    /// Y = -1
    /// </summary>
    /// <remarks>
    /// It is not yet clear how this is different from <see cref="Y00"/>.
    /// </remarks>
    Alt1Y00,

    /// <summary>
    /// Y = -1
    /// </summary>
    /// <remarks>
    /// It is not yet clear how this is different from <see cref="Y00"/>.
    /// </remarks>
    Alt2Y00,

    /// <summary>
    /// Y = 10 (autowalk)
    /// </summary>
    PipeIntroYB0,

    /// <summary>
    /// Y = 10 (autowalk)
    /// </summary>
    /// <remarks>
    /// It is not yet clear how this is different form <see cref="PipeIntroYB0"/>.
    /// </remarks>
    AltPipeIntroYB0,
}
