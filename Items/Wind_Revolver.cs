using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class Wind_Revolver : ModItem
    {
        private static int RoundsLeft = 6;
        private static int RoundsMax = 6;
        private static int reloading = 1;
        private static int reloadspeed = 5;
        private static int altfire = 0;
        public static short customGlowMask = 0;
        public override void SetDefaults()
        {
            //item.name = "lightning";          
            item.damage = 500;                        
			item.ranged = true;
            item.magic = true;
            item.width = 24;
            item.height = 28;
            //item.toolTip = "Casts a lightning bolt.";
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = 5;        //this is how the item is held
            item.noMelee = true;
            item.knockBack = 7.5f;        
            item.value = 1000;
            item.rare = 6;
			//item.UseSound = SoundID.DD2_SkyDragonsFurySwing;
            item.autoReuse = false;
			item.shoot = ProjectileID.Bullet;
            item.shootSpeed = 12.5f;    //projectile speed when shoot
            item.glowMask = customGlowMask;
			item.useAmmo = AmmoID.Bullet;
        }      
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Wind Revolver");
		  Tooltip.SetDefault(@"I'm not sure what to put here 
		  DisplayAmmo");
          customGlowMask = elementalmod.SetStaticDefaultsGlowMask(this);
		}
		
		public override void HoldItem (Player player){
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            item.autoReuse = false;
			modPlayer.reloadablegun = 2;
			reloading = Math.Max(reloading, 0);
			if(modPlayer.reloadgun && reloading <= 10/* && reloading > 0*/){
				altfire = -3;
			}
			if(modPlayer.reloadgun || RoundsLeft <= 0){
				if(RoundsLeft != RoundsMax && reloading == 0){
					reloading = 1;
					/*for(){
						Dust.NewDust(position + offset, 1, 1, 9, 0, 0, 0, Color.White, 0.3f);
					}*/
				}
				modPlayer.reloadgun = false;
			}
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = 5;
				item.useAnimation = (5 * RoundsLeft)+4;
				item.damage = 150;   
            }else{
				item.useTime = 10;
				item.useAnimation = 10;
				item.damage = 250;  
			}
		}
		
		public override void HoldStyle (Player player){
            //int dust3 = Dust.NewDust(player.Top+new Vector2(player.direction*-10, 0), 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
            //Main.dust[dust3].noGravity = true;
            //Main.dust[dust3].velocity = new Vector2(0, 0);
			reloading = Math.Max(reloading, 0);
			//altfire = Math.Min(altfire, reloadspeed-1);
			if(reloading > 0){
				reloading = reloading = Math.Max(reloading + (reloadspeed - (altfire)), 0);
				player.itemRotation = (reloading/10)*-player.direction;
				item.holdStyle = 1;
				item.position = player.itemLocation;
			}
			if(reloading >= 100){
				RoundsLeft = RoundsMax;
				reloading = 0;
				item.holdStyle = 0;
				altfire = 0;
			}
		}
		
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
		
 
        public override bool CanUseItem(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
			if(RoundsLeft == 0){
				return false;
			}
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = 5;
				item.useAnimation = (5 * RoundsLeft)+4;
				item.damage = 300;  
				altfire = 1;    
            }else{
				item.useTime = 7;
				item.useAnimation = 7;
				item.damage = 500;      
				altfire = -1;
				
            }
			if(reloading > 0 && RoundsLeft > 0){
				reloading = 0;
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
                if (tooltips[i].text == "Wind Revolver")
                {
                    tip = new TooltipLine(mod, "Wind Revolver",
                        "Wind Revolver");
                    tip.overrideColor = new Color(255, 255, 102, 200);
                    tooltips.RemoveAt(i);
                    tooltips.Insert(i, tip);
				}
            }
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Main.PlaySound(2, (int)player.position.X, (int)player.position.Y, 11);
			Main.PlaySound(42, (int)player.position.X, (int)player.position.Y, 220);
			RoundsLeft--;
			if(player.altFunctionUse == 2){
				player.itemRotation = player.itemRotation + ((-0.065f*player.direction) * (7-RoundsLeft));
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4)).RotatedBy(MathHelper.ToRadians(player.itemRotation*20)); // This defines the projectiles random spread . 30 degree spread.
				speedX = perturbedSpeed.X;
				speedY = perturbedSpeed.Y;
			}
			if(type == ProjectileID.Bullet){
				
			}
			if(!player.controlUseItem)player.itemAnimation = 0;
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
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
			Vector2 offset = new Vector2(speedX, speedY);
			offset.Normalize();
			Dust.NewDust(position + offset, 1, 1, 134, 0, 0, 0, Color.White, 0.3f);
			if(player.altFunctionUse == 2){
				int numberProjectiles = 5; //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
				for (int i = 0; i < numberProjectiles; i++)
				{
					Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(1)); // This defines the projectiles random spread . 30 degree spread.
					int a = Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType ("CrystalShot"), damage, knockBack, player.whoAmI);
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