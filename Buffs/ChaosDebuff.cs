using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Buffs
{
    public class ChaosDebuff : ModBuff
    {
		//float grav = 1f;
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Entropy");
			Description.SetDefault("Prepare to die.");
            //Main.buffName[Type] = "Wet"; //the name displayed when hovering over the buff ingame.
            //Main.buffTip[Type] = "Why is this water so heavy?"; //The description of the buff shown when hovering over ingame.          
            Main.debuff[Type] = true;   //Tells the game if this is a buff or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
			canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
			for(int i = 0; i < npc.immune.Length; i++){
				npc.immune[i] = 0;
			}
        }
        public override void Update(Player player, ref int buffIndex){
			player.immune = false;
        }
    }
}