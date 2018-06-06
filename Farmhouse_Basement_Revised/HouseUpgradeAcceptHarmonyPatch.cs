//using Harmony;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection.Emit;
//using System.Text;
//using System.Threading.Tasks;

//namespace Farmhouse_Basement_Revised {
//    [HarmonyPatch(typeof(StardewValley.GameLocation))]
//    [HarmonyPatch("houseUpgradeAccept")]
//    public static class GameLocation_HouseUpgradeAcceptHarmony_Patch { 

//        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) {
//            /* Algorithm to find relevant code to edit:
//             * Find the last code and nnote it's position
//             * Add in new code (somehow)
//             * Note the beginning IL Byte for uupgrade 3 and 4
//             * modify the switch code to have switch to new upgrade 3 and 4.
//             */

//            ILGenerator myIL = myMthdBuilder.GetILGenerator();

//            int house3Price = 75000;
//            int house3Material = 390;
//            int house3MaterialPrice = 300;

//            var house3Context = new List<CodeInstruction>();
//            var houseCellar = new List<CodeInstruction>();
//            Label[] il_switches = {};
//            int switch_loc = 0;

//            var codes = new List<CodeInstruction>(instructions);
//            // var fullStack = codes;
//            int startIndex = -1, endIndex = -1;

//            // Iterate through the call stack
//            int retCount = 0;
//            for (int i = 0; i < codes.Count; i++) {

//                // Find the switch statement and record switches
//                if (codes[i].opcode == OpCodes.Switch) {
//                    switch_loc = i;
//                    //object temp = codes[i].operand;
//                    //string s = temp.ToString();
//                    //string[] il_switches = s.Split(' ');
//                    //foreach(string il_switch in il_switches ) {
//                    //    switches.Add(il_switch);
//                    //}
//                }

//                // Identify when a return code is and increase count
//                if (codes[i].opcode == OpCodes.Ret) {
//                    retCount += 1;
//                }


//                // When the previous was a return, process it
//                if (codes[i - 1].opcode == OpCodes.Ret) {
//                    switch (retCount) {
//                        case 1:
//                            // start of first switch
//                            // add switch 
//                            il_switches[0] = i;
//                            break;
//                        case 4:
//                        case 5:
//                        case 6:
//                            // start of second upgrade
//                            // record codes
//                            il_switches[1] = i;
//                            houseCellar.Add(codes[i]);
//                            break;
//                        case 7:
//                            // end second upgrade
//                            // end recording
//                            break;
//                        case 9:
//                            // append new upgrade 3 to end
//                            // record instruction ID
//                            il_switches[2] = i;
                            
//                            break;
//                        case 12:
//                            // append old cellar upgrade code
//                            // record instruction ID
//                            break;
//                        case 13:
//                            // change switch statement to use new codes
//                            break;
//                        case 14:
//                            // EOF
//                            break;
//                    }
//                }
//            }
//        }
//    }
//}
