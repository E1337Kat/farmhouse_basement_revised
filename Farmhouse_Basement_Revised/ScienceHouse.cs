using StardewValley;
using StardewModdingAPI;
using xTile.Dimensions;
using System;

namespace Farmhouse_Basement_Revised
{
    class ScienceHouse : GameLocation
    {
        /// <summary>
        /// The main mod object.
        /// </summary>
        private ModEntry mod = null;

        /// <summary>
        /// Default constructor
        /// </summary>
        public ScienceHouse() { }

        /// <summary>
        /// Standard constructor
        /// </summary>
        /// <param name="mapPath">The path to the map for this location.</param>
        /// <param name="name">The name of this location.</param>
        public ScienceHouse(string mapPath, string name)
            : base(mapPath, name)
        { }

        /// <summary>
        /// Constructor that operates like the base constructor, but includes the ModEntry object for logging.
        /// </summary>
        /// <param name="mapPath">The path to the map for this location.</param>
        /// <param name="name">The name of this location.</param>
        /// <param name="mod">The main mod object.</param>
        public ScienceHouse(string mapPath, string name, ModEntry mod)
            : base(mapPath, name)
        {
            this.mod = mod;
            mod.Monitor.Log("Loading custom ScienceHouse.", LogLevel.Debug);
        }

        /// <summary>
        /// I dunno.
        /// </summary>
        protected override void initNetFields()
        {
            base.initNetFields();
        }

        /// <summary>
        /// Intercepts the answerDialogueAction code in GameLocation to implement more house upgrade choices.
        /// </summary>
        /// <param name="questionAndAnswer">The question and answer to ask.</param>
        /// <param name="questionParams">The parameters for the question if needed.</param>
        /// <returns>parent if not needed, true otherwise</returns>
        public override bool answerDialogueAction(string questionAndAnswer, string[] questionParams)
        {
            if (questionAndAnswer == null || !questionAndAnswer.Equals("carpenter_Upgrade") || !questionAndAnswer.Equals("upgrade_Yes"))
                return base.answerDialogueAction(questionAndAnswer, questionParams);
            if (mod != null) 
                mod.Monitor.Log("Successfully intercepted upgrade question.", LogLevel.Debug);
            switch (questionAndAnswer) {
                case "carpenter_Upgrade":
                    this.houseUpgradeOffer();
                    break;
                case "upgrade_Yes":
                    this.houseUpgradeAccept();
                    break;
            }
            return true;
        }

        /// <summary>
        /// Checks the given action. Currently does not work as Game2.getHook() does not work.
        /// </summary>
        /// <param name="tileLocation">Tile location to check action on.</param>
        /// <param name="viewport">Current viewport</param>
        /// <param name="who">Which Farmer</param>
        /// <returns>true if checkaction works correctly.</returns>
        public override bool checkAction(Location tileLocation, Rectangle viewport, Farmer who)
        {
            if (Game1.player.houseUpgradeLevel > 2 ) {
                if (mod != null)
                    mod.Monitor.Log("Successfully intercepted checkAction.", LogLevel.Debug);

                // @TODO: Fix Game2.getHook()
                return Game2.getHook().OnGameLocation_CheckAction(this, tileLocation, viewport, who, delegate {
                    if (who.IsLocalPlayer && who.currentUpgrade != null && name.Equals("Farm") && tileLocation.Equals(new Location((int)(who.currentUpgrade.positionOfCarpenter.X + 32f) / 64, (int)(who.currentUpgrade.positionOfCarpenter.Y + 32f) / 64))) {
                        if (who.currentUpgrade.daysLeftTillUpgradeDone == 1) {
                            Game1.drawDialogue(Game1.getCharacterFromName("Robin", false), Game1.content.LoadString("Data\\ExtraDialogue:Farm_RobinWorking_ReadyTomorrow"));
                        } else {
                            Game1.drawDialogue(Game1.getCharacterFromName("Robin", false), Game1.content.LoadString("Data\\ExtraDialogue:Farm_RobinWorking" + (Game1.random.Next(2) + 1)));
                        }
                    }
                    Microsoft.Xna.Framework.Vector2 vector = new Microsoft.Xna.Framework.Vector2((float)tileLocation.X, (float)tileLocation.Y);
                    xTile.ObjectModel.PropertyValue propertyValue = null;
                    xTile.Tiles.Tile tile = map.GetLayer("Buildings").PickTile(new Location(tileLocation.X * 64, tileLocation.Y * 64), viewport.Size);
                    tile?.Properties.TryGetValue("Action", out propertyValue);
                    if (propertyValue != null && (currentEvent != null || isCharacterAtTile(vector + new Microsoft.Xna.Framework.Vector2(0f, 1f)) == null)) {
                        return this.performAction(propertyValue, who, tileLocation);
                    }
                    return false;
                });
            }
            return base.checkAction(tileLocation, viewport, who);
        }

        /// <summary>
        /// Performs an action on a given tile. In this case catches the Carpenter action to inject custom code, and returns the base otherwise.
        /// </summary>
        /// <param name="action">What is the action that needs to be checked.</param>
        /// <param name="who">Which Farmer initiated the action.</param>
        /// <param name="tileLocation">Which tile is the action performed at?</param>
        /// <returns></returns>
        public new bool performAction(string action, Farmer who, Location tileLocation) {
            if (action != null && who.IsLocalPlayer) {
                string[] actionParams = action.Split(' ');
                if (actionParams[0] == "Carpenter") {
                    if (who.getTileY() > tileLocation.Y)
                        carpenters(tileLocation);
                }
            }
            if (Game1.player.houseUpgradeLevel > 2 ) {
                
            }
            return base.performAction(action, who, tileLocation);
        }

