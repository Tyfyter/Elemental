using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class windboots : ModItem
    {

        int manacost = 3;
        int fallchance = 2;
        int catches = 2;
        int maxcatches = 2;
        bool catched = false;
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
		  DisplayName.SetDefault("Boots of the Wind");
		  Tooltip.SetDefault("Fly, you fools!");
		}
        public override void AddRecipes()  //How to craft this item
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "WindMaterial", 20);   //you need 20 Wind
            recipe.AddIngredient(ItemID.RocketBoots, 1);
            recipe.AddTile(TileID.SkyMill);   //at work bench
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ElementalPlayer modPlayer = player.GetModPlayer<ElementalPlayer>(mod);
            player.canRocket = true;
            player.rocketBoots = 2;
            player.rocketTimeMax = 5;
            player.maxRunSpeed += 10;
            player.accRunSpeed += 4f;
            if(player.controlJump && catches > 0){
                if(/*player.statMana >= manacost*/ player.CheckMana((int)(manacost*player.manaCost), true) && player.rocketTime < player.rocketTimeMax){
                    player.rocketTime++;
                    //player.statMana -= (int)(manacost*player.manaCost);
                    //player.manaRegenDelay = 30;
                    if(player.statMana < (manacost*player.manaCost) && !catched){
                        catches--;
                        player.ManaEffect(catches);
                        catched = true;
                        if(catches == 0){
                            player.rocketTime = 0;
                        }
                    }
                    if(player.statMana >= (manacost*player.manaCost) && catched){
                        catched = false;
                    }
                }
            }
            if(player.velocity.Y == 0 && catches != maxcatches){
                catches = maxcatches;
                player.ManaEffect(catches);
            }
            if(player.velocity.Y > 50) player.armorEffectDrawOutlinesForbidden = true;
            if(!player.noFallDmg && Main.rand.Next(fallchance) >= 10-(int)(player.velocity.Y/5)) player.noFallDmg = true;
        }
    }
}