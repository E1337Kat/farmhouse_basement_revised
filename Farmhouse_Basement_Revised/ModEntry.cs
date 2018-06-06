using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Locations;
using xTile;

namespace Farmhouse_Basement_Revised
{
    class ModEntry : Mod
    {
        /*********
        ** Public methods
        *********/
        /// <summary>The mod entry point, called after the mod is first loaded.</summary>
        /// <param name="helper">Provides simplified APIs for writing mods.</param>
        public override void Entry(IModHelper helper)
        {
            // the game clears locations when loading the save, so do it after the save loads
            SaveEvents.AfterLoad += AfterSaveLoaded;
            

            InputEvents.ButtonPressed += this.InputEvents_ButtonPressed;
            
            helper.Content.AssetEditors.Add(new FarmhouseBasementUpgradeDialogueEditor());
            helper.Content.AssetLoaders.Add(new FarmhouseUpgrade4Loader());

            
        }


        /*********
        ** Private methods
        *********/
        /// <summary>The method invoked when the player presses a controller, keyboard, or mouse button.</summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="e">The event data.</param>
        private void InputEvents_ButtonPressed(object sender, EventArgsInput e)
        {
            if (Context.IsWorldReady) // save is loaded
            {
                this.Monitor.Log($"{Game1.player.Name} pressed {e.Button}.");
            }
        }

        private void AfterSaveLoaded(object sender, EventArgs args)
        {
            // load a map.tbin file from your mod's folder.
            Map farmHouse4Map = this.Helper.Content.Load<Map>("assets/farmhouse3.tbin", ContentSource.ModFolder);
            Map farmHouse4MarriageMap = this.Helper.Content.Load<Map>("assets/farmhouse3_marriage.tbin", ContentSource.ModFolder);

            // create a map asset key 
            string farmHouse4MapAssetKey = this.Helper.Content.GetActualAssetKey("FarmHouse3.xnb", ContentSource.ModFolder);
            string farmHouse4MarriageMapAssetKey = this.Helper.Content.GetActualAssetKey("FarmHouse3_marriage.xnb", ContentSource.ModFolder);

            // add the new location
            GameLocation farmhouse3 = new GameLocation(farmHouse4MapAssetKey, "FarmHouse") { IsOutdoors = false, IsFarm = false };
            GameLocation farmhouse3marriage = new GameLocation(farmHouse4MarriageMapAssetKey, "FarmHouse") { IsOutdoors = false, IsFarm = false };
            Game1.locations.Add(farmhouse3);
            Game1.locations.Add(farmhouse3marriage);


            // Attempt to add custom ScienceHouse code instead of generice Game Location for changes.
            Game1.locations.Insert(15, (GameLocation)new ScienceHouse("Maps\\ScienceHouse", "ScienceHouse"));
            Game1.locations[15].addCharacter(new NPC(new AnimatedSprite("Characters\\Maru", 0, 16, 32), new Vector2(128f, 256f), "ScienceHouse", 3, "Maru", true, (Dictionary<int, int[]>)null, Game1.content.Load<Texture2D>("Portraits\\Maru")));
            Game1.locations[15].addCharacter(new NPC(new AnimatedSprite("Characters\\Robin", 0, 16, 32), new Vector2(1344f, 256f), "ScienceHouse", 1, "Robin", false, (Dictionary<int, int[]>)null, Game1.content.Load<Texture2D>("Portraits\\Robin")));
            Game1.locations[15].addCharacter(new NPC(new AnimatedSprite("Characters\\Demetrius", 0, 16, 32), new Vector2(1216f, 256f), "ScienceHouse", 1, "Demetrius", false, (Dictionary<int, int[]>)null, Game1.content.Load<Texture2D>("Portraits\\Demetrius")));

            this.Monitor.Log("New ScienceHouse location loaded.");
        }
    }
}
