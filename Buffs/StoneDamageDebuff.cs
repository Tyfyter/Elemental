using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace elemental.Buffs
{
    public class StoneDamageDebuff : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("");
			Description.SetDefault("");
            //Main.buffName[Type] = "Wind"; //the name displayed when hovering over the buff ingame.
            //Main.buffTip[Type] = "You've lost your balance!"; //The description of the buff shown when hovering over ingame.          
            Main.debuff[Type] = true;   //Tells the game if this is a buff or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not. 
            Main.buffNoSave[Type] = true;
            longerExpertDebuff = true;
			canBeCleared = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
        }
		public override bool ReApply(Player player, int time, int buffIndex){
            player.buffTime[buffIndex] += time;
			return false;
		}
    }
}