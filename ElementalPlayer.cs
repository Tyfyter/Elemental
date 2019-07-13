using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using elemental.Items;
using static elemental.Extentions;
using elemental.Classes;
using elemental.Buffs;

namespace elemental {
    internal class ElementalPlayer : ModPlayer {
		public int test = 0;
        public bool FlameShield = false;
        public bool IceShield = false;
        public bool stone = false;
        public bool reloadgun = false;
		public int reloadablegun = 0;
        public bool reflect = false;
		public float multiplyCrit = 1;
        public int pullhook = 3;
        public int WindGauntletCD = 0;
        public int channelice = 0;
        public int channelsword = 0;
        public int channellightning = 0;
        public int channellightningid = 0;
		public int consumedgravityglobes = 0;
		public float chaoschaingunspool = 0;
		public bool onGround = false;
        public bool FireWhip = false;
        public bool FireWings = false;

        public override bool Autoload(ref string name) {
            return true;
        }

        public override void ResetEffects()
        {
            if(WindGauntletCD > 0)
            {
                WindGauntletCD--;
            }
            if(channelice > 0)
            {
                channelice--;
            }
            if(channelsword > 0)
            {
                channelsword--;
            }
            if(channellightning > 0)
            {
                channellightning--;
            }
            if(chaoschaingunspool > 0)
            {
                chaoschaingunspool = Math.Max(chaoschaingunspool-0.15f, 0);
            }
            FlameShield = false;
            IceShield = false;
            reflect = false;
            if(!FireWings)goto skipb;
            if(player.wet||player.HasBuff<WaterDebuff>())goto skipa;
            if(player.velocity.Y==0)goto skipa;
            if(player.HeldItem==null)goto skipa;
            if((player.HeldItem.toElementalItem().Elements&ElEnum.fire)!=0)goto skipb;
            skipa:
            if(FireWings&&player.oldVelocity.Y>4){
                int proj = Projectile.NewProjectile(player.Bottom, new Vector2(0,24), ProjectileID.RocketFireworkYellow, (int)(player.oldVelocity.Y*4), player.oldVelocity.Y/5, player.whoAmI);
                Main.projectile[proj].timeLeft = 0;
                Main.projectile[proj].magic = false;
                Main.projectile[proj].ranged = false;
            }
            FireWings = false;
            skipb:
            if(channelsword<=0)FireWhip = false;
			if(reloadablegun == 0){
				reloadgun = false;
			}
			reloadablegun--;
			multiplyCrit = 1.0f;
            pullhook = Math.Max(pullhook-1, 0);
            if(FireWings){
                if(Main.time%(player.manaSick?4:6)==0)FireWings = player.CheckMana(2,true);
                player.gravity = player.controlUseTile?0.1f:0.35f;
                player.wingTime = 30;
                player.wingsLogic = 29;
                player.thorns+=1;
                player.manaRegen/=3;
                player.maxFallSpeed*=1.75f;
                player.noFallDmg = true;
            }
            //onGround = 

        }

        public override void ModifyHitByProjectile(Projectile proj, ref int damage, ref bool crit)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (modPlayer.reflect)
            {
                Main.projectile[proj.whoAmI].owner = player.whoAmI;
                Main.projectile[proj.whoAmI].hostile = false;
                Main.projectile[proj.whoAmI].friendly = true;
                if (Math.Abs(Main.projectile[proj.whoAmI].velocity.ToRotation() - (float)Math.Atan2((player.Center - Main.MouseWorld).Y, (player.Center - Main.MouseWorld).X)) <= 20)
                {
                    Vector2 vel = player.Center - Main.MouseWorld;
                    vel.Normalize();
                    vel *= -Main.projectile[proj.whoAmI].velocity.Length();
                    Main.projectile[proj.whoAmI].velocity = vel;
                }
                //Main.projectile[proj.whoAmI].velocity *= -1;
                damage = 0;
                crit = true;
                player.immuneTime = 0;
                player.immune = false;
            }
        }
        public override bool ShiftClickSlot(Item[] inventory, int context, int slot){
            if(inventory[player.selectedItem]!=null)if(inventory[slot].type==ItemID.FragmentSolar&&inventory[slot].stack>=10)if(inventory[player.selectedItem].type==mod.ItemType<firewings>())if(!((firewings)inventory[player.selectedItem].modItem).charged){
                ((firewings)inventory[player.selectedItem].modItem).charged = true;
                inventory[slot].stack-=10;
                //if(player.HeldItem.stack<=0)player.HeldItem.TurnToAir();
                return !base.ShiftClickSlot(inventory, context, slot);
            }
            return base.ShiftClickSlot(inventory, context, slot);
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (modPlayer.reflect)
            {
                return false;
            }
            return base.CanBeHitByProjectile(proj);
        }

