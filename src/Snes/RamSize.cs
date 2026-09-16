// <copyright file="RamSize.cs" organization="Maseya">
//     Copyright (c) 2026 spel werdz rite. All rights reserved. Licensed
//     under GNU Affero General Public License. See LICENSE in project
//     root for full license information, or visit
//     https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Maseya.Snes;

public enum RamSize
{
    NoRam = 0,
    Size16Kbit = 0x01,
    Size64Kbit = 0x03,
    Size256Kbit = 0x05,
    Size512Kbit = 0x06,
    Size1Mbit = 0x07,
}
