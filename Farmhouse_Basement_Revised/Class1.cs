//using StardewValley;
//using System.Linq;
//using System.Collections.Generic;

//namespace Farmhouse_Basement_Revised
//{
//    class StardewValley
//    {
//        public string lastQuestionKey;

//        private void houseUpgradeAccept()
//        {
//            switch ((int)Game1.player.houseUpgradeLevel) {
//                case 0:
//                    if (Game1.player.Money >= 10000 && Game1.player.hasItemInInventory(388, 450, 0)) {
//                        Game1.player.daysUntilHouseUpgrade = 3;
//                        Game1.player.Money -= 10000;
//                        Game1.player.removeItemsFromInventory(388, 450);
//                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
//                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
//                    } else if (Game1.player.Money < 10000) {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
//                    } else {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood1"));
//                    }
//                    break;
//                case 1:
//                    if (Game1.player.Money >= 50000 && Game1.player.hasItemInInventory(709, 150, 0)) {
//                        Game1.player.daysUntilHouseUpgrade = 3;
//                        Game1.player.Money -= 50000;
//                        Game1.player.removeItemsFromInventory(709, 150);
//                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
//                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
//                    } else if (Game1.player.Money < 50000) {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
//                    } else {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_NotEnoughWood2"));
//                    }
//                    break;
//                case 2:
//                    if (Game1.player.Money >= 75000 && Game1.player.hasItemInInventory(390, 300, 0)) {
//                        Game1.player.daysUntilHouseUpgrade = 3;
//                        Game1.player.Money -= 75000;
//                        Game1.player.removeItemsFromInventory(390, 300);
//                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
//                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
//                    } else if (Game1.player.Money < 75000) {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
//                    } else {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_NotEnoughStone4"));
//                    }
//                    break;
//                case 3:
//                    if (Game1.player.Money >= 100000) {
//                        Game1.player.daysUntilHouseUpgrade = 3;
//                        Game1.player.Money -= 100000;
//                        Game1.getCharacterFromName("Robin", false).setNewDialogue(Game1.content.LoadString("Data\\ExtraDialogue:Robin_HouseUpgrade_Accepted"), false, false);
//                        Game1.drawDialogue(Game1.getCharacterFromName("Robin", false));
//                    } else if (Game1.player.Money < 100000) {
//                        Game1.drawObjectDialogue(Game1.content.LoadString("Strings\\UI:NotEnoughMoney3"));
//                    }
//                    break;
//            }
//        }

//        private void houseUpgradeOffer()
//        {
//            switch ((int)Game1.player.houseUpgradeLevel) {
//                case 0:
//                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse1")), createYesNoResponses(), "upgrade");
//                    break;
//                case 1:
//                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse2")), createYesNoResponses(), "upgrade");
//                    break;
//                case 2:
//                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse4")), createYesNoResponses(), "upgrade");
//                    break;
//                case 3:
//                    createQuestionDialogue(Game1.parseText(Game1.content.LoadString("Strings\\Locations:ScienceHouse_Carpenter_UpgradeHouse3")), createYesNoResponses(), "upgrade");
//                    break;
//            }
//        }

//        public void createQuestionDialogue(string question, Response[] answerChoices, string dialogKey)
//        {
//            lastQuestionKey = dialogKey;
//            Game1.drawObjectQuestionDialogue(question, answerChoices.ToList());
//        }

//        public Response[] createYesNoResponses()
//        {
//            return new Response[2]
//            {
//                new Response("Yes", Game1.content.LoadString("Strings\\Lexicon:QuestionDialogue_Yes")),
//                new Response("No", Game1.content.LoadString("Strings\\Lexicon:QuestionDialogue_No"))
//            };
//        }


//    }
//}
