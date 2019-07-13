using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class CrystalTorrent : ElementalItem
    {
        public override int Elements => 16;

        public override void SetDefaults()
        {
            //item.name = "lightning";          
            item.damage = 50;                        
            item.magic = true;                     //this make the item do magic damage
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
            item.mana = 5;             //mana use
            //item.UseSound = SoundID.CreateTrackable("dd2_wither_beast_crystal_impact", 3, SoundType.Sound);            //this is the sound when you use the item
			item.UseSound = SoundID.DD2_WitherBeastCrystalImpact;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("CrystalShot");  //this make the item shoot your projectile
            item.shootSpeed = 4.5f;    //projectile speed when shoot
        }      
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Crystal Torrent");
		  Tooltip.SetDefault("Chaos. Death. Destruction.");
		}
		
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = 3;
				item.useAnimation = 2;
				item.shoot = 94;
				item.shootSpeed = 3.5f;    //projectile speed when shoot      
				item.damage = 50;
                item.autoReuse = true;
                item.mana = 3;             //mana use


            }
			else{
				
            item.useTime = 5;
            item.useAnimation = 5;
            item.shootSpeed = 4.5f;    //projectile speed when shoot      
            item.damage = 50;
            //item.shoot = mod.ProjectileType ("CrystalShot");  //this make the item shoot your projectile
			item.shoot = 477;
            item.autoReuse = true;
            item.mana = 5;             //mana use
            }
            return base.CanUseItem(player);
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 5 + Main.rand.Next(5); //This defines how many projectiles to shot. 4 + Main.rand.Next(2)= 4 or 5 shots
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 30 degree spread.
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
			}
			return false;
		}  
    }
}