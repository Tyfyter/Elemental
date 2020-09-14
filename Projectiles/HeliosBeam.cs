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

    public class HeliosBeam : ModProjectile
    {
        public override bool CloneNewInstances => true;
        public Vector2 origin;
        private bool init = true;
        public bool small = false;

        public override void SetDefaults()
        {
            projectile.aiStyle = 48;
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.tileCollide = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.light = 0f;
            projectile.extraUpdates = 9;
            projectile.ignoreWater = true;
            projectile.alpha = 50;
            projectile.tileCollide = false;
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Helios Beam");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 4;
            ProjectileID.Sets.TrailingMode[projectile.type] = 1;
		}
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            if(init){
                origin = projectile.Center;
                init = false;
            }
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);  
            Lighting.AddLight(projectile.Center, 1, 0.25f, 0);
            if (projectile.localAI[0] > 3f){
                for (int i = 0; i < 4; i++){
                    Vector2 pos = new Vector2(Main.rand.NextFloat(-48,48),Main.rand.NextFloat(-32,32))*(small?0.45f:0.90f);
                    int d = Dust.NewDust(projectile.Center, 1, 1, 162, 0f, 0f, 0, default(Color), 1f);
                    Main.dust[d].position = projectile.Center+pos.RotatedBy(projectile.rotation);
                }
                return;
            }
        }
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection){
            target.AddBuff(ModContent.BuffType<IncinerateDebuff>(), 1);
        }
        //*
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor){
            //lightColor = Color.OrangeRed;
            DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], origin, projectile.velocity, 1.65f, projectile.damage, small?0.33f:0.66f, 1000, Color.Red);
            return false;
        }
        // */
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), origin, projHitbox.Center.ToVector2(), small?11:22, ref point)){
                return true;
            }
            return false;
        }
        public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, int damage, float scale = 0.66f, float maxDist = 200f, Color color = default(Color))
        {
            Vector2 origin;
            float maxl = (projectile.Center-start).Length();
            float r = unit.ToRotation();// + rotation??(float)(Math.PI/2);
            float l = unit.Length()*2.5f;
            float s = Math.Min(projectile.timeLeft/50f,1f);
            for (float i = 0; i <= 128; i += step){
                if((i*unit).Length()>maxl)break;
                origin = start + i * unit;
                int p = (int)(l*Main.rand.NextFloat(48/l));
                spriteBatch.Draw(texture, origin - Main.screenPosition,
                    new Rectangle(p, 0, (int)(small?(l+Math.Max(50-projectile.timeLeft,1))*2:l+Math.Max(50-projectile.timeLeft,1)), 32), Color.OrangeRed, r,
                    new Vector2(48 / 2, 32 / 2), scale*s, 0, 0);
                Lighting.AddLight(origin, 1*s, 0.25f*s, 0);
            }
        }
    }
}