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
	public class ChaosMaterialConverter : ModItem
	{
		int time = 0;
		int timetotal = 0;
		int souloflight = -1;
		int frequency = 15;
		int soulmemorytime = 0;
		public override bool CloneNewInstances => true;
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
		public override bool CanPickup(Player player){
			return souloflight <= -2 && soulmemorytime >= 60;
		}
		public override bool OnPickup(Player player){
			if(souloflight==-3)return false;
			time = 0;
		    timetotal = 0;
			souloflight = -1;
			frequency = 15;
			soulmemorytime = 0;
			return true;
		}
		public override void GrabRange(Player player, ref int grabRange)
		{
			if(souloflight == 2){
			grabRange = (int)(grabRange * 0.5);
				return;
			}
			grabRange *= 0;
		}
		public override bool GrabStyle(Player player)
		{
			PlayerDeathReason reason = new PlayerDeathReason();
			/*string pronoun = "he";
			if(!player.Male) pronoun = "s"+pronoun;
			reason.SourceCustomReason = " Tried to stop something " +pronoun+ " shouldn't have.";
			player.immune = false;*/
			player.Hurt(reason, 50, 0);
			return true;
		}
		public override void Update(ref float gravity, ref float maxFallSpeed){
			time++;
			if(souloflight==-2)soulmemorytime++;
			if(time >= frequency-(timetotal/4)){
				if(souloflight == -1){
					for(int i = 0; i < Main.item.Length; i++){
						if(Main.item[i].type == ItemID.SoulofLight && (item.position-Main.item[i].position).Length() <= 64){
							souloflight = i;
							Dust.NewDust(item.Center, 0, 0, DustID.AncientLight);
							break;
						}
					}
				}else{
					Dust.NewDust(item.Center, 0, 0, DustID.Shadowflame);
					if(!Main.item.IndexInRange(souloflight)){
						souloflight = -2;
						timetotal = 0;
						frequency = 30;
						foreach(Player target in Main.player){
							if((item.Center-target.Center).Length() <= 240){
								Vector2 veloc = (target.Center-item.Center);
								veloc.Normalize();
								int a = Projectile.NewProjectile(item.Center, veloc * 8, 496, 100, 10, 255, Main.rand.Next(-10,10), Main.rand.Next(-10,10));
								Main.projectile[a].hostile = true;
							}
						}
					}else if((Main.item[souloflight].type != ItemID.SoulofLight || !((item.position-Main.item[souloflight].position).Length() <= 64)) || souloflight == -2){
						if(Main.item[souloflight].type != ItemID.SoulofLight){
							souloflight = -2;
							timetotal = 0;
						frequency = 30;
						}
						if(souloflight!=-3)foreach(Player target in Main.player){
							if((item.Center-target.Center).Length() <= 120){
								Vector2 veloc = (target.Center-item.Center);
								veloc.Normalize();
								int a = Projectile.NewProjectile(item.Center, veloc * 8, 496, 100, 10, 255, Main.rand.Next(-10,10), Main.rand.Next(-10,10));
								//Main.projectile[a].friendly = false;
								Main.projectile[a].hostile = true;
							}
						}
						foreach(NPC target in Main.npc){
							if((item.Center-target.Center).Length() <= 120 && !target.dontTakeDamage){
								Vector2 veloc = (target.Center-item.Center);
								veloc.Normalize();
								int a = Projectile.NewProjectile(item.Center, veloc * -8, 496, 100, 10, 255, Main.rand.Next(-10,10), Main.rand.Next(-10,10));
								Main.projectile[a].friendly = true;
							}
						}
					}else if(Main.item[souloflight].stack>0&&item.stack>0){
						Dust.NewDust(item.Center, 0, 0, DustID.AncientLight);
						timetotal++;
						if(timetotal >= 64){
							//item.stack = (item.stack+Main.item[souloflight].stack)/2;
							//Main.item[souloflight].keepTime = 1;
							Item.NewItem(item.position, new Vector2(), ItemType<ChaosMaterial>(), (item.stack+Main.item[souloflight].stack)/2);
							//this.RightClick(Main.player[item.owner]);
							//item.type = ItemType("ChaosMaterial");
							Main.item[souloflight].stack=0;//.TurnToAir();
							souloflight = -3;
							item.stack=0;//.TurnToAir();
						}
					}
				}
				time = 0;
			}
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
