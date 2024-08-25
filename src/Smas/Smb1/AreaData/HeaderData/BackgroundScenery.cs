// <copyright file="BackgroundScenery.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.HeaderData;

/// <summary>
/// The layer 1 scenery to draw for the area.
/// </summary>
public enum BackgroundScenery
{
    /// <summary>
    /// Use no scenery.
    /// </summary>
    None,

    /// <summary>
    /// Clouds in sky.
    /// </summary>
    Clouds,

    /// <summary>
    /// Mountains and hills on ground.
    /// </summary>
    MountainsAndHills,

    /// <summary>
    /// Fences and trees on ground.
    /// </summary>
    FenceAndTrees,
}
