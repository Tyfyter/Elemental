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
    public class ChaosBuff : ModBuff
    {
		//float grav = 1f;
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Order And Chaos");
			Description.SetDefault("");
            //Main.buffName[Type] = "Wet"; //the name displayed when hovering over the buff ingame.
            //Main.buffTip[Type] = "Why is this water so heavy?"; //The description of the buff shown when hovering over ingame.   
        }

        public override void Update(Player player, ref int buffIndex)
        {
                if(player.HasBuff(BuffID.PotionSickness)){
                    player.buffTime[player.FindBuffIndex(BuffID.PotionSickness)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.PotionSickness)] - 2, 0);
                }
                player.potionDelayTime -= 2;
                player.potionDelay -= 2;
                player.lifeSteal += 0.1f;
                if(player.HasBuff(BuffID.MoonLeech)){
                    player.DelBuff(player.FindBuffIndex(BuffID.MoonLeech));
                }
                if(player.HasBuff(BuffID.ManaSickness)){
                    player.buffTime[player.FindBuffIndex(BuffID.ManaSickness)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.ManaSickness)] - 7, 0);
                }
        }

        public override bool ReApply(Player player, int time, int buffIndex){
            player.buffTime[buffIndex] = Math.Min(player.buffTime[buffIndex] + time, 900);
            return false;
        }
    }
}