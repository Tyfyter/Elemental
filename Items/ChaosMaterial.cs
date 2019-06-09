using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
namespace elemental.Items
{
	public class ChaosMaterial : ModItem
	{
		int time = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Raw Chaos");
			Tooltip.SetDefault("");
			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
			ItemID.Sets.AnimatesAsSoul[item.type] = true;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		public override void SetDefaults()
		{
			Item refItem = new Item();
			refItem.SetDefaults(ItemID.SoulofSight);
			item.width = refItem.width;
			item.height = refItem.height;
			item.useTime = 20;
			item.useAnimation = 20;
			item.useStyle = 0;
			item.knockBack = 6;
			item.value = 0;
			item.rare = 2;
            item.maxStack = 999;
			item.noGrabDelay = 0;
		}
		public override bool OnPickup(Player player){
			time = 0;
			return true;
		}
		public override void GrabRange(Player player, ref int grabRange)
		{
			grabRange *= 5;
		}
		/*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture = Main.itemTexture[item.type];
			Main.spriteBatch.Draw(Main.itemTexture[item.type], new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f), new Rectangle(0, 0, texture.Width, texture.Height), Color.DarkCyan, rotation, texture.Size() * 0.5f,scale, SpriteEffects.None, 0f);
		}*/
		
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI){
			/*if(time > 0){
				time--;
			}
			if(rng == 25){
				Lighting.AddLight(item.position, Color.DarkCyan.R/50, Color.DarkCyan.G/100, Color.DarkCyan.B/50);
			}else{
				Lighting.AddLight(item.position, Color.DarkCyan.R/125, Color.DarkCyan.G/250, Color.DarkCyan.B/125);
			}*/
			Lighting.AddLight(item.position, 2, 0, 1);
			return base.PreDrawInWorld(spriteBatch, Color.White, alphaColor, ref rotation, ref scale, whoAmI);
			//mod.GetPrefix("").
		}
	}
}
