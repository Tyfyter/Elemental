using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static elemental.Extentions;

namespace elemental.Projectiles
{

    public class EclipseHook : ModProjectile
    {
        int npchitlast = -1;
        bool returning = false;
        public override void SetDefaults()
        {
            //projectile.name = "Water Shot";  //projectile name
            projectile.width = 24;       //projectile width
            projectile.height = 24;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 180;   //how many time this projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.extraUpdates = 3;
            projectile.ignoreWater = true;   
			projectile.aiStyle = 0;
            projectile.alpha = 25;
            projectile.usesLocalNPCImmunity = true;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Eclipse Hook");
		}
        public override void AI(){        
            if(npchitlast == -1 && !returning){
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) - (float)(0.75*Math.PI); 
            }
            if((npchitlast != -1) || returning){
                Vector2 vel = elementalmod.NormalizeMin(Main.player[projectile.owner].MountedCenter - projectile.Center)*5.5f;
                if(npchitlast != -1){
                    NPC target = Main.npc[npchitlast];
                    if(!target.active)projectile.Kill();
                    projectile.Center = target.Center;
                    vel = lerp(vel*Math.Max(target.knockBackResist,1), target.velocity, 1-constrain(target.knockBackResist*1.25f, 0.1f, 1f));
                    target.velocity = projectile.velocity = vel;
                }else{
                    projectile.velocity = vel;
                }
            }
            if(Main.player[projectile.owner].controlUseTile){
                projectile.timeLeft = projectile.timeLeft>30?projectile.timeLeft:30;
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)(1.25*Math.PI);
            }
            Main.player[projectile.owner].GetModPlayer<ElementalPlayer>().channelsword = 2;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if(target.type == NPCID.TargetDummy)return;
            npchitlast = target.whoAmI;
            /*if(npchitlast != -1 && timehitlast < 15){
			    target.velocity = new Vector2(MathHelper.Lerp(target.velocity.X, projectile.position.X - target.position.X, 0.75f), MathHelper.Lerp(target.velocity.Y, projectile.position.Y - target.position.Y, 0.75f));
                return;
            }*/
			//target.velocity = new Vector2(MathHelper.Lerp(target.velocity.X, projectile.velocity.X*1.1f, 0.75f), MathHelper.Lerp(target.velocity.Y, projectile.velocity.Y*1.1f, 0.75f));
        }
        public override bool PreKill(int timeLeft){
            returning = true;
            return (projectile.Center-Main.player[projectile.owner].Center).Length()>64;
        }
        public override bool OnTileCollide(Vector2 oldVelocity){
            returning = true;
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            Lighting.AddLight(projectile.position, Color.BlueViolet.ToVector3()/1.5f);
            Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 center = projectile.Center;
			Vector2 distToProj = playerCenter - projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance))
			{
				distToProj.Normalize();                 //get unit vector
				distToProj *= 12f;                      //speed = 24
				center += distToProj;                   //update draw position
				distToProj = playerCenter - center;    //update distance
				distance = distToProj.Length();
				Color drawColor = lightColor;
				//Draw chain
				spriteBatch.Draw(Main.chain9Texture, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, Main.chain30Texture.Width, Main.chain30Texture.Height), drawColor, projRotation,
					new Vector2(Main.chain30Texture.Width * 0.5f, Main.chain30Texture.Height * 0.5f), 0.5f, SpriteEffects.None, 0f);
                Lighting.AddLight(center - Main.screenPosition, Color.BlueViolet.ToVector3()/3);
			}
            return true;
        }
    }
}