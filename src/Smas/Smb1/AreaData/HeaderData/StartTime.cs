// <copyright file="StartTime.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Smas.Smb1.AreaData.HeaderData;

/// <summary>
/// The starting time when the player enters the area.
/// </summary>
public enum StartTime
{
    /// <summary>
    /// No time. Use this for auto-walk areas.
    /// </summary>
    None = 0,

    /// <summary>
    /// 400 game seconds.
    /// </summary>
    Time400,

    /// <summary>
    /// 300 game seconds.
    /// </summary>
    Time300,

    /// <summary>
    /// 200 game seconds.
    /// </summary>
    Time200,
}
