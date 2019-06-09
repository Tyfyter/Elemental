using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class WaterMaterial : ModItem
    {
        public override void SetDefaults()
        {
            //item.name = "Water";
            item.width = 20;
            item.height = 20;
            //item.toolTip = "";
            item.value = 100;
            item.rare = -12;
            item.maxStack = 999;
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Water");
		  Tooltip.SetDefault("");
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightBlue;
        }
    }
}