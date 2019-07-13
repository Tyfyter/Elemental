using System;
using System.IO;
using elemental.Buffs;
using elemental.Classes;
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
    public class WaterPotion : ElementalItem {
        public override int Elements => ElEnum.water;
        public override void SetDefaults()
        {
            item.UseSound = SoundID.Item3;
            item.useStyle = 1;
			item.noMelee = true;
			item.damage = 55;
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
			item.shootSpeed = 11.5f;
        }
		public override void SetStaticDefaults()
		{
		DisplayName.SetDefault("Water Bottle");
		Tooltip.SetDefault("It's water, right click to drink. (heals 75 life)");
		}
		public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterMaterial", 1);
			recipe.AddIngredient(ItemID.Bottle, 16);
            recipe.AddTile(TileID.Bottles);
            recipe.SetResult(this, 16);
			recipe.alchemy = false;
            recipe.AddRecipe();
        }
		public override bool AltFunctionUse(Player player){
			return true;
		}
		public override bool CanUseItem(Player player){
			if(player.altFunctionUse==2){
            	item.buffType = mod.BuffType<WaterDebuff>();
            	item.buffTime = 1000;
				item.healLife = 75;
            	item.UseSound = SoundID.Item3;
            	item.useStyle = 2;
				item.useAnimation = 17;
				item.useTime = 17;
				item.shoot = 0;
			}else{
            	item.buffType = 0;
            	item.buffTime = 0;
				item.healLife = 0;
            	item.UseSound = new LegacySoundStyle(2,106);
            	item.useStyle = 1;
				item.useAnimation = 7;
				item.useTime = 7;
				item.shoot = ProjectileID.ToxicFlask;
			}
			return true;
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
			int proj = Projectile.NewProjectile(position, new Vector2(speedX,speedY), type, damage, knockBack, item.owner);
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).OverrideTexture = mod.GetTexture("Items/WaterPotion");
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).render = false;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).preColl = PColl;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).onHitNPC = PNPC;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).onHitPlayer = PHit;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).modHitPlayer = PMod;
			Main.projectile[proj].GetGlobalProjectile<ElementalGlobalProjectile>(mod).nullprecull = true;
			Main.projectile[proj].penetrate++;
			Main.projectile[proj].ignoreWater = true;
			return false;
		}
		public static bool PColl(Projectile proj){
			if(!proj.tileCollide)return true;
			Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 704, 1f);
			Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
			Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
			for(int i = 0; i < 19; i++){
				Dust a = Dust.NewDustDirect(proj.Center, 0, 0, Dust.dustWater());
				a.velocity.Y-=2.5f;
				a.velocity*=3;
			}
			Main.PlaySound(2,proj.Center,107);
			Rectangle HB = proj.Hitbox;
			HB.Inflate(HB.Width*3,HB.Height*3);
			proj.Hitbox = HB;
			proj.timeLeft = 2;
			proj.tileCollide = false;
			proj.penetrate = -1;
			proj.damage=(int)(proj.damage/1.5f);
			proj.hostile = true;
			return false;
		}
		public static void PNPC(Projectile proj, NPC targ, int damg, bool crit){
			if(!proj.tileCollide){
				targ.velocity = lerp(new Vector2(), targ.velocity, 1-constrain(targ.knockBackResist*1.25f, 0.1f, 1f));
				targ.AddBuff(elementalmod.mod.BuffType<WaterDebuff>(),targ.boss?10:60);
			}else{
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 704, 1f);
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				for(int i = 0; i < 19; i++){
					Dust a = Dust.NewDustDirect(proj.Center, 0, 0, Dust.dustWater());
					a.velocity.Y-=2.5f;
					a.velocity*=3;
				}
				Main.PlaySound(2,proj.Center,107);
				Rectangle HB = proj.Hitbox;
				HB.Inflate(HB.Width*3,HB.Height*3);
				proj.Hitbox = HB;
				proj.timeLeft = 2;
				proj.tileCollide = false;
				proj.penetrate = -1;
				proj.damage=(int)(proj.damage/1.5f);
				proj.hostile = true;
			}
		}
		///<summary>pronounced "fit"</summary>
		public static void PHit(Projectile proj, Player targ, int damg){
			if(!proj.tileCollide){
				targ.velocity = lerp(new Vector2(), targ.velocity, 0.85f);
				targ.AddBuff(elementalmod.mod.BuffType<WaterDebuff>(),450);
			}else{
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 704, 1f);
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				Gore.NewGore(proj.Center, -proj.oldVelocity * 0.2f, 705, 1f);
				for(int i = 0; i < 19; i++){
					Dust a = Dust.NewDustDirect(proj.Center, 0, 0, Dust.dustWater());
					a.velocity.Y-=1f;
					a.velocity*=3;
				}
				Main.PlaySound(2,proj.Center,107);
				Rectangle HB = proj.Hitbox;
				HB.Inflate(HB.Width*3,HB.Height*3);
				proj.Hitbox = HB;
				proj.timeLeft = 2;
				proj.tileCollide = false;
				proj.penetrate = -1;
				proj.damage=(int)(proj.damage/1.5f);
				proj.hostile = true;
			}
		}
		public static int PMod(Projectile proj, Player targ, int damg){
			return targ.whoAmI==proj.owner?damg/2:damg;
		}
    }
}