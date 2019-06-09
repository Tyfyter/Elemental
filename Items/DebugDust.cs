using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace elemental.Items
{
    public class DebugDust : ModItem
    {
        public override void SetDefaults()
        {
            //item.name = "Debug Dust";     //the name displayed when hovering over the Weapon ingame.
            item.damage = 40;  //The damage stat for the Weapon.
            //item.toolTip = "displays a different dust each time, right click to scroll back";  //The description of the Weapon shown when hovering over the Weapon ingame.
            item.noMelee = true;  //Setting to True allows the weapon sprite to stop doing damage, so only the projectile does the damge
            item.noUseGraphic = false;
            item.magic = true;    //This defines if it does magic damage and if its effected by magic increasing Armor/Accessories.
            item.mana = 5; //How mutch mana this weapon use
            item.rare = 5;   //The color the title of your Weapon when hovering over it ingame
            item.width = 28;   //The size of the width of the hitbox in pixels.
            item.height = 30;    //The size of the height of the hitbox in pixels.
            item.useTime = 7;   //How fast the Weapon is used.
            item.UseSound = SoundID.Item13;  //The sound played when using your Weapon
            item.useStyle = 5;   //The way your Weapon will be used, 5 is the Holding Out Used for: Guns, Spellbooks, Drills, Chainsaws, Flails, Spears for example
            item.shootSpeed = 5f;       //This defines the projectile speed when shoot
			item.shoot = 1;
            item.useAnimation = 7;                         //Speed is not important here
            item.value = Item.sellPrice(0, 3, 0, 0);//	How much the item is worth, in copper coins, when you sell it to a merchant. It costs 1/5th of this to buy it back from them. An easy way to remember the value is platinum, gold, silver, copper or PPGGSSCC (so this item price is 3gold)
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Debug Dust");
		  Tooltip.SetDefault("Displays a different dust each time, right click to scroll back");
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
		

		public void UseItem(Player player, int playerID)
		{
			if (player.altFunctionUse == 2){
				item.shoot -=1;
			}else{
				item.shoot +=1;
			}
			//if (item.shoot > 76) item.shoot = 0;
			//if (item.shoot < 0) item.shoot = 76;
			player.HealEffect(item.shoot);
			}

		public void UseItemEffect(Player player, Rectangle rectangle)
		{
			Color color = new Color();
			int dust = Dust.NewDust(new Vector2((float) rectangle.X, (float) rectangle.Y), rectangle.Width, rectangle.Height, item.shoot, (player.velocity.X) + (player.direction * 3), player.velocity.Y, 100, color, 2.0f);
			Main.dust[dust].noGravity = true;
		}
		
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
			Color color = new Color(255, 31, 174);
			int a = Dust.NewDust(position + new Vector2(speedX*3, speedY*3), 0, 0, item.shoot, 0, 0, 100, color, 2.0f);
			int b = Dust.NewDust(position + new Vector2(speedX*2, speedY*2), 0, 0, item.shoot, 0, 0, 100);
			Main.dust[a].noGravity = true;
			Main.dust[b].noGravity = true;
			return false;
		}
    }
}