        public override bool? CanHitNPC(Item item, NPC target)
        {
            if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.CorruptBunny || target.type == NPCID.CrimsonBunny)
            {
                return false;
            }
            return base.CanHitNPC(item, target);
        }

        public override bool? CanHitNPCWithProj(Projectile proj, NPC target)
        {
            if (target.type == NPCID.Bunny || target.type == NPCID.BunnySlimed || target.type == NPCID.BunnyXmas || target.type == NPCID.GoldBunny || target.type == NPCID.PartyBunny || target.type == NPCID.CorruptBunny || target.type == NPCID.CrimsonBunny)
            {
                return false;
            }
            return base.CanHitNPCWithProj(proj, target);
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            if(npc.type == NPCID.CorruptBunny || npc.type == NPCID.CrimsonBunny)
            {
                return false;
            }
            return base.CanBeHitByNPC(npc, ref cooldownSlot);
        }

        public override void ModifyHitByNPC(NPC npc, ref int damage, ref bool crit)
        {
            if (stone)
            {
                npc.AddBuff(BuffID.Confused, 2*damage);
                //player.AddBuff(mod.BuffType("StoneDamageDebuff"), (int)(damage*0.9));
                damage = 0;
            }
        }
        public override void CatchFish(Item fishingRod, Item bait, int power, int liquidType, int poolSize, int worldLayer, int questFish, ref int caughtType, ref bool junk){
            if(junk)return;
            if(player.ZoneHoly&&power>55&&caughtType==2307&&Main.rand.Next(4)!=0){
                caughtType = mod.ItemType<Chaos_Minnow>();
            }
        }

        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit){
            if (stone)
            {
                player.AddBuff(mod.BuffType("StoneDamageDebuff"), (int)(damage*0.45));
                player.statLife += (int)damage;
                damage = 0;
                quiet = true;
                player.chatOverhead.NewMessage(damage+"", 60);
                base.Hurt(pvp, quiet, damage, hitDirection, crit);
            }else{
            base.Hurt(pvp, quiet, damage, hitDirection, crit);
            }
        }
		
		public override void PostUpdateEquips()
		{
            if(stone){
                player.stoned = true;
            }
			if(player.HasBuff(mod.BuffType<WindDebuff>())){
				player.noKnockback = false;
				player.autoJump = false;
			}
			if(player.HasBuff<WaterDebuff>()){
				//player.merman = false;
                player.gills = false;
                player.merman = false;
                bool flag = Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
                if(player.armor[0].type==ItemID.FishBowl)flag = true;
                player.gills = flag;
                //player.wet = !player.wet;
                if(flag){
                    player.breath++;
                }else{
                    player.breath -= 4;
                }
                if(player.breath <= 0){
                    player.lifeRegenTime = 0;
                    player.breath = 0;
                    player.statLife -= 1;
                    if (player.statLife <= 0)
                    {
                        player.statLife = 0;
                        player.KillMe(PlayerDeathReason.ByOther(1), 10.0, 0, false);
                    }
                }
				player.buffImmune[69] = false;
				player.buffImmune[70] = false;
				player.wingTimeMax = (int)(player.wingTimeMax/4);
			}
			player.rangedCrit = (int)(player.rangedCrit * multiplyCrit);
			player.magicCrit = (int)(player.magicCrit * multiplyCrit);
			player.thrownCrit = (int)(player.thrownCrit * multiplyCrit);
			player.meleeCrit = (int)(player.meleeCrit * multiplyCrit);
		}
        public override void PostUpdateBuffs(){
            if(FireWings){
                player.manaRegenDelay+=2;
            }
        }

        /*public override void ModifyScreenPosition()
        {
            base.ModifyScreenPosition();
        }
        /*public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if(((CheatHotkeys)mod).GodMode) {
                return false;
            }

            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource) {
            if(((CheatHotkeys)mod).GodMode) {
                return false;
            }

            return base.PreKill(damage, hitDirection, pvp, ref playSound, ref genGore, ref damageSource);
        }

        public override void PreUpdateBuffs() {
            CheatHotkeys chmod = (CheatHotkeys)mod;

            if(chmod.GodMode) {
                chmod.RemoveDebuffs();
                player.statMana = player.statManaMax;
            }

            base.PreUpdateBuffs();
        }

        public override bool CanBeHitByProjectile(Projectile proj) {
            if(((CheatHotkeys)mod).GodMode) {
                return false;
            }

            return base.CanBeHitByProjectile(proj);
        }

        public override bool ConsumeAmmo(Item weapon, Item ammo) {
            if(((CheatHotkeys)mod).UnlimitedAmmo) {
                return false;
            }

            return base.ConsumeAmmo(weapon, ammo);
        }*/
    }
}
