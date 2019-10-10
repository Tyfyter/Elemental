using System;
using System.IO;
using elemental.Buffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace elemental.Projectiles
{

    public class AphelionSpike : ModProjectile
    {
        public override bool CloneNewInstances => true;
        public bool held = false;
        public int stabbed = -1;
        public Vector2 stabpos;
        public bool wall = false;
        public override void SetDefaults(){
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.light = 0f;
            projectile.extraUpdates = 6;
            projectile.ignoreWater = true;
            projectile.alpha = 50;
            projectile.tileCollide = false;
            projectile.hide = true;
            projectile.usesLocalNPCImmunity = true;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Aphelion Spike");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
		}
        public override bool PreAI(){
            //if(init)held = Main.player[projectile.owner].controlUseItem;
            projectile.tileCollide = held;
            if(held){
                projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X)+(float)Math.PI/2; 
                if(Main.player[projectile.owner].controlUseItem && !Main.player[projectile.owner].controlUseTile)held = false;
                projectile.Center = Main.player[projectile.owner].Center + (projectile.velocity * (float)(1+Math.Min((170f-projectile.timeLeft)/10, 0.5)));
                Main.player[projectile.owner].itemAnimation = 4;
                //projectile.position-=projectile.velocity;
                if(stabbed!=-1){
                    if(projectile.timeLeft<100)projectile.timeLeft = 100;
                    NPC stabee = Main.npc[stabbed];
                    stabee.Center = (projectile.Center+projectile.velocity)+stabpos;
                    if(!held){
                        stabee.velocity = projectile.velocity*3;
                        stabee.StrikeNPC(projectile.damage, 0, 0, Main.rand.Next(100)<Main.player[projectile.owner].magicCrit);
                    }
                }else if(projectile.timeLeft<100){
                    projectile.Kill();
                    Main.player[projectile.owner].itemAnimation = 0;
                }
            }else if(wall){
                if(projectile.timeLeft<172){
                    projectile.velocity*=0;
                }else projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X)+(float)Math.PI/2; 
                if(stabbed!=-1){
                    NPC stabee = Main.npc[stabbed];
                    stabee.Center = (projectile.Center+projectile.velocity)+stabpos;
                }
            }else projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X)+(float)Math.PI/2; 
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit){
            if(stabbed==-1){
                stabbed = target.whoAmI;
                stabpos = (target.Center-projectile.Center)*0.65f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity){
            Vector2 diff = oldVelocity-projectile.velocity;
            diff = diff.Normalized() * -2.5f;
            Vector2 pos = projectile.Center + diff.RotatedBy(Math.PI/2)*12.5f;
            (Projectile.NewProjectileDirect(pos-diff*10, diff, projectile.type, projectile.damage, projectile.knockBack, projectile.owner).modProjectile as AphelionSpike).wall = true;
            pos = projectile.Center + diff.RotatedBy(-Math.PI/2)*25;
            (Projectile.NewProjectileDirect(pos-diff*10, diff, projectile.type, projectile.damage, projectile.knockBack, projectile.owner).modProjectile as AphelionSpike).wall = true;
            return true;
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            if(stabbed!=-1)damage/=10;
        }
        public override void DrawBehind(int index, System.Collections.Generic.List<int> drawCacheProjsBehindNPCsAndTiles, System.Collections.Generic.List<int> drawCacheProjsBehindNPCs, System.Collections.Generic.List<int> drawCacheProjsBehindProjectiles, System.Collections.Generic.List<int> drawCacheProjsOverWiresUI){
            drawCacheProjsBehindNPCsAndTiles.Add(index);
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            if(!held)spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition - projectile.velocity,
                    new Rectangle(0, 0, 16, 32), lightColor = Color.Cyan, projectile.rotation,
                    new Vector2(8, 16), 1, 0, 0);
                Lighting.AddLight(projectile.Center, 0, 0.75f, 1);
            return true;
        }
    }
}