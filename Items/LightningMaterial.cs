using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Items
{
    public class LightningMaterial : ModItem
    {
        public override void SetDefaults()
        {
            //item.name = "Wind";
            item.width = 20;
            item.height = 20;
            //item.toolTip = "";
            item.value = 100;
            item.rare = -12;
            item.maxStack = 999;
            ItemID.Sets.ItemNoGravity[item.type] = true;  //this make that the item will float in air
        }
		public override void SetStaticDefaults()
		{
		  DisplayName.SetDefault("Thunder");
		  Tooltip.SetDefault("");
		}

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.LightSkyBlue;
        }
    }
}