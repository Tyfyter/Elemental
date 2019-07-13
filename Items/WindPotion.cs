using System;
using System.IO;
using elemental.Buffs;
using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using static elemental.Extentions;
 
namespace elemental.Items
{
    public class WindPotion : ElementalItem{
        public override int Elements => 2;

        public override void SetDefaults()
        {
            item.UseSound = new LegacySoundStyle(2,106);
            item.useStyle = 1;
			item.noMelee = true;
			item.damage = 50;
			item.thrown = true;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 20;
            item.height = 28;
            item.value = 100;                
            item.rare = 1;
			item.shootSpeed = 12.5f;
        }
        public override void SetStaticDefaults()
        {
          DisplayName.SetDefault("Wind in a Bottle");
          Tooltip.SetDefault("congradulations, you put air in a bottle.\nRight click to... drink?");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WindMaterial", 1);
			recipe.AddIngredient(ItemID.Bottle, 16);
            recipe.AddTile(TileID.SkyMill);
            recipe.SetResult(this, 16);
            recipe.AddRecipe();
        }
		public override bool AltFunctionUse(Player player){
			return true;
		}
		public override bool CanUseItem(Player player){
			if(player.altFunctionUse==2){
            	//item.buffType = mod.BuffType<WindDebuff>();
            	//item.buffTime = 2000;
            	item.buffType = 0;
            	item.buffTime = 0;
            	item.UseSound = null;
            	item.useStyle = 4;
				item.useAnimation = 15;
				item.useTime = 1;
				item.shoot = ProjectileID.ToxicFlask;
				item.noUseGraphic = false;
				item.consumable = false;
			}else{
            	item.buffType = 0;
            	item.buffTime = 0;
            	item.UseSound = new LegacySoundStyle(2,106);
            	item.useStyle = 1;
				item.useAnimation = 7;
				item.useTime = 7;
				item.shoot = ProjectileID.ToxicFlask;
				item.noUseGraphic = true;
				item.consumable = true;
			}
			return true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
			if(player.altFunctionUse==2){
				if(player.itemAnimation>2||player.itemAnimation<2)return false;
				item.noUseGraphic = true;
				item.consumable = true;
				int proj2 = Projectile.NewProjectile(player.itemLocation, new Vector2(speedX,speedY), type, damage, knockBack, item.owner);
				Main.projectile[proj2].GetGlobalProjectile<ElementalGlobalProjectile>(mod).OverrideTexture = mod.GetTexture("Items/WindPotion");
				Main.projectile[proj2].GetGlobalProjectile<ElementalGlobalProjectile>(mod).render = false;
				Main.projectile[proj2].GetGlobalProjectile<ElementalGlobalProjectile>(mod).onHitNPC = PNPC2;
				Main.projectile[proj2].GetGlobalProjectile<ElementalGlobalProjectile>(mod).onHitPlayer = PHit2;
				Main.projectile[proj2].GetGlobalProjectile<ElementalGlobalProjectile>(mod).modHitPlayer = PMod;
				Main.projectile[proj2].GetGlobalProjectile<ElementalGlobalProjectile>(mod).nullprecull = true;
				Main.projectile[proj2].damage*=2;
				Rectangle HB = Main.projectile[proj2].Hitbox;
				HB.Inflate(HB.Width,HB.Height);
				Main.projectile[proj2].Hitbox = HB;
				PColl(Main.projectile[proj2]);
				return false;
			}
			int proj = Projectile.NewProjectile(position, new Vector2(speedX,speedY), type, damage, knockBack, item.owner);
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).OverrideTexture = mod.GetTexture("Items/WindPotion");
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).render = false;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).preColl = PColl;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).onHitNPC = PNPC;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).onHitPlayer = PHit;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).modHitPlayer = PMod;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).nullprecull = true;
			Main.projectile[proj].penetrate++;
			return false;
		}
		public static bool PColl(Projectile proj){
			if(!proj.tileCollide)return true;
			Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 704, 1f);
			Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				for (int i = 0; i < 19; i++)Dust.NewDustDirect(proj.Center, 0, 0, DustID.Sandnado).velocity*=3;
			Main.PlaySound(2,proj.Center,107);
			Rectangle HB = proj.Hitbox;
			HB.Inflate(HB.Width*3,HB.Height*3);
			proj.Hitbox = HB;
			proj.timeLeft = 2;
			proj.tileCollide = false;
			proj.penetrate = -1;
			proj.damage/=3;
			proj.hostile = true;
			return false;
		}
		public static void PNPC(Projectile proj, NPC targ, int damg, bool crit){
			if(!proj.tileCollide){
				targ.velocity = lerp((targ.Center-proj.Center).Normalized()*Math.Max(targ.knockBackResist,1)*(640/(constrain(targ.Center, proj.TopLeft, proj.BottomRight)-proj.Center).Length())*(targ.noTileCollide?1:targ.noGravity?2.5f:6.25f), targ.velocity, 1-constrain(targ.knockBackResist*1.25f, 0.1f, 1f));
			}else{
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 704, 1f);
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				for (int i = 0; i < 19; i++)Dust.NewDustDirect(proj.Center, 0, 0, DustID.Sandnado).velocity*=3;
				Main.PlaySound(2,proj.Center,107);
				Rectangle HB = proj.Hitbox;
				HB.Inflate(HB.Width*3,HB.Height*3);
				proj.Hitbox = HB;
				proj.timeLeft = 2;
				proj.tileCollide = false;
				proj.penetrate = -1;
				proj.damage/=3;
				proj.hostile = true;
			}
		}
		///<summary>pronounced "fit"</summary>
		public static void PHit(Projectile proj, Player targ, int damg){
			if(!proj.tileCollide){
				targ.velocity = lerp((targ.MountedCenter-proj.Center).Normalized()*(64/(constrain(targ.Center, proj.TopLeft, proj.BottomRight)-proj.Center).Length())*7.5f, targ.velocity, targ.velocity.Y==0?0.5f:0.35f);
			}else{
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 704, 1f);
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				for (int i = 0; i < 19; i++)Dust.NewDustDirect(proj.Center, 0, 0, DustID.Sandnado).velocity*=3;
				Main.PlaySound(2,proj.Center,107);
				Rectangle HB = proj.Hitbox;
				HB.Inflate(HB.Width*3,HB.Height*3);
				proj.Hitbox = HB;
				proj.timeLeft = 2;
				proj.tileCollide = false;
				proj.penetrate = -1;
				proj.damage/=3;
				proj.hostile = true;
			}
		}
		public static void PNPC2(Projectile proj, NPC targ, int damg, bool crit){
			Main.PlaySound(2,proj.Center,107);
			targ.windDebuff(60);
			targ.velocity = lerp((targ.Center-proj.Center).Normalized()*Math.Max(targ.knockBackResist,1)*(640/(constrain(targ.Center, proj.TopLeft, proj.BottomRight)-proj.Center).Length())*(targ.noTileCollide?1:targ.noGravity?2.5f:6.5f), targ.velocity, 1-Math.Max(targ.knockBackResist, 0.1f));
		}
		public static void PHit2(Projectile proj, Player targ, int damg){
			targ.AddBuff(elementalmod.mod.BuffType<WindDebuff>(), 60);
			if(targ.whoAmI!=proj.owner)targ.velocity = lerp((targ.MountedCenter-proj.Center).Normalized()*(64/(constrain(targ.Center, proj.TopLeft, proj.BottomRight)-proj.Center).Length())*7.5f, targ.velocity, targ.velocity.Y==0?0.4f:0.6f);
		}
		public static int PMod(Projectile proj, Player targ, int damg){
			return targ.whoAmI==proj.owner?5:damg;
		}
    }
}