using System.Collections.Generic;
using elemental.Buffs;
using elemental.Classes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.NPCs
{
	public class ElementalGlobalNPC : GlobalNPC
	{
		public override bool InstancePerEntity => true;
		public override bool CloneNewInstances => true;
		public bool wasweta = false;
		public bool waswetb = false;
		public float baseKB = 0;
		public float currKB = 0;
		public int windKBtime = 0;
        public override bool PreAI(NPC npc){
			if(npc.HasBuff(BuffID.Frozen))npc.velocity = new Vector2();
			if(wasweta)npc.noTileCollide = true;
			if(waswetb)npc.noGravity = true;
			wasweta = false;
			waswetb = false;
            if (npc.HasBuff(mod.BuffType<WaterDebuff>())){
                if (npc.noTileCollide){
                    npc.noTileCollide = false;
                    wasweta = true;
                }
				if (npc.noGravity){
					npc.noGravity = false;
					waswetb = true;
				}
            }
			if(windKBtime>0){
				npc.knockBackResist = currKB;
				//Lighting.AddLight(npc.Center, Color.Goldenrod.ToVector3());
				if(--windKBtime<=0){
					npc.knockBackResist = baseKB;
				}
			}
			return base.PreAI(npc);
        }
		public override void AI(NPC npc){
			if(windKBtime>0)Lighting.AddLight(npc.Center, Color.Goldenrod.ToVector3()/2);
		}
        public override bool? CanHitNPC(NPC npc, NPC target){
            if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.CorruptBunny || target.type == NPCID.CrimsonBunny){
                return false;
            }
            return base.CanHitNPC(npc, target);
        }

        public override bool? CanBeHitByProjectile(NPC target, Projectile projectile)
        {
            if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.CorruptBunny || target.type == NPCID.CrimsonBunny){
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
