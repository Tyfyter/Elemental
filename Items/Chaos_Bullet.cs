using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class Chaos_Bullet : ModItem
    {


        public override void SetDefaults()
        {
            item.damage = 20;
            item.width = 10;
            item.height = 14;
            item.value = 10;
            item.rare = -11;
            item.shoot = Main.rand.Next(0, Main.maxProjectileTypes);
            item.ammo = AmmoID.Bullet;
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Chaos Rounds");
		  Tooltip.SetDefault(@"Pure mayhem.");
		}

        public override bool ConsumeAmmo(Player player){
            item.shoot = Main.rand.Next(0, Main.maxProjectileTypes);
            return (Main.rand.Next(0, 25) == 25 && base.ConsumeAmmo(player));
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
            type = Main.rand.Next(0, Main.maxProjectileTypes);
            int a = Projectile.NewProjectile(position, new Vector2(speedX, speedY), type, damage, knockBack, player.whoAmI);
            Main.projectile[a].hostile = false;
            Main.projectile[a].friendly = true;
            return false;
        }
        /*public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosMaterial", 20);   //you need 20 Wind
            recipe.AddTile(TileID.SkyMill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
		
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            for (int i = 0; i < tooltips.Count; i++)
            {
                TooltipLine tip;
                tip = new TooltipLine(mod, "", tooltips[i].text);
                tip.overrideColor = new Color(255, 32, 174, 200);
                tooltips.RemoveAt(i);
                tooltips.Insert(i, tip);
            }
        }
    }
}