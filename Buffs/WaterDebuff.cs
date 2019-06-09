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
    public class WaterDebuff : ModBuff
    {
		//float grav = 1f;
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Wet");
			Description.SetDefault("Why is this water so heavy?");
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
			//npc.velocity.Y = 1f;
			npc.noGravity = false;
			npc.noTileCollide = false;
			if(npc.buffTime[buffIndex]>500){
				npc.wet = true;
			}
			npc.buffImmune[69] = false;
			npc.buffImmune[70] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            player.breath -= 10;
            player.wet = true;
            player.buffImmune[4] = true;
            player.buffImmune[34] = true;
            player.merman = false;
            player.CheckDrowning();
            player.buffImmune[69] = false;
            player.buffImmune[70] = false;
            player.gravDir = 1;
			player.wingTimeMax = (int)(player.wingTimeMax/4);
			modPlayer.waterdebuffed = true;
        }
		public override bool ReApply(NPC npc, int time, int buffIndex){
			//grav += 1f;
			return false;
		}
    }
}