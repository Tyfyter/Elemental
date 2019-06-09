using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
	public class wind : ModItem
	{
		public override void SetDefaults()
		{
			//item.name = "Wind";
			item.damage = 5;
            item.magic = true;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.channel = true;
            item.mana = 1;
            item.width = 40;
			item.height = 40;
			item.useTime = 60;
			item.useAnimation = 5;
            item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.useStyle = 5;
            item.shoot = mod.ProjectileType("WindBeam");
            item.autoReuse = true;
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Wind");
		  Tooltip.SetDefault("");
		}

        public override void HoldStyle(Player player)

        {
            if ((!(player.velocity.Y > 0)) || player.sliding)
            {
                int dust3 = Dust.NewDust(player.Center, 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity /= 2;
            }
            else
            {
                int dust3 = Dust.NewDust(player.Top + new Vector2(player.direction * -10, 0), 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity /= 2;
            }

        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
 
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)     //2 is right click
            {
 
                item.useTime = 20;
                item.useAnimation = 20;
				item.shoot = mod.ProjectileType("WindShot");
				item.shootSpeed = 12.5f;    //projectile speed when shoot
 
                if (player.statMana >= 35)             //when the player has 35+ mana he can use this item
                {
                    player.statMana -= 35;                //when you use the item use 35 mana
                }
                else
                {
                    return false;                    //this make that when you have 0 mana you cant use the item
                }
 
 
            }
			else{
				
            item.shoot = mod.ProjectileType("WindBeam");
			}
            return base.CanUseItem(player);
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WindMaterial", 50);   //you need 50 Wind
            recipe.AddTile(TileID.SkyMill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
