using System;
using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class Corrupt_Throwing : ModItem
    {
        public override void SetDefaults()
        {
            //item.name = "lightning";          
            item.damage = 50;                        
            item.thrown = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 24;
            //item.toolTip = "Casts a lightning bolt.";
            item.useTime = 5;
            item.useAnimation = 5;
            item.useStyle = 5;        //this is how the item is held
            item.noMelee = true;
            item.noUseGraphic = true;
            item.knockBack = 0f;        
            item.value = 1000;
            item.rare = 6;
            item.mana = 5;             //mana use
            //item.UseSound = SoundID.CreateTrackable("dd2_wither_beast_crystal_impact", 3, SoundType.Sound);            //this is the sound when you use the item
			item.UseSound = null;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType<EclipseBlade>();  //this make the item shoot your projectile
            item.shootSpeed = 4.5f;    //projectile speed when shoot
        }      
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Eclipse");
		  Tooltip.SetDefault("");
		}
		
        public override bool AltFunctionUse(Player player)
        {
            return player.GetModPlayer<ElementalPlayer>().channelsword<=0;
        }
 
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = 30;
				item.useAnimation = 30;
                item.shoot = mod.ProjectileType<EclipseHook>();
				item.shootSpeed = 6.5f;    
				item.damage = 50;
                item.autoReuse = false;
                item.mana = 40;


            }
			else{
				
            item.useTime = 9;
            item.useAnimation = 6;
            item.shootSpeed = 4.5f; 
            item.damage = 50;
            item.shoot = mod.ProjectileType<EclipseBlade>();
            item.autoReuse = true;
            item.mana = 7;
            }
            return base.CanUseItem(player);
        }
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Main.PlaySound(42, (int)position.X, (int)position.Y, Main.rand.Next(163, 164), 1, 1);
            if(player.altFunctionUse != 2){
                damage = (int)(damage*0.35f);
            }
			//Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(10)); // This defines the projectiles random spread . 30 degree spread.
			Vector2 perturbedPosition = position + new Vector2(Main.rand.NextFloat(0, 48), 0).RotatedByRandom(MathHelper.ToRadians(180)); // This defines the projectiles random spread . 30 degree spread.
            for(int i = 0; i < 5; i++){
                int a = Dust.NewDust(perturbedPosition, 0, 0, DustID.CrystalPulse, 0, 0, 0, Color.BlueViolet, 0.75f);
                Main.dust[a].noGravity = true;
            }  
            Projectile.NewProjectile(perturbedPosition.X, perturbedPosition.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);
			return false;
		}  
    }
}