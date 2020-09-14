using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using elemental.Buffs;

namespace elemental.Items
{
    public class Discordian_Charm : ModItem
    {


        public override void SetDefaults()
        {
            item.width = 10;
            item.height = 14;
            item.useStyle = 5;
            item.useAnimation = 3;
            item.useTime = 3;
            item.value = 10;
            item.rare = -11;
            item.accessory = true;
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Discordian Band");
		  Tooltip.SetDefault("");
		}

        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ChaosMaterial", 20);   //you need 20 Wind
            //recipe.AddIngredient(ItemID.RodofDiscord);
            recipe.AddTile(TileID.Tombstones);   //at work bench
            recipe.AddTile(TileID.LunarMonolith);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
            player.Teleport(new Vector2(Main.mouseX, Main.mouseY) ,1);
            return false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if(player.HasBuff(BuffID.ChaosState)){
                player.AddBuff(BuffType<ChaosBuff>(), player.buffTime[player.FindBuffIndex(BuffID.ChaosState)]/35);
                player.buffTime[player.FindBuffIndex(BuffID.ChaosState)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.ChaosState)] - 1, 0);
            }
            /*
            if(player.HasBuff(BuffID.ChaosState)){
                if(player.HasBuff(BuffID.PotionSickness)){
                    player.buffTime[player.FindBuffIndex(BuffID.PotionSickness)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.PotionSickness)] - 3, 0);
                }
                player.potionDelayTime -= 3;
                player.potionDelay -= 3;
                if(player.HasBuff(BuffID.ManaSickness)){
                    player.buffTime[player.FindBuffIndex(BuffID.ManaSickness)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.ManaSickness)] - 7, 0);
                }
            }//*/
            
        }

        public override void UpdateInventory(Player player){
            if(player.HasBuff(BuffID.ChaosState)){
                if(item.prefix != PrefixID.Precise){
                    player.AddBuff(BuffType<ChaosBuff>(), player.buffTime[player.FindBuffIndex(BuffID.ChaosState)]/60);
                    player.buffTime[player.FindBuffIndex(BuffID.ChaosState)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.ChaosState)] - 1, 0);
                }else{
                    player.AddBuff(BuffType<ChaosBuff>(), player.buffTime[player.FindBuffIndex(BuffID.ChaosState)]/35);
                    player.buffTime[player.FindBuffIndex(BuffID.ChaosState)] = Math.Max(player.buffTime[player.FindBuffIndex(BuffID.ChaosState)] - 1, 0);
                }
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
                /*
				if (tooltips[i].text.Contains("DisplayCrit")){
                tip = new TooltipLine(mod, "DisplayCrit", "Melee Crit:"+player.meleeCrit+"\n Ranged Crit:"+player.rangedCrit+"\n Magic Crit:"+player.magicCrit+"\n Throwing Crit:"+player.thrownCrit);
                tip.overrideColor = new Color(255, 32, 174, 200);
				}
				if (tooltips[i].text.Contains("+100%") && item.prefix == 68){
                tip = new TooltipLine(mod, "+100%", "+150% crit chance");
                tip.overrideColor = new Color(255, 32, 174, 200);
				}//*/
                tooltips.RemoveAt(i);
                tooltips.Insert(i, tip);
            }
        }
    }
}