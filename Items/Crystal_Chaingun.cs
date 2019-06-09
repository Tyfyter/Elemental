using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class Crystal_Chaingun : ModItem
    {
        public static short customGlowMask = 0;
        public override void SetDefaults()
        {
            //item.name = "lightning";          
            item.damage = 50;                        
            item.magic = true;                     //this make the item do magic damage
			item.ranged = true;
            item.width = 24;
            item.height = 28;
            //item.toolTip = "Casts a lightning bolt.";
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;        //this is how the item is held
            item.noMelee = true;
            item.knockBack = 7.5f;        
            item.value = 1000;
            item.rare = 6;
            //item.UseSound = SoundID.CreateTrackable("dd2_wither_beast_crystal_impact", 3, SoundType.Sound);            //this is the sound when you use the item
			item.UseSound = SoundID.DD2_WitherBeastCrystalImpact;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("CrystalShot");  //this make the item shoot your projectile
            item.shootSpeed = 3.5f;    //projectile speed when shoot
            item.glowMask = customGlowMask;
			item.useAmmo = AmmoID.Bullet;
        }      
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Crystal Chaingun");
		  Tooltip.SetDefault(@"Chaos. Death. Destruction.
		  DisplayModifier");
          customGlowMask = elementalmod.SetStaticDefaultsGlowMask(this);
		}
		
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
		
 
        public override bool CanUseItem(Player player)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = Math.Max(4-(int)(modPlayer.chaoschaingunspool/5), 2);
				item.useAnimation = 12;
				item.shootSpeed = 5.5f;    //projectile speed when shoot     
				if(item.prefix == 57){
				item.damage = 60;
				}else{
					item.damage = 30;
				}
				//item.shoot = mod.ProjectileType ("CrystalShot");  //this make the item shoot your projectile
				item.shoot = 477;
				item.autoReuse = true;
				item.reuseDelay = Math.Max(5-(int)(modPlayer.chaoschaingunspool/2), 1);
            }else{
				item.useTime = Math.Max(10-(int)modPlayer.chaoschaingunspool, 1);
				item.useAnimation = Math.Max(10-(int)modPlayer.chaoschaingunspool, 4);
				item.shoot = 94;
				item.shootSpeed = 3.5f;    //projectile speed when shoot      
				if(item.prefix == 57){
				item.damage = 60;
				}else{
					item.damage = 50;
				}
                item.autoReuse = true;
            }
            return base.CanUseItem(player);
        }
		
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
					if(item.prefix == 57 && Main.expertMode){
					Main.projectile[a].ai[0] = 0.8f;
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
					Main.projectile[a].extraUpdates = 5;
					Main.projectile[b].extraUpdates = 3;
				}
				modPlayer.chaoschaingunspool = Math.Min(modPlayer.chaoschaingunspool + 2.0f, 9);
				player.HealEffect((int)modPlayer.chaoschaingunspool);
				return false;
			}
		}
    }
}