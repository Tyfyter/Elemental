using System.IO;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using elemental.Items;

namespace elemental
{
	public class ElementalWorld : ModWorld
	{
		public override void PostWorldGen()
		{
			List<int> IceChests = new List<int>{};
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
				if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 4 * 36)
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						if (chest.item[inventoryIndex].type == 0)
						{
							IceChests.Add(chestIndex);
							break;
						}
					}
				}
			}
			retryice:
			int iceindex = Main.rand.Next(IceChests);
			Chest icechestb = Main.chest[iceindex];
			bool icesuccess = false;
			for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
			{
				if (icechestb.item[inventoryIndex].type == 0)
				{
					icechestb.item[inventoryIndex].SetDefaults(mod.ItemType<fire>());
					icesuccess = true;
					break;
				}
			}
			if(!icesuccess){
				IceChests.Remove(iceindex);
				if(IceChests.Count>0)goto retryice;
			}
			if(Main.expertMode){
				List<int> ShadowChests = new List<int>{};
				for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
				{
					Chest chest = Main.chest[chestIndex];
					// If you look at the sprite for Chests by extracting Tiles_21.xnb, you'll see that the 12th chest is the Ice Chest. Since we are counting from 0, this is where 11 comes from. 36 comes from the width of each tile including padding. 
					if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 4 * 36)
					{
						for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
						{
							if (chest.item[inventoryIndex].type == 0)
							{
								ShadowChests.Add(chestIndex);
								break;
							}
						}
					}
				}
				retry:
				int index = Main.rand.Next(ShadowChests);
				Chest chestb = Main.chest[index];
				bool success = false;
				for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
				{
					if (chestb.item[inventoryIndex].type == 0)
					{
						chestb.item[inventoryIndex].SetDefaults(mod.ItemType<fire>());
						success = true;
						break;
					}
				}
				if(!success){
					ShadowChests.Remove(index);
					if(ShadowChests.Count>0)goto retry;
				}
			}
		}
	}
}