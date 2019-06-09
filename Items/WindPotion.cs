using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace elemental.Items
{
    public class WindPotion : ModItem
    {
        public override void SetDefaults()
        {
            //item.name = "Wind in a Bottle";// in game item name
            item.UseSound = SoundID.Item3;                //this is the sound that plays when you use the item
            item.useStyle = 2;                 //this is how the item is holded when used
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.maxStack = 30;                 //this is where you set the max stack of item
            item.consumable = true;           //this make that the item is consumable when used
            item.width = 20;
            item.height = 28;
            //item.toolTip = "";
            item.value = 100;                
            item.rare = 1;
            item.buffType = mod.BuffType("WindDebuff");    //this is where you put your Buff name
            item.buffTime = 2000;    //this is the buff duration        20000 = 6 min
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Wind in a Bottle");
		  Tooltip.SetDefault("congradulations, you put air in a bottle.");
		}
    }
}