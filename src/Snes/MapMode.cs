// <copyright file="MapMode.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

public enum MapMode
{
    Mode20 = 0x20,
    Mode21 = 0x21,
    Reserved = 0x22,
    Mode23Sa1 = 0x23,
    Mode25 = 0x25,
    Mode20Fast = 0x30,
    Mode21Fast = 0x31,
    Mode25Fast = 0x35,
}
