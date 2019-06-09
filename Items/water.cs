using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class water : ModItem
    {
        public override void SetDefaults()
        {
            //item.name = "Water";          
            item.damage = 35;                        
            item.magic = true;                     //this make the item do magic damage
            item.width = 24;
            item.height = 28;
            //item.toolTip = "Casts a water bolt.";
            item.useTime = 16;
            item.useAnimation = 30;
            item.useStyle = 5;        //this is how the item is holded
            item.noMelee = true;
            item.knockBack = 7.5f;        
            item.value = 1000;
            item.rare = 6;
            item.mana = 25;             //mana use
            item.UseSound = SoundID.Item21;            //this is the sound when you use the item
            item.autoReuse = true;
            item.shoot = mod.ProjectileType ("WaterShot");  //this make the item shoot your projectile
            item.reuseDelay = 60;
            item.shootSpeed = 12.5f;    //projectile speed when shoot
        }      
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Water");
		  Tooltip.SetDefault("Casts a water bolt.");
		}
		
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)     //2 is right click
            {
				item.useTime = 30;
				item.useAnimation = 30;
                item.mana = 100;             //mana use
                item.shoot = mod.ProjectileType("WaterBlast");
				item.shootSpeed = 10f;    //projectile speed when shoot      
				item.damage = 100;  
				item.reuseDelay = 0;     
 
 
            }
			else{
				
            item.useTime = 17;
            item.useAnimation = 30;
            item.mana = 25;             //mana use
            item.shoot = mod.ProjectileType ("WaterShot");
            item.shootSpeed = 12.5f;    //projectile speed when shoot      
            item.damage = 35;
            item.reuseDelay = 20;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WaterMaterial", 50);   //you need 50 Wind
            recipe.AddTile(TileID.WaterDrip);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}