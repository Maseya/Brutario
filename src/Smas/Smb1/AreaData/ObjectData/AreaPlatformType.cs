// <copyright file="AreaPlatformType.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.ObjectData;

/// <summary>
/// The platform type to use for the miscellaneous platform object.
/// </summary>
public enum AreaPlatformType
{
    /// <summary>
    /// Green tree platforms.
    /// </summary>
    Trees,

    /// <summary>
    /// Orange mushroom platforms.
    /// </summary>
    Mushrooms,

    /// <summary>
    /// Vertical bullet bill shooters.
    /// </summary>
    BulletBillTurrets,

    /// <summary>
    /// Cloud as ground tiles.
    /// </summary>
    CloudGround,
}
