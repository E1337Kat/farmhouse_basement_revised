using StardewModdingAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmhouse_Basement_Revised
{
    class FarmhouseBasementUpgradeDialogueEditor : IAssetEditor
    {
        public string upgrade4Question = "It looks like there used to be a basement in your house. I can stablize it, and add a bathroom if you want. It will cost 75,000g and you'll also need to provide me with 300 stones. Are you interested?";
        public string notEnoughStone = "Sorry... You have the money, but I also need 300 stones.";

        /// <summary>Get whether this instance can edit the given asset.</summary>
        /// <param name="asset">Basic metadata about the asset being loaded.</param>
        public bool CanEdit<T>(IAssetInfo asset)
        {
            return asset.AssetNameEquals("Strings/Locations"); // Add new string
        }

        /// <summary>Edit a matched asset.</summary>
        /// <param name="asset">A helper which encapsulates metadata about an asset and enables changes to it.</param>
        public void Edit<T>(IAssetData asset)
        {
            if (asset.AssetNameEquals("Strings.Locations")) {
                IDictionary<string, string> dialogue = asset.AsDictionary<string, string>().Data;

                // Add new dialogue for the offer
                dialogue
                    .Add(new KeyValuePair<string, string>(
                        "ScienceHouse_Carpenter_UpgradeHouse4",
                        upgrade4Question
                        ));

                //Add new dialogue for when there is not enough stone.
                dialogue
                    .Add(new KeyValuePair<string, string>(
                        "ScienceHouse_Carpenter_NotEnoughWood4", 
                        notEnoughStone
                        ));
                
            }
        }
    }
}
