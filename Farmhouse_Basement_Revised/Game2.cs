using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmhouse_Basement_Revised
{
    class Game2 : StardewValley.Game1
    {
        public static ModHooks getHook()
        {
            return Game1.hooks;
        }
    }
}
