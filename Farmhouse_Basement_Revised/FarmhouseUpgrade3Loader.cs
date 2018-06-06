using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmhouse_Basement_Revised
{
    class FarmhouseUpgrade3Loader : IAssetLoader
    {
        public bool CanLoad<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Maps/FarmHouse3");
        }

        public T Load<T>(IAssetInfo asset)
        {
            throw new NotImplementedException();
        }
    }
}
