using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class windacc : ModItem
    {


        public override void SetDefaults()
        {
            //item.name = "Bracelet of the Wind";
            item.width = 10;
            item.height = 14;
            //item.toolTip = "throw things";
            item.value = 10;
            item.rare = -11;
            item.accessory = true;
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Bracelet of the Wind");
		  Tooltip.SetDefault("throw things");
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WindMaterial", 20);   //you need 20 Wind
            recipe.AddTile(TileID.SkyMill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            modPlayer.reflect = true;
            player.thrownVelocity += 5f;
            player.thrownVelocity *= 20.1f;
            player.suffocateDelay = 10;
            player.meleeSpeed += 20f;
            player.meleeSpeed *= 2f;
            if (player.HeldItem.thrown)
            {
                player.HeldItem.shootSpeed = 25f;
                /*if (!player.HeldItem.name.Contains(" of wind")) {
                    player.HeldItem.name = player.HeldItem.name + " of wind";
                }*/
            }
        }
    }
}