using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace elemental.Items
{
	public class wind : ElementalItem
	{
        public override int Elements => 2;
        public override void SetDefaults()
		{
			//item.name = "Wind";
			item.damage = 15;
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
            item.knockBack = 7;
			item.value = 10000;
			item.rare = 2;
            item.useStyle = 5;
            item.shootSpeed = 12.5f;
            item.shoot = ProjectileType<WindBeam>();
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
                int dust3 = Dust.NewDust((player.direction==1?player.Left:player.Right)-new Vector2(4,-4), 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
                Main.dust[dust3].noGravity = true;
                Main.dust[dust3].velocity /= 2;
            }
            else
            {
                int dust3 = Dust.NewDust((player.direction==1?player.TopLeft:player.TopRight)-new Vector2(4,-4), 0, 0, 87, 0f, 0f, 25, Color.Goldenrod, 1.5f);
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
				item.shoot = ProjectileType<WindShot>();
                item.knockBack = 7;
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
                item.useTime = 60;
                item.useAnimation = 5;
			    item.shoot = ProjectileType<WindBeam>();
                item.knockBack = 4;
			}
            return base.CanUseItem(player);
        }

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WindMaterial", 10);   //you need 10 Wind
            recipe.AddTile(TileID.SkyMill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