        /// <summary>
        /// Defines the carpenter loop. This method modifies the vanilla loop to account for an additional house upgrade.
        /// </summary>
        /// <param name="tileLocation">The location that an action should be performed on.</param>
        private void carpenters(Location tileLocation)
        {
            // @TODO: implement proper carpenter code for new upgrade
            // (this is just copy and paster from decompiled source code)
            if (Game1.player.currentUpgrade == null) {
                foreach (NPC character in characters) {
                    if (character.Name.Equals("Robin")) {
                        if (!(Microsoft.Xna.Framework.Vector2.Distance(character.getTileLocation(), new Microsoft.Xna.Framework.Vector2((float)tileLocation.X, (float)tileLocation.Y)) > 3f)) {
                            character.faceDirection(2);
                            if (Game1.player.daysUntilHouseUpgrade < 0 && !Game1.getFarm().isThereABuildingUnderConstruction()) {
                                System.Collections.Generic.List<Response> options = new System.Collections.Generic.List<Response>();
                                options.Add(new Response("Shop", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_Shop")));
                                if (Game1.IsMasterGame) {
                                    if ((int)Game1.player.houseUpgradeLevel < 3) {
                                        options.Add(new Response("Upgrade", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_UpgradeHouse")));
                                    } else if ((Game1.MasterPlayer.mailReceived.Contains("ccIsComplete") || Game1.MasterPlayer.mailReceived.Contains("JojaMember") || Game1.MasterPlayer.hasCompletedCommunityCenter()) && (int)(Game1.getLocationFromName("Town") as StardewValley.Locations.Town).daysUntilCommunityUpgrade <= 0 && !Game1.MasterPlayer.mailReceived.Contains("pamHouseUpgrade")) {
                                        options.Add(new Response("CommunityUpgrade", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_CommunityUpgrade")));
                                    }
                                } else if ((int)Game1.player.houseUpgradeLevel < 2) {
                                    options.Add(new Response("Upgrade", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_UpgradeCabin")));
                                }
                                options.Add(new Response("Construct", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_Construct")));
                                options.Add(new Response("Leave", Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu_Leave")));
                                createQuestionDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_CarpenterMenu"), options.ToArray(), "carpenter");
                            } else {
                                Game1.activeClickableMenu = new StardewValley.Menus.ShopMenu(Utility.getCarpenterStock(), 0, "Robin");
                            }
                        }
                        return;
                    }
                }
                if (Game1.shortDayNameFromDayOfSeason(Game1.dayOfMonth).Equals("Tue")) {
                    Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_RobinAbsent").Replace('\n', '^'));
                }
            }
        }

        /// <summary>
        /// Custom code to determine if an upgrade is needed.
        /// </summary>
        private void houseUpgradeAccept()
        {
            switch ((int)Game1.player.houseUpgradeLevel) {
                case 0:
                    if (Game1.player.Money >= 10000 && Game1.player.hasItemInInventory(388, 450, 0)) {
                        Game1.player.daysUntilHouseUpgrade = 3;
                        Game1.player.Money -= 10000;
                        Game1.player.removeItemsFromInventory(388, 450);
                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
                    } else if (Game1.player.Money < 10000) {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                    } else {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood1"));
                    }
                    break;
                case 1:
                    if (Game1.player.Money >= 50000 && Game1.player.hasItemInInventory(709, 150, 0)) {
                        Game1.player.daysUntilHouseUpgrade = 3;
                        Game1.player.Money -= 50000;
                        Game1.player.removeItemsFromInventory(709, 150);
                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
                    } else if (Game1.player.Money < 50000) {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                    } else {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood2"));
                    }
                    break;
                case 2:
                    if (Game1.player.Money >= 75000 && Game1.player.hasItemInInventory(390, 300, 0)) {
                        Game1.player.daysUntilHouseUpgrade = 3;
                        Game1.player.Money -= 75000;
                        Game1.player.removeItemsFromInventory(390, 300);
                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
                    } else if (Game1.player.Money < 75000) {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                    } else {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_NotEnoughStone4"));
                    }
                    break;
                case 3:
                    if (Game1.player.Money >= 100000) {
                        Game1.player.daysUntilHouseUpgrade = 3;
                        Game1.player.Money -= 100000;
                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
                    } else if (Game1.player.Money < 100000) {
                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
                    }
                    break;
            }
        }

        /// <summary>
        /// Custom code the determine what questions should be asked.
        /// </summary>
        private void houseUpgradeOffer()
        {
            switch ((int)Game1.player.houseUpgradeLevel) {
                case 0:
                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse1")), createYesNoResponses(), "upgrade");
                    break;
                case 1:
                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse2")), createYesNoResponses(), "upgrade");
                    break;
                case 2:
                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse4")), createYesNoResponses(), "upgrade");
                    break;
                case 3:
                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse3")), createYesNoResponses(), "upgrade");
                    break;
            }
        }
    }
}
