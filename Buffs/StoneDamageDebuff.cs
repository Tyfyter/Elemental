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
            Main.debuff[Type] = true;
            Main.pvpBuff[Type] = true;
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