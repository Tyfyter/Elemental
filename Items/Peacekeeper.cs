using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class Peacekeeper : ModItem
    {
        private static int RoundsLeft = 6;
        private static int RoundsMax = 6;
        private static int reloading = 1;
        public override void SetDefaults()
        {
            //item.name = "lightning";          
            item.damage = 150;                        
			item.ranged = true;
            item.magic = true;
            item.width = 24;
            item.height = 28;
            //item.toolTip = "Casts a lightning bolt.";
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;        //this is how the item is held
            item.noMelee = true;
            item.knockBack = 7.5f;        
            item.value = 1000;
            item.rare = 6;
			//item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
            item.autoReuse = false;
			item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 12.5f;    //projectile speed when shoot
			item.useAmmo = AmmoID.Bullet;
        }      
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Peacekeeper");
		  Tooltip.SetDefault(@"I'm not sure what to put here 
		  DisplayAmmo");
		}
		
		public override void HoldItem (Player player){
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
			modPlayer.reloadablegun = 2;
			if(modPlayer.reloadgun || RoundsLeft == 0){
				if(RoundsLeft != RoundsMax && reloading == 0){
					item.reuseDelay = 120;
					reloading = 1;
					/*for(){
						Dust.NewDust(position + offset, 1, 1, 9, 0, 0, 0, Color.White, 0.3f);
					}*/
				}
				modPlayer.reloadgun = false;
			}
		}
		
		public override void HoldStyle (Player player){
			if(reloading > 0){
				reloading++;
				player.itemRotation = reloading;
				item.holdStyle = 1;
			}
			if(reloading == 100){
				RoundsLeft = RoundsMax;
				reloading = 0;
				item.holdStyle = 0;
			}
		}
		
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
		
 
        public override bool CanUseItem(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = 7;
				item.useAnimation = 7 * RoundsMax;
            }else{
				item.useTime = 10;
				item.useAnimation = 10;
            }
			if(reloading > 0 && RoundsLeft != 0){
				reloading = 0;
			}
			if(RoundsLeft <= 0){
				return false;
			}
            item.autoReuse = false;
            return base.CanUseItem(player);
        }
		
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                TooltipLine tip;
                if (tooltips[i].text.Contains("DisplayAmmo"))
                {
                    tip = new TooltipLine(mod, "DisplayAmmo",
                        "Rounds Left:" + RoundsLeft);
                    tip.overrideColor = new Color(255, 255, 102, 200);
                    tooltips.RemoveAt(i);
                    tooltips.Insert(i, tip);
                }
            }
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			if(RoundsLeft <= 0){
				return false;
			}
			Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11);
			RoundsLeft--;
			if(player.altFunctionUse == 2){
				player.itemRotation = player.itemRotation + (0.05f * (7-RoundsLeft));
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(20)).RotatedBy(MathHelper.ToRadians(player.itemRotation*10)); // This defines the projectiles random spread . 30 degree spread.
				speedX = perturbedSpeed.X;
				speedY = perturbedSpeed.Y;
			}
			return true;
		}
		/*
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].text.Contains("DisplayModifier"))
                {
                    TooltipLine tip;
					if(item.prefix == 57){
                    tip = new TooltipLine(mod, "DisplayModifier",
                        "Empowered");
                    tip.overrideColor = new Color(255, 32, 174, 200);
					}else{
                    tip = new TooltipLine(mod, "DisplayModifier",
                        "Dormant");
                    tip.overrideColor = new Color(174, 174, 174, 150);
					}
                    tooltips.RemoveAt(i);
                    tooltips.Insert(i, tip);
                }
            }
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
			Vector2 offset = new Vector2(speedX, speedY);
			offset.Normalize();
			Dust.NewDust(position + offset, 1, 1, 134, 0, 0, 0, Color.White, 0.3f);
			if(player.altFunctionUse == 2){
				int numberProjectiles = 5; //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(1)); // This defines the projectiles random spread . 30 degree spread.
					int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ProjectileType ("CrystalShot"), damage, knockBack, player.whoAmI);
					Main.projectile[a].ai[0] = 0.25f;
					if(item.prefix == 57){
					Main.projectile[a].ai[0] = 0.4f;
					}
				}
				modPlayer.chaoschaingunspool = Math.Min(modPlayer.chaoschaingunspool + 0.85f, 9);
				//Main.PlaySound(42, (int)player.position.X, (int)player.position.Y, 165);
				Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 91);
				player.HealEffect((int)modPlayer.chaoschaingunspool);
				return false;
			}else{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(((int)modPlayer.chaoschaingunspool*1.5f))); // This defines the projectiles random spread . 30 degree spread.
				player.itemRotation = (float)Math.Atan2(-perturbedSpeed.X, perturbedSpeed.Y);
				player.itemRotation = (player.itemRotation+(float)Math.Atan2(perturbedSpeed.X, -perturbedSpeed.Y))/2;
				if(modPlayer.chaoschaingunspool == 0.0f){
					damage *= 10;
				}
				int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, 89, damage, knockBack, player.whoAmI);
				int b = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X*3, perturbedSpeed.Y*3, 94, damage, knockBack, player.whoAmI);
				if(modPlayer.chaoschaingunspool == 0.0f){
					Main.projectile[a].extraUpdates = 6;
					Main.projectile[b].extraUpdates = 3;
				}
				modPlayer.chaoschaingunspool = Math.Min(modPlayer.chaoschaingunspool + 2.0f, 9);
				player.HealEffect((int)modPlayer.chaoschaingunspool);
				return false;
			}
		}*/
    }
}