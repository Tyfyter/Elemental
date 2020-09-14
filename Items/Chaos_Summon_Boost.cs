using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class Chaos_Summon_Boost : ModItem
    {


        public override void SetDefaults()
        {
            //item.name = "Bracelet of the Wind";
            item.width = 10;
            item.height = 14;
            //item.toolTip = "throw things";
			item.useStyle = 4;
            item.value = 10000000;
            item.rare = -11;
            item.consumable = true;  //Tells the game that this should be used up once fired
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Chaos");
		  Tooltip.SetDefault("Cause More Chaos!");
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(1131, 1);   //you need 1 gravity globe
            recipe.AddTile(TileID.Stone);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

		public override bool UseItem(Player player)
		{
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
			modPlayer.consumedgravityglobes++;
			if(modPlayer.consumedgravityglobes > 1){
				Main.NewText("Stop it, get some help");
			}
			return true;
		}
    }
}