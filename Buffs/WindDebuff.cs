using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace elemental.Buffs
{
    public class WindDebuff : ModBuff
    {
        public override void SetDefaults()
        {
			DisplayName.SetDefault("Wind");
			Description.SetDefault("You've lost your balance!");
            //Main.buffName[Type] = "Wind"; //the name displayed when hovering over the buff ingame.
            //Main.buffTip[Type] = "You've lost your balance!"; //The description of the buff shown when hovering over ingame.          
            Main.debuff[Type] = true;   //Tells the game if this is a buff or not.
            Main.pvpBuff[Type] = true;  //Tells the game if pvp buff or not.
            longerExpertDebuff = true;
			canBeCleared = false;
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
			if(npc.knockBackResist < 1f){
				npc.knockBackResist = 1f;
			}
        }
        public override void Update(Player player, ref int buffIndex)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            player.noKnockback = false;
            player.autoJump = false;
            player.dashDelay = 10;
        }
		public override bool ReApply(NPC npc, int time, int buffIndex){
			npc.knockBackResist += Math.Min(npc.knockBackResist+(5.0f-npc.knockBackResist)/5,5);
			return false;
		}
    }
}