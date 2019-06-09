using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.NPCs
{
	public class ElementalGlobalNPC : GlobalNPC
	{
        public override bool PreAI(NPC npc){
			if(npc.HasBuff(BuffID.Frozen))npc.velocity = new Vector2();
			return base.PreAI(npc);
        }
        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.CorruptBunny || target.type == NPCID.CrimsonBunny)
            {
                return false;
            }
            return base.CanHitNPC(npc, target);
        }

        public override bool? CanBeHitByProjectile(NPC target, Projectile projectile)
        {
            if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.CorruptBunny || target.type == NPCID.CrimsonBunny)
            {
                return false;
            }
            return base.CanBeHitByProjectile(target, projectile);
        }

        public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			/*if (type == NPCID.Dryad)
			{
				shop.item[nextSlot].SetDefaults(mod.ItemType<Items.CarKey>());
				nextSlot++;

				shop.item[nextSlot].SetDefaults(mod.ItemType<Items.CarKey>());
				shop.item[nextSlot].shopCustomPrice = new int?(2);
				shop.item[nextSlot].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
				nextSlot++;

				shop.item[nextSlot].SetDefaults(mod.ItemType<Items.CarKey>());
				shop.item[nextSlot].shopCustomPrice = new int?(3);
				shop.item[nextSlot].shopSpecialCurrency = ExampleMod.FaceCustomCurrencyID;
				nextSlot++;
			}*/
		}
	}
}
