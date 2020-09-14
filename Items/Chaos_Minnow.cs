using System;
using elemental.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using static Terraria.ModLoader.ModContent;

namespace elemental.Items
{
	public class Chaos_Minnow : ModItem
	{
		public override bool CloneNewInstances => true;
		Vector2 rand = new Vector2();
		int fishleft = 80;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chaos Minnow");
			Tooltip.SetDefault("Scientists studying hallowed life forms initially thought the first chaow minnow they found was injured. \nThey quickly realised that they were wrong.");
			ItemID.Sets.ItemIconPulse[item.type] = true;
		}
		public override void SetDefaults()
		{
			Item refItem = new Item();
			refItem.SetDefaults(ItemID.SoulofSight);
			item.width = 8;
			item.height = 8;
			item.value = 0;
			item.rare = 2;
            item.maxStack = 999;
			item.noGrabDelay = 0;
			fishleft = Main.rand.Next(100,220);
		}
		public override bool OnPickup(Player player){
			//fishleft = -fishleft;
			//Main.NewText(fishleft);
			return base.OnPickup(player);
		}
        public override void CaughtFishStack(ref int stack){
			//fishleft = -Main.rand.Next(40,100);
			//Main.NewText(fishleft);
		}
		public override void UpdateInventory(Player player){
			if(fishleft>40){
				fishleft--;
				return;
			}
			if(fishleft>0&&Main.rand.Next(7)==0){
				Vector2 pos = player.Center+(new Vector2(Main.rand.Next(160)).RotatedByRandom(Math.PI*2));
				NPC.NewNPC((int)pos.X, (int)pos.Y, NPCType<ChaosMinnow>(), Target:player.whoAmI);
				for (int i = 0; i < 7; i++)Dust.NewDust(pos, 8, 8, 164, newColor:new Color(255,0,255));
				fishleft--;
			}
		}
		/*public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture = Main.itemTexture[item.type];
			Main.spriteBatch.Draw(Main.itemTexture[item.type], new Vector2(item.position.X - Main.screenPosition.X + item.width * 0.5f, item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f), new Rectangle(0, 0, texture.Width, texture.Height), Color.DarkCyan, rotation, texture.Size() * 0.5f,scale, SpriteEffects.None, 0f);
		}*/
		public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale){
			scale *= 2;
			return base.PreDrawInInventory(spriteBatch, position, frame, drawColor, itemColor, origin, scale);
		}
		
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI){
			/*if(time > 0){
				time--;
			}*/
			if(Main.rand.Next(24) == 0){
				Lighting.AddLight(item.position, Color.DarkCyan.R/75, Color.DarkCyan.G/150, Color.DarkCyan.B/75);
			}else{
				Lighting.AddLight(item.position, Color.DarkCyan.R/125, Color.DarkCyan.G/250, Color.DarkCyan.B/125);
			}//*/
			item.position -= rand + new Vector2(0, 0.5f);
			rand = new Vector2(Main.rand.NextFloat(-1.5f, 1.5f), Main.rand.NextFloat(-1.5f, 1.5f));
			item.position += rand;
			Lighting.AddLight(item.position, 0.5f, 0, 0.25f);
			bool returned = base.PreDrawInWorld(spriteBatch, Color.White, alphaColor, ref rotation, ref scale, whoAmI);
			return returned;
			//mod.GetPrefix("").
		}
	}
}
