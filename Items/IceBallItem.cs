using elemental.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
	public class IceBallItem : ElementalItem
	{
		int charge = 0;
        public override int Elements => 4;
        public override void SetDefaults()
		{
			//item.name = "Ice Gauntlet";
			item.damage = 20;
            //item.magic = true;
            item.magic = true;
            item.thrown = true;
            item.noMelee = true;
            item.noUseGraphic = false;
            item.width = 26;
			item.height = 26;
			item.useTime = 1;
			item.useAnimation = 7;
            item.useStyle = 5;
            item.knockBack = 10;        
			item.value = 10000;
			item.rare = 2;
            item.useStyle = 1;
            item.shoot = mod.ProjectileType<IceBallProj>();
            item.shootSpeed = 6.5f;
            item.autoReuse = false;
            item.alpha = 100;
            item.consumable = true;
        }
		
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Ice Ball");
		  Tooltip.SetDefault("");
		}

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack){
            if(player.HasItem(item.type))player.selectedItem = player.FindItem(item.type);
            return true;
        }
    }
}
