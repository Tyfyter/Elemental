using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Buffs
{
    public class WaterDebuff : ModBuff
    {
		static float gravity = 0.3f;
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Wet");
			Description.SetDefault("Why is this water so heavy?");
            //Main.buffName[Type] = "Wet"; //the name displayed when hovering over the buff ingame.
            //Main.buffTip[Type] = "Why is this water so heavy?"; //The description of the buff shown when hovering over ingame.          
            Main.debuff[Type] = true;   //Tells the game if this is a buff or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            longerExpertDebuff = true;
			//canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
			//npc.velocity.Y = 1f;
			//npc.noGravity = false;
			//npc.noTileCollide = false;
			if(npc.buffTime[buffIndex]%3==0)npc.wet = true;
			npc.buffImmune[69] = false;
			npc.buffImmune[70] = false;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            //if(!player.wet)player.breath -= 2;
            //player.gills = player.wet;
            //player.merman = player.wet;
            //player.wet = !player.wet;
            /*if(player.breath <= 0){
                player.lifeRegenTime = 0;
                player.breath = 0;
                player.statLife -= 1;
                if (player.statLife <= 0)
                {
                    player.statLife = 0;
                    player.KillMe(PlayerDeathReason.ByOther(1), 10.0, 0, false);
                }
            }*/
            player.buffImmune[69] = false;
            player.buffImmune[70] = false;
            player.gravDir = 1;
			player.wingTimeMax = (int)(player.wingTimeMax/4);
        }
    }
}