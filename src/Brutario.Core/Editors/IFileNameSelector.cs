﻿// <copyright file="IFileNameSelector.cs" company="Public Domain">
//     Copyright (c) 2022 spel werdz rite. All rights reserved. Licensed under GNU Affero
//     General Public License. See LICENSE in project root for full license
//     information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Core;

public interface IFileNameSelector
{
    string? FileName { get; set; }

    PromptResult Prompt();
}
