using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class Chaos_Summon_Acc : ModItem
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
		  DisplayName.SetDefault("Chaos");
		  Tooltip.SetDefault(@"Cause Chaos!
		  DisplayConsumed");
		}
        /*public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosMaterial", 20);   //you need 20 Wind
            recipe.AddTile(TileID.SkyMill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            player.maxMinions += 9+(90*modPlayer.consumedgravityglobes);
			
            if (player.HeldItem.summon)
            {
				player.HeldItem.autoReuse = true;
				player.HeldItem.useTime = 6;
				player.HeldItem.useAnimation = 6;
				player.HeldItem.mana = 1;
			}
        }
		
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            for (int i = 0; i < tooltips.Count; i++)
            {
                if (tooltips[i].text.Contains("DisplayConsumed"))
                {
                    TooltipLine tip;
                    tip = new TooltipLine(mod, "DisplayConsumed", "Minion slot boost:" + 90*modPlayer.consumedgravityglobes);
                    tip.overrideColor = new Color(255, 32, 174, 200);
                    tooltips.RemoveAt(i);
                    tooltips.Insert(i, tip);
                }
                if (tooltips[i].text == "Chaos")
                {
                    TooltipLine tip;
                    tip = new TooltipLine(mod, "Chaos", "Chaos");
                    tip.overrideColor = new Color(255, 32, 174, 200);
                    tooltips.RemoveAt(i);
                    tooltips.Insert(i, tip);
				}
            }
        }
		
    }
}