﻿// <copyright file="StartTime.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under GNU
//     Affero General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
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
}
