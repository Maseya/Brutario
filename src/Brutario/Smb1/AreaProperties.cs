// <copyright file="AreaProperties.cs" company="Public Domain">
//     Copyright (c) 2022 Nelson Garcia. All rights reserved. Licensed under
//     GNU Affero General Public License. See LICENSE in project root for full
//     license information, or visit https://www.gnu.org/licenses/#AGPL
// </copyright>

namespace Brutario.Smb1
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class AreaProperties
    {
        public int LevelNumber
        {
            get;
            set;
        }

        public int LevelMenuNumber
        {
            get;
            set;
        }

        public int WorldNumber
        {
            get;
            set;
        }

        public int WorldMenuNumber
        {
            get;
            set;
        }

        public int AreaNumber
        {
            get;
            set;
        }

        public int AreaIndex
        {
            get;
            set;
        }
    }
}
