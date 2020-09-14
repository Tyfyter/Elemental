using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class Chaos_Ring : ModItem
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
		  DisplayName.SetDefault("Chaos Ring");
		  Tooltip.SetDefault(@"How did you make a ring out of pure chaos? 
+100% Crit Chance 
		  DisplayCrit");
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
			//player.HeldItem.crit = Math.Min(player.HeldItem.crit*2, 500000);
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>();
			modPlayer.multiplyCrit += 1.0f;
			if(item.prefix == 68){
			modPlayer.multiplyCrit += 0.5f;
			}
        }
		
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.player[item.owner];
            for (int i = 0; i < tooltips.Count; i++)
            {
                TooltipLine tip;
                tip = new TooltipLine(mod, "", tooltips[i].text);
                tip.overrideColor = new Color(255, 32, 174, 200);
				if (tooltips[i].text.Contains("DisplayCrit")){
                tip = new TooltipLine(mod, "DisplayCrit", "Melee Crit:"+player.meleeCrit+"\n Ranged Crit:"+player.rangedCrit+"\n Magic Crit:"+player.magicCrit+"\n Throwing Crit:"+player.thrownCrit);
                tip.overrideColor = new Color(255, 32, 174, 200);
				}
				if (tooltips[i].text.Contains("+100%") && item.prefix == 68){
                tip = new TooltipLine(mod, "+100%", "+150% crit chance");
                tip.overrideColor = new Color(255, 32, 174, 200);
				}
                tooltips.RemoveAt(i);
                tooltips.Insert(i, tip);
            }
        }
    }
}